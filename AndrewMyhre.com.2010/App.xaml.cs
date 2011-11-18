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
using System.Windows.Media.Imaging;
using Silverlight.PreLoading;
using System.IO;
using System.Xml;

namespace AndrewMyhre.com._2010
{
    public partial class App : Application
    {
        public static string[] FlickerVideos = null;
        public static string[] ContentVideos = null;
        public static string BaseUrl = "";
        private bool _testOnAMDotCom=false;

        readonly PreLoadingQueue queue = new PreLoadingQueue();

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            BaseUrl = string.Format("http://{0}:{1}", Application.Current.Host.Source.Host, Application.Current.Host.Source.Port);
            if (_testOnAMDotCom)
                BaseUrl = "http://andrewmyhre.com";

            InitializeComponent();
        }

        void queue_ItemLoaded(object sender, LoadedEventArgs e)
        {
            if (e.Key == "flickerVideos") FlickerVideos = ProcessVideoItemsFromXml(e.Loaded.Stream);
            else if (e.Key == "mainVideos") ContentVideos = ProcessVideoItemsFromXml(e.Loaded.Stream);

            if (queue.Count > 0)
                queue.LoadNext();
            else
                this.RootVisual = new MainPage();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            queue.Add("flickerVideos", new PreLoader(BaseUrl + "/xml/flicker.xml", "xml"));
            queue.Add("mainVideos", new PreLoader(BaseUrl + "/xml/content.xml", "xml"));
            queue.ItemLoaded += new EventHandler<LoadedEventArgs>(queue_ItemLoaded);
            queue.LoadNext();
        }

        private string[] ProcessVideoItemsFromXml(Stream xmlStream)
        {
            StreamReader sr = new StreamReader(xmlStream);
            XmlReader reader = XmlReader.Create(sr);

            List<string> items = new List<string>();

            while (!reader.EOF)
            {
                if (reader.Name == "video" && reader.NodeType == XmlNodeType.Element)
                    items.Add(reader.ReadElementContentAsString());
                else
                    reader.Read();
            }
            return items.ToArray();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
