using System.Text.RegularExpressions;

namespace Books.Helper
{
    internal class RegexHelper
    {


        /// <summary>
        /// 正则方式验证字符串（匹配单个结果）
        /// </summary>
        /// <param name="str">被验证的字符串</param>
        /// <param name="regexStr">正则规则</param>
        /// <param name="value">抛出返回值</param>
        /// <returns></returns>
        public static string Replace(string content, string regexStr, string newValue)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(regexStr))
            {
                return content;
            }

            var value = Regex.Replace(content, regexStr, newValue);
            return value;
        }
    }
}
