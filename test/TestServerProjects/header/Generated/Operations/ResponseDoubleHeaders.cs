// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure;
using Azure.Core;

namespace header
{
    internal class ResponseDoubleHeaders
    {
        private readonly Response _response;
        public ResponseDoubleHeaders(Response response)
        {
            _response = response;
        }
        public double? Value => _response.Headers.TryGetValue("value", out double? value) ? value : null;
    }
}
