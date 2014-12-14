using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TOYOTA_2;

namespace toyota.Class
{
    public class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetImageData(byte[] data)
        {
#if DEBUG
            if (data == null || data.Length == 0)
                Console.WriteLine("null");
            else Console.WriteLine(data.Length);
#endif
            var source = new BitmapImage();
       
            source.BeginInit();
            if (App.config.DecodePixelWidth!=-1)
                source.DecodePixelWidth = App.config.DecodePixelWidth;
            source.CacheOption = BitmapCacheOption.OnLoad;
            source.StreamSource = new MemoryStream(data);
            source.EndInit();
            // use public setter
            ImageSource = source;
        }


        ImageSource imageSource;
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
