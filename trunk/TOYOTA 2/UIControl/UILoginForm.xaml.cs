using Awesomium.Core;
using Facebook;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using toyota.Class;

namespace TOYOTA_2.UIControl
{
    public class FacebookEventArgs
    {
        public FacebookClient client { get; set; }
        public User user { get; set; }
    }
    /// <summary>
    /// Interaction logic for UILoginForm.xaml
    /// </summary>
    public partial class UILoginForm : UserControl
    {
        public event EventHandler<FacebookEventArgs> LoginSucessFacebook;
        public event EventHandler<User> LoginSucessEmail;
        public event EventHandler CompleteEvent;
        private bool isLogin = false;
        private User u;
        private FacebookClient FBClient;
        private string _userName = string.Empty;
        private string _passWord = string.Empty;
        public String appId = "1431678617045215";
        public String appSerect = "64f658347df2b6d773a32e313b87cf5f";
        private string url = "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri=http://www.facebook.com/connect/login_success.html&type=user_agent&display=popup&scope=publish_actions,publish_stream,user_likes";
        private int checkpoint = 0;
        private int oauth = 0;
        private RegexUtilities @RegexUtilities;
        public UILoginForm()
        {
            InitializeComponent();
            this.u = new User();
            this.RegexUtilities = new RegexUtilities();
        }

        #region Animation
        private void Animation_View_Popup_Err(bool isview = true, Action atc = null)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (isview)
            {
                da.From = -420;
                da.To = 0;
                da.Completed += (o, e) =>
                {
                    if (atc != null)
                    {
                        atc();
                    }
                };
            }
            else
            {
                da.From = 0;
                da.To = 420;
                da.Completed += (o, e) =>
                {
                    this.UIFrame5.setLeft(-420);
                    if (atc != null)
                    {
                        atc();
                    }
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);
            this.UIFrame5.BeginAnimation(Canvas.LeftProperty, da);

        }
        private void Animation_View_Popup(bool isview = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (isview)
            {
                da.From = -420;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 420;
                da.Completed += (o, e) =>
                {
                    this.UIFrame4.setLeft(-420);
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);
            this.UIFrame4.BeginAnimation(Canvas.LeftProperty, da);

        }
        private void Animation_View_Facebook(bool isview = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (isview)
            {
                da.From = -420;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 420;
                da.Completed += (o, e) =>
                {
                    this.UIFrame1.setLeft(-420);
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);
            this.UIFrame1.BeginAnimation(Canvas.LeftProperty, da);

        }
        private void Animation_View_Email(bool isview = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (isview)
            {
                da.From = -420;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 420;
                da.Completed += (o, e) =>
                {
                    this.UIFrame2.setLeft(-420);
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);
            this.UIFrame2.BeginAnimation(Canvas.LeftProperty, da);

        }
        private void Animation_View_Code(bool isview = true)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (isview)
            {
                da.From = -420;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 420;
                da.Completed += (o, e) =>
                {
                    this.UIFrame3.setLeft(-420);
                };
            }
            da.Duration = TimeSpan.FromMilliseconds(500);
            this.UIFrame3.BeginAnimation(Canvas.LeftProperty, da);

        }

        #endregion
        public void setDataPath(string path)
        {
            string filepath = System.IO.Path.Combine(path, string.Format("{0}", this.u.Cache));
            Console.WriteLine(filepath);
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                this.UIWebview.WebSession = WebCore.CreateWebSession(filepath, WebPreferences.Default);
                this.UIWebview.Source = new Uri(string.Format(this.url, this.appId));

            }
            else
            {
                this.u = new User();
                setDataPath(path);
            }

        }
        ~UILoginForm()
        {
            this.ClearCookie();
        }
        public void ClearCookie()
        {
            try
            {
                this.UIWebview.WebSession.ClearCache();
            }
            catch (Exception)
            {

            }
            // this.UIWebview.WebSession.ClearCookies();
        }

        private void UIWebview_AddressChanged(object sender, UrlEventArgs e)
        {
            Console.WriteLine(e.Url);
            this.u.lastUrl = e.Url.ToString();
            this.UILoadding.Opacity = 1;
            if (this.u.lastUrl.StartsWith("http://www.facebook.com/connect/login_success.html"))
            {
                this.u.Facebook = this.email.Text;
                this.u.Pass = this.pass.Password;
                this.u.AcessToken = this.UIWebview.Source.Fragment.Split('&')[0].Replace("#access_token=", "");
                FBClient = new FacebookClient(this.u.AcessToken);
                FBClient.AppId = this.appId;
                FBClient.AppSecret = this.appSerect;
                dynamic result = FBClient.Get("me", new { fields = new[] { "id", "name", "link" } });
                this.u.nameFacebook = result.name;
                this.u.idFacebook = result.id;
                this.u.linkFacebook = result.link;
                this.UIMsg2.Text = "Chúc mừng bạn đã đăng nhập thành công";
                this.Animation_View_Popup();
                this.u.Type = TypeUser.Facebook;
                if (this.LoginSucessFacebook != null)
                {
                    this.LoginSucessFacebook(this, new FacebookEventArgs() { client = this.FBClient, user = this.u });
                }
            }
        }
        private void UIWebview_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            this.UILoadding.Opacity = 0;
            this.UIView.Visibility = Visibility.Visible;
            string resultErr = this.UIWebview.ExecuteJavascriptWithResult("document.getElementsByClassName('login_error_box')[0]"); //undefined
            string checkpointButton = this.UIWebview.ExecuteJavascriptWithResult(string.Format("document.getElementById('checkpointSubmitButton')"));
            string checkPointSecondButton = this.UIWebview.ExecuteJavascriptWithResult(string.Format("document.getElementById('checkpointSecondaryButton')"));
            string checkPointCode = this.UIWebview.ExecuteJavascriptWithResult("document.getElementById('approvals_code').name");
            string confirmButton = this.UIWebview.ExecuteJavascriptWithResult(string.Format("document.getElementsByName('{0}')[0]", "__CONFIRM__"));
            if (resultErr != "undefined")
            {
                this.UIMsg.Text = "Tài khoản đăng nhập hoặc mật khẩu không đúng!";
                this.Animation_View_Popup_Err(true, new Action(() => { this.UIWebview.Source = new Uri(string.Format(this.url, this.appId)); }));
            }
            else if (checkPointCode == "approvals_code")
            {
                this.isLogin = true;
                Console.Beep();
                if (this.veryfile)
                {
                    this.UIMsg.Text = "Mã xác nhận không đúng!";
                    Animation_View_Popup_Err();
                }

                if (this.UIFrame3.getLeft() != 0)
                {
                    Animation_View_Code();
                    Animation_View_Facebook(false);
                }
            }
            else if (checkpointButton != "undefined")
            {
                if (confirmButton != "undefined")
                {
                    this.UIWebview.ExecuteJavascript(string.Format("document.getElementsByName('{0}')[0].click()", "__CONFIRM__"));
                    this.oauth++;
                    if (this.oauth < 2)
                    {
                        Task.Delay(1000).ContinueWith(_ =>
                        {
                            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                             delegate()
                             {
                                 //this.UIWebview.ExecuteJavascript(string.Format("document.getElementsByName('{0}')[0].click()", "__CONFIRM__"));
                                 this.UIWebview.Reload(false);
                             }));
                        }
                        );

                    }
                    else this.oauth = 0;
                }
                else
                {
                    this.isLogin = true;
                    this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('checkpointSubmitButton').click()"));
                }
            }
            else if (checkPointSecondButton != "undefined")
            {
                this.isLogin = true;
                this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('{0}').click()", "checkpointSecondaryButton"));
            }
            //else if (confirmButton != "undefined")
            //{
            //    this.UIWebview.ExecuteJavascript(string.Format("document.getElementsByName('{0}')[0].click()", "__CONFIRM__"));
            //    this.oauth++;
            //    if (this.oauth < 2)
            //    {
            //        Task.Delay(1000).ContinueWith(_ =>
            //        {
            //            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            //             delegate()
            //             {
            //                 this.UIWebview.ExecuteJavascript(string.Format("document.getElementsByName('{0}')[0].click()", "__CONFIRM__"));
            //             }));
            //        }
            //        );

            //    }
            //    else this.oauth = 0;
            //}
            //else if ((this.u.lastUrl.StartsWith("https://www.facebook.com/checkpoint/") || this.u.lastUrl.StartsWith("http://www.facebook.com/checkpoint/")))
            //{
            //    this.isLogin = true;
            //    string result = this.UIWebview.ExecuteJavascriptWithResult("document.getElementById('approvals_code').name");
            //    if (result == "approvals_code")
            //    {
            //        Console.Beep();
            //        if (this.UIFrame3.getLeft() != 0)
            //        {
            //            Animation_View_Code();
            //            Animation_View_Facebook(false);
            //        }

            //    }
            //    else if (checkpoint != 1)
            //    {
            //        this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('checkpointSubmitButton').click()"));
            //        checkpoint = 1;
            //    }
            //    else if (checkpoint == 1)
            //    {
            //        this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('{0}').click()", "checkpointSecondaryButton"));
            //        checkpoint = 2;
            //    }
            //}
            else if (this.u.lastUrl.StartsWith("https://www.facebook.com/v1.0/dialog/oauth?") || this.u.lastUrl.StartsWith("https://www.facebook.com/v1.0/dialog/oauth?"))
            {
                this.isLogin = true;
                string result = this.UIWebview.ExecuteJavascriptWithResult(string.Format("document.getElementsByName('{0}')[0]", "__CONFIRM__"));
                if (result != "undefined")
                {
                    this.UIWebview.ExecuteJavascript(string.Format("document.getElementsByName('{0}')[0].click()", "__CONFIRM__"));
                    this.oauth++;
                    if (this.oauth < 2)
                    {
                        Task.Delay(1000).ContinueWith(_ =>
                        {
                            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                             delegate()
                             {
                                 this.UIWebview.ExecuteJavascript(string.Format("document.getElementsByName('{0}')[0].click()", "__CONFIRM__"));
                             }));
                        }
                        );

                    }
                    else this.oauth = 0;
                }

            }


        }

        private void UIPreview_TouchUp_Forcus_Event(object sender, TouchEventArgs e)
        {

        }

        private void UIText_FocusableChanged(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void UIAnimation_Email_Event(object sender, MouseButtonEventArgs e)
        {
            this.Animation_View_Facebook();
            this.Animation_View_Email(false);
            this.u.Type = TypeUser.Facebook;
        }

        private void UIAnimation_Facebook_Event(object sender, MouseButtonEventArgs e)
        {
            this.Animation_View_Facebook(false);
            this.Animation_View_Email();
            this.u.Type = TypeUser.Mail;
        }

        private void UIReload_Event(object sender, MouseButtonEventArgs e)
        {
            this.UIWebview.Reload(false);
        }

        private void UITouch_Facebook_Event(object sender, TouchEventArgs e)
        {
            UIAnimation_Facebook_Event(sender, null);
        }

        private void UITouch_Email_Event(object sender, TouchEventArgs e)
        {
            this.UIAnimation_Email_Event(sender, null);
        }

        private void UIButton_Facebook_Login_Event(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(this.email.Text))
            {
                this.UIMsg.Text = "Tên đăng nhập không được để trống!";
                this.Animation_View_Popup_Err();
                return;
            }
            else if (string.IsNullOrEmpty(this.pass.Password))
            {
                this.UIMsg.Text = "Mật đăng nhập không được để trống!";
                this.Animation_View_Popup_Err();
                return;
            }

            this._userName = this.setKeys("email", this.email.Text);
            this._passWord = this.setKeys("pass", this.pass.Password);
            if (this._userName == "undefined" || this._passWord == "undefined")
            {
                this.UIWebview.Reload(false);
                return;
            }
            this.isLogin = true;
            this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('{0}').submit()", "login_form"));
        }


        public string setKeys(string element, string key)
        {
            return this.UIWebview.ExecuteJavascriptWithResult(string.Format("document.getElementById('{0}').value='{1}'", element, key));
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.UIView.Visibility == Visibility.Visible)
            {
                this.UIView.Visibility = Visibility.Hidden;

            }
            else
            {
                this.UIView.Visibility = Visibility.Visible;
                if (this.UIFrame1.getLeft() != 0)
                {
                    Animation_View_Facebook(true);
                }
            }
        }
        bool veryfile = false;
        private void UIButton_Set_Code_Event(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(this.approvals_code.Text))
            {
                this.UIMsg.Text = "Mã xác nhận không được để trống!";
                Animation_View_Popup_Err();
                return;
            }
            this.veryfile = true;
            string code = this.setKeys("approvals_code", this.approvals_code.Text);
            this.UIWebview.ExecuteJavascript(string.Format("document.getElementById('checkpointSubmitButton').click()"));
            this.checkpoint = 0;
        }

        private void UIButton_Complete_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.CompleteEvent != null)
            {
                this.CompleteEvent(this, new EventArgs());
            }
            try
            {
                this.UIWebview.Dispose();
                this.UIWebview = null;
            }
            catch (Exception)
            {

            }
        }

        private void UIButton_Login_Email_Event(object sender, MouseButtonEventArgs e)
        {
            if (!this.RegexUtilities.IsValidEmail(this.UIEmail_Text.Text))
            {
                this.UIMsg.Text = "Địa chỉ Email không đúng vui lòng nhập lại";
                Animation_View_Popup_Err();
                return;
            }
            this.u.Email = this.UIEmail_Text.Text;
            this.UIMsg2.Text = "Lưu địa chỉ Email thành công.";
            Animation_View_Popup();
            this.u.Type = TypeUser.Mail;
            if (this.LoginSucessEmail != null)
            {
                this.LoginSucessEmail(this, this.u);
            }
        }

        private void UIButton_Hide_Popup_Event(object sender, MouseButtonEventArgs e)
        {
            Animation_View_Popup_Err(false);
        }

        private void UITouch_Set_Code_Event(object sender, TouchEventArgs e)
        {
            UIButton_Set_Code_Event(sender, null);
        }

        private void UITouch_Reload_Event(object sender, TouchEventArgs e)
        {
            this.UIReload_Event(sender, null);
        }

        private void UITouch_Facebook_Login_Event(object sender, TouchEventArgs e)
        {
            this.UIButton_Facebook_Login_Event(sender, null);
        }

        private void UITouch_Login_Email_Event(object sender, TouchEventArgs e)
        {
            this.UIButton_Login_Email_Event(sender, null);
        }

        private void UITouch_Complete_Event(object sender, TouchEventArgs e)
        {
            this.UIButton_Complete_Event(sender, null);
        }

        private void UITouch_Hide_Popup_Event(object sender, TouchEventArgs e)
        {
            this.UIButton_Hide_Popup_Event(sender, null);
        }

        private void UIButton_Close_Event(object sender, MouseButtonEventArgs e)
        {
            if (this.CloseEvent != null)
            {
                this.CloseEvent(this, new EventArgs());
            }

            this.UIWebview.Dispose();
            this.UIWebview = null;
        }
        public event EventHandler CloseEvent;

        private void UITouch_Close_Event(object sender, TouchEventArgs e)
        {
            if (this.CloseEvent != null)
            {
                this.CloseEvent(this, new EventArgs());
            }
            this.UIWebview.Dispose();
            this.UIWebview = null;
        }

        private void UITouch_ViewForm(object sender, TouchEventArgs e)
        {

        }

        private void approvals_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                UIButton_Set_Code_Event(null, null);
        }

        private void UIEmail_Text_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIButton_Login_Email_Event(null, null);
            }
        }

        private void email_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIButton_Facebook_Login_Event(null, null);
            }
        }

    }
}
