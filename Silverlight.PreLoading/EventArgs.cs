using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Silverlight.PreLoading
{
    public class PreLoadingProgressEventArgs : EventArgs
    {
        public double Progress { get; set; }
        public string Message { get; set; }
        public IPreLoadable Item { get; set; }
    }

    public class PreLoadingItemCompleteEventArgs : EventArgs
    {
        public IPreLoadable Item { get; set; }
    }
}
