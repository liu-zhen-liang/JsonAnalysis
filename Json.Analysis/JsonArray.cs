using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Json.Analysis
{
    /// <summary>
    /// Json集合
    /// </summary>
    public class JsonArray : JsonElement, IList<JsonElement>,IEquatable<JsonArray>
    {
        public JsonArray()
        {
            _Elements = new List<JsonElement>();
        }

        private readonly List<JsonElement> _Elements;

        public IEnumerator<JsonElement> GetEnumerator()
        {
            return _Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(JsonElement item) => _Elements.Add(item);

        public void Clear() => _Elements.Clear();

        public bool Contains(JsonElement item) => _Elements.Contains(item);

        public void CopyTo(JsonElement[] array, int arrayIndex) => _Elements.CopyTo(array, arrayIndex);

        public bool Remove(JsonElement item) => _Elements.Remove(item);

        public int Count => _Elements.Count;
        public bool IsReadOnly => false;
        public int IndexOf(JsonElement item) => _Elements.IndexOf(item);

        public void Insert(int index, JsonElement item) => _Elements.Insert(index, item);

        public void RemoveAt(int index) => _Elements.RemoveAt(index);

        public JsonElement this[int index]
        {
            get => _Elements[index];
            set => _Elements[index] = value;
        }

        public override string ToString()
        {
            return $"[{string.Join(",", this)}]";
        }

        public bool Equals(JsonArray other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return !other._Elements.Where((t, i) => !t.Equals(this._Elements[i])).Any();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JsonArray) obj);
        }

        public override int GetHashCode()
        {
            return _Elements.Aggregate(0,
                (current, jsonElement) => current ^ jsonElement.GetHashCode());
        }
    }
}