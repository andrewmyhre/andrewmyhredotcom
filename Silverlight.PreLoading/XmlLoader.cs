using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Silverlight.PreLoading
{
    public class XmlLoader : IPreLoadable
    {
        public event EventHandler<PreLoadingProgressEventArgs> OnPreLoadingProgress;
        public string[] Values;
        public event EventHandler<PreLoadingItemCompleteEventArgs> Loaded;
        public event EventHandler Failed;
        readonly string _url;

        public XmlLoader(string url)
        {
            _url = url;
        }

        public void Load()
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ClientDownloadStringCompleted);
            client.DownloadStringAsync(new Uri(_url));
        }

        void ClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (Failed != null)
                    Failed(this, new EventArgs());
                return;
            }

            StringReader sr = new StringReader(e.Result);
            XmlReader reader = XmlReader.Create(sr);

            List<string> items = new List<string>();

            while (!reader.EOF)
            {
                if (reader.Name == "video" && reader.NodeType == XmlNodeType.Element)
                    items.Add(reader.ReadElementContentAsString());
                else
                    reader.Read();
            }
            Values=items.ToArray();

            if (Loaded != null)
                Loaded(this, new PreLoadingItemCompleteEventArgs() { Item = this });
        }
    }
}


