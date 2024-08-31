using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 对文件、字符串和流进行Base64编码或解码
    /// </summary>
    public static class Base64Helaper
    {
        #region 方法

        /// <summary>
        /// 将文件的二进制数据编码为Base64字符串（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>编码后的字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string FileToBase64(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    byte[] file_data = File.ReadAllBytes(filePath);
                    string output_string = Convert.ToBase64String(file_data);
                    return output_string;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("指定的文件不存在。");
            }
        }

        /// <summary>
        /// 将以Base64字符串形式存储的文件解码为二进制数据（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>解码后的二进制数据</returns>
        /// <exception cref="Exception"></exception>
        public static byte[] FileFromBase64(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string file_data = File.ReadAllText(filePath);
                    byte[] output_data = Convert.FromBase64String(file_data);
                    return output_data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("指定的文件不存在。");
            }
        }

        /// <summary>
        /// 对字符串进行Base64编码
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>编码后的字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string TextToBase64(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    byte[] string_data = Encoding.UTF8.GetBytes(inputString);
                    string output_string = Convert.ToBase64String(string_data);
                    return output_string;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("输入字符串不能为空。");
            }
        }

        /// <summary>
        /// 对字符串进行Base64解码
        /// </summary>
        /// <param name="base64String">输入的字符串</param>
        /// <returns>解码后的字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string TextFromBase64(string base64String)
        {
            if (!string.IsNullOrEmpty(base64String))
            {
                try
                {
                    byte[] string_data = Convert.FromBase64String(base64String);
                    string output_string = Encoding.UTF8.GetString(string_data);
                    return output_string;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("输入字符串不能为空。");
            }
        }

        /// <summary>
        /// 对流进行Base64编码
        /// </summary>
        /// <param name="inputStream">待编码的流</param>
        /// <returns>编码后的字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string StreamToBase64(Stream inputStream)
        {
            if (inputStream != null)
            {
                string output_string = string.Empty;
                byte[] buffer_data = new byte[60000];
                long read_length = 0;
                inputStream.Seek(read_length, SeekOrigin.Begin);
                while (read_length < inputStream.Length)
                {
                    int read = inputStream.Read(buffer_data, 0, buffer_data.Length);
                    output_string += Convert.ToBase64String(buffer_data.Take(read).ToArray());
                    read_length += read;
                }
                return output_string;
            }
            else
            {
                throw new Exception("给定的流无效。");
            }
        }

        /// <summary>
        /// 将Base64字符串解码为流
        /// </summary>
        /// <param name="base64String">编码后的字符串</param>
        /// <returns>解码后的流</returns>
        /// <exception cref="Exception"></exception>
        public static Stream StreamFromBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                byte[] stream_data = Convert.FromBase64String(base64String);
                Stream output_stream = new MemoryStream(stream_data);
                return output_stream;
            }
            else
            {
                throw new Exception("输入字符串不能为空。");
            }
        }

        #endregion
    }
}
