namespace Silverlight.PreLoading
{
    using System;
    using System.Runtime.CompilerServices;

    public class LoadedEventArgs : EventArgs
    {
        public string Key { get; set; }

        public IPreLoadable Loaded { get; set; }
    }
}

