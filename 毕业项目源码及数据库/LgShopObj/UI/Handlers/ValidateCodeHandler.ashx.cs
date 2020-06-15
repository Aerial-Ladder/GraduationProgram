using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;

namespace UI.Handlers
{
    /// <summary>
    /// ValidateCodeHandler 的摘要说明
    /// </summary>
    public class ValidateCodeHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //一般处理程序中生成验证码：
            Color[] colors = new Color[]
            {
Color.Red,Color.Green,Color.Yellow,Color.Blue,Color.AliceBlue,Color.Aqua,Color.Bisque,Color.Brown
            };
            //由像素处理的图像
            Image img = new Bitmap(120,40);
            //图像转为可编辑图层
            Graphics graphics = Graphics.FromImage(img);
            //画背景颜色
            graphics.Clear(Color.Gray);
            //0-999之间的毫秒数
            Random random = new Random(DateTime.Now.Millisecond);
            //97-122 小写字母 65-90 大写字母 48-57数字
            int charNum1 = random.Next(97, 122);
            int charNum2 = random.Next(65, 90);
            int charNum3 = random.Next(97, 122);
            int charNum4 = random.Next(48, 57);
            //保存验证码
            string validCode = string.Format("{0}{1}{2}{3}", (char)charNum1, (char)charNum2,(char)charNum3, (char)charNum4);
            context.Session["useryzm"] = validCode;
            //定义字体样式
            Font font = new Font("宋体", 24);
            //定义画笔，画笔在颜色数组中选取颜色
            Brush brush1 = new SolidBrush(colors[random.Next(0, colors.Length - 1)]);
            //干扰线
            for (int i = 0; i < 3; i++)
            {
                int x1 = random.Next(img.Width);
                int x2 = random.Next(img.Width);
                int y1 = random.Next(img.Height);
                int y2 = random.Next(img.Height);
                graphics.DrawLine(new Pen(Color.White), x1, x2, y1, y2);
            }
            //在可编辑图层上面，绘制字符串（文本内容、字体对象（字体、字号）、画笔、，x轴位置，y轴位置）
            graphics.DrawString(((char)charNum1).ToString(), font, brush1, 7, 0);
            Brush brush2 = new SolidBrush(colors[random.Next(0, colors.Length - 1)]);
            graphics.DrawString(((char)charNum2).ToString(), font, brush2, 33, -3);
            Brush brush3 = new SolidBrush(colors[random.Next(0, colors.Length - 1)]);
            graphics.DrawString(((char)charNum3).ToString(), font, brush3, 60, 2);
            Brush brush4 = new SolidBrush(colors[random.Next(0, colors.Length - 1)]);
            graphics.DrawString(((char)charNum4).ToString(), font, brush4, 80, -4);
            //解析为图片类型
            context.Response.ContentType = "image/jpeg";
            //保存图片，图片流的格式输出出去
            img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            //释放图片资源
            img.Dispose();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}