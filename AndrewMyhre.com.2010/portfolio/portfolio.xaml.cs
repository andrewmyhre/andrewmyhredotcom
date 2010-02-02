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

namespace AndrewMyhre.com._2010.portfolio
{
    public partial class portfolio : UserControl
    {
        IPortfolioRepository portfolioRepository;
        double screenWidth = 0;
        int portfolioIndex = 0;
        Storyboard sbFadeIn = new Storyboard();
        public event EventHandler Close;

        public portfolio()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(portfolio_SizeChanged);
            this.Loaded += new RoutedEventHandler(portfolio_Loaded);
            portfolioRepository = new PortfolioRepositoryFake();
        }

        void portfolio_Loaded(object sender, RoutedEventArgs e)
        {
            screenWidth = (double)Parent.GetValue(ActualWidthProperty);
            InitialisePortfolios();
        }

        void InitialisePortfolios()
        {
            int i=0;

            IEnumerable<PortfolioViewModel> portfolios = portfolioRepository.All();
            if (portfolios == null)
            {
                this.Visibility = System.Windows.Visibility.Collapsed;
                return;
            }

            foreach (PortfolioViewModel p in portfolios)
            {
                portfolioitem pItem = new portfolioitem(p) { Opacity = 0, Visibility = System.Windows.Visibility.Collapsed };
                TransformGroup tg = new TransformGroup();
                TranslateTransform tt = new TranslateTransform() { X = i == 0 ? 0 : screenWidth };
                tg.Children.Add(tt);
                pItem.RenderTransform = tg;
                portfolio_items.Children.Add(pItem);
                i++;
            }

            CreateMoveAnimation(portfolio_items.Children[portfolioIndex], screenWidth, 0);
            CreateFadeAnimation(portfolio_items.Children[portfolioIndex], 0, 1);
        }

        void CreateMoveAnimation(UIElement target, double from, double to)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.EasingFunction = new CubicEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            anim.Duration = TimeSpan.FromMilliseconds(500);
            sb.Children.Add(anim);

            TransformGroup tg = target.RenderTransform as TransformGroup;
            Storyboard.SetTarget(anim, tg.Children[0] as TranslateTransform);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(TranslateTransform.X)"));
            sb.Begin();
        }

        void CreateFadeAnimation(UIElement target, double from, double to)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = TimeSpan.FromMilliseconds(1000);
            Storyboard.SetTarget(anim, target);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.Opacity)"));
            sb.Children.Add(anim);
            sb.Begin();

            if (to != 0)
                target.Visibility = System.Windows.Visibility.Visible;
        }

        void sb_FadeOutCompleted(object sender, EventArgs e)
        {
        }

        void portfolio_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSizes();
        }

        private void UpdateSizes()
        {
            screenWidth = (double)Parent.GetValue(ActualWidthProperty);
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (portfolioIndex == 0) return;

            CreateMoveAnimation(portfolio_items.Children[portfolioIndex], 0, screenWidth);
            CreateFadeAnimation(portfolio_items.Children[portfolioIndex], 1, 0);
            portfolioIndex--;
            CreateMoveAnimation(portfolio_items.Children[portfolioIndex], -screenWidth, 0);
            CreateFadeAnimation(portfolio_items.Children[portfolioIndex], 0, 1);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (portfolioIndex == portfolio_items.Children.Count-1) return;

            CreateMoveAnimation(portfolio_items.Children[portfolioIndex], 0, -screenWidth);
            CreateFadeAnimation(portfolio_items.Children[portfolioIndex], 1, 0);
            portfolioIndex++;
            CreateMoveAnimation(portfolio_items.Children[portfolioIndex], screenWidth, 0);
            CreateFadeAnimation(portfolio_items.Children[portfolioIndex], 0, 1);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (Close != null)
                Close(this, new EventArgs());
        }
    }
}
