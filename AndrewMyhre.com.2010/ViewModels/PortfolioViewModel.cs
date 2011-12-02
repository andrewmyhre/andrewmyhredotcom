namespace AndrewMyhre.com._2010.ViewModels
{
    public class PortfolioViewModel
    {
        public string Project { get; set; }
        public string Client { get; set; }
        public string ClientText { get { return string.Format("Client: {0}", Client); } }
        public string Description { get; set; }
        public string[] ImageFilenames { get; set; }
        public string VideoFilename { get; set; }
    }
}
