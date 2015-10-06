using Scrap.Models;
using System.ComponentModel;
using System.Windows;

namespace Scrap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker zoomBw, swagBw;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoginSwag_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameSwag.Text, password = txtPasswordSwag.Password;

            swagBw = new BackgroundWorker();
            swagBw.WorkerSupportsCancellation = true;
            swagBw.DoWork += delegate(object o, DoWorkEventArgs args) { new SwagModel(username, password, swagBw); };
            swagBw.RunWorkerAsync();
        }

        private void btnLoginZoom_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameZoom.Text, password = txtPasswordZoom.Password;

            zoomBw = new BackgroundWorker();
            zoomBw.WorkerSupportsCancellation = true;
            zoomBw.DoWork += delegate(object o, DoWorkEventArgs args) { new ZoomModel(username, password, zoomBw); };
            zoomBw.RunWorkerAsync();
        }

        private void btnStopZoom_Click(object sender, RoutedEventArgs e)
        {
            zoomBw.CancelAsync();
        }

        private void btnStopSwag_Click(object sender, RoutedEventArgs e)
        {
            swagBw.CancelAsync();
        }
    }
}
