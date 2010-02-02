using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Silverlight.PreLoading
{
    public class VideoLoader : IPreLoadable
    {
        readonly string _url;
        readonly Panel _container;
        MediaElement _mediaElement;
        public MediaElement MediaElement { get { return _mediaElement; } }
        public event EventHandler<PreLoadingProgressEventArgs> OnPreLoadingProgress;
        public event EventHandler<PreLoadingItemCompleteEventArgs> Loaded;
        public event EventHandler Failed;
        private bool _loaded = false;
        
        public VideoLoader(string url, Panel container)
        {
            _url = url;
            _container = container;
        }

        public void Load()
        {
            _mediaElement = new MediaElement();
            _mediaElement.Source = new Uri(_url);
            _mediaElement.Volume = 0;
            _mediaElement.Visibility = Visibility.Collapsed;
            _mediaElement.AutoPlay = false;
            _mediaElement.MediaOpened += new RoutedEventHandler(MediaElementMediaOpened);
            _mediaElement.DownloadProgressChanged += new RoutedEventHandler(MediaElementDownloadProgressChanged);
            _mediaElement.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(MediaElementMediaFailed);
            _container.Children.Add(_mediaElement);
        }

        void MediaElementMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (Failed != null)
                Failed(this, new EventArgs());
        }

        void MediaElementDownloadProgressChanged(object sender, RoutedEventArgs e)
        {
            if (_loaded) return;

            Debug.WriteLine(string.Format("Downloading {0} - {1}", System.IO.Path.GetFileName(_url), _mediaElement.DownloadProgress));

            if (_mediaElement.DownloadProgress == 1 && Loaded != null)
            {
                Loaded(this, new PreLoadingItemCompleteEventArgs() {Item = this});
                _loaded = true;
            }
            else if (_mediaElement.DownloadProgress < 1 && OnPreLoadingProgress != null)
                OnPreLoadingProgress(this, new PreLoadingProgressEventArgs() { Progress = _mediaElement.DownloadProgress, Message = "Loading video..." });
        }


        void MediaElementMediaOpened(object sender, RoutedEventArgs e)
        {
            //if (Loaded != null)
                //Loaded(this, new PreLoadingItemCompleteEventArgs() { Item = this} );
        }
    }
}


