using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace AndrewMyhre.com.Web.Controllers
{
    public class AzureController : Controller
    {
        //
        // GET: /Azure/

        public ActionResult Index()
        {
            var viewModel = new AzureIndexViewModel();
            viewModel.Log = AzureAssetUrlGenerator.Log.ToString();
            return View(viewModel);
        }

        public ActionResult Generate()
        {
            MvcApplication.GenerateAzureAssetUrls();
            return RedirectToAction("Index");
        }

        public ActionResult Wallpapers()
        {
            var xml = new XDocument(
                new XElement("videos",
                    MvcApplication.MediaUrls
                    .Where(m=>m.Type=="wallpaper")
                    .Select(url => new XElement("video", url.ContentUrl)).ToArray()));

            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                xml.WriteTo(writer);
                writer.Flush();
            }

            return new ContentResult()
            {
                Content = output.ToString(),
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/xml"
            };
        }

        public ActionResult Flickers()
        {
            var xml = new XDocument(
                new XElement("videos",
                    MvcApplication.MediaUrls
                    .Where(m => m.Type == "flicker")
                    .Select(url => new XElement("video", url.ContentUrl)).ToArray()));

            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                xml.WriteTo(writer);
                writer.Flush();
            }

            return new ContentResult()
            {
                Content = output.ToString(),
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/xml"
            };
        }

    }

    public class AzureIndexViewModel
    {
        public string Log { get; set; }
    }
}
