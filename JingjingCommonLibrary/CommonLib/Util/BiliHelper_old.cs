using System;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// B站AV号-BV号互转（已废弃）<br />
    /// 注意：此代码已废弃，仅用于留档，不可使用。
    /// </summary>
    internal static class BiliHelper_old
    {
        #region 字段

        /// <summary>
        /// 编码表（顺序不可改变）
        /// </summary>
        private static string table = "fZodR9XQDSUm21yCkr6zBqiveYah8bt4xsWpHnJE7jL5VG3guMTKNPAwcF";

        /// <summary>
        /// 由10位BV号的索引构成的数组（顺序不可改变）
        /// </summary>
        private static int[] ss = new int[] { 11, 10, 3, 8, 4, 6, 2, 9, 5, 7 };

        /// <summary>
        /// 运算值1（xor）
        /// </summary>
        private static long xor = 177451812;

        /// <summary>
        /// 运算值2（add）
        /// </summary>
        private static long add = 8728348608l;

        #endregion

        #region 方法

        /// <summary>
        /// 将AV号转为BV号
        /// </summary>
        /// <param name="AV_Num">AV号</param>
        /// <returns>BV号</returns>
        private static string AVtoBV(string AV_Num)
        {
            //初始化AV号
            long s = 0;
            if (AV_Num.ToUpper().IndexOf("AV") >= 0)
            {
                for (int i = 2; i < AV_Num.Length; i++)
                    if (AV_Num[i] < '0' || AV_Num[i] > '9')
                        return "输入的AV号格式不正确！";
                s = Convert.ToInt64(AV_Num.Substring(2));
            }
            else
            {
                for (int i = 0; i < AV_Num.Length; i++)
                    if (AV_Num[i] < '0' || AV_Num[i] > '9')
                        return "输入的AV号格式不正确！";
                s = Convert.ToInt64(AV_Num);
            }
            //初始化BV号
            char[] cb = "BV1??4?1?7??".ToCharArray();
            //核心算法
            s = (s ^ xor) + add;
            for (int i = 0; i < 6; i++)
            {
                char r = table[(int)(s / Math.Pow(58, i) % 58)];
                cb[ss[i]] = r;
            }
            string output_string = new string(cb);
            return output_string;
        }

        /// <summary>
        /// 将BV号转为AV号
        /// </summary>
        /// <param name="BV_Num">BV号</param>
        /// <returns>AV号</returns>
        private static string BVtoAV(string BV_Num)
        {
            //初始化BV号
            if (BV_Num.ToUpper().IndexOf("BV") >= 0)
            {
                if (BV_Num.Length != 12)
                    return "输入的BV号格式不正确！";
                for (int i = 2; i < BV_Num.Length; i++)
                    if (table.IndexOf(BV_Num[i]) < 0)
                        return "输入的BV号格式不正确！";
            }
            else
            {
                if (BV_Num.Length != 10)
                    return "输入的BV号格式不正确！";
                for (int i = 0; i < BV_Num.Length; i++)
                    if (table.IndexOf(BV_Num[i]) < 0)
                        return "输入的BV号格式不正确！";
                BV_Num += "BV";
            }
            //核心算法
            long r = 0;
            for (int i = 0; i < 6; i++)
                r += (long)(table.IndexOf(BV_Num.Substring(ss[i], 1)) * Math.Pow(58, i));
            string output_string = "AV" + ((r - add) ^ xor).ToString();
            return output_string;
        }

        #endregion
    }
}
