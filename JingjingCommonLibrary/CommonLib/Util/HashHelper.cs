using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 计算文件、字符串和流的哈希值
    /// </summary>
    public static class HashHelper
    {
        #region 方法

        /// <summary>
        /// 初始化CRC32码表
        /// </summary>
        /// <returns>CRC32码表</returns>
        private static ulong[] GetCRC32Table()
        {
            ulong[] crc32_table = new ulong[256];
            ulong crc;
            int i, j;
            for (i = 0; i < 256; i++)
            {
                crc = (ulong)i;
                for (j = 8; j > 0; j--)
                {
                    if ((crc & 1) == 1)
                        crc = (crc >> 1) ^ 0xEDB88320;
                    else
                        crc >>= 1;
                }
                crc32_table[i] = crc;
            }
            return crc32_table;
        }

        /// <summary>
        /// 计算文件的MD5值（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>MD5字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetMD5CodeFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] file_data = File.ReadAllBytes(filePath);
                    byte[] hash_data = md5.ComputeHash(file_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(32, '0');
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
        /// 计算文件的SHA1值（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>SHA1字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA1CodeFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] file_data = File.ReadAllBytes(filePath);
                    byte[] hash_data = sha1.ComputeHash(file_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(40, '0');
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
        /// 计算文件的SHA256值（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>SHA256字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA256CodeFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    SHA256 sha256 = new SHA256CryptoServiceProvider();
                    byte[] file_data = File.ReadAllBytes(filePath);
                    byte[] hash_data = sha256.ComputeHash(file_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(64, '0');
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
        /// 计算文件的SHA384值（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>SHA384字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA384CodeFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    SHA384 sha384 = new SHA384CryptoServiceProvider();
                    byte[] file_data = File.ReadAllBytes(filePath);
                    byte[] hash_data = sha384.ComputeHash(file_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(96, '0');
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
        /// 计算文件的SHA512值（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>SHA512字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA512CodeFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    SHA512 sha512 = new SHA512CryptoServiceProvider();
                    byte[] file_data = File.ReadAllBytes(filePath);
                    byte[] hash_data = sha512.ComputeHash(file_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(128, '0');
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
        /// 计算文件的CRC32值（文件大小不得超过2GB）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>CRC32字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetCRC32CodeFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    ulong[] crc32_table = GetCRC32Table();
                    byte[] file_data = File.ReadAllBytes(filePath);
                    ulong value = 0xffffffff;
                    for (int i = 0; i < file_data.Length; i++)
                        value = (value >> 8) ^ crc32_table[(value & 0xFF) ^ file_data[i]];
                    string output_string = Convert.ToString(Convert.ToInt64(value ^ 0xffffffff), 16).PadLeft(8, '0');
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
        /// 计算字符串的MD5值
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>MD5字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetMD5CodeFromText(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] string_data = Encoding.UTF8.GetBytes(inputString);
                    byte[] hash_data = md5.ComputeHash(string_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(32, '0');
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
        /// 计算字符串的SHA1值
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>SHA1字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA1CodeFromText(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] string_data = Encoding.UTF8.GetBytes(inputString);
                    byte[] hash_data = sha1.ComputeHash(string_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(40, '0');
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
        /// 计算字符串的SHA256值
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>SHA256字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA256CodeFromText(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    SHA256 sha256 = new SHA256CryptoServiceProvider();
                    byte[] string_data = Encoding.UTF8.GetBytes(inputString);
                    byte[] hash_data = sha256.ComputeHash(string_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(64, '0');
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
        /// 计算字符串的SHA384值
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>SHA384字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA384CodeFromText(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    SHA384 sha384 = new SHA384CryptoServiceProvider();
                    byte[] string_data = Encoding.UTF8.GetBytes(inputString);
                    byte[] hash_data = sha384.ComputeHash(string_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(96, '0');
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
        /// 计算字符串的SHA512值
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>SHA512字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA512CodeFromText(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    SHA512 sha512 = new SHA512CryptoServiceProvider();
                    byte[] string_data = Encoding.UTF8.GetBytes(inputString);
                    byte[] hash_data = sha512.ComputeHash(string_data);
                    string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(128, '0');
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
        /// 计算字符串的CRC32值
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <returns>CRC32字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetCRC32CodeFromText(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                try
                {
                    ulong[] crc32_table = GetCRC32Table();
                    byte[] string_data = Encoding.UTF8.GetBytes(inputString);
                    ulong value = 0xffffffff;
                    for (int i = 0; i < string_data.Length; i++)
                        value = (value >> 8) ^ crc32_table[(value & 0xFF) ^ string_data[i]];
                    string output_string = Convert.ToString(Convert.ToInt64(value ^ 0xffffffff), 16).PadLeft(8, '0');
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
        /// 计算流的MD5值
        /// </summary>
        /// <param name="inputStream">输入的流</param>
        /// <returns>MD5字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetMD5CodeFromStream(Stream inputStream)
        {
            if (inputStream != null)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hash_data = md5.ComputeHash(inputStream);
                string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(32, '0');
                return output_string;
            }
            else
            {
                throw new Exception("给定的流无效。");
            }
        }

        /// <summary>
        /// 计算流的SHA1值
        /// </summary>
        /// <param name="inputStream">输入的流</param>
        /// <returns>SHA1字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA1CodeFromStream(Stream inputStream)
        {
            if (inputStream != null)
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] hash_data = sha1.ComputeHash(inputStream);
                string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(40, '0');
                return output_string;
            }
            else
            {
                throw new Exception("给定的流无效。");
            }
        }

        /// <summary>
        /// 计算流的SHA256值
        /// </summary>
        /// <param name="inputStream">输入的流</param>
        /// <returns>SHA256字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA256CodeFromStream(Stream inputStream)
        {
            if (inputStream != null)
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] hash_data = sha256.ComputeHash(inputStream);
                string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(64, '0');
                return output_string;
            }
            else
            {
                throw new Exception("给定的流无效。");
            }
        }

        /// <summary>
        /// 计算流的SHA384值
        /// </summary>
        /// <param name="inputStream">输入的流</param>
        /// <returns>SHA384字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA384CodeFromStream(Stream inputStream)
        {
            if (inputStream != null)
            {
                SHA384 sha384 = new SHA384CryptoServiceProvider();
                byte[] hash_data = sha384.ComputeHash(inputStream);
                string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(96, '0');
                return output_string;
            }
            else
            {
                throw new Exception("给定的流无效。");
            }
        }

        /// <summary>
        /// 计算流的SHA512值
        /// </summary>
        /// <param name="inputStream">输入的流</param>
        /// <returns>SHA512字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetSHA512CodeFromStream(Stream inputStream)
        {
            if (inputStream != null)
            {
                SHA512 sha512 = new SHA512CryptoServiceProvider();
                byte[] hash_data = sha512.ComputeHash(inputStream);
                string output_string = BitConverter.ToString(hash_data).Replace("-", "").PadLeft(128, '0');
                return output_string;
            }
            else
            {
                throw new Exception("给定的流无效。");
            }
        }

        /// <summary>
        /// 计算流的CRC32值
        /// </summary>
        /// <param name="inputStream">输入的流</param>
        /// <returns>CRC32字符串</returns>
        /// <exception cref="Exception"></exception>
        public static string GetCRC32CodeFromStream(Stream inputStream)
        {
            if (inputStream != null)
            {
                try
                {
                    ulong[] crc32_table = GetCRC32Table();
                    ulong value = 0xffffffff;
                    for (long i = 0; i < inputStream.Length; i++)
                        value = (value >> 8) ^ crc32_table[(value & 0xFF) ^ Convert.ToByte(inputStream.ReadByte())];
                    string output_string = Convert.ToString(Convert.ToInt64(value ^ 0xffffffff), 16).PadLeft(8, '0');
                    return output_string;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("给定的流无效。");
            }
        }

        #endregion
    }
}
