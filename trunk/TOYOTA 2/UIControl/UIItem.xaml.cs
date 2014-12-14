using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace toyota.UIControl
{
    /// <summary>
    /// Interaction logic for UIItem.xaml
    /// </summary>
    public partial class UIItem : UserControl
    {
        public static bool Mode = true;
        private string _path = string.Empty;
        private MainModel model;
        public event EventHandler<string> SelectEvent;
        private bool _isselect = false;
        private bool isDrag = false;

        public bool isSelect
        {
            get
            {
                return this._isselect;
            }
            set
            {
                if (value)
                {
                    this.UIBorder.Visibility = Visibility.Visible;
                    if (this.SelectEvent != null)
                    {
                        this.SelectEvent(this, this.ImagePath);
                    }
                }
                else
                {
                    this.UIBorder.Visibility = Visibility.Hidden;
                }
                this._isselect = value;
            }
        }
        public string ImagePath
        {
            get
            {
                return this._path;
            }
            set
            {
                if (File.Exists(value))
                {
                    if (this.model == null)
                        this.model = new MainModel();
                    this.model.SetImageData(File.ReadAllBytes(value));
                }
                else
                {
                    this.model = null;
                }
                this._path = value;
                this.DataContext = this.model;
                if (this.PathChanged != null && this.isSelect)
                {
                    this.PathChanged(this, this._path);
                }

            }
        }
        public BitmapImage SetImageData(string path)
        {
            var source = new BitmapImage();
            source.BeginInit();
            byte[] bytes;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
            }
            source.StreamSource = new MemoryStream(bytes);
            source.EndInit();
            return source;
        }
        public event EventHandler<string> PathChanged;
        public UIItem()
        {
            if (this.model == null)
                this.model = new MainModel();
            this.DataContext = model;
            InitializeComponent();
        }

        

        private void Item_TouchUp(object sender, TouchEventArgs e)
        {
            if (!this.isSelect)
                this.isSelect = true;
            this.isDrag = false;
               // e.Handled = false;
        }

        private void UIBorder_DragLeave(object sender, DragEventArgs e)
        {
            this.isDrag = false;
        }

        private void UITouch_Move_Event(object sender, TouchEventArgs e)
        {
            if (this.isSelect && Mode&& this.isDrag && this.DoDragDrop==null)
            {
                DragDrop.DoDragDrop(this, this.ImagePath, DragDropEffects.Copy);
            }
            else if (this.isDrag && this.isSelect)
            {
                this.DoDragDrop(this, this.ImagePath);
            }
        }
        public event EventHandler<string> DoDragDrop;
        private void UITouch_Down_Event(object sender, TouchEventArgs e)
        {
            this.isDrag = true;
        }

        private void UIButton_Choose_Event(object sender, MouseButtonEventArgs e)
        {
            if (!this.isSelect)
                this.isSelect = true;
            this.isDrag = false;
        }

        private void UIButton_Down_Event(object sender, MouseButtonEventArgs e)
        {
            this.isDrag = true;
        }

        private void UIButton_Move_Event(object sender, MouseEventArgs e)
        {
            if (this.isSelect && Mode && this.isDrag && this.DoDragDrop == null)
            {
                DragDrop.DoDragDrop(this, this.ImagePath, DragDropEffects.Copy);
            }
            else if (this.isDrag && this.isSelect)
            {
                this.DoDragDrop(this, this.ImagePath);
            }
        }
     
    }
}
