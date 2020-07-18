namespace Json.Analysis
{
    /// <summary>
    /// 数组
    /// </summary>
    public class JsonString : JsonElement
    {
        public JsonString(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"\"{Value}\"";
        }
    }
}