using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AndrewMyhre.com._2010.ViewModels;
using System.Windows.Media.Imaging;

namespace AndrewMyhre.com._2010.portfolio
{
    public partial class portfolioitem : UserControl
    {
        private PortfolioViewModel _portfolioItem;

        public portfolioitem(PortfolioViewModel portfolioItem)
        {
            InitializeComponent();
            _portfolioItem = portfolioItem;
            this.Loaded += new RoutedEventHandler(portfolio_withimages_Loaded);
        }

        void portfolio_withimages_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _portfolioItem;

            if (string.IsNullOrEmpty(App.BaseUrl)) return;
            if (_portfolioItem.ImageFilenames != null && _portfolioItem.ImageFilenames.Length>0)
            {
                foreach (string imageFilename in _portfolioItem.ImageFilenames)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(imageFilename));
                    images.Children.Add(image);
                    image.Width = 400;
                    image.Margin = new Thickness(5);
                }
                imagesContainer.Visibility = System.Windows.Visibility.Visible;
            }
            else if (!string.IsNullOrEmpty(_portfolioItem.VideoFilename))
            {
                video.Source = new Uri(_portfolioItem.VideoFilename);
                mediaControls.Media = video;
                mediaControls.btnFullScreen.Visibility = System.Windows.Visibility.Collapsed;
                videoContainer.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void video_BufferingProgressChanged(object sender, RoutedEventArgs e)
        {
            loadingProgress.Text = string.Format("{0:#}%", video.BufferingProgress * 100);
        }


    }
}
