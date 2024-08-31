using System;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 生成GUID
    /// </summary>
    public static class GUIDHelper
    {
        #region 方法

        /// <summary>
        /// 生成带有连字符“-”的GUID
        /// </summary>
        /// <returns>GUID字符串</returns>
        public static string GetGUID()
        {
            string guid_string = Guid.NewGuid().ToString();
            return guid_string;
        }

        /// <summary>
        /// 生成GUID，可以指定是否带有连字符“-”
        /// </summary>
        /// <param name="hyphen">是否带有连字符“-”</param>
        /// <returns>GUID字符串</returns>
        public static string GetGUID(bool hyphen)
        {
            if (hyphen)
            {
                string guid_string = Guid.NewGuid().ToString();
                return guid_string;
            }
            else
            {
                string guid_string = Guid.NewGuid().ToString();
                string[] guid_parts = guid_string.Split(new char[] { '-' });
                guid_string = string.Join(string.Empty, guid_parts);
                return guid_string;
            }
        }

        #endregion
    }
}
