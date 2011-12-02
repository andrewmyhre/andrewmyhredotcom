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
using AndrewMyhre.com._2010.Services;
using AndrewMyhre.com._2010.portfolio;
using Silverlight.PreLoading;

namespace AndrewMyhre.com._2010
{
    public partial class idea2 : UserControl
    {
        private readonly IAndrewMyhreService _service;
        PreLoadingQueue queue = new PreLoadingQueue();
        portfolio.portfolio portfolio=null;

        public idea2(IAndrewMyhreService service)
        {
            _service = service;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(idea2_Loaded);
        }

        void idea2_Loaded(object sender, RoutedEventArgs e)
        {
            tv.InitialiseQueue(queue);
            queue.ItemLoaded += new EventHandler<LoadedEventArgs>(queue_ItemLoaded);
            queue.ConnectionCount = 3;
            queue.PreLoadAll();
        }

        void queue_ItemLoaded(object sender, LoadedEventArgs e)
        {
            if (queue.Count == 0 && queue.DownloadingCount == 0)
            {
                //CreateTextElements();
                andrewmyhredotcom.Visibility = System.Windows.Visibility.Visible;
                amFadeIn.Begin();
                foreach (Ident ident in gridLinks.Children)
                {
                    ident.Visibility = System.Windows.Visibility.Visible;
                    ident.Begin();
                }
                tv.Start();
            }
        }

        void identPortfolio_Click(object sender, EventArgs eventArgs)
        {
            mainContent.Children.Clear();
            portfolio = new _2010.portfolio.portfolio(_service);
            mainContent.Children.Add(portfolio);
            portfolio.Close += new EventHandler(portfolio_Close);
            mainContent.Visibility = System.Windows.Visibility.Visible;
        }

        void portfolio_Close(object sender, EventArgs e)
        {
            mainContent.Children.Remove(portfolio);
            mainContent.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void linkClosePortfolio_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
