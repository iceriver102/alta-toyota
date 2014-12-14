using Altamedia_MTC_CMD.Class;
using Awesomium.Core;
using Awesomium.Windows.Controls;
using Facebook;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using toyota;
using toyota.Assets.Themes;
using toyota.Class;
using TOYOTA_2;

namespace Alta_Keyboard.UControl
{
    public enum Mode
    {
        Empty, Email, Facebook
    }
    public class FacebookEventArgs
    {
        public FacebookClient client { get; set; }
        public User user { get; set; }
    }


    /// <summary>
    /// Interaction logic for UIFacebook.xaml
    /// </summary>
    public partial class UIFacebook : UserControl
    {
        public ManualResetEvent manualReset = new ManualResetEvent(false);
        private MainModel model;
        private string _previousFill;
        public event EventHandler<User> Comfirm_User_Event;
        public event EventHandler<User> Close_User_Event;
        public ModePrint @ModePrint = ModePrint.Default;
        public bool isNotCover = false;
        public int PrintID { get; set; }
        public string ImagePath
        {
            get
            {
                return this.u.ImageOriginal;
            }
            set
            {
                if (File.Exists(value))
                {
                    if (this.model == null)
                        this.model = new MainModel();
                    this.model.SetImageData(File.ReadAllBytes(value));
                    this.u.ImageOriginal = value;

                }
                else
                {
                    this.u.ImageOriginal = string.Empty;
                    this.model = null;

                }
                this.DataContext = this.model;
                this.UICover_View.Source = value;
                this.UITimeline_View.Source = value;
                this.UIImage_Timeline.Source = value;
                this.UIImage_Cover.Source = value;

                //this.UICover_View.DataContext = this.model;
                //this.UITimeline_View.DataContext = this.model;
                //this.UIImage_Timeline.DataContext = this.model;
                //this.UIImage_Cover.DataContext = this.model;
                //this.UITimeline_Review.DataContext = this.model;
                //this.UICover_Review.DataContext = this.model;

            }
        }
        private Mode mode = Mode.Empty;
        public User @User
        {
            get
            {
                return this.u;
            }
            set
            {
                this.u = value;
                if (this.u.Type == TypeUser.Facebook)
                {
                    this.Mode = UControl.Mode.Facebook;
                    this.FBClient = new FacebookClient(value.AcessToken);
                    this.FBClient.AppId = this.appId;
                    this.FBClient.AppSecret = this.appSerect;
                    this.UITextName.Text = value.nameFacebook.ToUpper();
                }
                else
                {
                    this.Mode = UControl.Mode.Email;
                    this.UITextName.Text = value.Email.ToUpper();
                }

            }
        }
        public void Clone()
        {
            UIElementInformation inf = UICover_View.getTransform(this.bar1.Num);
            UIImage_Cover.setTransform(inf, this.bar1.Num);
            UIElementInformation inf2 = UITimeline_View.getTransform(this.bar1.Num);
            UIImage_Timeline.setTransform(inf2, this.bar1.Num, 1);
        }
        public Mode @Mode
        {
            get
            {
                return this.mode;
            }
            private set
            {
                if (value == UControl.Mode.Email)
                {
                    this.UIFooter_Facebook.Visibility = Visibility.Hidden;
                    this.UIFooter_Mail.Visibility = Visibility.Visible;
                }
                else
                {
                    this.UIFooter_Facebook.Visibility = Visibility.Visible;
                    this.UIFooter_Mail.Visibility = Visibility.Hidden;
                }
                this.mode = value;
            }
        }
        private User u;
        private FacebookClient FBClient;
        public bool flagSucess;
        private string _userName = string.Empty;
        private string _passWord = string.Empty;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private bool isPageLike = false;
        // public String ImgFileName;
        public String appId = "1431678617045215";
        public String appSerect = "64f658347df2b6d773a32e313b87cf5f";
        private string url_cover = "https://www.facebook.com/profile.php?preview_cover={0}";// id image;
        private string url_fanpage = string.Empty;
        public event EventHandler<User> Completed;
        public static int delta = 90;
        private int checkpoint = 0;
        private Random rand;
        public UIFacebook()
        {
            if (this.model == null)
                this.model = new MainModel();
            this.DataContext = this.model;
            InitializeComponent();
            this.UIImage_Timeline.Width = App.config.width;
            this.UIImage_Timeline.Height = App.config.height;
            this.rand = new Random();
            this.url_fanpage = string.Format("https://www.facebook.com/{0}", App.config.fanPageID);
      
#if DEBUG
            this.u = new User();
            this.ImagePath = @"C:\Users\ThanhGiang\Desktop\8200931949_463396bc37_h.jpg";
#endif
        }
        public void setDataPath(string path)
        {
            string mainPath = string.Format("{0}_{1}", path, this.rand.Next(999999));
            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
                this.UIWebview.WebSession = WebCore.CreateWebSession(mainPath, new WebPreferences { SmoothScrolling = true });
            }
            else
            {
                this.setDataPath(path);
            }
        }



        public void ClearCookie()
        {
            this.UIWebview.WebSession.ClearCache();
            this.UIWebview.WebSession.ClearCookies();
        }

        private void UIWebview_AddressChanged(object sender, Awesomium.Core.UrlEventArgs e)
        {
            this.UILoadding.Opacity = 1;
            this.u.lastUrl = e.Url.ToString();
            Console.WriteLine(this.u.lastUrl);
            if (this.u.lastUrl.StartsWith(this.url_fanpage))
            {
                this.UIPopup_Facebook.Visibility = Visibility.Visible;                //u_0_1r
            }
        }

        private void UITouch_Refresh(object sender, TouchEventArgs e)
        {
            this.UIWebview.Reload(false);
        }
        private void UIMouse_Refresh(object sender, MouseButtonEventArgs e)
        {
            this.UIWebview.Reload(false);
        }

        #region Animation
        public void Animation_View_Welcome(bool isView = true)
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
                da.Completed += (o, s) =>
                {
                    Canvas.SetLeft(UIFrame0, -683);
                };
            }
            da.Duration = TimeSpan.FromSeconds(0.5);
            UIFrame0.BeginAnimation(Canvas.LeftProperty, da);
        }
        private void Animation_View_Popup(bool visible = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (visible)
            {
                da.From = -683;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 683;
                da.Completed += (o, s) =>
                {
                    Canvas.SetLeft(UIFrame4, -683);
                };
            }
            da.Duration = TimeSpan.FromSeconds(0.5);
            UIFrame4.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void Animation_View_Facebook(bool visible = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (visible)
            {
                da.From = -683;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 683;
                da.Completed += (o, s) =>
                {
                    Canvas.SetLeft(UIFrame1, -683);
                };
            }
            da.Duration = TimeSpan.FromSeconds(0.5);
            UIFrame1.BeginAnimation(Canvas.LeftProperty, da);
        }


        private void Animation_View_Themes(Action complete, bool visible = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (visible)
            {
                da.From = -683;
                da.To = 0;
                da.Completed += (o, e) =>
                {
                    if (complete != null)
                    {
                        complete();
                    }
                };
            }
            else
            {
                da.From = 0;
                da.To = 683;
                da.Completed += (o, e) =>
                {
                    if (complete != null)
                    {
                        complete();
                    }
                    Canvas.SetLeft(UIImage, -683);
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);

            UIImage.BeginAnimation(Canvas.LeftProperty, da);

        }
        private void Animation_View_Alert(bool visible = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (visible)
            {
                da.From = -683;
                da.To = 0;

            }
            else
            {
                da.From = 0;
                da.To = 683;
                da.Completed += (o, e) =>
                {
                    Canvas.SetLeft(UIFrame5, -683);
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);

            UIFrame5.BeginAnimation(Canvas.LeftProperty, da);

        }
        private void Animation_View_Email_Popup(bool visible = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (visible)
            {
                da.From = -683;
                da.To = 0;

            }
            else
            {
                da.From = 0;
                da.To = 683;
                da.Completed += (o, e) =>
                {
                    Canvas.SetLeft(UIFrame6, -683);
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);

            UIFrame6.BeginAnimation(Canvas.LeftProperty, da);
        }
        #endregion

        private void UIWebview_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            this.UILoadding.Opacity = 0;
            string resultErr = this.UIWebview.ExecuteJavascriptWithResult("document.getElementsByClassName('login_error_box')[0]"); //undefined
            if (resultErr != "undefined")
            {
                this.isNotCover = true;
                this.Animation_View_Facebook(false);
                this.Animation_View_Themes(null);
            }
            else if ((this.u.lastUrl.CompareTo("https://www.facebook.com/") == 0 || this.u.lastUrl.CompareTo("http://www.facebook.com/") == 0) && !this.isLogin)
            {
                this._email = this.setKeys("email", this.u.Facebook);
                this._passWord = this.setKeys("pass", this.u.Pass);
                if (this._email != "undefined")
                {
                    this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('{0}').submit()", "login_form"));
                    this.isLogin = true;

                }
                else
                {
                    this.UIWebview.Reload(false);
                }
            }
            else if ((this.u.lastUrl.StartsWith("https://www.facebook.com/checkpoint/") || this.u.lastUrl.StartsWith("http://www.facebook.com/checkpoint/")))
            {
                string result = this.UIWebview.ExecuteJavascriptWithResult("document.getElementById('approvals_code').name");
                if (result == "approvals_code")
                {
                    this.isNotCover = true;
                    this.Animation_View_Facebook(false);
                    this.Animation_View_Themes(null);
                }
                else if (checkpoint != 1)
                {
                    string resultButton = this.UIWebview.ExecuteJavascriptWithResult("document.getElementById('checkpointSubmitButton').Name");
                    if (resultButton != "undefined")
                    {
                        this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('checkpointSubmitButton').click()"));
                        checkpoint = 1;
                    }
                    else
                    {
                        string resultSecond = this.UIWebview.ExecuteJavascriptWithResult("document.getElementById('checkpointSecondaryButton').Name");
                        if (resultSecond != "undefined")
                        {
                            this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('{0}').click()", "checkpointSecondaryButton"));
                            checkpoint = 2;
                        }
                    }
                }

            }
            else if (this.u.lastUrl.IndexOf("?preview_cover=", StringComparison.CurrentCultureIgnoreCase) > 0)
            {
                this.UIWebview.ExecuteJavascript("window.scrollTo(300,150)");
            }
            else if (this.u.lastUrl != this.url_fanpage && !this.isPageLike)
            {
                this.UIWebview.Source = new Uri(this.url_fanpage);
                this.isPageLike = true;
            }
            else if (this.u.lastUrl.StartsWith(this.url_fanpage))
            {
                this.UIPopup_Facebook.Visibility = Visibility.Visible;
            }
        }
        public string setKeys(string element, string key)
        {
            return this.UIWebview.ExecuteJavascriptWithResult(string.Format("document.getElementById('{0}').value='{1}'", element, key));
        }

        private void UIButton_Like(object sender, MouseButtonEventArgs e)
        {
            if (this.isPageLike)
            {
                try
                {
                    this.UIWebview.ExecuteJavascript("document.getElementById('pagesHeaderLikeButton').getElementsByTagName('Button')[0].click()");
                    this.isPageLike = true;
                    Animation_View_Facebook(false);
                    Animation_View_Themes(new Action(() => { this.UIPopup_Facebook.Visibility = Visibility.Hidden; }));
                }
                catch (Exception)
                {

                }
            }
        }

        private void UIButton_Dont_Like(object sender, MouseButtonEventArgs e)
        {
            Animation_View_Facebook(false);
            Animation_View_Themes(new Action(() => { this.UIPopup_Facebook.Visibility = Visibility.Hidden; }));
        }

        private void UINext_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.u.isPrint)
            {
                // MessageBox.Show("Hình ảnh của bạn đã được in ra! vui lòng hoàn tất Game!");
                this.UIMsg.Text = "Hình ảnh của bạn đã được in ra! Bạn không thể chọn Theme khác.";
                Animation_View_Alert();
                return;
            }
            if (this.bar1.Num < toyota.UIControl.bar.max)
            {
                this.bar1.Num++;
                this.bar2.Num++;
                this.AnimationGUI(this.bar1.Num);
            }
            if (this.bar1.Num >= toyota.UIControl.bar.max)
            {
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.CacheOption = BitmapCacheOption.OnLoad;
                //bi.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_right_hover.png", UriKind.RelativeOrAbsolute);
                //bi.EndInit();
                this.UIRight1.Visibility= Visibility.Hidden;
                this.UIRight2.Visibility= Visibility.Hidden;
            }
            else
            {
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.CacheOption = BitmapCacheOption.OnLoad;
                //bi.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_right.png", UriKind.RelativeOrAbsolute);
                //bi.EndInit();
                this.UIRight1.Visibility = Visibility.Visible;
                this.UIRight2.Visibility = Visibility.Visible;

            }

            //BitmapImage bi1 = new BitmapImage();
            //bi1.BeginInit();
            //bi1.CacheOption = BitmapCacheOption.OnLoad;
            //bi1.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_left.png", UriKind.RelativeOrAbsolute);
            //bi1.EndInit();
            this.UILeft1.Visibility = Visibility.Visible;
            this.UILeft2.Visibility = Visibility.Visible;

        }
        private void AnimationGUI(int pos)
        {
            this.UICover_View.AnimationGUI(pos);
            this.UITimeline_View.AnimationGUI(pos);
            this.UIImage_Timeline.AnimationGUI(pos);
            this.UIImage_Cover.AnimationGUI(pos);
        }

        private void UIBack_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.u.isPrint)
            {
                this.UIMsg.Text = "Hình ảnh của bạn đã được in ra! Bạn không thể chọn Theme khác.";
                Animation_View_Alert();
                return;
            }
            if (this.bar1.Num > toyota.UIControl.bar.min)
            {
                this.bar1.Num--;
                this.bar2.Num--;
                this.AnimationGUI(this.bar1.Num);
            }
            if (this.bar1.Num <= toyota.UIControl.bar.min)
            {
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.CacheOption = BitmapCacheOption.OnLoad;
                //bi.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_left_hover.png", UriKind.RelativeOrAbsolute);
                //bi.EndInit();
                this.UILeft2.Visibility = Visibility.Hidden;
                this.UILeft1.Visibility = Visibility.Hidden;
            }
            else
            {
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.CacheOption = BitmapCacheOption.OnLoad;
                //bi.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_left.png", UriKind.RelativeOrAbsolute);
                //bi.EndInit();
                this.UILeft1.Visibility = Visibility.Visible;
                this.UILeft2.Visibility = Visibility.Visible;
            }

            //BitmapImage bi1 = new BitmapImage();
            //bi1.BeginInit();
            //bi1.CacheOption = BitmapCacheOption.OnLoad;
            //bi1.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_right.png", UriKind.RelativeOrAbsolute);
            //bi1.EndInit();
            this.UIRight1.Visibility = Visibility.Visible;
            this.UIRight2.Visibility = Visibility.Visible;


        }

        private void Touch_Swipe_Event(object sender, TouchEventArgs e)
        {
            if (!AlreadySwiped)
            {
                var Touch = e.GetTouchPoint(this);
                if (TouchStart != null && Touch.Position.X > (TouchStart.Position.X + delta))
                {
                    Console.Beep();
                    UITouch_Next_Event(sender, null);
                    AlreadySwiped = true;

                }

                if (TouchStart != null && Touch.Position.X < TouchStart.Position.X - delta)
                {
                    Console.Beep();
                    UITouch_Back_Event(sender, null);
                    AlreadySwiped = true;
                }

            }
            e.Handled = true;
        }

        private void Touch_Down_Event(object sender, TouchEventArgs e)
        {
            TouchStart = e.GetTouchPoint(this);
            e.Handled = true;
        }

        private void Touch_Up_Event(object sender, TouchEventArgs e)
        {
            AlreadySwiped = false;
        }
        private void UIWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private TouchPoint TouchStart { get; set; }
        private bool AlreadySwiped = false;
        private bool isLogin;

        /// <summary>
        /// Hàm gửi Gmail
        /// </summary>
        /// <param name="from">email gủi đi</param>
        /// <param name="fromName">Tên người gửi</param>
        /// <param name="pass">Mật khẩu email gửi đi</param>
        /// <param name="to">Email nhận</param>
        /// <param name="toName">Tên người nhận</param>
        /// <param name="msg">Nội dung tin nhắn bằn Html</param>
        /// <param name="Image1">Hình 1</param>
        /// <param name="Image2">Hình 2</param>
        /// <param name="subject">Tiêu đề Email</param>
        private void SendEmail(string from, string fromName, string pass, string to, string toName, string msg, string Image1 = "", string Image2 = "", string subject = "")
        {
            try
            {
                var fromAddress = new MailAddress(from, fromName, System.Text.Encoding.UTF8);
                var toAddress = new MailAddress(to, toName);
                string fromPassword = pass;
                string bodyEmail = String.Format(msg, toName, "Pic1");
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                (bodyEmail, null, MediaTypeNames.Text.Html);
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                };

                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(Image1))
                {
                    LinkedResource pic1 = new LinkedResource(@"Asset\header.png", MediaTypeNames.Image.Jpeg);
                    pic1.ContentId = "Pic1";
                    avHtml.LinkedResources.Add(pic1);
                }

                message.AlternateViews.Add(avHtml);
                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(Image1);
                System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(Image2);
                message.Attachments.Add(attachment1);
                message.Attachments.Add(attachment2);
                Guid sendGuid = Guid.NewGuid();
                smtp.SendCompleted += SendCompletedCallback;
                smtp.SendAsync(message, sendGuid);

            }
            catch (WebException we)
            {
                this.UIMsg.Text = "       Không thể kết nối với mail server! Vui lòng kiểm tra kết nối internet";
                Animation_View_Alert();
                Console.Beep();
                Console.WriteLine(we.GetBaseException().ToString());
            }
            catch (Exception e)
            {
                this.UIMsg.Text = "       Không thể kết nối với mail server! Vui lòng kiểm tra kết nối internet";
                Animation_View_Alert();
               // Animation_View_Email_Popup();
                Console.Beep();
                Console.Beep();
                Console.WriteLine(e.GetBaseException().ToString());
            }
        }

        private void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.UIMsg.Text = "Quá trình gửi mail đã bị huỷ!";
                Animation_View_Alert();
            }
            else if (e.Error != null)
            {
                this.UIMsg.Text = "Không thể gửi email tới địa chỉ thư này vui lòng kiểm tra lại!";
                Animation_View_Alert();
            }
            else
            {
                Animation_View_Email_Popup();
            }
        }


        private void UIButon_Set_Cover(object sender, MouseButtonEventArgs e)
        {
            if (this.UIThread2 == null)
            {
                this.UIThread2 = new Thread(ThreadUIFunction2);
                this.UIThread2.IsBackground = true;
                this.UIThread2.Start();
            }
            if (string.IsNullOrEmpty(this.u.ImagePost))
            {
                this.u.ImagePost = this.UIImage_Timeline.SaveImage(App.config.folderSave + "/Timeline");
                this.u.ImageCover = this.UIImage_Cover.SaveImage(App.config.folderSave + "/Cover");
            }
            if (!this.u.isPrint)
            {
                //FileInfo file = new FileInfo(this.u.ImagePost);
                //if (this.ModePrint == toyota.Class.ModePrint.Nice)
                //    file.PrintImage(this.PrintID);
                //else
                //    file.PrintImageDefault();
              
                this.u.isPrint = true;
            }
            this.UIPopup_Facebook.Visibility = Visibility.Hidden;
            Grid.SetRowSpan(this.UIWebview, 1);
            this.UILayout_Action.Visibility = Visibility.Visible;
            //
            if (string.IsNullOrEmpty(this.u.idImageCover))
            {
                this.u.idImageCover = this.FBClient.PostData(string.Format(App.config.facebookPostMsg, this.u.nameFacebook), this.u.ImageCover);
            }
            if (!this.isNotCover)
            {
                this.UIWebview.Source = new Uri(string.Format(this.url_cover, this.u.idImageCover));
                Animation_View_Facebook(true);
                Animation_View_Themes(null, false);
            }
            else
            {
                this.Animation_View_Popup();
            }
        }



        private void UIButton_Complete_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.Completed != null)
            {
                this.Completed(this, this.u);
            }
        }

        private void UIButton_Back_Event(object sender, MouseButtonEventArgs e)
        {
            Animation_View_Facebook(false);
            Animation_View_Themes(new Action(() => { Grid.SetRowSpan(this.UIWebview, 1); this.UILayout_Action.Visibility = Visibility.Hidden; }));
        }

        private void UIButton_Post_Timeline(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(this.u.ImagePost))
            {
                this.u.ImagePost = this.UIImage_Timeline.SaveImage(App.config.folderSave + "/Timeline");
                this.u.ImageCover = this.UIImage_Cover.SaveImage(App.config.folderSave + "/Cover");
            }
            if (!this.u.isPrint)
            {
                //FileInfo file = new FileInfo(this.u.ImagePost);
                //if (this.ModePrint == toyota.Class.ModePrint.Nice)
                //    file.PrintImage(this.PrintID);
                //else
                //    file.PrintImageDefault();
                this.u.isPrint = true;
            }
            if (string.IsNullOrEmpty(this.u.idImagePost))
            {
                this.u.idImagePost = this.FBClient.PostData(string.Format(App.config.facebookPostMsg, this.u.nameFacebook), this.u.ImagePost);
            }
            Animation_View_Popup(true);
            this.UIImagePost.IsEnabled = false;
        }

        private void UIButton_Back_TimeLine_Event(object sender, MouseButtonEventArgs e)
        {
            Animation_View_Popup(false);
        }

        private void UIButton_OK_Alert(object sender, MouseButtonEventArgs e)
        {
            Animation_View_Alert(false);
        }

        private Thread UIThread;
        private Thread UIThread2;
        private void ThreadUIFunction()
        {
            while (true)
            {
                UIFunction();
                Thread.Sleep(50);
            }
        }
        private void ThreadUIFunction2()
        {
            while (true)
            {
                UIFunction2();
                Thread.Sleep(50);
            }
        }
        void UIFunction2()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                  delegate()
                  {
                      BitmapImage bi = new BitmapImage();
                      bi.BeginInit();
                      bi.CacheOption = BitmapCacheOption.OnLoad;
                      bi.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_big_hover.png", UriKind.RelativeOrAbsolute);
                      bi.EndInit();
                      this.UIImageCover.Source = bi;
                  }));
            try
            {
                UIThread2.Abort();
                UIThread2 = null;
            }
            catch (Exception)
            {

            }
        }

        void UIFunction()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                  delegate()
                  {
                      BitmapImage bi = new BitmapImage();
                      bi.BeginInit();
                      bi.CacheOption = BitmapCacheOption.OnLoad;
                      bi.UriSource = new Uri("pack://application:,,,/TOYOTA 2;component/Assets/Images/button_normal_hover.png", UriKind.RelativeOrAbsolute);
                      bi.EndInit();
                      this.UIButton_SendEmail.Source = bi;
                      this.UIImagePost.Source = bi;
                  }));
            try
            {
                UIThread.Abort();
                UIThread = null;
            }
            catch (Exception)
            {

            }
        }
        private void UIBUtton_View_Popup(object sender, MouseButtonEventArgs e)
        {
            if (UIThread == null)
            {
                UIThread = new Thread(ThreadUIFunction);
                UIThread.IsBackground = true;
                UIThread.Start();
            }
            if (string.IsNullOrEmpty(this.u.ImagePost))
            {
                this.u.ImagePost = this.UIImage_Timeline.SaveImage(App.config.folderSave + "/Timeline");
                this.u.ImageCover = this.UIImage_Cover.SaveImage(App.config.folderSave + "/Cover");
            }
            if (!this.u.isPrint)
            {
                //FileInfo file = new FileInfo(this.u.ImagePost);
                //if (this.ModePrint == toyota.Class.ModePrint.Nice)
                //    file.PrintImage(this.PrintID);
                //else
                //    file.PrintImageDefault();
              
                this.u.isPrint = true;
            }
            if (!this.u.isSendEmail)
            {
                this.SendEmail(App.config.email, "Toyota", App.config.passEmail, this.u.Email, this.u.Name, App.config.emailBody, this.u.ImagePost, this.u.ImageCover, App.config.subject);
                this.u.isSendEmail = true;
            }
            
            this.UIButton_SendEmail.IsEnabled = false;
            Animation_View_Email_Popup();

        }
        private void UIButton_Back_Email_Event(object sender, MouseButtonEventArgs e)
        {
            Animation_View_Email_Popup(false);
        }

        #region Touch Event
        private void UITouch_Next_Event(object sender, TouchEventArgs e)
        {
            UINext_Event(sender, null);
        }

        private void UITouch_Back_Event(object sender, TouchEventArgs e)
        {
            UIBack_Event(sender, null);
        }

        private void UITouch_Back_Button_Event(object sender, TouchEventArgs e)
        {
            UIButton_Back_Event(sender, null);
        }

        private void UITouch_Complete_Event(object sender, TouchEventArgs e)
        {
            UIButton_Complete_Event(sender, null);
        }

        private void UITouch_Ok_Event(object sender, TouchEventArgs e)
        {
            UIButton_Like(sender, null);
        }

        private void UITouch_Cancel_Event(object sender, TouchEventArgs e)
        {
            UIButton_Dont_Like(sender, null);
        }


        private void UITouch_Post_Timeline_Event(object sender, TouchEventArgs e)
        {
            UIButton_Post_Timeline(sender, null);
        }

        private void UITouch_Set_Cover_Event(object sender, TouchEventArgs e)
        {
            UIButon_Set_Cover(sender, null);
        }


        private void UITouch_Back_TimeLine_Event(object sender, TouchEventArgs e)
        {
            UIButton_Back_TimeLine_Event(sender, null);
        }
        private void UITouch_OK_Alert_Event(object sender, TouchEventArgs e)
        {
            UIButton_OK_Alert(sender, null);
        }


        private void UITouch_Back_Email_Event(object sender, TouchEventArgs e)
        {
            UIButton_Back_Email_Event(sender, null);
        }
        private void UITouch_Compete_Set_Cover_Event(object sender, TouchEventArgs e)
        {
            UIButton_Compete_Set_Cover_Event(sender, null);
        }
        private void UITouch_Close_Event(object sender, TouchEventArgs e)
        {
            UIClose_Event(sender, null);
        }

        private void UITouch_Comfirm_Event(object sender, TouchEventArgs e)
        {
            UIButton_Comfirm_Event(sender, null);
        }

        private void UITouch_Cancel_User_Event(object sender, TouchEventArgs e)
        {
            UIButton_Cancel_User_Event(sender, null);
        }
        #endregion

        private void UIWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                this._previousFill = this.ImagePath;
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                this.ImagePath = dataString;
            }
        }

        private void UIWindow_DragLeave(object sender, DragEventArgs e)
        {
            this.ImagePath = this._previousFill;
        }

        private void UIWindow_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                this.ImagePath = dataString;
            }
        }

        private void UIButton_Compete_Set_Cover_Event(object sender, MouseButtonEventArgs e)
        {
            Animation_View_Facebook(false);
            Animation_View_Themes(new Action(() => { Animation_View_Popup(true); }), true);
        }

        private void UITouch_View_Popup_Event(object sender, TouchEventArgs e)
        {
            UIBUtton_View_Popup(sender, null);
        }

        private void TouchBehavior_Event(object sender, EventArgs e)
        {
            this.Clone();
        }
        private void UIButton_Comfirm_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.Comfirm_User_Event != null)
            {
                this.Comfirm_User_Event(this, this.User);
            }
            if (this.u.Type == TypeUser.Facebook)
                this.UIWebview.Source = new Uri("https://www.facebook.com/");
            Animation_View_Welcome(false);
            if (this.Mode == UControl.Mode.Facebook)
            {

                this.Animation_View_Facebook();
            }
            else
            {
                this.Animation_View_Themes(null);
            }
        }

        private void UIButton_Cancel_User_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.Close_User_Event != null)
            {
                this.Close_User_Event(this, null);
            }
        }

        private void UIClose_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.Close_User_Event != null)
            {
                this.Close_User_Event(this, this.u);
            }
        }




    }
}
