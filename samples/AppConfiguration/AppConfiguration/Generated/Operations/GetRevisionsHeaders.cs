// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure;
using Azure.Core;

namespace AppConfiguration
{
    internal class GetRevisionsHeaders
    {
        private readonly Azure.Response _response;
        public GetRevisionsHeaders(Azure.Response response)
        {
            _response = response;
        }
        public string? SyncToken => _response.Headers.TryGetValue("Sync-Token", out string? value) ? value : null;
    }
}
