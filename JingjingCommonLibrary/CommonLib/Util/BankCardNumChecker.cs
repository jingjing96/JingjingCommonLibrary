using System;
using System.Text.RegularExpressions;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// 校验银行卡号的有效性<br />
    /// 注意：此程序仅用于校验银行卡号的编码格式是否正确，无法用于检测银行卡号是否真实存在。
    /// </summary>
    public static class BankCardNumChecker
    {
        #region 方法

        /// <summary>
        /// 校验银行卡号的有效性
        /// </summary>
        /// <param name="cardNum">银行卡号</param>
        /// <returns>银行卡号是否有效</returns>
        public static bool Check(string cardNum)
        {
            if (!string.IsNullOrEmpty(cardNum))
            {
                //检测格式
                Regex pattern = new Regex(@"^\d+$");
                if (!pattern.IsMatch(cardNum))
                    return false;
                //反转卡号
                char[] card_num = cardNum.ToCharArray();
                Array.Reverse(card_num);
                //检测校验码
                int sum = 0;
                for (int i = 0; i < card_num.Length; i++)
                {
                    if (i % 2 == 0)
                        //奇数位
                        sum += int.Parse(card_num[i].ToString());
                    else
                    {
                        //偶数位
                        int temp = int.Parse(card_num[i].ToString()) * 2;
                        if (temp >= 10)
                            temp -= 9;
                        sum += temp;
                    }
                }
                if (sum % 10 == 0)
                    return true;
                else
                    return false;
                /* 
                 * 算法原理：
                 * 1. 从卡号的最后一位算起，逆向计算；
                 * 2. 对于奇数位的数字，直接相加；
                 * 3. 对于偶数位的数字，先乘2；
                 * 4. 如果结果是一位数（小于10），则直接相加；
                 * 5. 如果结果是两位数（大于等于10），则先减9，再相加；
                 * 6. 将奇数位之和与偶数位之和相加；
                 * 7. 如果最终结果可以被10整除，则校验通过，否则校验失败。
                 */
            }
            else
            {
                throw new Exception("输入的银行卡号不能为空。");
            }
        }

        #endregion
    }
}
