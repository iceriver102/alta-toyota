
using System;
using System.Collections.Generic;
using System.IO;
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
using toyota.Class;

namespace TOYOTA_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static double WIDTH = 1366;
        public static double HEIGHT = 768;
        public UserCache Cache;
        public string CacheStringDrag = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            this.Cache = UserCache.Read(App.config.userFile);
            this.UIBar.PrintID = App.config.printId[0];
            this.UIBar.ModePrint = App.config.Print1;
            this.UITVC1.PrintID = App.config.printId[0];
            this.UITVC1.ModePrint = App.config.Print2;
            this.UITVC2.ModePrint = App.config.Print3;
            if (App.config.printId.Length > 1)
                this.UITVC2.PrintID = App.config.printId[1];
            else this.UITVC2.PrintID = App.config.printId[0];
        }


        private void ChooseUser(object sender, User e)
        {
            if (this.UITVC1.User == null)
            {
                this.UITVC1.User = e;
            }
            else if (this.UITVC2.User == null)
            {
                this.UITVC2.User = e;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.E:
                        this.WriteFileCsv();
                        break;
                }
            }
        }
        void WriteFileCsv(string fileName = @"Data/DataExport.csv")
        {
            using (CsvFileWriter writer = new CsvFileWriter(fileName))
            {
                int len = this.Cache.ListUser.Count;
                for (int i = 0; i < len; i++)
                {
                    writer.WriteRow(this.Cache.ListUser[i].toStringCSV());
                }
            }
        }

        private void UIFacebook_Complete_User(object sender, User e)
        {
            if (e == null)
                return;
            this.Cache.Add(e);
            if (File.Exists(e.ImageOriginal))
            {
                File.Delete(e.ImageOriginal);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UIRoot.RenderTransform = new ScaleTransform(this.Width / WIDTH, this.Height / HEIGHT);
        }

        private void UIWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserCache.Write(App.config.userFile, this.Cache);
        }

        private void UIFacebook_Confirm_User_Event(object sender, toyota.Class.User e)
        {
            if (e == null)
                return;
            this.UIBar.removeUser(e);
        }
        private void UIBar_DoDragDrop(object sender, string e)
        {
            this.CacheStringDrag = e;
        }

        private void UIWindow_TouchUp(object sender, TouchEventArgs e)
        {
            if (string.IsNullOrEmpty(this.CacheStringDrag))
                return;
            var Touch = e.GetTouchPoint(this);
            double y = Math.Min(Touch.Position.Y, 179);
            double x = Touch.Position.X;
            if (y == 179 && x <= 683)
            {
                UITVC_TouchMove(UITVC1, null);
            }
            else if (y == 179 && x >= 683)
            {
                UITVC_TouchMove(UITVC2, null);
            }
            this.CacheStringDrag = string.Empty;
        }

        private void UITVC_TouchMove(object sender, TouchEventArgs e)
        {
            if (string.IsNullOrEmpty(this.CacheStringDrag))
                return;
            UITVC TVC = sender as UITVC;
            if (TVC.UIFrame1.Children.Count > 0)
            {
                foreach (UIElement E in TVC.UIFrame1.Children)
                {
                    if (E is Alta_Keyboard.UControl.UIFacebook)
                    {
                        Alta_Keyboard.UControl.UIFacebook face = E as Alta_Keyboard.UControl.UIFacebook;
                        if (face.ImagePath == this.CacheStringDrag)
                        {
                            return;
                        }
                        else
                        {
                            face.ImagePath = this.CacheStringDrag;
                            this.CacheStringDrag = string.Empty;
                        }


                    }
                }
            }
        }

        private void UIButton_Up_Event(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(this.CacheStringDrag))
                return;
            var Touch = e.GetPosition(this);
            double y = Math.Min(Touch.Y, 179);
            double x = Touch.X;
            if (y == 179 && x <= 683)
            {
                UITVC_TouchMove(UITVC1, null);
            }
            else if (y == 179 && x >= 683)
            {
                UITVC_TouchMove(UITVC2, null);
            }
            this.CacheStringDrag = string.Empty;
        }

    }
}
