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
    public partial class idea4 : UserControl
    {
        public idea4()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
                {
                    video.Source = new Uri(App.ContentVideos[0]);
                    video.Play();
                };
        }
    }
}
