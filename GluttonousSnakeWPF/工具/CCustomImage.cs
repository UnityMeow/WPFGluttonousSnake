using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GluttonousSnakeWPF
{
    //向量
    public struct Vector2
    {
        public double x;
        public double y;
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    //自定义Image
    class CCustomImage : Image
    {
        //位置
        private Vector2 m_pos = new Vector2();
        //角度
        private RotateTransform rotate = new RotateTransform();
        public CCustomImage(System.Drawing.Bitmap bmp)
        {
            //设置该自定义Image源
            this.Source = BitmapToBitmapImage(bmp);
        }
        public CCustomImage(System.Drawing.Bitmap bmp, double w, double h) : this(bmp)
        {
            //初始化宽高
            this.Width = w;
            this.Height = h;
        }
        //设置尺寸 宽高
        public void SetSize(double w, double h)
        {
            this.Width = w;
            this.Height = h;
        }
        //设置位置 XY
        public void SetPos(double x, double y)
        {
            //设置位置
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            //保存位置
            m_pos.x = x;
            m_pos.y = y;
        }
        //设置角度
        public void SetAngle(int angle)
        {
            Point point = new Point();
            point.X = 0.5;
            point.Y = 0.5;
            rotate.Angle = angle;
            this.RenderTransform = rotate;
            this.RenderTransformOrigin = point;
        }
        //获取位置 XY
        public Vector2 GetPos()
        {
            return m_pos;
        }
        public void SetPic(System.Drawing.Bitmap bmp)
        {
            //设置该自定义Image源
            this.Source = BitmapToBitmapImage(bmp);
        }
        //bitmap转bitmapImage
        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bmp)
        {
            BitmapImage img = new BitmapImage();
            using (System.IO.MemoryStream s = new System.IO.MemoryStream())
            {
                bmp.Save(s, bmp.RawFormat);
                img.BeginInit();
                img.StreamSource = s;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                img.Freeze();
            }
            return img;
        }
    }
}
