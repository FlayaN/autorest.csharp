﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoRest.CSharp.V3.Input;
using AutoRest.CSharp.V3.Output.Models;
using AutoRest.CSharp.V3.Output.Models.Requests;
using AutoRest.CSharp.V3.Output.Models.Responses;
using AutoRest.CSharp.V3.Output.Models.Serialization;
using AutoRest.CSharp.V3.Output.Models.Shared;
using AutoRest.CSharp.V3.Output.Models.TypeReferences;
using AutoRest.CSharp.V3.Utilities;
using Azure.Core;
using Request = AutoRest.CSharp.V3.Output.Models.Requests.Request;

namespace AutoRest.CSharp.V3.Output.Builders
{
    internal class ClientBuilder
    {
        public static Client BuildClient(OperationGroup operationGroup)
        {
            var allClientParameters = operationGroup.Operations
                .SelectMany(op => op.Request.Parameters)
                .Where(p => p.Implementation == ImplementationLocation.Client)
                .Distinct();
            Dictionary<string, Parameter> clientParameters = new Dictionary<string, Parameter>();
            // Deduplication required because of https://github.com/Azure/autorest.modelerfour/issues/100
            foreach (RequestParameter clientParameter in allClientParameters)
            {
                clientParameters[clientParameter.Language.Default.Name] = BuildParameter(clientParameter);
            }

            string clientName = operationGroup.CSharpName();
            Dictionary<string, OperationMethod> processedMethods = new Dictionary<string, OperationMethod>();
            foreach (Operation operation in operationGroup.Operations)
            {
                Method? method = BuildMethod(operation, clientName, clientParameters);
                if (method != null)
                {
                    processedMethods.Add(operation.Language.Default.Name, new OperationMethod(operation, method));
                }
            }

            List<Method> nextPageMethods = new List<Method>();
            List<Paging> pagingMethods = new List<Paging>();
            foreach ((string processedName, OperationMethod processed) in processedMethods)
            {
                IDictionary<object, object>? pageable = processed.Operation.Extensions.GetValue<IDictionary<object, object>>("x-ms-pageable");
                if (pageable != null)
                {
                    //TODO: Assuming operationName is in this operation group: https://github.com/Azure/autorest.modelerfour/issues/85
                    string? extensionOperationName = pageable.GetValue<string>("operationName");
                    string? operationName = extensionOperationName?.Split('_').Last();

                    OperationMethod? next = null;
                    if (operationName != null)
                    {
                        if (!processedMethods.TryGetValue(operationName, out OperationMethod nextOperationMethod))
                        {
                            throw new Exception(
                                $"The x-ms-pageable operationName \"{extensionOperationName}\" for operation {operationGroup.Key}_{processedName} was not found.");
                        }

                        next = nextOperationMethod;
                    }
                    // If there is no operationName or we didn't find an existing operation, we use the original method to construct the nextPageMethod.
                    Method nextPageMethod = next?.Method ?? BuildNextPageMethod(processed.Method);
                    // Only add the method if it didn't previously exist
                    if (next == null)
                    {
                        nextPageMethods.Add(nextPageMethod);
                    }
                    //TODO: This is a hack since we don't have the model information at this point
                    ObjectSchema? schemaForPaging = ((processed.Method.Response.ResponseBody as ObjectResponseBody)?.Type as SchemaTypeReference)?.Schema as ObjectSchema;
                    Paging pagingMethod = GetClientMethodPaging(processed.Method, nextPageMethod, pageable, schemaForPaging);
                    pagingMethods.Add(pagingMethod);
                }
            }

            Method[] methods = processedMethods.Select(om => om.Value.Method).Concat(nextPageMethods).ToArray();
            return new Client(clientName,
                operationGroup.Language.Default.Description,
                OrderParameters(clientParameters.Values),
                methods,
                pagingMethods.ToArray());
        }

        private struct OperationMethod
        {
            public OperationMethod(Operation operation, Method method)
            {
                Operation = operation;
                Method = method;
            }

            public Operation Operation { get; }
            public Method Method { get; }
        }

        private static Parameter[] OrderParameters(IEnumerable<Parameter> parameters) => parameters.OrderBy(p => p.DefaultValue != null).ToArray();

        private static Method? BuildMethod(Operation operation, string clientName, IReadOnlyDictionary<string, Parameter> clientParameters)
        {
            HttpRequest? httpRequest = operation.Request.Protocol.Http as HttpRequest;
            //TODO: Handle multiple responses
            ServiceResponse? response = operation.Responses.FirstOrDefault();
            HttpResponse? httpResponse = response?.Protocol.Http as HttpResponse;
            if (httpRequest == null || httpResponse == null)
            {
                return null;
            }

            HttpWithBodyRequest? httpRequestWithBody = httpRequest as HttpWithBodyRequest;
            Dictionary<string, PathSegment> uriParameters = new Dictionary<string, PathSegment>();
            Dictionary<string, PathSegment> pathParameters = new Dictionary<string, PathSegment>();
            List<QueryParameter> query = new List<QueryParameter>();
            List<RequestHeader> headers = new List<RequestHeader>();
            List<Parameter> methodParameters = new List<Parameter>();

            RequestBody? body = null;
            foreach (RequestParameter requestParameter in operation.Request.Parameters ?? Array.Empty<RequestParameter>())
            {
                string defaultName = requestParameter.Language.Default.Name;
                string serializedName = requestParameter.Language.Default.SerializedName ?? defaultName;
                ParameterOrConstant constantOrParameter;
                Schema valueSchema = requestParameter.Schema;

                if (requestParameter.Implementation == ImplementationLocation.Method)
                {
                    switch (requestParameter.Schema)
                    {
                        case ConstantSchema constant:
                            constantOrParameter = BuilderHelpers.ParseConstant(constant);
                            valueSchema = constant.ValueType;
                            break;
                        case BinarySchema _:
                            // skip
                            continue;
                        default:
                            constantOrParameter = BuildParameter(requestParameter);
                            break;
                    }

                    if (!constantOrParameter.IsConstant)
                    {
                        methodParameters.Add(constantOrParameter.Parameter);
                    }
                }
                else
                {
                    constantOrParameter = clientParameters[requestParameter.Language.Default.Name];
                }

                if (requestParameter.Protocol.Http is HttpParameter httpParameter)
                {
                    SerializationFormat serializationFormat = BuilderHelpers.GetSerializationFormat(valueSchema);
                    bool skipEncoding = requestParameter.Extensions!.TryGetValue("x-ms-skip-url-encoding", out var value) && Convert.ToBoolean(value);
                    switch (httpParameter.In)
                    {
                        case ParameterLocation.Header:
                            headers.Add(new RequestHeader(serializedName, constantOrParameter, serializationFormat));
                            break;
                        case ParameterLocation.Query:
                            query.Add(new QueryParameter(serializedName, constantOrParameter, GetSerializationStyle(httpParameter, valueSchema), !skipEncoding, serializationFormat));
                            break;
                        case ParameterLocation.Path:
                            pathParameters.Add(serializedName, new PathSegment(constantOrParameter, !skipEncoding, serializationFormat));
                            break;
                        case ParameterLocation.Body:
                            Debug.Assert(httpRequestWithBody != null);
                            var serialization = SerializationBuilder.Build(httpRequestWithBody.KnownMediaType, requestParameter.Schema, requestParameter.IsNullable());
                            body = new RequestBody(constantOrParameter, serialization);
                            break;
                        case ParameterLocation.Uri:
                            if (defaultName == "$host")
                            {
                                skipEncoding = true;
                            }
                            uriParameters[defaultName] = new PathSegment(constantOrParameter, !skipEncoding, serializationFormat);
                            break;
                    }
                }
            }

            if (httpRequestWithBody != null)
            {
                headers.AddRange(httpRequestWithBody.MediaTypes.Select(mediaType => new RequestHeader("Content-Type", BuilderHelpers.StringConstant(mediaType))));
            }

            Request request = new Request(
                ToCoreRequestMethod(httpRequest.Method) ?? RequestMethod.Get,
                ToPathParts(httpRequest.Uri, uriParameters),
                ToPathParts(httpRequest.Path, pathParameters),
                query.ToArray(),
                headers.ToArray(),
                body
            );

            ResponseBody? responseBody = null;
            if (response is SchemaResponse schemaResponse)
            {
                Schema schema = schemaResponse.Schema is ConstantSchema constantSchema ? constantSchema.ValueType : schemaResponse.Schema;
                TypeReference responseType = BuilderHelpers.CreateType(schema, isNullable: false);

                ObjectSerialization serialization = SerializationBuilder.Build(httpResponse.KnownMediaType, schema, isNullable: false);

                responseBody = new ObjectResponseBody(responseType, serialization);
            }
            else if (response is BinaryResponse)
            {
                responseBody = new StreamResponseBody();
            }

            Response clientResponse = new Response(
                responseBody,
                httpResponse.StatusCodes.Select(ToStatusCode).ToArray(),
                BuildResponseHeaderModel(operation, httpResponse)
            );

            string operationName = operation.CSharpName();
            return new Method(
                operationName,
                BuilderHelpers.EscapeXmlDescription(operation.Language.Default.Description),
                request,
                OrderParameters(methodParameters),
                clientResponse,
                new Diagnostic($"{clientName}.{operationName}", Array.Empty<DiagnosticAttribute>())
            );
        }

        private static Method BuildNextPageMethod(Method method)
        {
            var nextPageUrlParameter = new Parameter(
                "nextLink",
                "The URL to the next page of results.",
                new FrameworkTypeReference(typeof(string)),
                null,
                true);
            var headerParameterNames = method.Request.Headers.Where(h => !h.Value.IsConstant).Select(h => h.Value.Parameter.Name).ToArray();
            var parameters = method.Parameters.Where(p =>  headerParameterNames.Contains(p.Name)).Append(nextPageUrlParameter).ToArray();
            var request = new Request(
                method.Request.HttpMethod,
                new[] { new PathSegment(nextPageUrlParameter, false, SerializationFormat.Default),  },
                Array.Empty<PathSegment>(),
                Array.Empty<QueryParameter>(),
                method.Request.Headers,
                null
            );

            return new Method(
                $"{method.Name}NextPage",
                method.Description,
                request,
                parameters,
                method.Response,
                method.Diagnostics);
        }

        private static Paging GetClientMethodPaging(Method method, Method nextPageMethod, IDictionary<object, object> pageable, ObjectSchema? schema)
        {
            var nextLinkName = pageable.GetValue<string>("nextLinkName");
            var itemName = pageable.GetValue<string>("itemName");
            //TODO: Hack to figure out the property name on the model
            var itemProperty = schema?.Properties?.FirstOrDefault(p => p.SerializedName == itemName);
            itemName = itemProperty?.CSharpName() ?? itemName ?? "Value";
            var nextLinkProperty = schema?.Properties?.FirstOrDefault(p => p.SerializedName == nextLinkName);
            nextLinkName = nextLinkProperty?.CSharpName() ?? nextLinkName;
            // If itemName resolves to Value, we can't use itemProperty. So, get the correct property.
            var itemTypeProperty = schema?.Properties?.FirstOrDefault(p => p.CSharpName() == itemName);
            var itemTypeValueSchema = (itemTypeProperty?.Schema as ArraySchema)?.ElementType;
            var itemType = BuilderHelpers.CreateType(itemTypeValueSchema ?? new Schema(), false);
            var name = $"{method.Name}Pageable";
            return new Paging(method, nextPageMethod, name, nextLinkName, itemName, itemType);
        }

        private static Parameter BuildParameter(RequestParameter requestParameter) => new Parameter(
            requestParameter.CSharpName(),
            CreateDescription(requestParameter),
            BuilderHelpers.CreateType(requestParameter.Schema, requestParameter.IsNullable()),
            BuilderHelpers.ParseConstant(requestParameter),
            requestParameter.Required == true);

        private static ResponseHeaderGroupType? BuildResponseHeaderModel(Operation operation, HttpResponse httpResponse)
        {
            if (!httpResponse.Headers.Any())
            {
                return null;
            }

            ResponseHeader CreateResponseHeader(HttpResponseHeader header) =>
                new ResponseHeader(header.Header.ToCleanName(), header.Header, BuilderHelpers.CreateType(header.Schema, true));

            string operationName = operation.CSharpName();

            return new ResponseHeaderGroupType(
                operationName + "Headers",
                $"Header model for {operationName}",
                httpResponse.Headers.Select(CreateResponseHeader).ToArray()
                );
        }

        private static QuerySerializationStyle GetSerializationStyle(HttpParameter httpParameter, Schema valueSchema)
        {
            Debug.Assert(httpParameter.In == ParameterLocation.Query);

            switch (httpParameter.Style)
            {
                case null:
                case SerializationStyle.Form:
                    return valueSchema is ArraySchema ? QuerySerializationStyle.CommaDelimited : QuerySerializationStyle.Simple;
                case SerializationStyle.PipeDelimited:
                    return QuerySerializationStyle.PipeDelimited;
                case SerializationStyle.SpaceDelimited:
                    return QuerySerializationStyle.SpaceDelimited;
                case SerializationStyle.TabDelimited:
                    return QuerySerializationStyle.TabDelimited;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ParameterOrConstant[] ToParts(string httpRequestUri, Dictionary<string, ParameterOrConstant> parameters)
        {
            List<ParameterOrConstant> host = new List<ParameterOrConstant>();
            foreach ((string text, bool isLiteral) in StringExtensions.GetPathParts(httpRequestUri))
            {
                host.Add(isLiteral ? BuilderHelpers.StringConstant(text) : parameters[text]);
            }

            return host.ToArray();
        }

        private static PathSegment[] ToPathParts(string httpRequestUri, Dictionary<string, PathSegment> parameters)
        {
            PathSegment TextSegment(string text)
            {
                return new PathSegment(BuilderHelpers.StringConstant(text), false, SerializationFormat.Default);
            }

            List<PathSegment> host = new List<PathSegment>();
            foreach ((string text, bool isLiteral) in StringExtensions.GetPathParts(httpRequestUri))
            {
                host.Add(isLiteral ? TextSegment(text) : parameters[text]);
            }

            return host.ToArray();
        }

        private static int ToStatusCode(StatusCodes arg) => int.Parse(arg.ToString().Trim('_'));

        private static RequestMethod? ToCoreRequestMethod(HttpMethod method) => method switch
        {
            HttpMethod.Delete => RequestMethod.Delete,
            HttpMethod.Get => RequestMethod.Get,
            HttpMethod.Head => RequestMethod.Head,
            HttpMethod.Options => (RequestMethod?)null,
            HttpMethod.Patch => RequestMethod.Patch,
            HttpMethod.Post => RequestMethod.Post,
            HttpMethod.Put => RequestMethod.Put,
            HttpMethod.Trace => null,
            _ => null
        };

        private static string CreateDescription(RequestParameter requestParameter)
        {
            return string.IsNullOrWhiteSpace(requestParameter.Language.Default.Description) ?
                $"The {requestParameter.Schema.Name} to use." :
                BuilderHelpers.EscapeXmlDescription(requestParameter.Language.Default.Description);
        }
    }
}
