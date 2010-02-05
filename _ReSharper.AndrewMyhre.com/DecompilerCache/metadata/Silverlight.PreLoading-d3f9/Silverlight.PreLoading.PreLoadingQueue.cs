// Type: Silverlight.PreLoading.PreLoadingQueue
// Assembly: Silverlight.PreLoading, Version=1.0.0.0, Culture=neutral
// Assembly location: D:\Workspace\git\am.com\AndrewMyhre.com.2010\lib\Silverlight.PreLoading.dll

using System;
using System.Collections.Generic;

namespace Silverlight.PreLoading
{
    public class PreLoadingQueue
    {
        public int ConnectionCount;
        public bool AutoAdvance { get; set; }
        public int Count { get; }
        public int CompletedCount { get; }
        public int DownloadingCount { get; }
        public double TotalProgress { get; }
        public IEnumerable<IPreLoadable> Queue { get; }
        public IEnumerable<IPreLoadable> Downloading { get; }
        public IEnumerable<IPreLoadable> Completed { get; }
        public void Add(string key, IPreLoadable preLoadable);
        public void PreLoadAll();
        public void LoadNext();
        public event EventHandler<LoadedEventArgs> ItemLoaded;
        public event EventHandler<PreLoadingProgressEventArgs> OnItemProgress;
        public event EventHandler ItemFailed;
    }
}