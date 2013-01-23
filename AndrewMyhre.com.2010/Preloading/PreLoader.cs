namespace Silverlight.PreLoading
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PreLoader : IPreLoadable
    {
        private WebClient _client;
        private readonly string _uri;
        public string[] Values;

        public event EventHandler<PreLoadingItemCompleteEventArgs> OnPreLoaded;

        public event EventHandler OnPreLoadFailed;

        public event EventHandler<PreLoadingProgressEventArgs> OnPreLoadingProgress;

        public PreLoader(string uriString, string category)
        {
            this._uri = uriString;
            this.Category = category;
        }

        private void ClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Progress = ((double) e.ProgressPercentage) / 100.0;
            if (this.OnPreLoadingProgress != null)
            {
                PreLoadingProgressEventArgs args = new PreLoadingProgressEventArgs {
                    Item = this,
                    Message = "Loading...",
                    Progress = ((double) e.ProgressPercentage) / 100.0
                };
                this.OnPreLoadingProgress(this, args);
            }
        }

        private void ClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var ex = e.Error;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                while(ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }


                if (e.Error.InnerException != null)
                    throw e.Error.InnerException;

                throw e.Error;
            }
            this.Stream = e.Result;
            if (this.OnPreLoaded != null)
            {
                PreLoadingItemCompleteEventArgs args = new PreLoadingItemCompleteEventArgs {
                    Item = this
                };
                this.OnPreLoaded(this, args);
            }
        }

        public void PreLoad()
        {
            this._client = new WebClient();
            this._client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.ClientDownloadProgressChanged);
            this._client.OpenReadCompleted += new OpenReadCompletedEventHandler(this.ClientOpenReadCompleted);
            System.Diagnostics.Debug.WriteLine("media: " + this._uri);
            this._client.OpenReadAsync(new Uri(this._uri));
        }

        public string Category { get; private set; }

        public double Progress { get; private set; }

        public System.IO.Stream Stream { get; private set; }
    }
}

