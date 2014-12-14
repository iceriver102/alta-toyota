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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using toyota.Class;

namespace toyota.Assets.Themes
{
    public class UIElementInformation
    {
        public double height { get; set; }
        public double width { get; set; }
        public double top { get; set; }
        public double left { get; set; }
        public Transform transform { get; set; }
        public Point RenderTransformOrigin { get; set; }
         public SimpleJSON.JSONNode tojson()
        {
            SimpleJSON.JSONNode jsonNode = SimpleJSON.JSON.Parse("{" + string.Format("height:{0}", this.height)+ "}");
            jsonNode.Add("width", this.width.ToString());
            MatrixConverter mConverter = new MatrixConverter();
            jsonNode.Add("transform", mConverter.ConvertToString(this.transform.Value));
            SimpleJSON.JSONNode jsonNodeP = SimpleJSON.JSON.Parse("{" + string.Format("X:{0}", this.RenderTransformOrigin.X)+ "}");
            jsonNodeP.Add("Y", this.RenderTransformOrigin.Y.ToString());
            jsonNode.Add("RenderTransformOrigin", jsonNodeP);
            return jsonNode;
        }
    }
    /// <summary>
    /// Interaction logic for Theme_Cover_0.xaml
    /// </summary>
    public partial class Theme_Cover_0 : UserControl
    {

        private BehaviorCollection behaviors;
        private BehaviorCollection behaviors1;
        private BehaviorCollection behaviors2;
        private BehaviorCollection behaviors3;
        private BehaviorCollection behaviors4;
        private BehaviorCollection behaviors5;
        private BehaviorCollection behaviors6;
        private BehaviorCollection behaviors7;
        private BehaviorCollection behaviors8;
        private BehaviorCollection behaviors9;
        private BehaviorCollection behaviors10;
        private MultiTouchBehavior MTtouchAni;
        private MultiTouchBehavior MTtouchAni1;
        private MultiTouchBehavior MTtouchAni2;
        private MultiTouchBehavior MTtouchAni3;
        private MultiTouchBehavior MTtouchAni4;
        private MultiTouchBehavior MTtouchAni5;
        private MultiTouchBehavior MTtouchAni6;
        private MultiTouchBehavior MTtouchAni7;
        private MultiTouchBehavior MTtouchAni8;
        private MultiTouchBehavior MTtouchAni9;
        private MultiTouchBehavior MTtouchAni10;
        private double W = 852;
        private double H = 316;
        private int _indextheme = 0;
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

        public Theme_Cover_0()
        {

            InitializeComponent();
            this.behaviors = Interaction.GetBehaviors(this.img1);
            this.behaviors1 = Interaction.GetBehaviors(this.img2);
            this.behaviors2 = Interaction.GetBehaviors(this.img3);
            this.behaviors3 = Interaction.GetBehaviors(this.img4);
            this.behaviors4 = Interaction.GetBehaviors(this.img5);
            this.behaviors5 = Interaction.GetBehaviors(this.img6);
            this.behaviors6 = Interaction.GetBehaviors(this.img7);
            this.behaviors7 = Interaction.GetBehaviors(this.img8);
            this.behaviors8 = Interaction.GetBehaviors(this.img9);
            this.behaviors9 = Interaction.GetBehaviors(this.img10);
            this.behaviors10 = Interaction.GetBehaviors(this.img11);


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

            MTtouchAni6 = new MultiTouchBehavior();
            MTtouchAni6.IsRotateEnabled = true;
            MTtouchAni6.IsScaleEnabled = true;
            MTtouchAni6.IsTranslateXEnabled = true;
            MTtouchAni6.IsTranslateYEnabled = true;
            behaviors6.Add(MTtouchAni6);

            MTtouchAni7 = new MultiTouchBehavior();
            MTtouchAni7.IsRotateEnabled = true;
            MTtouchAni7.IsScaleEnabled = true;
            MTtouchAni7.IsTranslateXEnabled = true;
            MTtouchAni7.IsTranslateYEnabled = true;
            behaviors7.Add(MTtouchAni7);

            MTtouchAni8 = new MultiTouchBehavior();
            MTtouchAni8.IsRotateEnabled = true;
            MTtouchAni8.IsScaleEnabled = true;
            MTtouchAni8.IsTranslateXEnabled = true;
            MTtouchAni8.IsTranslateYEnabled = true;
            behaviors8.Add(MTtouchAni8);

            MTtouchAni9 = new MultiTouchBehavior();
            MTtouchAni9.IsRotateEnabled = true;
            MTtouchAni9.IsScaleEnabled = true;
            MTtouchAni9.IsTranslateXEnabled = true;
            MTtouchAni9.IsTranslateYEnabled = true;
            behaviors9.Add(MTtouchAni9);

            MTtouchAni10 = new MultiTouchBehavior();
            MTtouchAni10.IsRotateEnabled = true;
            MTtouchAni10.IsScaleEnabled = true;
            MTtouchAni10.IsTranslateXEnabled = true;
            MTtouchAni10.IsTranslateYEnabled = true;
            behaviors10.Add(MTtouchAni10);

        }
        public BitmapImage SetImageData(byte[] data)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(data);
            source.EndInit();
            return source;
        }
        public string SaveImage(string path, FileTypeImage Type = FileTypeImage.JPG)
        {
            return this.UIRoot.saveCanvasToFile(Type, path);
        }

        private void UIWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
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
        private void img_TouchUp(object sender, TouchEventArgs e)
        {
            Image img = sender as Image;
            img.setZIndex(0);
            if (this.TouchBehavior != null)
            {
                this.TouchBehavior(this, new EventArgs());
            }
        }

        private void img_TouchDown(object sender, TouchEventArgs e)
        {
            this.img1.setZIndex(3);
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
            else if (index == 9)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img11.RenderTransformOrigin,
                    top = this.img11.getTop(),
                    left = this.img11.getLeft(),
                    transform = this.img11.RenderTransform,
                    height = this.img11.Height,
                    width = this.img11.Width
                };
            }
            else if (index == 5)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img7.RenderTransformOrigin,
                    top = this.img7.getTop(),
                    left = this.img7.getLeft(),
                    transform = this.img7.RenderTransform,
                    height = this.img7.Height,
                    width = this.img7.Width
                };
            }
            else if (index == 6)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img8.RenderTransformOrigin,
                    top = this.img8.getTop(),
                    left = this.img8.getLeft(),
                    transform = this.img8.RenderTransform,
                    height = this.img8.Height,
                    width = this.img8.Width
                };
            }
            else if (index ==7)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img9.RenderTransformOrigin,
                    top = this.img9.getTop(),
                    left = this.img9.getLeft(),
                    transform = this.img9.RenderTransform,
                    height = this.img9.Height,
                    width = this.img9.Width
                };
            }
            else if (index == 8)
            {
                return new UIElementInformation()
                {
                    RenderTransformOrigin = this.img10.RenderTransformOrigin,
                    top = this.img10.getTop(),
                    left = this.img10.getLeft(),
                    transform = this.img10.RenderTransform,
                    height = this.img10.Height,
                    width = this.img10.Width
                };
            }
            else if (index == 10)
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

        public void setTransform(UIElementInformation U, int index = 0, double r=1)
        {
            if (index == 0)
            {
                this.img1.setTop(U.top * r);
                this.img1.setLeft(U.left * r);
                this.img1.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img1.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 1)
            {
                this.img2.setTop(U.top * r);
                this.img2.setLeft(U.left * r);
                this.img2.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img2.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 2)
            {
                this.img3.setTop(U.top * r);
                this.img3.setLeft(U.left * r);
                this.img3.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img3.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 3)
            {
                this.img4.setTop(U.top * r);
                this.img4.setLeft(U.left * r);
                this.img4.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img4.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 4)
            {
                this.img5.setTop(U.top * r);
                this.img5.setLeft(U.left * r);
                this.img5.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img5.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 10)
            {
                this.img6.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img6.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 5)
            {

                this.img6.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img6.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 6)
            {

                this.img7.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img7.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 7)
            {

                this.img8.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img8.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 8)
            {

                this.img9.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img9.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
            else if (index == 9)
            {

                this.img10.RenderTransformOrigin = U.RenderTransformOrigin;
                this.img10.RenderTransform = U.transform;
                this.UIRoot.RenderTransform = new ScaleTransform(this.Width / W, this.Height / H);
            }
        }

        private void img2_2_TouchDown(object sender, TouchEventArgs e)
        {
            this.img2.setZIndex(3);
        }

        private void img3_3_TouchDown(object sender, TouchEventArgs e)
        {
            this.img3.setZIndex(3);
        }

        private void img4_TouchDown(object sender, TouchEventArgs e)
        {
            this.img4.setZIndex(3);
        }

        private void img5_5_TouchDown(object sender, TouchEventArgs e)
        {
            this.img5.setZIndex(3);
        }

        private void img7_TouchDown(object sender, TouchEventArgs e)
        {
            this.img7.setZIndex(3);
        }

        private void img8_8_TouchDown(object sender, TouchEventArgs e)
        {
            this.img8.setZIndex(3);
        }

        private void img9_9_TouchDown(object sender, TouchEventArgs e)
        {
            this.img9.setZIndex(3);
        }

        private void img10_10_TouchDown(object sender, TouchEventArgs e)
        {
            this.img10.setZIndex(3);
        }

        private void img11_11_TouchDown(object sender, TouchEventArgs e)
        {
            this.img11.setZIndex(3);
        } 
    }
}
