using System;
using System.Text.RegularExpressions;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 校验18位身份证号的有效性<br />
    /// 注意：此程序仅用于校验身份证号的编码格式是否正确，无法用于检测身份证号是否真实存在。
    /// </summary>
    public static class IDCardNumChecker
    {
        #region 字段

        /// <summary>
        /// 前17位的合法字符
        /// </summary>
        private static string num1 = "0123456789";

        /// <summary>
        /// 第18位的合法字符
        /// </summary>
        private static string num2 = "0123456789X";

        /// <summary>
        /// 前17位的对应系数
        /// </summary>
        private static int[] weight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

        /// <summary>
        /// 校验码
        /// </summary>
        private static char[] verify_code = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        #endregion

        #region 方法

        /// <summary>
        /// 校验18位身份证号的有效性
        /// </summary>
        /// <param name="idNum">18位身份证号</param>
        /// <returns>身份证号是否有效</returns>
        public static bool Check(string idNum)
        {
            if (!string.IsNullOrEmpty(idNum))
            {
                //检测格式
                Regex pattern = new Regex(@"^\d{17}[\dxX]$");
                if (!pattern.IsMatch(idNum))
                    return false;
                //检测校验位
                int sum = 0;
                for (int i = 0; i <= 16; i++)
                    sum += int.Parse(idNum[i].ToString()) * weight[i];
                if (idNum[17].ToString() == verify_code[sum % 11].ToString())
                    return true;
                else
                    return false;
                /* 
                 * 校验位算法原理：
                 * 1. 将前17位分别与对应系数（weight数组）相乘；
                 * 2. 将这17个乘积相加；
                 * 3. 相加结果对11取余；
                 * 4. 取余结果是verify_code数组中的元素下标；
                 * 5. 第18位即为verify_code数组中的对应元素。
                 */
            }
            else
            {
                throw new Exception("输入的身份证号不能为空。");
            }
        }

        #endregion
    }
}
