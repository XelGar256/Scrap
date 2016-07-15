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
        private BackgroundWorker zoomBw, swagBw, rebelBw, giftHulkBw, InboxBw, pedBw;

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoginSwag_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameSwag.Text, password = txtPasswordSwag.Password;
            bool vids = false;
            bool openBucks = false;


            swagBw = new BackgroundWorker();
            swagBw.WorkerSupportsCancellation = true;
            swagBw.DoWork += delegate (object o, DoWorkEventArgs args) { new SwagModel(username, password, swagBw, vids, openBucks); };
            swagBw.RunWorkerAsync();
        }

        private void btnLoginZoom_Click(object sender, RoutedEventArgs e)
        {
            bool justZoom = false;
            string username = txtUsernameZoom.Text, password = txtPasswordZoom.Password;

            zoomBw = new BackgroundWorker();
            zoomBw.WorkerSupportsCancellation = true;
            zoomBw.DoWork += delegate (object o, DoWorkEventArgs args) { new ZoomModel(username, password, zoomBw, justZoom); };
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
            bool openBucks = false;

            swagBw = new BackgroundWorker();
            swagBw.WorkerSupportsCancellation = true;
            swagBw.DoWork += delegate (object o, DoWorkEventArgs args) { new SwagModel(username, password, swagBw, vids, openBucks); };
            swagBw.RunWorkerAsync();
        }

        private void btnLoginGiftHulk_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameGiftHulk.Text, password = txtPasswordGiftHulk.Password;
            bool openHulk = false;

            giftHulkBw = new BackgroundWorker();
            giftHulkBw.WorkerSupportsCancellation = true;
            giftHulkBw.DoWork += delegate (object o, DoWorkEventArgs args) { new GiftHulkModel(username, password, giftHulkBw, openHulk); };
            giftHulkBw.RunWorkerAsync();
        }

        private void btnStopGiftHulk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_openZoom_Click(object sender, RoutedEventArgs e)
        {
            bool justZoom = true;
            string username = txtUsernameZoom.Text, password = txtPasswordZoom.Password;

            zoomBw = new BackgroundWorker();
            zoomBw.WorkerSupportsCancellation = true;
            zoomBw.DoWork += delegate (object o, DoWorkEventArgs args) { new ZoomModel(username, password, zoomBw, justZoom); };
            zoomBw.RunWorkerAsync();
        }

        private void btnOpenBucks_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameSwag.Text, password = txtPasswordSwag.Password;
            bool vids = false;
            bool openBucks = true;

            swagBw = new BackgroundWorker();
            swagBw.WorkerSupportsCancellation = true;
            swagBw.DoWork += delegate (object o, DoWorkEventArgs args) { new SwagModel(username, password, swagBw, vids, openBucks); };
            swagBw.RunWorkerAsync();
        }

        private void btnOpenRebel_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameRebel.Text, password = txtPasswordRebel.Password;
            bool openRebel = true;

            rebelBw = new BackgroundWorker();
            rebelBw.WorkerSupportsCancellation = true;
            rebelBw.DoWork += delegate (object o, DoWorkEventArgs args) { new RebelModel(username, password, rebelBw, openRebel); };
            rebelBw.RunWorkerAsync();
        }

        private void btnOpenHulk_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameGiftHulk.Text, password = txtPasswordGiftHulk.Password;
            bool openHulk = true;

            giftHulkBw = new BackgroundWorker();
            giftHulkBw.WorkerSupportsCancellation = true;
            giftHulkBw.DoWork += delegate (object o, DoWorkEventArgs args) { new GiftHulkModel(username, password, giftHulkBw, openHulk); };
            giftHulkBw.RunWorkerAsync();
        }

        private void btnLoginPed_Click(object sender, RoutedEventArgs e)
        {
            //PED
            string username = txtUsernamePed.Text, password = txtPasswordPed.Password;
            bool openPed = false;

            pedBw = new BackgroundWorker();
            pedBw.WorkerSupportsCancellation = true;
            pedBw.DoWork += delegate (object o, DoWorkEventArgs args) { new PedModel(username, password, pedBw, openPed); };
            pedBw.RunWorkerAsync();
        }

        private void btnStopPed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoginInbox_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameInbox.Text, password = txtPasswordInbox.Password;

            InboxBw = new BackgroundWorker();
            InboxBw.WorkerSupportsCancellation = true;
            InboxBw.DoWork += delegate (object o, DoWorkEventArgs args) { new InboxModel(username, password, InboxBw); };
            InboxBw.RunWorkerAsync();
        }

        private void btnStopInbox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_openPed_Click(object sender, RoutedEventArgs e)
        {
            //PED
            string username = txtUsernamePed.Text, password = txtPasswordPed.Password;
            bool openPed = true;

            pedBw = new BackgroundWorker();
            pedBw.WorkerSupportsCancellation = true;
            pedBw.DoWork += delegate (object o, DoWorkEventArgs args) { new PedModel(username, password, pedBw, openPed); };
            pedBw.RunWorkerAsync();
        }

        private void btnStopPalace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoginRebel_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameRebel.Text, password = txtPasswordRebel.Password;
            bool openRebel = false;

            rebelBw = new BackgroundWorker();
            rebelBw.WorkerSupportsCancellation = true;
            rebelBw.DoWork += delegate (object o, DoWorkEventArgs args) { new RebelModel(username, password, rebelBw, openRebel); };
            rebelBw.RunWorkerAsync();
        }

        private void btnStopSwag_Click(object sender, RoutedEventArgs e)
        {
            swagBw.CancelAsync();
        }
    }
}
