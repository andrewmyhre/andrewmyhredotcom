namespace Silverlight.PreLoading
{
    using System;
    using System.IO;

    public interface IPreLoadable
    {
        event EventHandler<PreLoadingItemCompleteEventArgs> OnPreLoaded;

        event EventHandler OnPreLoadFailed;

        event EventHandler<PreLoadingProgressEventArgs> OnPreLoadingProgress;

        void PreLoad();

        string Category { get; }

        double Progress { get; }

        System.IO.Stream Stream { get; }
    }
}

