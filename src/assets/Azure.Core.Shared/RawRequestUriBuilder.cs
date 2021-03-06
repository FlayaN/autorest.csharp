// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Core
{
    internal class RawRequestUriBuilder: RequestUriBuilder
    {
        private RawWritingPosition _position = RawWritingPosition.Scheme;

        public void AppendRaw(string value, bool escape)
        {
            const string schemeSeparator = "://";
            const char hostSeparator = '/';
            const char portSeparator = ':';
            char[] hostOrPort = new[] {hostSeparator, portSeparator};
            while (!string.IsNullOrWhiteSpace(value))
            {
                if (_position == RawWritingPosition.Scheme)
                {
                    int separator = value.IndexOf(schemeSeparator, StringComparison.InvariantCultureIgnoreCase);
                    if (separator == -1)
                    {
                        Scheme += value;
                        value = string.Empty;
                    }
                    else
                    {
                        Scheme = value.Substring(0, separator);
                        value = value.Substring(separator + schemeSeparator.Length);
                        _position = RawWritingPosition.Host;
                    }
                }
                else if (_position == RawWritingPosition.Host)
                {
                    int separator = value.IndexOfAny(hostOrPort);
                    if (separator == -1)
                    {
                        Host += value;
                        value = string.Empty;
                    }
                    else
                    {
                        Host = value.Substring(0, separator);

                        _position = value[separator] == hostSeparator ? RawWritingPosition.Rest : RawWritingPosition.Port;

                        value = value.Substring(separator + 1);
                    }
                }
                else if (_position == RawWritingPosition.Port)
                {
                    int separator = value.IndexOf(hostSeparator);
                    if (separator == -1)
                    {
                        Port = int.Parse(value);
                        value = string.Empty;
                    }
                    else
                    {
                        Port = int.Parse(value.Substring(0, separator));
                        value = value.Substring(separator + 1);
                        _position = RawWritingPosition.Rest;
                    }
                }
                else
                {
                    AppendPath(value, escape);
                    value = string.Empty;
                }
            }
        }

        private enum RawWritingPosition
        {
            Scheme,
            Host,
            Port,
            Rest
        }
    }
}
