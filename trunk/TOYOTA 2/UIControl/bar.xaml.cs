using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace toyota.UIControl
{
    /// <summary>
    /// Interaction logic for bar.xaml
    /// </summary>
    public partial class bar : UserControl
    {
        public static int max = 10;
        public static int min = 0;
        public bar()
        {
            InitializeComponent();
        }
        private int _num = 0;
        public int Num
        {
            get
            {
                return _num;
            }
            set
            {
                var E=this.FindName("UIPos" + _num);
                if (E != null &&  E.GetType() == typeof(Ellipse)) 
                {
                    Ellipse e = E as Ellipse;
                    e.Fill = new SolidColorBrush(new Color() { A = 255, B = 245, G = 244, R = 244});
                }
                if (_num != value)
                {
                    _num = value;
                    var E2 = this.FindName("UIPos" + _num);
                    if (E2!=null&&E2.GetType() == typeof(Ellipse))
                    {
                        Ellipse e = E2 as Ellipse;
                        e.Fill = new SolidColorBrush(new Color() { A = 255, B = 0, G = 0, R = 244 });
                    }
                }
            }
        }
    }
}
