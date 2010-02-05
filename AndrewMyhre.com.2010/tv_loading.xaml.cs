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
    public partial class tv_loading : UserControl
    {
        Storyboard sbFadeIn = new Storyboard();
        public Storyboard FadeInStoryboard { get { return sbFadeIn; } }
        DoubleAnimation fadeInBlur;

        public tv_loading()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(tv_loading_Loaded);
            InitialiseStoryboard();
        }

        private void InitialiseStoryboard()
        {
            blurEffect.Radius = 100;

            fadeInBlur = new DoubleAnimation();
            fadeInBlur.From = 1000;
            fadeInBlur.To = 1000;
            fadeInBlur.Duration = TimeSpan.FromMilliseconds(100);
            Storyboard.SetTarget(fadeInBlur, blurEffect);
            Storyboard.SetTargetProperty(fadeInBlur, new PropertyPath("BlurEffect.Radius"));

            sbFadeIn.Children.Add(fadeInBlur);
        }

        public void SetProgress(double progress)
        {
            fadeInBlur.From = blurEffect.Radius;
            fadeInBlur.To = ((1 - progress) * 30) + 2;
            fadeInBlur.Duration = TimeSpan.FromMilliseconds(10);
            this.loadingText.Opacity = progress;
            sbFadeIn.Begin();
        }

        void tv_loading_Loaded(object sender, RoutedEventArgs e)
        {
            //Ident loading = new Ident("loading...", 80, 30);
        }
    }
}
