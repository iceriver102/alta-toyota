using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using toyota.Class;

namespace TOYOTA_2
{
    /// <summary>
    /// Interaction logic for UIPinUser.xaml
    /// </summary>
    public partial class UIPinUser : UserControl
    {
        private User u=null;
        private bool isDrag = false;
        public event EventHandler<User> ChooseUserEvent;
        public User @User
        {
            get
            {
                return u;
            }
            set
            {
                if (value == null)
                {
                    this.empty.Visibility = Visibility.Visible;
                    this.not_empty.Visibility = Visibility.Hidden;
                    this._text.Foreground = Brushes.White;
                }
                else
                {
                    this.empty.Visibility = Visibility.Hidden;
                    this.not_empty.Visibility = Visibility.Visible;
                    this._text.Foreground = new SolidColorBrush(new Color() { A=100, R=108, G=113, B=116 });
                    //6c7174
                }
                this.u = value;
            }
        }
        public string Text
        {
            get
            {
                return this._text.Content.ToString();
            }
            set
            {
                this._text.Content = value;
            }
        }
        public UIPinUser()
        {
            this.InitializeComponent();
        }

        private void UITouch_Down_Event(object sender, TouchEventArgs e)
        {
            if (this.u != null)
            {
                this.isDrag = true;
            }
        }

        private void UITouch_Up_Event(object sender, TouchEventArgs e)
        {
            this.isDrag = false;
            if (this.ChooseUserEvent != null)
            {
                this.ChooseUserEvent(this, this.User);
            }
            
        }

        private void UITouch_Move_Event(object sender, TouchEventArgs e)
        {
            if (this.isDrag && this.u != null && this.ChooseUserEvent == null)
            {
                DataObject d = new DataObject();
                d.SetData(DataFormats.Serializable, this.u);
                DragDrop.DoDragDrop(this, d, DragDropEffects.Copy);
                
            }
        }

        private void UIButton_Choose(object sender, MouseButtonEventArgs e)
        {
            if (this.ChooseUserEvent != null)
            {
                this.ChooseUserEvent(this, this.User);
            }
        }
    }
}