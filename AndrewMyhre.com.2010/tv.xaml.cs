using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Silverlight.PreLoading;
using System.Diagnostics;

namespace AndrewMyhre.com._2010
{
    public partial class tv : UserControl
    {
        private readonly List<MediaElement> _flickers = new List<MediaElement>();
        private readonly Random _rand = new Random();
        private int _nextContentVideoIndex;
        private int _queueSize;
        private DispatcherTimer _timer;
        private int _videoIndex = -1;
        private int _loadedItems=0;
        private PreLoadingQueue queue; 

        public tv()
        {
            InitializeComponent();
            if (App.ContentVideos == null) return;
            Loaded += TvLoaded;

            _nextContentVideoIndex = App.ContentVideos.Length - 1;

            content.MediaFailed += MediaElement_MediaEnded;
        }

        public event EventHandler OnBackButtonPressed;

        public void InitialiseQueue(PreLoadingQueue queue)
        {
            this.queue = queue;
            queue.ItemLoaded += QueueItemLoaded;
            queue.ItemFailed += new EventHandler(queue_ItemFailed);
            queue.OnItemProgress += new EventHandler<PreLoadingProgressEventArgs>(queue_OnItemProgress);
            int index = 0;
            foreach (string videoFilename in App.FlickerVideos)
            {
                _flickers.Add(null);
                queue.Add(index++.ToString(), new PreLoader(videoFilename, "flicker video"));
            }
            _queueSize = queue.Count;
            loading.Visibility = Visibility.Visible;
        }

        void queue_OnItemProgress(object sender, PreLoadingProgressEventArgs e)
        {
            Debug.WriteLine(string.Format("loading {0:#}%", queue.TotalProgress * 100));
            this.loading.loadingText.Text = string.Format("loading {0:#}%", queue.TotalProgress*100);
            Debug.WriteLine(string.Format("blur={0}", (1 - queue.TotalProgress) * 30));
            loading.SetProgress(queue.TotalProgress);
        }

        void queue_ItemFailed(object sender, EventArgs e)
        {
            
        }

        public void Start()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += TimerTick;
            _timer.Start();
            NextFlickerVideo();
            loading.Visibility = Visibility.Collapsed;
        }

        private void QueueItemLoaded(object sender, LoadedEventArgs e)
        {
            if (e.Loaded.Category != "flicker video") return;
            MediaElement flickerMediaElement = new MediaElement();
            
            this.flickerContainer.Children.Add(flickerMediaElement);
            int index = int.Parse(e.Key);
            _flickers[index] = flickerMediaElement;
            flickerMediaElement.SetSource(e.Loaded.Stream);
            InitialiseFlickerVideo(flickerMediaElement);
            flickerMediaElement.MediaEnded += FlickerMediaEnded;
            _loadedItems++;
        }

        private static void InitialiseFlickerVideo(MediaElement flicker)
        {
            flicker.AutoPlay = false;
            flicker.Volume = 0.7;
            flicker.Opacity = 0.6;
            flicker.HorizontalAlignment = HorizontalAlignment.Stretch;
            flicker.VerticalAlignment = VerticalAlignment.Stretch;
            flicker.Stretch = Stretch.Fill;
            flicker.Visibility = Visibility.Collapsed;
            flicker.Position = TimeSpan.Zero;
            flicker.Pause();
        }

        private static void DeactivateFlickerVideo(MediaElement flicker)
        {
            flicker.Pause();
            flicker.Visibility = Visibility.Collapsed;
            flicker.Position = TimeSpan.Zero;
        }

        private static void ActivateFlickerVideo(MediaElement flicker)
        {
            flicker.Visibility = Visibility.Visible;
            flicker.Play();
        }

        private static void TvLoaded(object sender, RoutedEventArgs e)
        {
        }

        /*
        public void ShowPortfolio()
        {
            content.Stop();
            content.Visibility = Visibility.Collapsed;
            backButton.Visibility = Visibility.Visible;

            portfolioContainer.Children.Add(new portfolio.portfolio());
        }

        public void HidePortfolio()
        {
            portfolioContainer.Children.Clear();
            content.Play();
            content.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Collapsed;
        }*/

        private void NextFlickerVideo()
        {
            MediaElement currentFlicker = null;

            if (_videoIndex == -1)
            {
                // first play, choose a specific video to start
                _videoIndex = Array.IndexOf(App.FlickerVideos, App.FlickerVideos.Single(fv => fv.Contains("flicker_4.mp4")));
            }
            else
            {

                if (_videoIndex > -1) currentFlicker = _flickers[_videoIndex];
                _videoIndex++;
                if (_videoIndex > _flickers.Count - 1)
                    _videoIndex = 1; // only show the first video once
            }

            MediaElement nextFlicker = _flickers[_videoIndex];

            ActivateFlickerVideo(nextFlicker);
            if (currentFlicker != null) DeactivateFlickerVideo(currentFlicker);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            content.Visibility = Visibility.Visible;
            _timer.Stop();
            string sourceUrl = App.ContentVideos[_nextContentVideoIndex];
            System.Diagnostics.Debug.WriteLine("playing {0}", sourceUrl);
            if (sourceUrl.StartsWith("/"))
                sourceUrl = App.BaseUrl + sourceUrl;
            content.Source =
                new Uri(sourceUrl, UriKind.Absolute);
            //_nextContentVideoIndex = _rand.Next(App.ContentVideos.Length - 1);
            _nextContentVideoIndex++;
            if (_nextContentVideoIndex == App.ContentVideos.Length)
                _nextContentVideoIndex = 0;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            content.Visibility = Visibility.Collapsed;
            ((MediaElement) sender).Pause();
            _timer.Start();
        }

        private void FlickerMediaEnded(object sender, RoutedEventArgs e)
        {
            NextFlickerVideo();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (OnBackButtonPressed != null)
                OnBackButtonPressed(this, new EventArgs());
        }
    }
}