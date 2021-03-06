// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using body_dictionary.Models;

namespace body_dictionary
{
    internal partial class DictionaryOperations
    {
        private string host;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of DictionaryOperations. </summary>
        public DictionaryOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000")
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            this.host = host;
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        internal HttpMessage CreateGetNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/null", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get null dictionary value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, int>>> GetNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetNull");
            scope.Start();
            try
            {
                using var message = CreateGetNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get null dictionary value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, int>> GetNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetNull");
            scope.Start();
            try
            {
                using var message = CreateGetNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetEmptyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/empty", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get empty dictionary value {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, int>>> GetEmptyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetEmptyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get empty dictionary value {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, int>> GetEmpty(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetEmptyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutEmptyRequest(IDictionary<string, string> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/empty", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteStringValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value empty {}. </summary>
        /// <param name="arrayBody"> The Dictionary of string to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutEmptyAsync(IDictionary<string, string> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutEmpty");
            scope.Start();
            try
            {
                using var message = CreatePutEmptyRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value empty {}. </summary>
        /// <param name="arrayBody"> The Dictionary of string to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutEmpty(IDictionary<string, string> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutEmpty");
            scope.Start();
            try
            {
                using var message = CreatePutEmptyRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetNullValueRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/nullvalue", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get Dictionary with null value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, string>>> GetNullValueAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetNullValue");
            scope.Start();
            try
            {
                using var message = CreateGetNullValueRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get Dictionary with null value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, string>> GetNullValue(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetNullValue");
            scope.Start();
            try
            {
                using var message = CreateGetNullValueRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetNullKeyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/nullkey", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get Dictionary with null key. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, string>>> GetNullKeyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetNullKey");
            scope.Start();
            try
            {
                using var message = CreateGetNullKeyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get Dictionary with null key. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, string>> GetNullKey(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetNullKey");
            scope.Start();
            try
            {
                using var message = CreateGetNullKeyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetEmptyStringKeyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/keyemptystring", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get Dictionary with key as empty string. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, string>>> GetEmptyStringKeyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetEmptyStringKey");
            scope.Start();
            try
            {
                using var message = CreateGetEmptyStringKeyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get Dictionary with key as empty string. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, string>> GetEmptyStringKey(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetEmptyStringKey");
            scope.Start();
            try
            {
                using var message = CreateGetEmptyStringKeyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetInvalidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/invalid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get invalid Dictionary value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, string>>> GetInvalidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetInvalid");
            scope.Start();
            try
            {
                using var message = CreateGetInvalidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get invalid Dictionary value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, string>> GetInvalid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetInvalid");
            scope.Start();
            try
            {
                using var message = CreateGetInvalidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetBooleanTfftRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/boolean/tfft", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: true, &quot;1&quot;: false, &quot;2&quot;: false, &quot;3&quot;: true }. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, bool>>> GetBooleanTfftAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBooleanTfft");
            scope.Start();
            try
            {
                using var message = CreateGetBooleanTfftRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, bool> value = new Dictionary<string, bool>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBoolean());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: true, &quot;1&quot;: false, &quot;2&quot;: false, &quot;3&quot;: true }. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, bool>> GetBooleanTfft(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBooleanTfft");
            scope.Start();
            try
            {
                using var message = CreateGetBooleanTfftRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, bool> value = new Dictionary<string, bool>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBoolean());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutBooleanTfftRequest(IDictionary<string, bool> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/boolean/tfft", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteBooleanValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: true, &quot;1&quot;: false, &quot;2&quot;: false, &quot;3&quot;: true }. </summary>
        /// <param name="arrayBody"> The DictionaryOfboolean to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutBooleanTfftAsync(IDictionary<string, bool> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutBooleanTfft");
            scope.Start();
            try
            {
                using var message = CreatePutBooleanTfftRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: true, &quot;1&quot;: false, &quot;2&quot;: false, &quot;3&quot;: true }. </summary>
        /// <param name="arrayBody"> The DictionaryOfboolean to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutBooleanTfft(IDictionary<string, bool> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutBooleanTfft");
            scope.Start();
            try
            {
                using var message = CreatePutBooleanTfftRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetBooleanInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/boolean/true.null.false", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: true, &quot;1&quot;: null, &quot;2&quot;: false }. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, bool>>> GetBooleanInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBooleanInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetBooleanInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, bool> value = new Dictionary<string, bool>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBoolean());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: true, &quot;1&quot;: null, &quot;2&quot;: false }. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, bool>> GetBooleanInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBooleanInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetBooleanInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, bool> value = new Dictionary<string, bool>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBoolean());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetBooleanInvalidStringRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/boolean/true.boolean.false", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get boolean dictionary value &apos;{&quot;0&quot;: true, &quot;1&quot;: &quot;boolean&quot;, &quot;2&quot;: false}&apos;. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, bool>>> GetBooleanInvalidStringAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBooleanInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetBooleanInvalidStringRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, bool> value = new Dictionary<string, bool>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBoolean());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get boolean dictionary value &apos;{&quot;0&quot;: true, &quot;1&quot;: &quot;boolean&quot;, &quot;2&quot;: false}&apos;. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, bool>> GetBooleanInvalidString(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBooleanInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetBooleanInvalidStringRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, bool> value = new Dictionary<string, bool>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBoolean());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetIntegerValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/integer/1.-1.3.300", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, int>>> GetIntegerValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetIntegerValid");
            scope.Start();
            try
            {
                using var message = CreateGetIntegerValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, int>> GetIntegerValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetIntegerValid");
            scope.Start();
            try
            {
                using var message = CreateGetIntegerValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutIntegerValidRequest(IDictionary<string, int> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/integer/1.-1.3.300", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteNumberValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="arrayBody"> The DictionaryOfinteger to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutIntegerValidAsync(IDictionary<string, int> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutIntegerValid");
            scope.Start();
            try
            {
                using var message = CreatePutIntegerValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="arrayBody"> The DictionaryOfinteger to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutIntegerValid(IDictionary<string, int> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutIntegerValid");
            scope.Start();
            try
            {
                using var message = CreatePutIntegerValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetIntInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/integer/1.null.zero", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: null, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, int>>> GetIntInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetIntInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetIntInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: null, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, int>> GetIntInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetIntInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetIntInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetIntInvalidStringRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/integer/1.integer.0", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: &quot;integer&quot;, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, int>>> GetIntInvalidStringAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetIntInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetIntInvalidStringRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: &quot;integer&quot;, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, int>> GetIntInvalidString(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetIntInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetIntInvalidStringRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, int> value = new Dictionary<string, int>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt32());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetLongValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/long/1.-1.3.300", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, long>>> GetLongValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetLongValid");
            scope.Start();
            try
            {
                using var message = CreateGetLongValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, long> value = new Dictionary<string, long>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, long>> GetLongValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetLongValid");
            scope.Start();
            try
            {
                using var message = CreateGetLongValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, long> value = new Dictionary<string, long>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutLongValidRequest(IDictionary<string, long> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/long/1.-1.3.300", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteNumberValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="arrayBody"> The DictionaryOfinteger to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutLongValidAsync(IDictionary<string, long> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutLongValid");
            scope.Start();
            try
            {
                using var message = CreatePutLongValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: 1, &quot;1&quot;: -1, &quot;2&quot;: 3, &quot;3&quot;: 300}. </summary>
        /// <param name="arrayBody"> The DictionaryOfinteger to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutLongValid(IDictionary<string, long> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutLongValid");
            scope.Start();
            try
            {
                using var message = CreatePutLongValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetLongInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/long/1.null.zero", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get long dictionary value {&quot;0&quot;: 1, &quot;1&quot;: null, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, long>>> GetLongInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetLongInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetLongInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, long> value = new Dictionary<string, long>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get long dictionary value {&quot;0&quot;: 1, &quot;1&quot;: null, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, long>> GetLongInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetLongInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetLongInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, long> value = new Dictionary<string, long>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetLongInvalidStringRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/long/1.integer.0", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get long dictionary value {&quot;0&quot;: 1, &quot;1&quot;: &quot;integer&quot;, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, long>>> GetLongInvalidStringAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetLongInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetLongInvalidStringRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, long> value = new Dictionary<string, long>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get long dictionary value {&quot;0&quot;: 1, &quot;1&quot;: &quot;integer&quot;, &quot;2&quot;: 0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, long>> GetLongInvalidString(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetLongInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetLongInvalidStringRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, long> value = new Dictionary<string, long>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetInt64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetFloatValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/float/0--0.01-1.2e20", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, float>>> GetFloatValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetFloatValid");
            scope.Start();
            try
            {
                using var message = CreateGetFloatValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, float> value = new Dictionary<string, float>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetSingle());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, float>> GetFloatValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetFloatValid");
            scope.Start();
            try
            {
                using var message = CreateGetFloatValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, float> value = new Dictionary<string, float>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetSingle());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutFloatValidRequest(IDictionary<string, float> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/float/0--0.01-1.2e20", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteNumberValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="arrayBody"> The DictionaryOfnumber to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutFloatValidAsync(IDictionary<string, float> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutFloatValid");
            scope.Start();
            try
            {
                using var message = CreatePutFloatValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="arrayBody"> The DictionaryOfnumber to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutFloatValid(IDictionary<string, float> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutFloatValid");
            scope.Start();
            try
            {
                using var message = CreatePutFloatValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetFloatInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/float/0.0-null-1.2e20", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0.0, &quot;1&quot;: null, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, float>>> GetFloatInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetFloatInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetFloatInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, float> value = new Dictionary<string, float>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetSingle());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0.0, &quot;1&quot;: null, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, float>> GetFloatInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetFloatInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetFloatInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, float> value = new Dictionary<string, float>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetSingle());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetFloatInvalidStringRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/float/1.number.0", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: 1.0, &quot;1&quot;: &quot;number&quot;, &quot;2&quot;: 0.0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, float>>> GetFloatInvalidStringAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetFloatInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetFloatInvalidStringRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, float> value = new Dictionary<string, float>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetSingle());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: 1.0, &quot;1&quot;: &quot;number&quot;, &quot;2&quot;: 0.0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, float>> GetFloatInvalidString(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetFloatInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetFloatInvalidStringRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, float> value = new Dictionary<string, float>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetSingle());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDoubleValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/double/0--0.01-1.2e20", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, double>>> GetDoubleValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDoubleValid");
            scope.Start();
            try
            {
                using var message = CreateGetDoubleValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, double> value = new Dictionary<string, double>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDouble());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, double>> GetDoubleValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDoubleValid");
            scope.Start();
            try
            {
                using var message = CreateGetDoubleValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, double> value = new Dictionary<string, double>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDouble());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutDoubleValidRequest(IDictionary<string, double> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/double/0--0.01-1.2e20", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteNumberValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="arrayBody"> The DictionaryOfnumber to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutDoubleValidAsync(IDictionary<string, double> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDoubleValid");
            scope.Start();
            try
            {
                using var message = CreatePutDoubleValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value {&quot;0&quot;: 0, &quot;1&quot;: -0.01, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="arrayBody"> The DictionaryOfnumber to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutDoubleValid(IDictionary<string, double> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDoubleValid");
            scope.Start();
            try
            {
                using var message = CreatePutDoubleValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDoubleInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/double/0.0-null-1.2e20", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0.0, &quot;1&quot;: null, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, double>>> GetDoubleInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDoubleInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetDoubleInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, double> value = new Dictionary<string, double>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDouble());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get float dictionary value {&quot;0&quot;: 0.0, &quot;1&quot;: null, &quot;2&quot;: 1.2e20}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, double>> GetDoubleInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDoubleInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetDoubleInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, double> value = new Dictionary<string, double>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDouble());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDoubleInvalidStringRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/double/1.number.0", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: 1.0, &quot;1&quot;: &quot;number&quot;, &quot;2&quot;: 0.0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, double>>> GetDoubleInvalidStringAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDoubleInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetDoubleInvalidStringRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, double> value = new Dictionary<string, double>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDouble());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get boolean dictionary value {&quot;0&quot;: 1.0, &quot;1&quot;: &quot;number&quot;, &quot;2&quot;: 0.0}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, double>> GetDoubleInvalidString(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDoubleInvalidString");
            scope.Start();
            try
            {
                using var message = CreateGetDoubleInvalidStringRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, double> value = new Dictionary<string, double>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDouble());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetStringValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/string/foo1.foo2.foo3", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get string dictionary value {&quot;0&quot;: &quot;foo1&quot;, &quot;1&quot;: &quot;foo2&quot;, &quot;2&quot;: &quot;foo3&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, string>>> GetStringValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetStringValid");
            scope.Start();
            try
            {
                using var message = CreateGetStringValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get string dictionary value {&quot;0&quot;: &quot;foo1&quot;, &quot;1&quot;: &quot;foo2&quot;, &quot;2&quot;: &quot;foo3&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, string>> GetStringValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetStringValid");
            scope.Start();
            try
            {
                using var message = CreateGetStringValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutStringValidRequest(IDictionary<string, string> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/string/foo1.foo2.foo3", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteStringValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value {&quot;0&quot;: &quot;foo1&quot;, &quot;1&quot;: &quot;foo2&quot;, &quot;2&quot;: &quot;foo3&quot;}. </summary>
        /// <param name="arrayBody"> The Dictionary of string to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutStringValidAsync(IDictionary<string, string> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutStringValid");
            scope.Start();
            try
            {
                using var message = CreatePutStringValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value {&quot;0&quot;: &quot;foo1&quot;, &quot;1&quot;: &quot;foo2&quot;, &quot;2&quot;: &quot;foo3&quot;}. </summary>
        /// <param name="arrayBody"> The Dictionary of string to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutStringValid(IDictionary<string, string> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutStringValid");
            scope.Start();
            try
            {
                using var message = CreatePutStringValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetStringWithNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/string/foo.null.foo2", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get string dictionary value {&quot;0&quot;: &quot;foo&quot;, &quot;1&quot;: null, &quot;2&quot;: &quot;foo2&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, string>>> GetStringWithNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetStringWithNull");
            scope.Start();
            try
            {
                using var message = CreateGetStringWithNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get string dictionary value {&quot;0&quot;: &quot;foo&quot;, &quot;1&quot;: null, &quot;2&quot;: &quot;foo2&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, string>> GetStringWithNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetStringWithNull");
            scope.Start();
            try
            {
                using var message = CreateGetStringWithNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetStringWithInvalidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/string/foo.123.foo2", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get string dictionary value {&quot;0&quot;: &quot;foo&quot;, &quot;1&quot;: 123, &quot;2&quot;: &quot;foo2&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, string>>> GetStringWithInvalidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetStringWithInvalid");
            scope.Start();
            try
            {
                using var message = CreateGetStringWithInvalidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get string dictionary value {&quot;0&quot;: &quot;foo&quot;, &quot;1&quot;: 123, &quot;2&quot;: &quot;foo2&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, string>> GetStringWithInvalid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetStringWithInvalid");
            scope.Start();
            try
            {
                using var message = CreateGetStringWithInvalidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, string> value = new Dictionary<string, string>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetString());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDateValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: &quot;2000-12-01&quot;, &quot;1&quot;: &quot;1980-01-02&quot;, &quot;2&quot;: &quot;1492-10-12&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, DateTimeOffset>>> GetDateValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateValid");
            scope.Start();
            try
            {
                using var message = CreateGetDateValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("D"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get integer dictionary value {&quot;0&quot;: &quot;2000-12-01&quot;, &quot;1&quot;: &quot;1980-01-02&quot;, &quot;2&quot;: &quot;1492-10-12&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, DateTimeOffset>> GetDateValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateValid");
            scope.Start();
            try
            {
                using var message = CreateGetDateValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("D"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutDateValidRequest(IDictionary<string, DateTimeOffset> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteStringValue(item.Value, "D");
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value  {&quot;0&quot;: &quot;2000-12-01&quot;, &quot;1&quot;: &quot;1980-01-02&quot;, &quot;2&quot;: &quot;1492-10-12&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfdate to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutDateValidAsync(IDictionary<string, DateTimeOffset> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDateValid");
            scope.Start();
            try
            {
                using var message = CreatePutDateValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value  {&quot;0&quot;: &quot;2000-12-01&quot;, &quot;1&quot;: &quot;1980-01-02&quot;, &quot;2&quot;: &quot;1492-10-12&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfdate to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutDateValid(IDictionary<string, DateTimeOffset> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDateValid");
            scope.Start();
            try
            {
                using var message = CreatePutDateValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDateInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date/invalidnull", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2012-01-01&quot;, &quot;1&quot;: null, &quot;2&quot;: &quot;1776-07-04&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, DateTimeOffset>>> GetDateInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetDateInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("D"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2012-01-01&quot;, &quot;1&quot;: null, &quot;2&quot;: &quot;1776-07-04&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, DateTimeOffset>> GetDateInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetDateInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("D"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDateInvalidCharsRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date/invalidchars", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2011-03-22&quot;, &quot;1&quot;: &quot;date&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, DateTimeOffset>>> GetDateInvalidCharsAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateInvalidChars");
            scope.Start();
            try
            {
                using var message = CreateGetDateInvalidCharsRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("D"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2011-03-22&quot;, &quot;1&quot;: &quot;date&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, DateTimeOffset>> GetDateInvalidChars(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateInvalidChars");
            scope.Start();
            try
            {
                using var message = CreateGetDateInvalidCharsRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("D"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDateTimeValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date-time/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get date-time dictionary value {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: &quot;1980-01-02T00:11:35+01:00&quot;, &quot;2&quot;: &quot;1492-10-12T10:15:01-08:00&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, DateTimeOffset>>> GetDateTimeValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeValid");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("S"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get date-time dictionary value {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: &quot;1980-01-02T00:11:35+01:00&quot;, &quot;2&quot;: &quot;1492-10-12T10:15:01-08:00&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, DateTimeOffset>> GetDateTimeValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeValid");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("S"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutDateTimeValidRequest(IDictionary<string, DateTimeOffset> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date-time/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteStringValue(item.Value, "S");
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value  {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: &quot;1980-01-02T00:11:35+01:00&quot;, &quot;2&quot;: &quot;1492-10-12T10:15:01-08:00&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfdate-time to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutDateTimeValidAsync(IDictionary<string, DateTimeOffset> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDateTimeValid");
            scope.Start();
            try
            {
                using var message = CreatePutDateTimeValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value  {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: &quot;1980-01-02T00:11:35+01:00&quot;, &quot;2&quot;: &quot;1492-10-12T10:15:01-08:00&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfdate-time to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutDateTimeValid(IDictionary<string, DateTimeOffset> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDateTimeValid");
            scope.Start();
            try
            {
                using var message = CreatePutDateTimeValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDateTimeInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date-time/invalidnull", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: null}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, DateTimeOffset>>> GetDateTimeInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("S"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: null}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, DateTimeOffset>> GetDateTimeInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("S"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDateTimeInvalidCharsRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date-time/invalidchars", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: &quot;date-time&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, DateTimeOffset>>> GetDateTimeInvalidCharsAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeInvalidChars");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeInvalidCharsRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("S"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get date dictionary value {&quot;0&quot;: &quot;2000-12-01t00:00:01z&quot;, &quot;1&quot;: &quot;date-time&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, DateTimeOffset>> GetDateTimeInvalidChars(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeInvalidChars");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeInvalidCharsRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("S"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDateTimeRfc1123ValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date-time-rfc1123/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get date-time-rfc1123 dictionary value {&quot;0&quot;: &quot;Fri, 01 Dec 2000 00:00:01 GMT&quot;, &quot;1&quot;: &quot;Wed, 02 Jan 1980 00:11:35 GMT&quot;, &quot;2&quot;: &quot;Wed, 12 Oct 1492 10:15:01 GMT&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, DateTimeOffset>>> GetDateTimeRfc1123ValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeRfc1123Valid");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeRfc1123ValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("R"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get date-time-rfc1123 dictionary value {&quot;0&quot;: &quot;Fri, 01 Dec 2000 00:00:01 GMT&quot;, &quot;1&quot;: &quot;Wed, 02 Jan 1980 00:11:35 GMT&quot;, &quot;2&quot;: &quot;Wed, 12 Oct 1492 10:15:01 GMT&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, DateTimeOffset>> GetDateTimeRfc1123Valid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDateTimeRfc1123Valid");
            scope.Start();
            try
            {
                using var message = CreateGetDateTimeRfc1123ValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, DateTimeOffset> value = new Dictionary<string, DateTimeOffset>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetDateTimeOffset("R"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutDateTimeRfc1123ValidRequest(IDictionary<string, DateTimeOffset> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/date-time-rfc1123/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteStringValue(item.Value, "R");
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: &quot;Fri, 01 Dec 2000 00:00:01 GMT&quot;, &quot;1&quot;: &quot;Wed, 02 Jan 1980 00:11:35 GMT&quot;, &quot;2&quot;: &quot;Wed, 12 Oct 1492 10:15:01 GMT&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfdate-time to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutDateTimeRfc1123ValidAsync(IDictionary<string, DateTimeOffset> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDateTimeRfc1123Valid");
            scope.Start();
            try
            {
                using var message = CreatePutDateTimeRfc1123ValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value empty {&quot;0&quot;: &quot;Fri, 01 Dec 2000 00:00:01 GMT&quot;, &quot;1&quot;: &quot;Wed, 02 Jan 1980 00:11:35 GMT&quot;, &quot;2&quot;: &quot;Wed, 12 Oct 1492 10:15:01 GMT&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfdate-time to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutDateTimeRfc1123Valid(IDictionary<string, DateTimeOffset> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDateTimeRfc1123Valid");
            scope.Start();
            try
            {
                using var message = CreatePutDateTimeRfc1123ValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDurationValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/duration/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get duration dictionary value {&quot;0&quot;: &quot;P123DT22H14M12.011S&quot;, &quot;1&quot;: &quot;P5DT1H0M0S&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, TimeSpan>>> GetDurationValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDurationValid");
            scope.Start();
            try
            {
                using var message = CreateGetDurationValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, TimeSpan> value = new Dictionary<string, TimeSpan>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetTimeSpan("P"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get duration dictionary value {&quot;0&quot;: &quot;P123DT22H14M12.011S&quot;, &quot;1&quot;: &quot;P5DT1H0M0S&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, TimeSpan>> GetDurationValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDurationValid");
            scope.Start();
            try
            {
                using var message = CreateGetDurationValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, TimeSpan> value = new Dictionary<string, TimeSpan>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetTimeSpan("P"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutDurationValidRequest(IDictionary<string, TimeSpan> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/duration/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteStringValue(item.Value, "P");
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Set dictionary value  {&quot;0&quot;: &quot;P123DT22H14M12.011S&quot;, &quot;1&quot;: &quot;P5DT1H0M0S&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfduration to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutDurationValidAsync(IDictionary<string, TimeSpan> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDurationValid");
            scope.Start();
            try
            {
                using var message = CreatePutDurationValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Set dictionary value  {&quot;0&quot;: &quot;P123DT22H14M12.011S&quot;, &quot;1&quot;: &quot;P5DT1H0M0S&quot;}. </summary>
        /// <param name="arrayBody"> The DictionaryOfduration to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutDurationValid(IDictionary<string, TimeSpan> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDurationValid");
            scope.Start();
            try
            {
                using var message = CreatePutDurationValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetByteValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/byte/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get byte dictionary value {&quot;0&quot;: hex(FF FF FF FA), &quot;1&quot;: hex(01 02 03), &quot;2&quot;: hex (25, 29, 43)} with each item encoded in base64. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, byte[]>>> GetByteValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetByteValid");
            scope.Start();
            try
            {
                using var message = CreateGetByteValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, byte[]> value = new Dictionary<string, byte[]>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBytesFromBase64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get byte dictionary value {&quot;0&quot;: hex(FF FF FF FA), &quot;1&quot;: hex(01 02 03), &quot;2&quot;: hex (25, 29, 43)} with each item encoded in base64. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, byte[]>> GetByteValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetByteValid");
            scope.Start();
            try
            {
                using var message = CreateGetByteValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, byte[]> value = new Dictionary<string, byte[]>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBytesFromBase64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutByteValidRequest(IDictionary<string, byte[]> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/byte/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteBase64StringValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Put the dictionary value {&quot;0&quot;: hex(FF FF FF FA), &quot;1&quot;: hex(01 02 03), &quot;2&quot;: hex (25, 29, 43)} with each elementencoded in base 64. </summary>
        /// <param name="arrayBody"> The DictionaryOfbyte-array to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutByteValidAsync(IDictionary<string, byte[]> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutByteValid");
            scope.Start();
            try
            {
                using var message = CreatePutByteValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Put the dictionary value {&quot;0&quot;: hex(FF FF FF FA), &quot;1&quot;: hex(01 02 03), &quot;2&quot;: hex (25, 29, 43)} with each elementencoded in base 64. </summary>
        /// <param name="arrayBody"> The DictionaryOfbyte-array to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutByteValid(IDictionary<string, byte[]> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutByteValid");
            scope.Start();
            try
            {
                using var message = CreatePutByteValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetByteInvalidNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/byte/invalidnull", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get byte dictionary value {&quot;0&quot;: hex(FF FF FF FA), &quot;1&quot;: null} with the first item base64 encoded. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, byte[]>>> GetByteInvalidNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetByteInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetByteInvalidNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, byte[]> value = new Dictionary<string, byte[]>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBytesFromBase64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get byte dictionary value {&quot;0&quot;: hex(FF FF FF FA), &quot;1&quot;: null} with the first item base64 encoded. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, byte[]>> GetByteInvalidNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetByteInvalidNull");
            scope.Start();
            try
            {
                using var message = CreateGetByteInvalidNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, byte[]> value = new Dictionary<string, byte[]>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBytesFromBase64());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetBase64UrlRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/prim/base64url/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get base64url dictionary value {&quot;0&quot;: &quot;a string that gets encoded with base64url&quot;, &quot;1&quot;: &quot;test string&quot;, &quot;2&quot;: &quot;Lorem ipsum&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, byte[]>>> GetBase64UrlAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBase64Url");
            scope.Start();
            try
            {
                using var message = CreateGetBase64UrlRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, byte[]> value = new Dictionary<string, byte[]>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBytesFromBase64("U"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get base64url dictionary value {&quot;0&quot;: &quot;a string that gets encoded with base64url&quot;, &quot;1&quot;: &quot;test string&quot;, &quot;2&quot;: &quot;Lorem ipsum&quot;}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, byte[]>> GetBase64Url(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetBase64Url");
            scope.Start();
            try
            {
                using var message = CreateGetBase64UrlRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, byte[]> value = new Dictionary<string, byte[]>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetBytesFromBase64("U"));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetComplexNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/complex/null", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get dictionary of complex type null value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, Widget>>> GetComplexNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexNull");
            scope.Start();
            try
            {
                using var message = CreateGetComplexNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get dictionary of complex type null value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, Widget>> GetComplexNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexNull");
            scope.Start();
            try
            {
                using var message = CreateGetComplexNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetComplexEmptyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/complex/empty", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get empty dictionary of complex type {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, Widget>>> GetComplexEmptyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetComplexEmptyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get empty dictionary of complex type {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, Widget>> GetComplexEmpty(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetComplexEmptyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetComplexItemNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/complex/itemnull", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get dictionary of complex type with null item {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1&quot;: null, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, Widget>>> GetComplexItemNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexItemNull");
            scope.Start();
            try
            {
                using var message = CreateGetComplexItemNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get dictionary of complex type with null item {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1&quot;: null, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, Widget>> GetComplexItemNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexItemNull");
            scope.Start();
            try
            {
                using var message = CreateGetComplexItemNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetComplexItemEmptyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/complex/itemempty", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get dictionary of complex type with empty item {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1:&quot; {}, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, Widget>>> GetComplexItemEmptyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexItemEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetComplexItemEmptyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get dictionary of complex type with empty item {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1:&quot; {}, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, Widget>> GetComplexItemEmpty(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexItemEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetComplexItemEmptyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetComplexValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/complex/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get dictionary of complex type with {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1&quot;: {&quot;integer&quot;: 3, &quot;string&quot;: &quot;4&quot;}, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, Widget>>> GetComplexValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexValid");
            scope.Start();
            try
            {
                using var message = CreateGetComplexValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get dictionary of complex type with {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1&quot;: {&quot;integer&quot;: 3, &quot;string&quot;: &quot;4&quot;}, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, Widget>> GetComplexValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetComplexValid");
            scope.Start();
            try
            {
                using var message = CreateGetComplexValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, Widget> value = new Dictionary<string, Widget>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, Widget.DeserializeWidget(property.Value));
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutComplexValidRequest(IDictionary<string, Widget> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/complex/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteObjectValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Put an dictionary of complex type with values {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1&quot;: {&quot;integer&quot;: 3, &quot;string&quot;: &quot;4&quot;}, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="arrayBody"> The DictionaryOfWidget to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutComplexValidAsync(IDictionary<string, Widget> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutComplexValid");
            scope.Start();
            try
            {
                using var message = CreatePutComplexValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Put an dictionary of complex type with values {&quot;0&quot;: {&quot;integer&quot;: 1, &quot;string&quot;: &quot;2&quot;}, &quot;1&quot;: {&quot;integer&quot;: 3, &quot;string&quot;: &quot;4&quot;}, &quot;2&quot;: {&quot;integer&quot;: 5, &quot;string&quot;: &quot;6&quot;}}. </summary>
        /// <param name="arrayBody"> The DictionaryOfWidget to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutComplexValid(IDictionary<string, Widget> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutComplexValid");
            scope.Start();
            try
            {
                using var message = CreatePutComplexValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetArrayNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/array/null", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get a null array. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, ICollection<string>>>> GetArrayNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayNull");
            scope.Start();
            try
            {
                using var message = CreateGetArrayNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get a null array. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, ICollection<string>>> GetArrayNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayNull");
            scope.Start();
            try
            {
                using var message = CreateGetArrayNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetArrayEmptyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/array/empty", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an empty dictionary {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, ICollection<string>>>> GetArrayEmptyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetArrayEmptyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an empty dictionary {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, ICollection<string>>> GetArrayEmpty(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetArrayEmptyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetArrayItemNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/array/itemnull", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an dictionary of array of strings {&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: null, &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, ICollection<string>>>> GetArrayItemNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayItemNull");
            scope.Start();
            try
            {
                using var message = CreateGetArrayItemNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an dictionary of array of strings {&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: null, &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, ICollection<string>>> GetArrayItemNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayItemNull");
            scope.Start();
            try
            {
                using var message = CreateGetArrayItemNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetArrayItemEmptyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/array/itemempty", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an array of array of strings [{&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: [], &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, ICollection<string>>>> GetArrayItemEmptyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayItemEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetArrayItemEmptyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an array of array of strings [{&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: [], &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, ICollection<string>>> GetArrayItemEmpty(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayItemEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetArrayItemEmptyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetArrayValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/array/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an array of array of strings {&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: [&quot;4&quot;, &quot;5&quot;, &quot;6&quot;], &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, ICollection<string>>>> GetArrayValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayValid");
            scope.Start();
            try
            {
                using var message = CreateGetArrayValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an array of array of strings {&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: [&quot;4&quot;, &quot;5&quot;, &quot;6&quot;], &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, ICollection<string>>> GetArrayValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetArrayValid");
            scope.Start();
            try
            {
                using var message = CreateGetArrayValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, ICollection<string>> value = new Dictionary<string, ICollection<string>>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                ICollection<string> value0 = new List<string>();
                                foreach (var item in property.Value.EnumerateArray())
                                {
                                    value0.Add(item.GetString());
                                }
                                value.Add(property.Name, value0);
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutArrayValidRequest(IDictionary<string, ICollection<string>> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/array/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteStartArray();
                foreach (var item0 in item.Value)
                {
                    content.JsonWriter.WriteStringValue(item0);
                }
                content.JsonWriter.WriteEndArray();
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Put An array of array of strings {&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: [&quot;4&quot;, &quot;5&quot;, &quot;6&quot;], &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="arrayBody"> The DictionaryOfpaths·dictionary-array-valid·put·requestbody·content·application-json·schema·additionalproperties to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutArrayValidAsync(IDictionary<string, ICollection<string>> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutArrayValid");
            scope.Start();
            try
            {
                using var message = CreatePutArrayValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Put An array of array of strings {&quot;0&quot;: [&quot;1&quot;, &quot;2&quot;, &quot;3&quot;], &quot;1&quot;: [&quot;4&quot;, &quot;5&quot;, &quot;6&quot;], &quot;2&quot;: [&quot;7&quot;, &quot;8&quot;, &quot;9&quot;]}. </summary>
        /// <param name="arrayBody"> The DictionaryOfpaths·dictionary-array-valid·put·requestbody·content·application-json·schema·additionalproperties to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutArrayValid(IDictionary<string, ICollection<string>> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutArrayValid");
            scope.Start();
            try
            {
                using var message = CreatePutArrayValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDictionaryNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/dictionary/null", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an dictionaries of dictionaries with value null. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, object>>> GetDictionaryNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryNull");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an dictionaries of dictionaries with value null. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, object>> GetDictionaryNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryNull");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDictionaryEmptyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/dictionary/empty", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, object>>> GetDictionaryEmptyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryEmptyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, object>> GetDictionaryEmpty(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryEmptyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDictionaryItemNullRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/dictionary/itemnull", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: null, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, object>>> GetDictionaryItemNullAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryItemNull");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryItemNullRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: null, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, object>> GetDictionaryItemNull(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryItemNull");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryItemNullRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDictionaryItemEmptyRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/dictionary/itemempty", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: {}, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, object>>> GetDictionaryItemEmptyAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryItemEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryItemEmptyRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: {}, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, object>> GetDictionaryItemEmpty(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryItemEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryItemEmptyRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetDictionaryValidRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/dictionary/valid", false);
            request.Uri = uri;
            return message;
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: {&quot;4&quot;: &quot;four&quot;, &quot;5&quot;: &quot;five&quot;, &quot;6&quot;: &quot;six&quot;}, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<IDictionary<string, object>>> GetDictionaryValidAsync(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryValid");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryValidRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: {&quot;4&quot;: &quot;four&quot;, &quot;5&quot;: &quot;five&quot;, &quot;6&quot;: &quot;six&quot;}, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IDictionary<string, object>> GetDictionaryValid(CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.GetDictionaryValid");
            scope.Start();
            try
            {
                using var message = CreateGetDictionaryValidRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = JsonDocument.Parse(message.Response.ContentStream);
                            IDictionary<string, object> value = new Dictionary<string, object>();
                            foreach (var property in document.RootElement.EnumerateObject())
                            {
                                value.Add(property.Name, property.Value.GetObject());
                            }
                            return Response.FromValue(value, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreatePutDictionaryValidRequest(IDictionary<string, object> arrayBody)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/dictionary/dictionary/valid", false);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteStartObject();
            foreach (var item in arrayBody)
            {
                content.JsonWriter.WritePropertyName(item.Key);
                content.JsonWriter.WriteObjectValue(item.Value);
            }
            content.JsonWriter.WriteEndObject();
            request.Content = content;
            return message;
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: {&quot;4&quot;: &quot;four&quot;, &quot;5&quot;: &quot;five&quot;, &quot;6&quot;: &quot;six&quot;}, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="arrayBody"> The DictionaryOfany to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutDictionaryValidAsync(IDictionary<string, object> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDictionaryValid");
            scope.Start();
            try
            {
                using var message = CreatePutDictionaryValidRequest(arrayBody);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get an dictionaries of dictionaries of type &lt;string, string&gt; with value {&quot;0&quot;: {&quot;1&quot;: &quot;one&quot;, &quot;2&quot;: &quot;two&quot;, &quot;3&quot;: &quot;three&quot;}, &quot;1&quot;: {&quot;4&quot;: &quot;four&quot;, &quot;5&quot;: &quot;five&quot;, &quot;6&quot;: &quot;six&quot;}, &quot;2&quot;: {&quot;7&quot;: &quot;seven&quot;, &quot;8&quot;: &quot;eight&quot;, &quot;9&quot;: &quot;nine&quot;}}. </summary>
        /// <param name="arrayBody"> The DictionaryOfany to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response PutDictionaryValid(IDictionary<string, object> arrayBody, CancellationToken cancellationToken = default)
        {
            if (arrayBody == null)
            {
                throw new ArgumentNullException(nameof(arrayBody));
            }

            using var scope = clientDiagnostics.CreateScope("DictionaryOperations.PutDictionaryValid");
            scope.Start();
            try
            {
                using var message = CreatePutDictionaryValidRequest(arrayBody);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
