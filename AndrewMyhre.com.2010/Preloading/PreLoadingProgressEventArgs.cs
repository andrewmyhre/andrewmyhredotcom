namespace Silverlight.PreLoading
{
    using System;
    using System.Runtime.CompilerServices;

    public class PreLoadingProgressEventArgs : EventArgs
    {
        public IPreLoadable Item { get; set; }

        public string Message { get; set; }

        public double Progress { get; set; }
    }
}

