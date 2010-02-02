using System;

namespace Silverlight.PreLoading
{
    public interface IPreLoadable
    {
        void Load();
        event EventHandler<PreLoadingProgressEventArgs> OnPreLoadingProgress;
        event EventHandler<PreLoadingItemCompleteEventArgs> Loaded;
        event EventHandler Failed;
    }
}