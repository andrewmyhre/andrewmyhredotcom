using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace AndrewMyhre.com.Web.Controllers
{
    public class VideosController : Controller
    {
        //
        // GET: /Viewos/

        public ActionResult Get(string id)
        {
            return
                new FileContentResult(
                    System.IO.File.ReadAllBytes(Path.Combine(ConfigurationManager.AppSettings["ContentVideosPath"], id)),
                    "video/mp4");
        }

        public ActionResult Content()
        {
            var files = new DirectoryInfo(ConfigurationManager.AppSettings["ContentVideosPath"])
                .GetFiles("v*.mp4")
                .OrderByDescending(f=>f.CreationTime);

            var xml = new XDocument(
                new XElement("videos",
                    files.Select(f => new XElement("video", "/videos/get/" + f.Name)).ToArray()));

            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                xml.WriteTo(writer);
                writer.Flush();
            }

            return new ContentResult()
                       {
                           Content=output.ToString(),
                           ContentEncoding = Encoding.UTF8,
                           ContentType="text/xml"
                       };

        }

        public ActionResult Flicker()
        {
            var files = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/videos")).GetFiles("flicker_*.mp4");

            var xml = new XDocument(
                new XElement("videos",
                    files.Select(f => new XElement("video", "/videos/" + f.Name)).ToArray()));

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
}
