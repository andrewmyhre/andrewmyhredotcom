using System;
using System.Collections.Generic;
using System.Linq;

namespace Silverlight.PreLoading
{
    public class PreLoadingQueue
    {
        readonly Dictionary<IPreLoadable, string> _queue = new Dictionary<IPreLoadable, string>();
        public event EventHandler<LoadedEventArgs> ItemLoaded;
        public event EventHandler<PreLoadingProgressEventArgs> OnItemProgress;
        public event EventHandler ItemFailed;

        public void Add(string key, IPreLoadable preLoadable)
        {
            _queue.Add(preLoadable, key);
            preLoadable.Loaded += PreLoadingItemCompleted;
            preLoadable.OnPreLoadingProgress += PreLoadableOnPreLoadingProgress;
            preLoadable.Failed += PreLoadableFailed;
        }

        void PreLoadableFailed(object sender, EventArgs e)
        {
            if (ItemFailed != null)
                ItemFailed(this, new EventArgs());
        }

        void PreLoadableOnPreLoadingProgress(object sender, PreLoadingProgressEventArgs e)
        {
            if (OnItemProgress != null)
                OnItemProgress(this, e);
        }

        public void LoadNext()
        {
            if (Count == 0) return;

            var toLoad = _queue.Keys.First();
            toLoad.Load();
        }

        public int Count
        {
            get { return _queue.Count; }
        }

        public IEnumerable<IPreLoadable> Items
        {
            get { return _queue.Keys.ToArray(); }
        }

        void PreLoadingItemCompleted(object sender, PreLoadingItemCompleteEventArgs eventArgs)
        {
            IPreLoadable item = eventArgs.Item;
            string key = _queue[item];
            _queue.Remove(item);

            if (ItemLoaded != null)
                ItemLoaded(this, new LoadedEventArgs() { Loaded = item, Key = key});
        }
    }

    public class LoadedEventArgs : EventArgs
    {
        public string Key { get; set; }
        public IPreLoadable Loaded { get; set; }
    }
}


