using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Silverlight.PreLoading
{
    public class ImageLoader : IPreLoadable
    {
        readonly Panel _container;
        readonly string _url;
        BitmapImage _bitmapImage;
        public BitmapImage Image { get { return _bitmapImage; } }
        public event EventHandler<PreLoadingProgressEventArgs> OnPreLoadingProgress;
        public event EventHandler<PreLoadingItemCompleteEventArgs> Loaded;
        public event EventHandler Failed;

        public ImageLoader(string url, Panel container)
        {
            _url = url;
            _container = container;
        }

        public void Load()
        {
            _bitmapImage = new BitmapImage(new Uri(_url));
            _bitmapImage.ImageOpened += new EventHandler<RoutedEventArgs>(BitmapImageOpened);
            _bitmapImage.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(BitmapImageFailed);
            _bitmapImage.DownloadProgress += new EventHandler<DownloadProgressEventArgs>(BitmapDownloadProgress);
            Image image = new Image();
            image.Visibility = Visibility.Collapsed;
            image.Source = _bitmapImage;
            _container.Children.Add(image);
        }

        void BitmapDownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            if (OnPreLoadingProgress != null)
                OnPreLoadingProgress(this, new PreLoadingProgressEventArgs() { Progress = e.Progress, Message="Loading images..."});
        }


        void BitmapImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (Failed != null)
                Failed(this, new EventArgs());
        }

        void BitmapImageOpened(object sender, RoutedEventArgs e)
        {
            if (Loaded != null)
                Loaded(this, new PreLoadingItemCompleteEventArgs() { Item = this });
        }

    }
}


