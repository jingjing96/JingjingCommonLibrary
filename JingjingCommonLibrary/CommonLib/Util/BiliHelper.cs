using System;
using System.Text.RegularExpressions;

namespace Jingjing.CommonLib.Util
{
    /// <summary>
    /// B站AV号-BV号互转
    /// </summary>
    public static class BiliHelper
    {
        #region 字段

        /// <summary>
        /// 编码表（顺序不可改变）
        /// </summary>
        private static string table = "FcwAPNKTMug3GV5Lj7EJnHpWsx4tb8haYeviqBz6rkCy12mUSDQX9RdoZf";

        /// <summary>
        /// AV号的最小值
        /// </summary>
        private static long min_avid = 1;

        /// <summary>
        /// AV号的最大值
        /// </summary>
        private static long max_avid = 1L << 51;

        /// <summary>
        /// 运算值1（xor）
        /// </summary>
        private static long xor = 23442827791579;

        /// <summary>
        /// 运算值2（mask）
        /// </summary>
        private static long mask = 2251799813685247;

        #endregion

        #region 方法

        /// <summary>
        /// 将AV号转为BV号
        /// </summary>
        /// <param name="AVID">AV号</param>
        /// <returns>BV号</returns>
        public static string AVtoBV(string AVID)
        {
            //初始化AV号
            long cur_avid = 0;
            Regex pattern1 = new Regex(@"^[aA][vV]\d+$");
            Regex pattern2 = new Regex(@"^\d+$");
            if (pattern1.IsMatch(AVID))
                cur_avid = Convert.ToInt64(AVID.Substring(2));
            else if (pattern2.IsMatch(AVID))
                cur_avid = Convert.ToInt64(AVID);
            else
                return "输入的AV号格式不正确！";
            if (cur_avid < min_avid)
                return "输入的AV号过小！";
            if (cur_avid > max_avid)
                return "输入的AV号过大！";
            //初始化BV号
            char[] cur_bvid = "BV1?????????".ToCharArray();
            //核心算法
            int index = cur_bvid.Length - 1;
            long temp = (max_avid | cur_avid) ^ xor;
            while (temp != 0)
            {
                cur_bvid[index] = table[(int)(temp % table.Length)];
                temp /= table.Length;
                index--;
            }
            char temp_char = cur_bvid[3];
            cur_bvid[3] = cur_bvid[9];
            cur_bvid[9] = temp_char;
            temp_char = cur_bvid[4];
            cur_bvid[4] = cur_bvid[7];
            cur_bvid[7] = temp_char;
            return new string(cur_bvid);
        }

        /// <summary>
        /// 将BV号转为AV号
        /// </summary>
        /// <param name="BVID">BV号</param>
        /// <returns>AV号</returns>
        public static string BVtoAV(string BVID)
        {
            //初始化BV号
            char[] cur_bvid = new char[12];
            Regex pattern1 = new Regex(@"^[bB][vV][0-9a-zA-Z]{10}$");
            Regex pattern2 = new Regex(@"^[0-9a-zA-Z]{10}$");
            if (pattern1.IsMatch(BVID))
                cur_bvid = BVID.ToCharArray();
            else if (pattern2.IsMatch(BVID))
                cur_bvid = ("BV" + BVID).ToCharArray();
            else
                return "输入的BV号格式不正确！";
            for (int i = 0; i < cur_bvid.Length; i++)
                if (table.IndexOf(cur_bvid[i]) < 0)
                    return "输入的BV号格式不正确！";
            //核心算法
            char temp_char = cur_bvid[3];
            cur_bvid[3] = cur_bvid[9];
            cur_bvid[9] = temp_char;
            temp_char = cur_bvid[4];
            cur_bvid[4] = cur_bvid[7];
            cur_bvid[7] = temp_char;
            long temp = 0;
            string temp_string = new string(cur_bvid).Substring(3);
            foreach (char c in temp_string)
            {
                long index = table.IndexOf(c);
                temp = temp * table.Length + index;
            }
            long cur_avid = (temp & mask) ^ xor;
            return "AV" + cur_avid.ToString();
        }

        #endregion
    }
}
