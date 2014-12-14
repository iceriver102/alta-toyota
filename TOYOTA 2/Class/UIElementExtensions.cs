using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace toyota.Class
{
    public static class UIElementExtensions
    {
        public static void setLeft(this UIElement e, double left)
        {
            Canvas.SetLeft(e, left);
        }
        public static void setTop(this UIElement e, double top)
        {
            Canvas.SetTop(e, top);
        }

        public static double getLeft(this UIElement e)
        {
            return Canvas.GetLeft(e);
        }
        public static double getTop(this UIElement e)
        {
            return Canvas.GetTop(e);
        }
        public static void setZIndex(this UIElement e, int zindex)
        {
            Canvas.SetZIndex(e,zindex);
        }
        public static int getZIndex(this UIElement e)
        {
            return Canvas.GetZIndex(e);
        }
    }
}
