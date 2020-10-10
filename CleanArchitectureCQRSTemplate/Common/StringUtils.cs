using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.Common
{
    /// <summary>
    /// String 擴充方法定義
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// 將字串轉換為第一個字大寫.
        /// </summary>
        /// <param name="TextString"></param>
        /// <returns></returns>
        public static string ToUpperFirstWord(this string TextString)
        {
            string result = "";
            if (TextString.Length > 0)
            {
                result = TextString.Substring(0, 1).ToUpper() + TextString.Substring(1, TextString.Length - 1);
            }
            return result;
        }
        /// <summary>
        /// 將字串轉換為第一個字小寫，並加上 _ 底線 (for private 使用)
        /// </summary>
        /// <param name="TextString"></param>
        /// <returns></returns>
        public static string ToLowerFirstWord_Private(this string TextString)
        {
            string result = "";
            if (TextString.Length > 0)
            {
                result = string.Format("_{0}", TextString.Substring(0, 1).ToLower()) + TextString.Substring(1, TextString.Length - 1);
            }
            return result;
        }
    }
}
