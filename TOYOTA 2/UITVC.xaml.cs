using Alta_Keyboard.UControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using toyota.Class;

namespace TOYOTA_2
{
    /// <summary>
    /// Interaction logic for UITVC.xaml
    /// </summary>
    public partial class UITVC : UserControl
    {
        private User u;
        public event EventHandler<User> Comfirm_User_Event;
        public event EventHandler<User> Complete_User_Event;
        public ModePrint ModePrint = ModePrint.Default;
        public int PrintID { get; set; }
        public User @User
        {
            get
            {
                return this.u;
            }
            set
            {
                this.u = value;
                if (value != null)
                {
                    UIFacebook UIF = new UIFacebook();
                    UIF.Height = 572;
                    UIF.Width = 683;
                    UIF.User = value;
                    UIF.setDataPath(System.IO.Path.Combine(App.config.folderCache, value.Cache));
                    UIF.Comfirm_User_Event += UIF_Comfirm_User_Event;
                    UIF.Close_User_Event += UIF_Close_User_Event;
                    UIF.PrintID = this.PrintID;
                    UIF.Animation_View_Welcome();
                    UIF.Completed += UIF_Completed;
                    UIF.ModePrint = this.ModePrint;
                    this.UIFrame1.Children.Add(UIF);
                    Animation_View_GUI(null);
                    Animation_View_TVC(false);
                }
            }
        }

        void UIF_Completed(object sender, User e)
        {
            Animation_View_TVC(true);
            Animation_View_GUI(new Action(() =>
            {
                foreach (UIElement E in this.UIFrame1.Children)
                {
                    if (E is UIFacebook)
                    {
                        this.UIFrame1.Children.Remove(E);
                        break;
                    }
                }
            }), false);

            if(this.Comfirm_User_Event!=null)
                this.Complete_User_Event(this, e);


            //if (!string.IsNullOrEmpty(App.config.runFile))
            //{
            //    FileInfo fInfo = new FileInfo(App.config.PrintManager);
            //    ProcessStartInfo pInfo = new ProcessStartInfo(fInfo.Name);
            //    pInfo.WorkingDirectory = fInfo.DirectoryName;
            //    bool isStart = true;
            //    Process[] processlist = Process.GetProcesses();
            //    foreach (Process theprocess in processlist)
            //    {
            //        if (theprocess.ProcessName == "TOYOTA CMD")
            //        {
            //            isStart = false;
            //        }
            //    }
            //    // if(pInfo.is)
            //    if (isStart)
            //    {
            //        Process p = Process.Start(pInfo);
            //    }
            //}
            //else
            //{
            //    FileInfo file = new FileInfo(e.ImagePost);
            //    if (this.ModePrint == toyota.Class.ModePrint.Nice)
            //        file.PrintImage(this.PrintID);
            //    else
            //        file.PrintImageDefault();
            //}
            this.User = null;

        }

        void UIF_Close_User_Event(object sender, User e)
        {

            Animation_View_TVC(true);
            Animation_View_GUI(new Action(() =>
            {
                foreach (UIElement E in this.UIFrame1.Children)
                {
                    if (E is UIFacebook)
                    {
                        this.UIFrame1.Children.Remove(E);
                        break;
                    }
                }
            }), false);

            if (e != null)
            {
                this.Complete_User_Event(this, e);
            }
            
            this.User = null;
        }

        void UIF_Comfirm_User_Event(object sender, User e)
        {
            if (this.Comfirm_User_Event != null)
            {
                this.Comfirm_User_Event(this, this.u);
            }
        }

        public UITVC()
        {
            InitializeComponent();
        }

        #region Animation
        private void Animation_View_GUI(Action act, bool isView = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (isView)
            {
                da.From = -683;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 683;
                da.Completed += (o, e) => { this.UIFrame1.setLeft(-683); if (act != null) act(); };
            }
            this.UIFrame1.BeginAnimation(Canvas.LeftProperty, da);
        }
        private void Animation_View_TVC(bool isView = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (isView)
            {
                da.From = -683;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 683;
                da.Completed += (o, e) => { this.UIFrame0.setLeft(-683); };
            }
            this.UIFrame0.BeginAnimation(Canvas.LeftProperty, da);
        }
        #endregion
    }
}
