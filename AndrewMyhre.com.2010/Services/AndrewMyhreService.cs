using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using AndrewMyhre.com._2010.ViewModels;

namespace AndrewMyhre.com._2010.Services
{
    public class AndrewMyhreService : IAndrewMyhreService
    {
        public ICollection<MenuViewModel> GetMainMenu()
        {
            return new ObservableCollection<MenuViewModel>()
                       {
                           new MenuViewModel(){Text="Portfolio"},
                           new MenuViewModel(){Text="Blog"},
                           new MenuViewModel(){Text="LinkedIn"},
                           new MenuViewModel(){Text="Twitter"},
                           new MenuViewModel(){Text="CV"}
                       };
        }

        public ICollection<PortfolioViewModel> GetPortfolio()
        {
            XDocument doc = XDocument.Load("Portfolio.xml");

            return (from e in doc.Element("ArrayOfPortfolioViewModel").Elements("PortfolioViewModel")
                    select new PortfolioViewModel()
                               {
                                   Client = e.Element("Client").Value,
                                   Description = e.Element("Description").Value,
                                   Project = e.Element("Project").Value,
                                   VideoFilename = e.Element("VideoFilename") != null ? e.Element("VideoFilename").Value : "",
                                   ImageFilenames = e.Element("ImageFilenames") != null && e.Element("ImageFilenames").HasElements
                                   ? (from image in e.Element("ImageFilenames").Elements("string") select image.Value).ToArray()
                                   : new string[0]
                               }).ToList();
        }
    }
}