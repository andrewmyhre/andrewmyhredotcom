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
using AndrewMyhre.com._2010.ViewModels;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace AndrewMyhre.com._2010
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Host.Content.Resized += new EventHandler(Content_Resized);
            this.Width = Application.Current.Host.Content.ActualWidth;
            this.Height = Application.Current.Host.Content.ActualHeight;


            idea_presenter.Children.Add(new idea3(new AndrewMyhreService()));
        }

        void Content_Resized(object sender, EventArgs e)
        {
            this.Width = Application.Current.Host.Content.ActualWidth;
            this.Height = Application.Current.Host.Content.ActualHeight;
        }
    }
}
