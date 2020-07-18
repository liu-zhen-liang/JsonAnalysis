using System;
using Json.Analysis;

namespace Json.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //如果在解析字符串的时候，拿不准这个是不是正确的JOSN，你可以在这个上面测试一下，有利于对自己代码的测试 http://lzltool.com/Json
            var jsonObj = (JsonObject)JsonConvert.AnalysisJson("{\"Name\":\"张三\",\"Age\":18}");
            foreach (var item in jsonObj)
            {
                Console.WriteLine($"Key:{item.Key} Value:{item.Value}");
            }

            Console.ReadKey();
        }
    }
}
