using System;

namespace Json.Analysis
{
    /// <summary>
    /// JSON数值类型
    /// </summary>
    public class JsonNumber : JsonElement,IEquatable<JsonNumber>
    {
        public JsonNumber(double value)
        {
            Value = value;
        }
        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool Equals(JsonNumber other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JsonNumber) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}