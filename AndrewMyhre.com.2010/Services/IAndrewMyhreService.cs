using System.Collections.Generic;
using AndrewMyhre.com._2010.ViewModels;

namespace AndrewMyhre.com._2010.Services
{
    public interface IAndrewMyhreService
    {
        ICollection<MenuViewModel> GetMainMenu();
        ICollection<PortfolioViewModel> GetPortfolio();
    }
}