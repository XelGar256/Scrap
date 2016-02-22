using System.ComponentModel;

namespace Application.Current
{
    internal class MainWindow
    {
        public static CancelEventHandler Closing { get; internal set; }
    }
}