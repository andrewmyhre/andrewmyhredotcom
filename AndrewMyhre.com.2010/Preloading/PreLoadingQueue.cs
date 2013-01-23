namespace Silverlight.PreLoading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PreLoadingQueue
    {
        private readonly Dictionary<IPreLoadable, string> _completed = new Dictionary<IPreLoadable, string>();
        private readonly Dictionary<IPreLoadable, string> _downloading = new Dictionary<IPreLoadable, string>();
        private readonly Dictionary<IPreLoadable, string> _queue = new Dictionary<IPreLoadable, string>();
        public int ConnectionCount = 1;

        public event EventHandler ItemFailed;

        public event EventHandler<LoadedEventArgs> ItemLoaded;

        public event EventHandler<PreLoadingProgressEventArgs> OnItemProgress;

        public void Add(string key, IPreLoadable preLoadable)
        {
            this._queue.Add(preLoadable, key);
            preLoadable.OnPreLoaded += new EventHandler<PreLoadingItemCompleteEventArgs>(this.PreLoadingItemCompleted);
            preLoadable.OnPreLoadingProgress += new EventHandler<PreLoadingProgressEventArgs>(this.PreLoadableOnPreLoadingProgress);
            preLoadable.OnPreLoadFailed += new EventHandler(this.PreLoadableFailed);
        }

        public void LoadNext()
        {
            if (this.Count != 0)
            {
                IPreLoadable key = this._queue.Keys.First<IPreLoadable>();
                this._downloading.Add(key, this._queue[key]);
                this._queue.Remove(key);
                key.PreLoad();
            }
        }

        private void PreLoadableFailed(object sender, EventArgs e)
        {
            if (this.ItemFailed != null)
            {
                this.ItemFailed(this, new EventArgs());
            }
        }

        private void PreLoadableOnPreLoadingProgress(object sender, PreLoadingProgressEventArgs e)
        {
            if (this.OnItemProgress != null)
            {
                this.OnItemProgress(this, e);
            }
        }

        public void PreLoadAll()
        {
            this.AutoAdvance = true;
            while ((this._downloading.Count < this.ConnectionCount) && (this._queue.Count > 0))
            {
                this.LoadNext();
            }
        }

        private void PreLoadingItemCompleted(object sender, PreLoadingItemCompleteEventArgs eventArgs)
        {
            IPreLoadable item = eventArgs.Item;
            string str = this._downloading[item];
            this._completed.Add(item, str);
            this._downloading.Remove(item);
            if (this.ItemLoaded != null)
            {
                LoadedEventArgs e = new LoadedEventArgs {
                    Loaded = item,
                    Key = str
                };
                this.ItemLoaded(this, e);
            }
            if (this.AutoAdvance)
            {
                this.LoadNext();
            }
        }

        public bool AutoAdvance { get; set; }

        public IEnumerable<IPreLoadable> Completed
        {
            get
            {
                return this._completed.Keys.ToArray<IPreLoadable>();
            }
        }

        public int CompletedCount
        {
            get
            {
                return this._completed.Count;
            }
        }

        public int Count
        {
            get
            {
                return this._queue.Count;
            }
        }

        public IEnumerable<IPreLoadable> Downloading
        {
            get
            {
                return this._downloading.Keys.ToArray<IPreLoadable>();
            }
        }

        public int DownloadingCount
        {
            get
            {
                return this._downloading.Count;
            }
        }

        public IEnumerable<IPreLoadable> Queue
        {
            get
            {
                return this._queue.Keys.ToArray<IPreLoadable>();
            }
        }

        public double TotalProgress
        {
            get
            {
                double num = 1.0 / ((double) ((this._queue.Count + this._completed.Count) + this._downloading.Count));
                double num2 = Enumerable.Sum<KeyValuePair<IPreLoadable, string>>(this._downloading, (Func<KeyValuePair<IPreLoadable, string>, double>) (d => d.Key.Progress));
                return ((num * this._completed.Count) + (num * num2));
            }
        }
    }
}

