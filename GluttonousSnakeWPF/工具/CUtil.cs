using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GluttonousSnakeWPF
{
    class CUtil
    {
        //主面板画布
        public static Canvas mainCanvas;
        //添加控件
        public static void AddControlInCanvas(UIElement c)
        {
            mainCanvas.Children.Add(c);
        }
        //删除单个控件
        public static void RemoveInCanvas(UIElement c)
        {
            mainCanvas.Children.Remove(c);
        }
        //清空所有控件
        public static void Clear()
        {
            mainCanvas.Children.Clear();
        }
    }
}
