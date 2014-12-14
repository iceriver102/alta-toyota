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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alta_Keyboard.UControl
{
    public enum TypeKeyboard
    {
        Char,num
    }
    /// <summary>
    /// Interaction logic for UKeyboard.xaml
    /// </summary>
    public partial class UKeyboard : UserControl
    {
        public TypeKeyboard @Type
        {
            get;
            set;
        }
        public event EventHandler EnterPress;
        public event EventHandler<string> KeyPress;
        public event EventHandler BackSpace;
        public UKeyboard()
        {
            InitializeComponent();
        }
        private void Shift_Event(object sender, string e)
        {
            if (Type == TypeKeyboard.Char)
            {
                var keys = this.UIContainer_Char.Children.OfType<Key>();
                keys.ToList().ForEach(i => i.isShift = !i.isShift);
            }
            else
            {
                var keys = this.UIContainer_Num.Children.OfType<Key>();
                keys.ToList().ForEach(i => i.isShift = !i.isShift);
            }
            
        }
        private void key_press(object sender, string e)
        {
            Console.WriteLine(e);
            if (this.KeyPress != null)
            {
                this.KeyPress(this, e);
            }
        }
        private void Change_KeyBoard(object sender, string e)
        {
            if (Type == TypeKeyboard.Char)
            {
                DoubleAnimation da = new DoubleAnimation(0,-this.UIRoot.Width, TimeSpan.FromSeconds(0.3));
                this.UIBoard.BeginAnimation(Canvas.LeftProperty, da);
                Type = TypeKeyboard.num;
            }
            else
            {
                DoubleAnimation da = new DoubleAnimation(-this.UIRoot.Width,0, TimeSpan.FromSeconds(0.3));
                this.UIBoard.BeginAnimation(Canvas.LeftProperty, da);
                Type = TypeKeyboard.Char;
            }
        }

        private void Key_Enter_Press(object sender, string e)
        {
            if (this.EnterPress != null)
            {
                this.EnterPress(this, new EventArgs());
            }
        }

        private void Key_Backspace_Press(object sender, string e)
        {
            if (this.BackSpace != null)
            {
                this.BackSpace(this, new EventArgs());
            }
        }
    }
}
