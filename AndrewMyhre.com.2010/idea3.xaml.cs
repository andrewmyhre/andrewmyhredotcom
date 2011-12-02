using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AndrewMyhre.com._2010
{
    public partial class idea3 : UserControl
    {
        private readonly IAndrewMyhreService _service;
        public idea3(IAndrewMyhreService service)
        {
            _service = service;

            InitializeComponent();
            this.Loaded += new RoutedEventHandler(idea3_Loaded);
        }

        void idea3_Loaded(object sender, RoutedEventArgs e)
        {
            menu.ItemsSource = _service.GetMainMenu();
            var portfolioData = _service.GetPortfolio();
            portfolio.ItemsSource = portfolioData;
        }

        private void menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = menu.SelectedItem as MenuViewModel;
            if (item == null) return;
            if (item.Text=="Portfolio")
            {
                portfolio.Visibility = Visibility.Visible;
                portfolio.SelectedIndex = -1;
            } else
            {
                portfolio.Visibility = Visibility.Collapsed;
                portfolioDetail.Visibility = Visibility.Collapsed;
            }
        }

        private void portfolio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = portfolio.SelectedItem as PortfolioViewModel;
            if (item == null) return;

            portfolioDetail.DataContext = item;
            portfolioDetail.Visibility = Visibility.Visible;
            
        }
    }
}
