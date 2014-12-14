using System;
using System.Collections.Generic;
using System.Text;
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

namespace TOYOTA_2
{
	/// <summary>
	/// Interaction logic for UIRound.xaml
	/// </summary>
	public partial class UIRound : UserControl
	{
		public UIRound()
		{
			this.InitializeComponent();
		}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.UIBackground.Width = this.Width - 2;
            this.UIBackground.Height = this.Height - 2;
            this.UIEffect.Opacity = 0;
        }
        public void RunAnimation(bool isloop=true)
        {
            DoubleAnimation da = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400));
            if(isloop)
                da.RepeatBehavior = RepeatBehavior.Forever;
            da.EasingFunction = new PowerEase() {  EasingMode = EasingMode.EaseInOut };
            this.UIEffect.BeginAnimation(OpacityProperty, da);
        }
        public void RunAnimation(int time )
        {
            DoubleAnimation da = new DoubleAnimation(0, 0.8, TimeSpan.FromMilliseconds(400));
            int count = 0;
            da.Completed += (o, e) =>
            {
                if (count < time)
                {
                    count++;
                    this.UIEffect.BeginAnimation(OpacityProperty, da);
                }
                else
                {
                    this.UIEffect.Opacity = 0;
                }
            };
            da.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut };
            this.UIEffect.BeginAnimation(OpacityProperty, da);
        }
        public void setText(string text)
        {
            this.UIContent.Content = text;
        }

	}
}