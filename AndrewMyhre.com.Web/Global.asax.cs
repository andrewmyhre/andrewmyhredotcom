using System.Timers;
using System.Web.Mvc;
using System.Web.Routing;

namespace AndrewMyhre.com.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static VideoAsset[] MediaUrls = new VideoAsset[0];
        private static Timer AzureCheckTimer;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AzureCheckTimer = new Timer();
            AzureCheckTimer.Interval = 12 * 60 * 60 * 1000; // every 12 hours
            AzureCheckTimer.Elapsed += (sender, args) => GenerateAzureAssetUrls();
            AzureCheckTimer.Start();

            GenerateAzureAssetUrls();
        }

        public static void GenerateAzureAssetUrls()
        {
            var mediaUrls = new AzureAssetUrlGenerator().Generate();
            MediaUrls = mediaUrls;
        }
    }
}