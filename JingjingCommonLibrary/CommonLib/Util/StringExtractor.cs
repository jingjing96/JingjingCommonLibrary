using System;
using System.Collections;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 截取两个指定字符串中间的字符串
    /// </summary>
    public static class StringExtractor
    {
        #region 方法

        /// <summary>
        /// 截取两个指定字符串中间的字符串
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="startString">起始字符串</param>
        /// <param name="endString">结束字符串</param>
        /// <returns>截取出的字符串数组</returns>
        /// <exception cref="Exception"></exception>
        public static string[] Extract(string inputString, string startString, string endString)
        {
            if (string.IsNullOrEmpty(inputString))
                throw new Exception("输入的字符串不能为空。");
            if (string.IsNullOrEmpty(startString))
                throw new Exception("起始字符串不能为空。");
            if (string.IsNullOrEmpty(endString))
                throw new Exception("结束字符串不能为空。");
            ArrayList string_list = new ArrayList();
            try
            {
                int start_index = inputString.IndexOf(startString);
                int end_index = inputString.IndexOf(endString);
                int onuput_string_length = 0;
                if (start_index < 0)
                    throw new Exception("起始字符串不存在。");
                if (end_index < 0)
                    throw new Exception("结束字符串不存在。");
                if (start_index > end_index)
                    throw new Exception("结束字符串不能位于起始字符串之前。");
                while (start_index >= 0 && end_index >= 0)
                {
                    onuput_string_length = end_index - start_index - startString.Length;
                    string_list.Add(inputString.Substring(start_index + startString.Length, onuput_string_length));
                    inputString = inputString.Substring(start_index + onuput_string_length + startString.Length + endString.Length);
                    start_index = inputString.IndexOf(startString);
                    end_index = inputString.IndexOf(endString);
                }
                if (string_list.Count > 0)
                {
                    string[] output_string = new string[string_list.Count];
                    for (int index = 0; index < string_list.Count; index++)
                        output_string[index] = string_list[index].ToString();
                    return output_string;
                }
                else
                {
                    throw new Exception("不存在符合要求的字符串。");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
