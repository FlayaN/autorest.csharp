// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;

namespace body_complex.Models
{
    /// <summary> MISSING·SCHEMA-DESCRIPTION-OBJECTSCHEMA. </summary>
    public partial class SmartSalmon : Salmon, IDictionary<string, object>
    {
        /// <summary> Initializes a new instance of SmartSalmon. </summary>
        public SmartSalmon()
        {
            Fishtype = "smart_salmon";
        }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string? CollegeDegree { get; set; }
        private readonly IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();
        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _additionalProperties.GetEnumerator();
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _additionalProperties.GetEnumerator();
        /// <inheritdoc />
        public ICollection<string> Keys => _additionalProperties.Keys;
        /// <inheritdoc />
        public ICollection<object> Values => _additionalProperties.Values;
        /// <inheritdoc />
        public bool TryGetValue(string key, out object value) => _additionalProperties.TryGetValue(key, out value);
        /// <inheritdoc />
        public void Add(string key, object value) => _additionalProperties.Add(key, value);
        /// <inheritdoc />
        public bool ContainsKey(string key) => _additionalProperties.ContainsKey(key);
        /// <inheritdoc />
        public bool Remove(string key) => _additionalProperties.Remove(key);
        /// <inheritdoc />
        public int Count => _additionalProperties.Count;
        /// <inheritdoc />
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly => _additionalProperties.IsReadOnly;
        /// <inheritdoc />
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> value) => _additionalProperties.Add(value);
        /// <inheritdoc />
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> value) => _additionalProperties.Remove(value);
        /// <inheritdoc />
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> value) => _additionalProperties.Contains(value);
        /// <inheritdoc />
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] destination, int offset) => _additionalProperties.CopyTo(destination, offset);
        /// <inheritdoc />
        void ICollection<KeyValuePair<string, object>>.Clear() => _additionalProperties.Clear();
        /// <inheritdoc />
        public object this[string key]
        {
            get => _additionalProperties[key];
            set => _additionalProperties[key] = value;
        }
    }
}
