using MultiTouch.Behaviors.WPF4;
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
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using toyota.Assets.Themes;
using toyota.Class;

namespace TOYOTA_2.Assets.Themes
{
    /// <summary>
    /// Interaction logic for UITimeLine.xaml
    /// </summary>
    public partial class UITimeLine : UserControl
    {
        public static double W = 600;
        public static double H = 400;

        private int _indextheme = 0;
        private BehaviorCollection behaviors;
        private BehaviorCollection behaviors1;
        private BehaviorCollection behaviors2;
        private BehaviorCollection behaviors3;
        private BehaviorCollection behaviors4;
        private BehaviorCollection behaviors5;
        private MultiTouchBehavior MTtouchAni;
        private MultiTouchBehavior MTtouchAni1;
        private MultiTouchBehavior MTtouchAni2;
        private MultiTouchBehavior MTtouchAni3;
        private MultiTouchBehavior MTtouchAni4;
        private MultiTouchBehavior MTtouchAni5;
        public event EventHandler TouchBehavior;
        public int Index
        {
            get
            {
                return this._indextheme;
            }
            set
            {
                this._indextheme = value;
            }
        }
        private string img;
        public string Source
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
                if (File.Exists(value))
                {
                    var bimap = SetImageData(File.ReadAllBytes(value));
                    img1.Source = bimap;
                    img2.Source = bimap;
                    img3.Source = bimap;
                    img4.Source = bimap;
                    img5.Source = bimap;
                }
                else
                {
                    img1.Source = null;
                    img2.Source = null;
                    img3.Source = null;
                    img4.Source = null;
                    img5.Source = null;
                }

            }
        }

        public BitmapImage SetImageData(byte[] data)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(data);
            source.EndInit();
            return source;
        }

        public UITimeLine()
        {
            InitializeComponent();
            this.behaviors = Interaction.GetBehaviors(this.img1);
            this.behaviors1 = Interaction.GetBehaviors(this.img2);
            this.behaviors2 = Interaction.GetBehaviors(this.img3);
            this.behaviors3 = Interaction.GetBehaviors(this.img4);
            this.behaviors4 = Interaction.GetBehaviors(this.img5);
            this.behaviors5 = Interaction.GetBehaviors(this.img6);

            MTtouchAni = new MultiTouchBehavior();
            MTtouchAni.IsRotateEnabled = true;
            MTtouchAni.IsScaleEnabled = true;
            MTtouchAni.IsTranslateXEnabled = true;
            MTtouchAni.IsTranslateYEnabled = true;
            behaviors.Add(MTtouchAni);

            MTtouchAni1 = new MultiTouchBehavior();
            MTtouchAni1.IsRotateEnabled = true;
            MTtouchAni1.IsScaleEnabled = true;
            MTtouchAni1.IsTranslateXEnabled = true;
            MTtouchAni1.IsTranslateYEnabled = true;
            behaviors1.Add(MTtouchAni1);

            MTtouchAni2 = new MultiTouchBehavior();
            MTtouchAni2.IsRotateEnabled = true;
            MTtouchAni2.IsScaleEnabled = true;
            MTtouchAni2.IsTranslateXEnabled = true;
            MTtouchAni2.IsTranslateYEnabled = true;
            behaviors2.Add(MTtouchAni2);

            MTtouchAni3 = new MultiTouchBehavior();
            MTtouchAni3.IsRotateEnabled = true;
            MTtouchAni3.IsScaleEnabled = true;
            MTtouchAni3.IsTranslateXEnabled = true;
            MTtouchAni3.IsTranslateYEnabled = true;
            behaviors3.Add(MTtouchAni3);

            MTtouchAni4 = new MultiTouchBehavior();
            MTtouchAni4.IsRotateEnabled = true;
            MTtouchAni4.IsScaleEnabled = true;
            MTtouchAni4.IsTranslateXEnabled = true;
            MTtouchAni4.IsTranslateYEnabled = true;
            behaviors4.Add(MTtouchAni4);

            MTtouchAni5 = new MultiTouchBehavior();
            MTtouchAni5.IsRotateEnabled = true;
            MTtouchAni5.IsScaleEnabled = true;
            MTtouchAni5.IsTranslateXEnabled = true;
            MTtouchAni5.IsTranslateYEnabled = true;
            behaviors5.Add(MTtouchAni5);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height/H);
        }
        public void AnimationGUI(int i)
        {

            DoubleAnimation da = new DoubleAnimation(-this.Index * W, -i * W, TimeSpan.FromMilliseconds(600));
            da.Completed += (o, e) =>
            {
                this.Index = i;
            };
            this.UILayout.BeginAnimation(Canvas.LeftProperty, da);

        }

        public string SaveImage(string path, FileTypeImage Type = FileTypeImage.JPG)
        {
            return this.UIRoot.saveCanvasToFile(Type, path);
        }
        public UIElementInformation getTransform(int index = 0)
        {
            if (index == 0)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img1.RenderTransformOrigin,
                    top = this.img1.getTop(),
                    left = this.img1.getLeft(),
                    transform = this.img1.RenderTransform,
                    height = this.img1.Height,
                    width = this.img1.Width
                };
            }
            else if (index == 1)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img2.RenderTransformOrigin,
                    top = this.img2.getTop(),
                    left = this.img2.getLeft(),
                    transform = this.img2.RenderTransform,
                    height = this.img2.Height,
                    width = this.img2.Width
                };
            }
            else if (index == 2)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img3.RenderTransformOrigin,
                    top = this.img3.getTop(),
                    left = this.img3.getLeft(),
                    transform = this.img3.RenderTransform,
                    height = this.img3.Height,
                    width = this.img3.Width
                };
            }
            else if (index == 3)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img4.RenderTransformOrigin,
                    top = this.img4.getTop(),
                    left = this.img4.getLeft(),
                    transform = this.img4.RenderTransform,
                    height = this.img4.Height,
                    width = this.img4.Width
                };
            }
            else if (index == 4)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img5.RenderTransformOrigin,
                    top = this.img5.getTop(),
                    left = this.img5.getLeft(),
                    transform = this.img5.RenderTransform,
                    height = this.img5.Height,
                    width = this.img5.Width
                };
            }
            else if (index == 5)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img6.RenderTransformOrigin,
                    top = this.img6.getTop(),
                    left = this.img6.getLeft(),
                    transform = this.img6.RenderTransform,
                    height = this.img6.Height,
                    width = this.img6.Width
                };
            }
            return null;
        }

        public void setTransform(UIElementInformation U, int index = 0)
        {
            if (index == 0)
            {
                this.img1.setTop(U.top);
                this.img1.setLeft(U.left);
                this.img1.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img1.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if(index==1)
            {
                this.img2.setTop(U.top);
                this.img2.setLeft(U.left);
                this.img2.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img2.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 2)
            {
                this.img3.setTop(U.top);
                this.img3.setLeft(U.left);
                this.img3.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img3.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if(index ==3)
            {
                this.img4.setTop(U.top);
                this.img4.setLeft(U.left);
                this.img4.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img4.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 4)
            {
                this.img5.setTop(U.top);
                this.img5.setLeft(U.left);
                this.img5.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img5.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 5)
            {
                this.img6.setTop(U.top);
                this.img6.setLeft(U.left);
                this.img6.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img6.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
        }

        private void img_TouchDown(object sender, TouchEventArgs e)
        {
            this.img1.setZIndex(3);
        }

        private void img_TouchUp(object sender, TouchEventArgs e)
        {
            Image img = sender as Image;
            img.setZIndex(0);
            if (TouchBehavior != null)
            {
                TouchBehavior(this, new EventArgs());
            }
        }

        private void img2_2_TouchDown(object sender, TouchEventArgs e)
        {
            this.img2.setZIndex(3);
        }

        private void img5_5_TouchDown(object sender, TouchEventArgs e)
        {
            this.img5.setZIndex(3);
        }

        private void img4_4_TouchDown(object sender, TouchEventArgs e)
        {
            this.img4.setZIndex(3);
        }

        private void img3_3_TouchDown(object sender, TouchEventArgs e)
        {
            this.img3.setZIndex(3);
        }
    }
}
