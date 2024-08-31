using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 对字符串进行URL编码或解码
    /// </summary>
    public static class URLHelper
    {
        #region 方法

        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="isUpper">编码后的字符串是否大写</param>
        /// <returns>编码后的字符串</returns>
        public static string URLEncode(string inputString, bool isUpper)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    string result = HttpUtility.UrlEncode(inputString, Encoding.UTF8);
                    if (isUpper)
                    {
                        Regex regex = new Regex(@"\%[0-9A-Fa-f]{2}");
                        foreach (Match match in regex.Matches(result))
                        {
                            result = result.Replace(match.Value, match.Value.ToUpper());
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("输入的字符串不能为空。");
            }
        }

        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string URLDecode(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    string result = HttpUtility.UrlDecode(inputString, Encoding.UTF8);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("输入的字符串不能为空。");
            }
        }

        #endregion
    }
}
