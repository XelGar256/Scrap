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
        private BackgroundWorker zoomBw, swagBw, rebelBw, vertsBw, iRazooBw;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoginSwag_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameSwag.Text, password = txtPasswordSwag.Password;
            bool vids = false;

            swagBw = new BackgroundWorker();
            swagBw.WorkerSupportsCancellation = true;
            swagBw.DoWork += delegate(object o, DoWorkEventArgs args) { new SwagModel(username, password, swagBw, vids); };
            swagBw.RunWorkerAsync();
        }

        private void btnLoginZoom_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameZoom.Text, password = txtPasswordZoom.Password;

            zoomBw = new BackgroundWorker();
            zoomBw.WorkerSupportsCancellation = true;
            zoomBw.DoWork += delegate (object o, DoWorkEventArgs args) { new ZoomModel(username, password, zoomBw); };
            zoomBw.RunWorkerAsync();
        }

        private void btnStopZoom_Click(object sender, RoutedEventArgs e)
        {
            zoomBw.CancelAsync();
        }

        private void btnStopRebel_Click(object sender, RoutedEventArgs e)
        {
            rebelBw.CancelAsync();
        }

        private void btnVideos_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameZoom.Text, password = txtPasswordZoom.Password;
            bool vids = true;

            zoomBw = new BackgroundWorker();
            zoomBw.WorkerSupportsCancellation = true;
            zoomBw.DoWork += delegate (object o, DoWorkEventArgs args) { new SwagModel(username, password, zoomBw, vids); };
            zoomBw.RunWorkerAsync();
        }

        private void btnStopVerts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStopiRazoo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoginiRazoo_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameiRazoo.Text, password = txtPasswordiRazoo.Password;

            iRazooBw = new BackgroundWorker();
            iRazooBw.WorkerSupportsCancellation = true;
            iRazooBw.DoWork += delegate (object o, DoWorkEventArgs args) { new iRazooModel(username, password, iRazooBw); };
            iRazooBw.RunWorkerAsync();
        }

        private void btnLoginVerts_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameVerts.Text, password = txtPasswordVerts.Password;

            vertsBw = new BackgroundWorker();
            vertsBw.WorkerSupportsCancellation = true;
            vertsBw.DoWork += delegate (object o, DoWorkEventArgs args) { new PaidVertsModel(username, password, vertsBw); };
            vertsBw.RunWorkerAsync();
        }

        private void btnLoginRebel_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameRebel.Text, password = txtPasswordRebel.Password;

            rebelBw = new BackgroundWorker();
            rebelBw.WorkerSupportsCancellation = true;
            rebelBw.DoWork += delegate (object o, DoWorkEventArgs args) { new RebelModel(username, password, rebelBw); };
            rebelBw.RunWorkerAsync();
        }

        private void btnStopSwag_Click(object sender, RoutedEventArgs e)
        {
            swagBw.CancelAsync();
        }
    }
}
