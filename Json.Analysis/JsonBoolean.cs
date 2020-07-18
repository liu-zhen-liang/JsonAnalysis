using System;

namespace Json.Analysis
{
    /// <summary>
    /// JSON布尔值
    /// </summary>
    public class JsonBoolean : JsonElement,IEquatable<JsonBoolean>
    {
        public JsonBoolean(bool value)
        {
            Value = value;
        }
        public bool Value { get; set; }

        public override string ToString()
        {
            return Value ? "true" : "false";
        }

        public bool Equals(JsonBoolean other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JsonBoolean) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}