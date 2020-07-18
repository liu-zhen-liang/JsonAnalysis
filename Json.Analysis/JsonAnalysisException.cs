using System;

namespace Json.Analysis
{
    /// <summary>
    /// JSON解析异常
    /// </summary>
    public class JsonAnalysisException : Exception
    {
        /// <summary>
        /// 实例化JSON解析异常
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        public JsonAnalysisException( string message, int index) 
            : base(message)
        {
            Index = index;
        }
        /// <summary>
        /// 解析异常位置
        /// </summary>
        public int Index { get; }
    }
}