using System;
using System.Drawing;

namespace Jingjing.CommonLib
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    public class CaptchaImage
    {
        #region 字段

        /// <summary>
        /// 验证码文本
        /// </summary>
        private string code;

        /// <summary>
        /// 验证码图片
        /// </summary>
        private Bitmap image;

        #endregion

        #region 属性

        /// <summary>
        /// 验证码文本
        /// </summary>
        public string Code
        {
            get { return code; }
        }

        /// <summary>
        /// 验证码图片
        /// </summary>
        public Bitmap Image
        {
            get { return image; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 生成随机验证码
        /// </summary>
        /// <param name="length">验证码的长度</param>
        /// <returns>验证码文本</returns>
        private string GetRandomCode(int length)
        {
            string seed = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            RandomCode random_code = new RandomCode();
            string code = random_code.Next(seed, length);
            return code;
        }

        /// <summary>
        /// 绘制验证码图片
        /// </summary>
        /// <param name="code">验证码文本</param>
        /// <returns>验证码图片</returns>
        private Bitmap DrowImage(string code)
        {
            //随机颜色表
            Color[] colors = {
                Color.Red, Color.OrangeRed, Color.SaddleBrown,
                Color.LimeGreen, Color.Green, Color.MediumAquamarine,
                Color.Blue, Color.MediumOrchid, Color.Black,
                Color.DarkBlue, Color.Orange, Color.Brown,
                Color.DarkCyan, Color.Purple
            };
            Color[] back_colors = {
                Color.FromArgb(250, 230, 230), Color.FromArgb(230, 250, 230),
                Color.FromArgb(230, 230, 250), Color.FromArgb(250, 250, 230),
                Color.FromArgb(250, 230, 250), Color.FromArgb(230, 250, 250),
                Color.FromArgb(230, 230, 230), Color.FromArgb(250, 250, 250)
            };
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            Random random = new Random();
            Bitmap bitmap = new Bitmap(code.Length * 20, 32);
            Graphics graphics = Graphics.FromImage(bitmap);
            //填充背景色
            graphics.Clear(back_colors[random.Next(back_colors.Length)]);
            //绘制底层噪点
            for (int i = 0; i < random.Next(code.Length * 15, code.Length * 20 + 1); i++)
            {
                int pointX = random.Next(bitmap.Width);
                int pointY = random.Next(bitmap.Height);
                graphics.DrawLine(new Pen(colors[random.Next(colors.Length)], 1), pointX, pointY, pointX + 1, pointY);
            }
            //绘制底层线条
            for (int i = 0; i < 3; i++)
            {
                graphics.DrawLine(
                    new Pen(colors[random.Next(colors.Length)], random.Next(3)),
                    new Point(
                        random.Next(bitmap.Width),
                        random.Next(bitmap.Height)),
                    new Point(random.Next(bitmap.Width),
                        random.Next(bitmap.Height)));
            }
            //绘制验证码文字
            int angle;
            for (int i = 0; i < code.Length; i++)
            {
                angle = random.Next(-20, 21);
                graphics.TranslateTransform(16 * i + 8, 8);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-(16 * i + 8), -8);
                graphics.DrawString(
                    code.Substring(i, 1),
                    new Font(fonts[random.Next(fonts.Length)], 15, FontStyle.Bold),
                    new SolidBrush(colors[random.Next(colors.Length)]),
                    16 * i + 4,
                    random.Next(0, 5));
                graphics.ResetTransform();
            }
            //绘制顶层噪点
            for (int i = 0; i < random.Next(code.Length * 8, code.Length * 12 + 1); i++)
            {
                int pointX = random.Next(bitmap.Width);
                int pointY = random.Next(bitmap.Height);
                graphics.DrawLine(new Pen(colors[random.Next(colors.Length)], 1), pointX, pointY, pointX, pointY + 1);
            }
            //绘制顶层线条
            for (int i = 0; i < 3; i++)
            {
                graphics.DrawLine(
                    new Pen(colors[random.Next(colors.Length)], random.Next(3)),
                    new Point(
                        random.Next(bitmap.Width),
                        random.Next(bitmap.Height)),
                    new Point(random.Next(bitmap.Width),
                        random.Next(bitmap.Height)));
            }
            return bitmap;
        }

        /// <summary>
        /// 重新生成一个给定长度的验证码
        /// </summary>
        /// <param name="length">验证码的长度</param>
        /// <returns>验证码文本</returns>
        public string Next(int length)
        {
            code = GetRandomCode(length);
            image = DrowImage(code);
            return code;
        }

        #endregion
    }
}
