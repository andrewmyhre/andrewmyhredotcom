using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace GetMyVids
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("content.xml");
            var videos = from v in doc.Element("videos").Elements("video")
                         select v.Value;

            WebClient client = new WebClient();
            string folder = Path.Combine(Environment.CurrentDirectory, "downloaded");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            foreach(var video in videos)
            {
                string videoUrl = string.Format("http://www.andrewmyhre.com/customassets/andrewmyhre/file/{0}", video);
                Console.Write("downloading " + videoUrl + "...");
                try
                {
                    client.DownloadFile(videoUrl, Path.Combine(folder, video));
                } catch
                {
                    Console.WriteLine();
                    throw;
                }
                Console.WriteLine("ok");
            }
        }
    }
}
