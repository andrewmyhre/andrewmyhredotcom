namespace Silverlight.PreLoading
{
    using System;
    using System.Runtime.CompilerServices;

    public class PreLoadingItemCompleteEventArgs : EventArgs
    {
        public IPreLoadable Item { get; set; }
    }
}

