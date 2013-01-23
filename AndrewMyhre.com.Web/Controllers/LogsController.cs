using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndrewMyhre.com.Web.Controllers
{
    public class LogsController : Controller
    {
        //
        // GET: /Logs/

        public ActionResult Index()
        {
            return 
                new ContentResult()
                    {
                        Content = AzureAssetUrlGenerator.Log.ToString(),
                        ContentType="text/html"
                    };
        }

    }
}
