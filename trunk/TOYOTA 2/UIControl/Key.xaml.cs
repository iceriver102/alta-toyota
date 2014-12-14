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

namespace Alta_Keyboard.UControl
{
    /// <summary>
    /// Interaction logic for Key.xaml
    /// </summary>
    public partial class Key : UserControl
    {

        #region DependencyProperty
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Key));
        #endregion
        #region Khai bao bien

        private bool _isShift = false;
        private string code
        {
            get;
            set;
        }
        public bool isShift
        {
            get
            {
                return this._isShift;
            }
            set
            {
                if (value)
                {
                    this.Text = this.shift_Text;
                    this.code = this.shift_code;
                }
                else
                {
                    this.Text = this.normal_Text;
                    this.code = this.normal_code;
                }
                this._isShift = value;
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        private string _normal_code;
        public string normal_code
        {
            get
            {
                return _normal_code;
            }
            set
            {
                this._normal_code = value;
                this.code = value;
            }
        }
        private string _shift_code;
        public string shift_code
        {
            get
            {
                return this._shift_code;
            }
            set
            {
                this._shift_code = value;
            }
        }
        private string _shift_text;

        public string shift_Text
        {
            get
            {
                return this._shift_text;
            }
            set
            {
                if (string.IsNullOrEmpty(this._shift_code))
                {
                    this._shift_code = value;
                }
                this._shift_text = value;
            }
        }
        
        private string _text;

        public string normal_Text
        {
            get
            {
                return this._text;
            }
            set
              {
                  this._text = value;
                  if(string.IsNullOrEmpty(this.Text))
                    this.Text = value;
                  if (string.IsNullOrEmpty(this.normal_code))
                  {
                      this.normal_code = value;
                  }

             }
        }

        #endregion

        #region Event

        public event EventHandler<string> Press;

        #endregion

        public Key()
        {
            InitializeComponent();
            Console.WriteLine(this.normal_Text);
        }

        private void Key_Click(object sender, MouseButtonEventArgs e)
        {
            if (Press != null)
            {
                this.Press(this,this.code);
            }
        }

        private void Key_TouchUp(object sender, TouchEventArgs e)
        {
            if (Press != null)
            {
                this.Press(this, this.code);
            }
        }
    }
}
