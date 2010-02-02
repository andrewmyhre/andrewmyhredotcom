using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using AndrewMyhre.com._2010.ViewModels;
using System.IO;
using System.Windows.Resources;

namespace AndrewMyhre.com._2010
{
    public class XmlPortfolioRepository : IPortfolioRepository
    {
        public System.Collections.Generic.IEnumerable<ViewModels.PortfolioViewModel> All()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PortfolioViewModel[]));
            PortfolioViewModel[] portfolio = null;
            try
            {
                StreamResourceInfo sri = Application.GetResourceStream(new Uri("portfolio.xml", UriKind.Relative));
                portfolio = serializer.Deserialize(sri.Stream) as PortfolioViewModel[];
            }
            catch
            {
            }

            return portfolio;

            
        }
    }
}
