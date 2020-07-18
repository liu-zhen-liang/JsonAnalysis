using System.Text;

namespace Json.Analysis
{
    /// <summary>
    /// JSON解析类型
    /// 如果在解析字符串的时候，拿不准这个是不是正确的JOSN，你可以在这个上面测试一下，有利于对自己代码的测试 http://lzltool.com/Json
    /// </summary>
    public static class JsonConvert
    {
        /// <summary>
        /// 解析JSON
        /// </summary>
        /// <param name="text">待解析的JSON字符串</param>
        /// <returns>解析完成的JSON结构对象</returns>
        public static JsonElement AnalysisJson(string text)
        {
            var index = 0;
            //读取到非空白字符
            ReadToNonBlankIndex(text, ref index);
            if (text[index++] == '[')
                //解析数组
                return AnalysisJsonArray(text, ref index);
            //解析对象
            return AnalysisJsonObject(text, ref index);
        }

        /// <summary>
        /// 解析JSON对象
        /// </summary>
        /// <param name="text">JSON字符串</param>
        /// <param name="index">开始索引位置</param>
        /// <returns>JSON对象</returns>
        private static JsonObject AnalysisJsonObject(string text, ref int index)
        {
            var jsonArray = new JsonObject();
            do
            {
                ReadToNonBlankIndex(text, ref index);
                if (text[index] != '"') throw new JsonAnalysisException($"不能识别的字符“{text[index]}”！应为“\"”", index);
                index++;
                //读取字符串
                var name = ReadString(text, ref index);
                if (jsonArray.ContainsKey(name)) throw new JsonAnalysisException($"已经添加键值：“{name}”", index);
                ReadToNonBlankIndex(text, ref index);
                if (text[index] != ':') throw new JsonAnalysisException($"不能识别的字符“{text[index]}”！", index);
                index++;
                ReadToNonBlankIndex(text, ref index);
                //读取下一个Element
                jsonArray.Add(name, ReadElement(text, ref index));
                //读取到非空白字符
                ReadToNonBlankIndex(text, ref index);
                var ch = text[index++];
                if (ch == '}') break;
                if (ch != ',') throw new JsonAnalysisException($"不能识别的字符“{text[index - 1]}”！", index - 1);
            } while (true);

            return jsonArray;
        }

        /// <summary>
        /// 解析JSON数组
        /// </summary>
        /// <param name="text">JSON字符串</param>
        /// <param name="index">开始索引位置</param>
        /// <returns>JSON数组</returns>
        private static JsonArray AnalysisJsonArray(string text, ref int index)
        {
            var jsonArray = new JsonArray();
            do
            {
                ReadToNonBlankIndex(text, ref index);
                //读取下一个Element
                jsonArray.Add(ReadElement(text, ref index));
                //读取到非空白字符
                ReadToNonBlankIndex(text, ref index);
                var ch = text[index++];
                if (ch == ']') break;
                if (ch != ',') throw new JsonAnalysisException($"不能识别的字符“{text[index - 1]}”！", index - 1);
            } while (true);

            return jsonArray;
        }

        /// <summary>
        /// 读取JSONElement
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="index">开始下标</param>
        /// <returns>下一个Element</returns>
        private static JsonElement ReadElement(string text, ref int index)
        {
            switch (text[index++])
            {
                case '[':
                    return AnalysisJsonArray(text, ref index);
                case '{':
                    return AnalysisJsonObject(text, ref index);
                case '"':
                    return new JsonString(ReadString(text, ref index));
                case 't':
                    return ReadJsonTrue(text, ref index);
                case 'f':
                    return ReadJsonFalse(text, ref index);
                case 'n':
                    return ReadJsonNull(text, ref index);
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return ReadJsonNumber(text, ref index);
                default:
                    throw new JsonAnalysisException($"未知Element“{text[index - 1]}”应该为【[、{{、\"、true、false、null】",
                        index - 1);
            }
        }

        /// <summary>
        /// 读取值类型
        /// </summary>
        /// <param name="text">JSON字符串</param>
        /// <param name="index">开始索引</param>
        /// <returns>JSON数值类型</returns>
        private static JsonNumber ReadJsonNumber(string text, ref int index)
        {
            var i = index;
            while (i < text.Length && char.IsNumber(text[i]) || text[i] == '.') i++;
            if (double.TryParse(text.Substring(index - 1, i - index + 1), out var value))
            {
                index = i;
                return new JsonNumber(value);
            }

            throw new JsonAnalysisException("不能识别的数字类型！", i);
        }

        /// <summary>
        /// 读取NULL
        /// </summary>
        /// <param name="text">JSON字符串</param>
        /// <param name="index">开始索引</param>
        /// <returns>读取NULL</returns>
        private static JsonNull ReadJsonNull(string text, ref int index)
        {
            if (text[index++] == 'u' &&
                text[index++] == 'l' &&
                text[index++] == 'l')
            {
                return new JsonNull();
            }

            throw new JsonAnalysisException("读取null出错！", index - 1);
        }

        /// <summary>
        /// 读取FALSE
        /// </summary>
        /// <param name="text">JSON字符串</param>
        /// <param name="index">开始索引</param>
        /// <returns>布尔值-假</returns>
        private static JsonBoolean ReadJsonFalse(string text, ref int index)
        {
            if (text[index++] == 'a' &&
                text[index++] == 'l' &&
                text[index++] == 's' &&
                text[index++] == 'e')
            {
                return new JsonBoolean(false);
            }

            throw new JsonAnalysisException("读取布尔值出错！", index - 1);
        }

        /// <summary>
        /// 读取TRUE
        /// </summary>
        /// <param name="text">JSON字符串</param>
        /// <param name="index">开始索引</param>
        /// <returns>布尔值-真</returns>
        private static JsonBoolean ReadJsonTrue(string text, ref int index)
        {
            if (text[index++] == 'r' &&
                text[index++] == 'u' &&
                text[index++] == 'e')
            {
                return new JsonBoolean(true);
            }

            throw new JsonAnalysisException("读取布尔值出错！", index - 1);
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="text">JSON字符串</param>
        /// <param name="index">开始索引</param>
        /// <returns>字符串值</returns>
        private static string ReadString(string text, ref int index)
        {
            //是否处于转义状态
            var value = new StringBuilder();
            while (index < text.Length)
            {
                var c = text[index++];
                if (c == '\\')
                {
                    value.Append('\\');
                    if (index >= text.Length)
                        throw new JsonAnalysisException("未知的结尾！", index - 1);
                    c = text[index++];
                    value.Append(c);
                    if (c == 'u')
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            c = text[index++];
                            if (IsHex(c))
                            {
                                value.Append(c);
                            }
                            else
                            {
                                throw new JsonAnalysisException("不是有效的Unicode字符！", index - 1);
                            }
                        }
                    }
                }
                else if (c == '"')
                {
                    break;
                }
                else if (c == '\r' || c == '\n')
                {
                    throw new JsonAnalysisException("传入的JSON字符串内容中不允许有换行！", index - 1);
                }
                else
                {
                    value.Append(c);
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// 判断是否为16进制字符
        /// </summary>
        private static bool IsHex(char c)
        {
            return c >= '0' && c <= '9' || c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z';
        }

        /// <summary>
        /// 读取到非空白字符
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="index">开始下标</param>
        /// <returns>非空白字符下标</returns>
        private static void ReadToNonBlankIndex(string text, ref int index)
        {
            while (index < text.Length && char.IsWhiteSpace(text[index])) index++;
        }
    }
}