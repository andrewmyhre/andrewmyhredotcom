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
    public partial class arrow : UserControl
    {
        bool _pointLeft;
        public arrow(bool PointLeft)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(leftarrow_Loaded);
            _pointLeft = PointLeft;
        }

        void leftarrow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_pointLeft)
                rotation.Angle = 180;


        }
    }
}
