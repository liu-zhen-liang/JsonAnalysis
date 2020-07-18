using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Json.Analysis
{
    /// <summary>
    /// JSON的对象
    /// </summary>
    public class JsonObject : JsonElement, IDictionary<string, JsonElement>, IEquatable<JsonObject>
    {
        public JsonObject()
        {
            _propertyMap = new Dictionary<string, JsonElement>();
        }

        /// <summary>
        /// 属性字典
        /// </summary>
        private readonly Dictionary<string, JsonElement> _propertyMap;

        public bool ContainsKey(string key) => _propertyMap.ContainsKey(key);

        public void Add(string key, JsonElement value) => _propertyMap.Add(key, value);

        public bool Remove(string key) => _propertyMap.Remove(key);

        public bool TryGetValue(string key, out JsonElement value) => _propertyMap.TryGetValue(key, out value);

        public JsonElement this[string name]
        {
            get => _propertyMap[name];
            set => _propertyMap[name] = value;
        }

        public ICollection<string> Keys => _propertyMap.Keys;
        public ICollection<JsonElement> Values => _propertyMap.Values;

        public IEnumerator<KeyValuePair<string, JsonElement>> GetEnumerator() => _propertyMap.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, JsonElement> item) => _propertyMap.Add(item.Key, item.Value);

        public void Clear() => _propertyMap.Clear();

        public bool Contains(KeyValuePair<string, JsonElement> item) => _propertyMap.Contains(item);

        void ICollection<KeyValuePair<string, JsonElement>>.CopyTo(KeyValuePair<string, JsonElement>[] array,
            int arrayIndex)
        {
            throw new Exception();
        }

        public bool Remove(KeyValuePair<string, JsonElement> item)
        {
            return _propertyMap.Remove(item.Key);
        }

        public int Count => _propertyMap.Count;
        public bool IsReadOnly => false;

        public bool Equals(JsonObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.Count != this.Count) return false;
            foreach (var property in this._propertyMap)
            {
                if (!other.TryGetValue(property.Key, out var value) || !value.Equals(property.Value))
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JsonObject) obj);
        }

        public override int GetHashCode()
        {
            return _propertyMap.Aggregate(0,
                (current, jsonElement) => current ^ jsonElement.Key.GetHashCode() ^ jsonElement.Value.GetHashCode());
        }

        public override string ToString()
        {
            return $"{{{string.Join(",", _propertyMap.Select(x => $"\"{x.Key}\":{x.Value}"))}}}";
        }
    }
}