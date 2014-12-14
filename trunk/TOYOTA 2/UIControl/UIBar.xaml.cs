using MultiTouch.Behaviors.WPF4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using toyota.Class;
using TOYOTA_2;
using TOYOTA_2.UIControl;

namespace toyota.UIControl
{
    public class UserEventArgs
    {
        public int index { get; set; }
        public string path { get; set; }
    }
    /// <summary>
    /// Interaction logic for UIBar.xaml
    /// </summary>
    public partial class UIBar : UserControl
    {
        private bool AlreadySwiped = false;
        private bool _modeLoad = true;
        private string path_image_select = string.Empty;
        private int _index_page = 0;
        private string currentPath = string.Empty;
        private List<string> Files;
        private Thread checkFileThread;
        private double delta = 90;
        private int _maxpage = 0;
        public ModePrint ModePrint = ModePrint.Default;
        private int maxPage
        {
            get
            {
                return this._maxpage;
            }
            set
            {
                this._maxpage = value;
                this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                delegate()
                {
                    this.UIPageNum.Content = string.Format("{0}/{1}", this.Page+1, value==0 ? 1: value);
                }));
            }
        }
        private List<User> PenddingUser;
        public int PrintID {get;set;}
        public int Page
        {
            get
            {
                return this._index_page;
            }
            set
            {
                this._index_page = value;
                this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                delegate()
                {
                    this.UIPageNum.Content = string.Format("{0}/{1}", value+1, this.maxPage==0 ? 1 : this.maxPage);
                }));
            }
        }

        public bool isModeLoad
        {
            get
            {
                return this._modeLoad;
            }
            set
            {
                if (value)
                {
                    this.currentPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, App.config.folderData);

                }
                else
                {
                    this.currentPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, App.config.folderSave, "TimeLine");

                }
                UIItem.Mode = value;

                this._modeLoad = value;
            }
        }
        private double left = 0;
        private bool reload = false;
        private BackgroundWorker bw;
        public UIBar()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            this.PenddingUser = new List<User>();
            this.currentPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, App.config.folderData);
            this.Load(this.currentPath);
            this.LoadGUI();
            this.checkFileThread = new Thread(ThreadFunction);
            this.checkFileThread.IsBackground = true;
            this.checkFileThread.Start();
            this.Login();

            if (App.pin.user1 != null)
            {
                this.UIPin1.User = App.pin.user1;
            }
            if (App.pin.user2 != null)
            {
                this.UIPin2.User = App.pin.user2;
            }
            if (App.pin.user3 != null)
            {
                this.UIPin3.User = App.pin.user3;
            }
        }
        public void Login()
        {
            if (this.UIlayoutLogin.Children.Count > 0)
            {
                this.UIlayoutLogin.Children.Clear();
            }
            UILoginForm Form = new UILoginForm();
            Form.Height = 150;
            Form.Width = 420;
            Form.RenderTransformOrigin = new Point(0.5, 0.5);
            Form.RenderTransform = new RotateTransform() { Angle = 180 };
            Form.setLeft(0);
            Form.setTop(0);
            Form.setDataPath(App.config.folderCache);
            Form.LoginSucessFacebook += Form_LoginSucessFacebook;
            Form.LoginSucessEmail += Form_LoginSucessEmail;
            Form.CompleteEvent += Form_CompleteEvent;
            Form.CloseEvent += Form_CloseEvent;
            this.UIlayoutLogin.Children.Add(Form);
        }

        void Form_CloseEvent(object sender, EventArgs e)
        {
            this.Login();
        }

        void Form_CompleteEvent(object sender, EventArgs e)
        {
            this.Login();
        }

        void Form_LoginSucessEmail(object sender, User e)
        {
            if (e != null)
            {
                if (this.UIPin1.User == null)
                {
                    this.UIPin1.User = e;
                    App.pin.user1 = e;
                    CacheUserPin.Write("userPin.xml",App.pin);
                }
                else if (this.UIPin2.User == null)
                {
                    this.UIPin2.User = e;
                    App.pin.user2 = e;
                    CacheUserPin.Write("userPin.xml", App.pin);
                }
                else if (this.UIPin3.User == null)
                {
                    this.UIPin3.User = e;
                    App.pin.user3 = e;
                    CacheUserPin.Write("userPin.xml", App.pin);
                }
            }
        }

        void Form_LoginSucessFacebook(object sender, FacebookEventArgs e)
        {
            if (e.user != null)
            {
                if (this.UIPin1.User == null)
                {
                    this.UIPin1.User = e.user;
                    App.pin.user1 = e.user;
                    CacheUserPin.Write("userPin.xml", App.pin);
                }
                else if (this.UIPin2.User == null)
                {
                    this.UIPin2.User = e.user;
                    App.pin.user2 = e.user;
                    CacheUserPin.Write("userPin.xml", App.pin);
                }
                else if (this.UIPin3.User == null)
                {
                    this.UIPin3.User = e.user;
                    App.pin.user3 = e.user;
                    CacheUserPin.Write("userPin.xml", App.pin);
                }
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.reload = false;
            this.AnimationGUI(this.Page);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                 delegate()
                 {
                     int len = this.Files.Count;
                     this.UICount.setText(string.Format("{0}",len));
                     int lenE = this.UILayoutTranslate.Children.Count;
                     if (len < lenE)
                     {
                         this.UILayoutTranslate.Children.Clear();
                         this.UILayoutTranslate.Width = (len + 1) * 195;
                         for (int i = 0; i < len; i++)
                         {
                             UIItem item = new UIItem();
                             item.ImagePath = this.Files[i];
                             item.setLeft(i * 195);
                             item.SelectEvent += UIItem_Select;
                             item.PathChanged += PathChanged_Event;
                             item.DoDragDrop +=item_DoDragDrop;
                             this.UILayoutTranslate.Children.Add(item);
                         }
                     }
                     else if (len == lenE)
                     {
                         for (int i = 0; i < len; i++)
                         {

                             if (this.UILayoutTranslate.Children[i] is UIItem)
                             {
                                 UIItem item = this.UILayoutTranslate.Children[i] as UIItem;
                                 if (item.ImagePath.CompareTo(this.Files[i]) != 0)
                                     item.ImagePath = this.Files[i];
                                 Console.WriteLine(this.Files[i]);
                             }
                         }
                     }
                     else
                     {
                         this.UICount.RunAnimation(5);
                         this.UILayoutTranslate.Width = (len + 1) * 195;
                         for (int i = 0; i < len ; i++)
                         {
                             if (i < lenE)
                             {
                                 if (this.UILayoutTranslate.Children[i] is UIItem)
                                 {
                                     UIItem item = this.UILayoutTranslate.Children[i] as UIItem;
                                     if (item.ImagePath.CompareTo(this.Files[i]) != 0)
                                         item.ImagePath = this.Files[i];
                                 }
                             }
                             else
                             {
                                 UIItem item = new UIItem();
                                 item.ImagePath = this.Files[i];
                                 item.setLeft(i * 195);
                                 item.SelectEvent += UIItem_Select;
                                 item.PathChanged += PathChanged_Event;
                                 item.DoDragDrop += item_DoDragDrop;
                                 this.UILayoutTranslate.Children.Add(item);
                             }
                         }
                     }
               }));
        }
        public event EventHandler<string> DoDragDrop;
        void item_DoDragDrop(object sender, string e)
        {
            if (this.DoDragDrop != null)
            {
                this.DoDragDrop(this, e);
            }
        }

        private void ThreadFunction(object obj)
        {
            while (true)
            {
                App.config = Config.Read(App.fileName);
                List<string> files = this.getAllFile(this.currentPath);
                if (files != null)
                {
                   
                    try
                    {
                        foreach (string file in this.Files)
                        {
                            bool flag = true;
                            foreach (string f in files)
                            {
                                if (f.CompareTo(file) == 0)
                                {
                                    files.Remove(f);
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                this.Files.Remove(file);
                            }
                        };
                        foreach (string f in files)
                        {
                            this.Files.Add(f);
                        }

                    }
                    catch (Exception)
                    {
                        this.Files = files;
                    }
                    finally
                    {
                        this.Files.Sort((s1, s2) =>
                        {
                            FileInfo file = new FileInfo(s1);
                            FileInfo file1 = new FileInfo(s2);
                            if (file.CreationTime > file1.CreationTime)
                                return -1;
                            else if (file.CreationTime == file1.CreationTime)
                                return 0;
                            else return 1;
                        });
                        
                        int len = this.Files.Count;
                        for (int i = len-1; i > 49; i--)
                        {
                            if (this.isModeLoad)
                            {
                                File.Delete(this.Files[i]);
                            }
                            this.Files.RemoveAt(i);
                        }
                        len = this.Files.Count;
                            this.maxPage = len / 3;
                       
                        if (len % 3 != 0)
                        {
                            this.maxPage++;
                        }
                        this.reload = true;
                    }
                }
                else
                {
                    this.Files = new List<string>();
                    this.reload = true;
                    this.maxPage = 1;
                }
                if (this.reload)
                {
                    LoadGUI();
                }
                Thread.Sleep(Convert.ToInt32((App.config.timecheck * 1000)));
                //Thread.Sleep(100);
            }
        }

        private List<string> getAllFile(string path)
        {
            if (!Directory.Exists(path))
                return null;
            return Directory.GetFiles(path).Where(file => file.ToLower().EndsWith("png") || file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("jpge")).ToList();
        }

        public void Load(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            this.Files = this.getAllFile(path);
            this.Files.Sort((s1, s2) =>
            {
                FileInfo file = new FileInfo(s1);
                FileInfo file1 = new FileInfo(s2);
                if (file.CreationTime > file1.CreationTime)
                    return -1;
                else if (file.CreationTime == file1.CreationTime)
                    return 0;
                else return 1;
            });
            int len = this.Files.Count;
            for (int i = len-1; i > 49; i--)
            {
                if (this.isModeLoad)
                {
                    File.Delete(this.Files[i]);
                }
                this.Files.RemoveAt(i);
            }
            len = this.Files.Count;
            this.maxPage = len / 3;
            if (len % 3 != 0)
            {
                this.maxPage++;
            }
        }

        public void removeFile(string file)
        {
            int len = this.Files.Count;
            for (int i = 0; i < len; i++)
            {
                if (this.Files[i].CompareTo(file) == 0)
                {
                    this.Files.RemoveAt(i);
                    break;
                }
            }
        }
        public void LoadGUI()
        {
            if (!this.bw.IsBusy)
            {
                this.bw.RunWorkerAsync();
            }
        }

        public void AnimationGUI(int page = 0)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                 delegate()
                 {
                     if (page >= this.maxPage && this.maxPage>0)
                     {
                         page = this.maxPage - 1;
                     }
                     this.Page = page;
                     DoubleAnimation da = new DoubleAnimation(this.left, -page * 580, TimeSpan.FromMilliseconds(500));
                     da.Completed += (o, e) =>
                     {
                         this.left = -page * 580;
                     };
                     this.UILayoutTranslate.BeginAnimation(Canvas.LeftProperty, da);
                 }));
        }


        private void UIItem_Select(object sender, string e)
        {
            int len = this.UILayoutTranslate.Children.Count;
            for (int i = 0; i < len; i++)
            {
                if (this.UILayoutTranslate.Children[i] is UIItem)
                {
                    UIItem item = this.UILayoutTranslate.Children[i] as UIItem;
                    if (item.ImagePath != e)
                    {
                        item.isSelect = false;
                    }
                }
            }
            this.path_image_select = e;
        }
        private void PathChanged_Event(object sender, string e)
        {
            this.path_image_select = e;
        }

        private void UIButon_Mode_Load(object sender, MouseButtonEventArgs e)
        {
            this.isModeLoad = true;
            this.Load(this.currentPath);
            this.LoadGUI();
        }

        private void UIButon_Print(object sender, MouseButtonEventArgs e)
        {
           FileInfo file = new FileInfo(this.path_image_select);
           if (this.ModePrint == Class.ModePrint.Default)
               file.PrintImageDefault();
           else
               file.PrintImage(this.PrintID);
        }       

        private void UIButton_Delete(object sender, MouseButtonEventArgs e)
        {
            if (this.isModeLoad)
            {
                int len = this.UILayoutTranslate.Children.Count;
                string path = string.Empty;
                UIElementCollection Elements = this.UILayoutTranslate.Children;
                for (int i = 0; i < len; i++)
                {
                    if (Elements[i] is UIItem)
                    {
                        UIItem item = Elements[i] as UIItem;
                        if (item.isSelect)
                        {
                            path = item.ImagePath;
                            break;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(path))
                {
                    File.Delete(path);
                    this.Load(this.currentPath);
                    (this.UILayoutTranslate.Children[0] as UIItem).isSelect = true;
                    this.removeFile(path);
                    this.LoadGUI();
                }
            }
        }

        #region Touch Event

        private void UITouch_Mode_Load_Event(object sender, TouchEventArgs e)
        {
            UIButon_Mode_Load(sender, null);
        }

        private void UITouch_Print_Event(object sender, TouchEventArgs e)
        {
            UIButon_Print(sender, null);
        }

        private void UITouch_Delete_Event(object sender, TouchEventArgs e)
        {
            UIButton_Delete(sender, null);
        }

        #endregion

        private TouchPoint TouchStart { get; set; }

        internal void RemoveFile(string p)
        {
            if (this.Files != null)
            {
                this.Files.Remove(p);
                this.LoadGUI();
            }
        }

        private void UITouch_Down_Event(object sender, TouchEventArgs e)
        {
            this.TouchStart = e.GetTouchPoint(this);
        }

        private void UITouch_Up_Event(object sender, TouchEventArgs e)
        {
            this.AlreadySwiped = false;
            this.TouchStart = null;
        }

        private void UITouch_Move_Event(object sender, TouchEventArgs e)
        {
            if (this.TouchStart != null && !this.AlreadySwiped)
            {
                var Touch = e.GetTouchPoint(this);
                if (this.TouchStart != null && Touch.Position.X > (this.TouchStart.Position.X + delta))
                {
                    AlreadySwiped = true;
                    if (this.Page > 0)
                    {
                        AnimationGUI(this.Page - 1);
                       
                    }
                }

                if (this.TouchStart != null && Touch.Position.X < this.TouchStart.Position.X - delta)
                {
                    AlreadySwiped = true;
                    if (this.Page < this.maxPage - 1)
                    {
                        AnimationGUI(this.Page + 1);
                        
                    }
                   
                }
            }
        }

        private void UIButon_Mode_Load_Timeline(object sender, MouseButtonEventArgs e)
        {
            this.isModeLoad = false;
            this.Load(this.currentPath);
            this.LoadGUI();
        }

        private void UITouch_Mode_Load_Timeline_Event(object sender, TouchEventArgs e)
        {
            this.UIButon_Mode_Load_Timeline(sender, null);
        }

        internal void removeUser(User e)
        {
            if (this.UIPin1.User != null && this.UIPin1.User.id == e.id)
            {
                this.UIPin1.User = null;
                App.pin.user1 = null;
                CacheUserPin.Write("userPin.xml", App.pin);
            }
            else if (this.UIPin2.User != null && this.UIPin2.User.id == e.id)
            {
                this.UIPin2.User = null;
                App.pin.user2 = null;
                CacheUserPin.Write("userPin.xml", App.pin);
            }
            else if (this.UIPin3.User != null && this.UIPin3.User.id == e.id)
            {
                App.pin.user3 = null;
                this.UIPin3.User = null;
                CacheUserPin.Write("userPin.xml", App.pin);
            }
        }
        public event EventHandler<User> ChooseUserEvent;
        private void UIChooseUser(object sender, User e)
        {
            if (this.ChooseUserEvent != null)
            {
                this.ChooseUserEvent(this, e);
            }
        }
    }
}
