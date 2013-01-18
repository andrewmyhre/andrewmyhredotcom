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

namespace AndrewMyhre.com._2010
{
    public partial class tv_new : UserControl
    {
        public tv_new()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
                {
                    System.Diagnostics.Debug.WriteLine("playing {0}", App.ContentVideos[0]);
                    content.Source = new Uri(App.ContentVideos[0]);
                    content.Play();
                };
        }
    }
}
