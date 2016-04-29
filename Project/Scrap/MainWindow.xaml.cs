using Scrap.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace Scrap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker zoomBw, swagBw, rebelBw, giftHulkBw, lpBw, instaGCBw;

        public MainWindow()
        {
            chromeDriverKiller();
            InitializeComponent();
        }

        private void chromeDriverKiller()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("chromedriver"))
                {
                    proc.Kill();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            swagBw = new BackgroundWorker();
            swagBw.WorkerSupportsCancellation = true;
            swagBw.DoWork += delegate (object o, DoWorkEventArgs args) { new SwagModel(username, password, swagBw, vids); };
            swagBw.RunWorkerAsync();
        }

        private void btnLoginGiftHulk_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameGiftHulk.Text, password = txtPasswordGiftHulk.Password;

            giftHulkBw = new BackgroundWorker();
            giftHulkBw.WorkerSupportsCancellation = true;
            giftHulkBw.DoWork += delegate (object o, DoWorkEventArgs args) { new GiftHulkModel(username, password, giftHulkBw); };
            giftHulkBw.RunWorkerAsync();
        }

        private void btnStopGiftHulk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoginGC_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameGiftHulk.Text, password = txtPasswordGiftHulk.Password;

            instaGCBw = new BackgroundWorker();
            instaGCBw.WorkerSupportsCancellation = true;
            instaGCBw.DoWork += delegate (object o, DoWorkEventArgs args) { new InstaGCModel(username, password, instaGCBw); };
            instaGCBw.RunWorkerAsync();
        }

        private void btnStopGC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoginPalace_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernamePalace.Text, password = txtPasswordPalace.Password;

            lpBw = new BackgroundWorker();
            lpBw.WorkerSupportsCancellation = true;
            lpBw.DoWork += delegate (object o, DoWorkEventArgs args) { new LootPalaceModel(username, password, lpBw); };
            lpBw.RunWorkerAsync();
        }

        private void btnStopPalace_Click(object sender, RoutedEventArgs e)
        {

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
