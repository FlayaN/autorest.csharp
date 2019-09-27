﻿using System;
using System.Collections.Generic;
using SharpYaml.Serialization;

namespace AutoRest.CSharp.V3.PipelineModels
{
    internal static class CodeModelDeserializer
    {
        private static SerializerSettings RegisterTagMapping(this SerializerSettings serializerSettings, IEnumerable<KeyValuePair<string, Type>> mapping)
        {
            foreach (var (tagName, tagType) in mapping)
            {
                serializerSettings.RegisterTagMapping(tagName, tagType);
            }
            return serializerSettings;
        }

        private static KeyValuePair<string, Type> CreateTagPair<T>() => new KeyValuePair<string, Type>($"!{typeof(T).Name}", typeof(T));

        // From: https://github.com/Azure/perks/blob/57a85fe6e26629ee6b420753d3b6b4f1db4b2719/codemodel/model/yaml-schema.ts#L28-L97
        private static readonly IEnumerable<KeyValuePair<string, Type>> TagMap = new[]
        {
            CreateTagPair<HttpModel>(),
            CreateTagPair<HttpParameter>(),
            CreateTagPair<HttpStreamRequest>(),
            CreateTagPair<HttpMultipartRequest>(),
            CreateTagPair<HttpResponse>(),
            CreateTagPair<HttpStreamResponse>(),
            CreateTagPair<HttpWithBodyRequest>(),
            CreateTagPair<HttpRequest>(),
            CreateTagPair<SchemaResponse>(),
            CreateTagPair<StreamResponse>(),
            CreateTagPair<Response>(),
            CreateTagPair<Parameter>(),
            CreateTagPair<Property>(),
            CreateTagPair<Value>(),
            CreateTagPair<Operation>(),
            CreateTagPair<ParameterGroupSchema>(),
            CreateTagPair<FlagSchema>(),
            CreateTagPair<FlagValue>(),
            CreateTagPair<NumberSchema>(),
            CreateTagPair<StringSchema>(),
            CreateTagPair<ArraySchema>(),
            CreateTagPair<ObjectSchema>(),
            CreateTagPair<ChoiceValue>(),
            CreateTagPair<ConstantValue>(),
            CreateTagPair<ChoiceSchema>(),
            CreateTagPair<SealedChoiceSchema>(),
            CreateTagPair<ConstantSchema>(),
            CreateTagPair<BooleanSchema>(),
            CreateTagPair<ODataQuerySchema>(),
            CreateTagPair<CredentialSchema>(),
            CreateTagPair<UriSchema>(),
            CreateTagPair<UuidSchema>(),
            CreateTagPair<DurationSchema>(),
            CreateTagPair<DateTimeSchema>(),
            CreateTagPair<DateSchema>(),
            CreateTagPair<CharSchema>(),
            CreateTagPair<ByteArraySchema>(),
            CreateTagPair<UnixTimeSchema>(),
            CreateTagPair<DictionarySchema>(),
            CreateTagPair<AndSchema>(),
            CreateTagPair<OrSchema>(),
            CreateTagPair<XorSchema>(),
            CreateTagPair<Schema>(),
            CreateTagPair<CodeModel>(),
            CreateTagPair<Request>(),
            CreateTagPair<Schemas>(),
            CreateTagPair<Discriminator>(),
            CreateTagPair<ExternalDocumentation>(),
            CreateTagPair<Contact>(),
            CreateTagPair<Info>(),
            CreateTagPair<License>(),
            CreateTagPair<Metadata>(),
            CreateTagPair<OperationGroup>(),
            CreateTagPair<APIKeySecurityScheme>(),
            CreateTagPair<BearerHTTPSecurityScheme>(),
            CreateTagPair<ImplicitOAuthFlow>(),
            CreateTagPair<NonBearerHTTPSecurityScheme>(),
            CreateTagPair<OAuth2SecurityScheme>(),
            CreateTagPair<OAuthFlows>(),
            CreateTagPair<OpenIdConnectSecurityScheme>(),
            CreateTagPair<PasswordOAuthFlow>(),
            CreateTagPair<AuthorizationCodeOAuthFlow>(),
            CreateTagPair<ClientCredentialsFlow>(),
            CreateTagPair<HttpServer>(),
            CreateTagPair<ServerVariable>(),
            //CreateTagPair<Languages>(),
            new KeyValuePair<string, Type>("!Languages", typeof(LanguagesOfSchemaMetadata)),
            CreateTagPair<Protocols>(),
            CreateTagPair<ApiVersion>()
            //CreateTagPair<Primitives>()
        };

        private static SerializerSettings _serializerSettings;
        private static SerializerSettings SerializerSettings => _serializerSettings ??= new SerializerSettings().RegisterTagMapping(TagMap);

        public static CodeModel CreateCodeModel(string yaml)
        {
            var serializer = new Serializer(SerializerSettings);
            return serializer.Deserialize<CodeModel>(yaml);
        }
    }
}