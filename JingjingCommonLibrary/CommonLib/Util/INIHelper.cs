using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 操作配置文件（INI文件）
    /// </summary>
    public static class INIHelper
    {
        #region 方法

        /// <summary>
        /// 从配置文件中读取数据，此为kernel32.dll中的外部方法
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="defval">默认值</param>
        /// <param name="retval">缓冲区（读取到的值）</param>
        /// <param name="size">缓冲区大小</param>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>复制到缓冲区的字符数</returns>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string defval, StringBuilder retval, int size, string filepath);

        /// <summary>
        /// 向配置文件中写入数据，此为kernel32.dll中的外部方法
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>方法是否执行成功</returns>
        [DllImport("kernel32.dll")]
        private static extern bool WritePrivateProfileString(string section, string key, string value, string filepath);

        /// <summary>
        /// 从配置文件中读取数据
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="defval">默认值</param>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>复制到缓冲区的字符数</returns>
        public static string getValue(string section, string key, string defval, string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    StringBuilder sb = new StringBuilder(1024);
                    GetPrivateProfileString(section, key, defval, sb, 1024, filepath);
                    return sb.ToString();
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
        /// 向配置文件中写入数据
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>方法是否执行成功</returns>
        public static bool setValue(string section, string key, string value, string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    bool flag = WritePrivateProfileString(section, key, value, filepath);
                    return flag;
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
        /// 从配置文件中删除指定的键
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>方法是否执行成功</returns>
        public static bool deleteKey(string section, string key, string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    bool flag = WritePrivateProfileString(section, key, null, filepath);
                    return flag;
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
        /// 从配置文件中删除指定的节
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>方法是否执行成功</returns>
        public static bool deleteSection(string section, string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    bool flag = WritePrivateProfileString(section, null, null, filepath);
                    return flag;
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

        #endregion
    }
}
