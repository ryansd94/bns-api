using BNS.Utilities.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Utilities.Implement
{
    public class Captcha : ICaptcha
    {
        byte[] ICaptcha.GenerateImage(string text, int width, int height, string familyName)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, width, height);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
            g.FillRectangle(hatchBrush, rect);
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            do
            {
                fontSize--;
                font = new Font(familyName, fontSize + 10, FontStyle.Bold);

                size = g.MeasureString(text, font);
            } while (size.Width > rect.Width);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            GraphicsPath path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, 30, rect, format);
            float v = 4F;
            PointF[] points =
            {
  new PointF(new Random().Next(rect.Width) / v, new Random().Next(rect.Height) / v),
  new PointF(rect.Width - new Random().Next(rect.Width) / v, new Random().Next(rect.Height) / v),
  new PointF(new Random().Next(rect.Width) / v, rect.Height - new Random().Next(rect.Height) / v),
  new PointF(rect.Width -new Random().Next(rect.Width) / v,rect.Height -
   new Random().Next(rect.Height) / v)
  };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
            hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.Black, Color.DarkGray);
            g.FillPath(hatchBrush, path);
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = new Random().Next(rect.Width);
                int y = new Random().Next(rect.Height);
                int w = new Random().Next(m / 50);
                int h = new Random().Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return  stream.ToArray();
            }
        }

    }
}
