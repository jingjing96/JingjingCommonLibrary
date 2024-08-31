using System;

namespace Jingjing.CommonLib
{
    /// <summary>
    /// 生成随机字符串
    /// </summary>
    public class RandomCode
    {
        #region 字段

        /// <summary>
        /// 随机字符串
        /// </summary>
        private string code;

        /// <summary>
        /// 随机字符串的长度
        /// </summary>
        private int length;

        /// <summary>
        /// 随机字符串的种子
        /// </summary>
        private string seed;

        #endregion

        #region 属性

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string Code
        {
            get { return code; }
        }

        /// <summary>
        /// 随机字符串的长度
        /// </summary>
        public int Length
        {
            get { return length; }
        }

        /// <summary>
        /// 随机字符串的种子，字符串的内容将从这里挑选
        /// </summary>
        public string Seed
        {
            get { return seed; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 使用给定的种子和长度生成一个随机字符串
        /// </summary>
        /// <param name="seed">随机字符串的种子，字符串的内容将从这里挑选</param>
        /// <param name="length">随机字符串的长度</param>
        /// <returns></returns>
        public string Next(string seed, int length)
        {
            this.seed = seed;
            this.length = length;
            code = string.Empty;
            Random random = new Random();
            for (int i = 0; i < length; i++)
                code += seed[random.Next(seed.Length)];
            return code;
        }

        #endregion
    }
}
