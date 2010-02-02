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
using System.Windows.Media.Effects;

namespace AndrewMyhre.com._2010
{
    public partial class Ident : UserControl
    {
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Ident), null);
        public static DependencyProperty FadeTimeProperty = DependencyProperty.Register("FadeTime", typeof(Duration), typeof(Ident), null);
        public static DependencyProperty NavigateUriProperty = DependencyProperty.Register("NavigateUri", typeof(Uri), typeof(Ident), null);
        public static DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(string), typeof(Ident), null);

        public string Text 
        {
            get { return GetValue(TextProperty) as string; }
            set { SetValue(TextProperty, value); }
        }
        public Duration FadeTime { get { return (Duration)GetValue(FadeTimeProperty); } set { SetValue(FadeTimeProperty, value); } }
        public Uri NavigateUri { get { return GetValue(NavigateUriProperty) as Uri; } set { SetValue(NavigateUriProperty, value); } }
        public string Target { get { return GetValue(TargetProperty) as string; } set { SetValue(TargetProperty, value); } }

        public event EventHandler Click;

        public Ident()
        {
            this.DataContext = this;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Ident_Loaded);
        }

        void Ident_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Begin()
        {
            blurFadeIn.Duration = FadeTime;
            opacityFadeIn.Duration = FadeTime;
            fadeIn.Begin();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
    }
}
