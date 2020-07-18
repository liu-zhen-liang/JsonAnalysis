using System;

namespace Json.Analysis
{
    /// <summary>
    /// JSON NULL 类型
    /// </summary>
    public class JsonNull : JsonElement,IEquatable<JsonNull>
    {
        public override string ToString()
        {
            return "null";
        }

        public bool Equals(JsonNull other)
        {
            return other != null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JsonNull) obj);
        }

        public override int GetHashCode()
        {
            return "null".GetHashCode();
        }
    }
}