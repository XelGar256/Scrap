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
        private BackgroundWorker zoomBw, swagBw, rebelBw, giftHulkBw, InboxBw, grindBw;
        Window settings = new Settings();

        public MainWindow()
        {
            chromeDriverKiller();
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (Properties.Settings.Default.ZoomUser != string.Empty)
            {
                txtUsernameZoom.Text = Properties.Settings.Default.ZoomUser;
                txtPasswordZoom.Password = Properties.Settings.Default.ZoomPass;
            }
            if (Properties.Settings.Default.SwagUser != string.Empty)
            {
                txtUsernameSwag.Text = Properties.Settings.Default.SwagUser;
                txtPasswordSwag.Password = Properties.Settings.Default.SwagPass;
            }
            if (Properties.Settings.Default.RebelUser != string.Empty)
            {
                txtUsernameRebel.Text = Properties.Settings.Default.RebelUser;
                txtPasswordRebel.Password = Properties.Settings.Default.RebelPass;
            }
            if (Properties.Settings.Default.GiftUser != string.Empty)
            {
                txtUsernameGiftHulk.Text = Properties.Settings.Default.GiftUser;
                txtPasswordGiftHulk.Password = Properties.Settings.Default.GiftPass;
            }
            if (Properties.Settings.Default.InboxUser != string.Empty)
            {
                txtUsernameInbox.Text = Properties.Settings.Default.InboxUser;
                txtPasswordInbox.Password = Properties.Settings.Default.InboxPass;
            }
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
            int cards = 0;

            if (rdoCard.IsChecked == true)
            {
                cards = 0;
            }
            if (rdoRank.IsChecked == true)
            {
                cards = 1;
            }
            if (rdoSuit.IsChecked == true)
            {
                cards = 2;
            }

            giftHulkBw = new BackgroundWorker();
            giftHulkBw.WorkerSupportsCancellation = true;
            giftHulkBw.DoWork += delegate (object o, DoWorkEventArgs args) { new GiftHulkModel(username, password, giftHulkBw, openHulk, cards); };
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
            int cards = 0;

            giftHulkBw = new BackgroundWorker();
            giftHulkBw.WorkerSupportsCancellation = true;
            giftHulkBw.DoWork += delegate (object o, DoWorkEventArgs args) { new GiftHulkModel(username, password, giftHulkBw, openHulk, cards); };
            giftHulkBw.RunWorkerAsync();
        }

        private void btnLoginGrind_Click(object sender, RoutedEventArgs e)
        {
            //Grindabuck
            string username = txtUsernameGrind.Text, password = txtPasswordGrind.Password;
            bool openGrind = false;

            grindBw = new BackgroundWorker();
            grindBw.WorkerSupportsCancellation = true;
            grindBw.DoWork += delegate (object o, DoWorkEventArgs args) { new GrindaModel(username, password, grindBw, openGrind); };
            grindBw.RunWorkerAsync();
        }

        private void btnStopGrind_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoginInbox_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameInbox.Text, password = txtPasswordInbox.Password;
            bool tv = false;

            InboxBw = new BackgroundWorker();
            InboxBw.WorkerSupportsCancellation = true;
            InboxBw.DoWork += delegate (object o, DoWorkEventArgs args) { new InboxModel(username, password, InboxBw, tv); };
            InboxBw.RunWorkerAsync();
        }

        private void btnInboxTv_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameInbox.Text, password = txtPasswordInbox.Password;
            bool tv = true;

            InboxBw = new BackgroundWorker();
            InboxBw.WorkerSupportsCancellation = true;
            InboxBw.DoWork += delegate (object o, DoWorkEventArgs args) { new InboxModel(username, password, InboxBw, tv); };
            InboxBw.RunWorkerAsync();
        }

        private void mnuSettings_Click(object sender, RoutedEventArgs e)
        {
            settings.Show();
        }

        private void LoadSetting_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.ZoomUser != string.Empty)
            {
                txtUsernameZoom.Text = Properties.Settings.Default.ZoomUser;
                txtPasswordZoom.Password = Properties.Settings.Default.ZoomPass;
            }
            if (Properties.Settings.Default.SwagUser != string.Empty)
            {
                txtUsernameSwag.Text = Properties.Settings.Default.SwagUser;
                txtPasswordSwag.Password = Properties.Settings.Default.SwagPass;
            }
            if (Properties.Settings.Default.RebelUser != string.Empty)
            {
                txtUsernameRebel.Text = Properties.Settings.Default.RebelUser;
                txtPasswordRebel.Password = Properties.Settings.Default.RebelPass;
            }
            if (Properties.Settings.Default.GiftUser != string.Empty)
            {
                txtUsernameGiftHulk.Text = Properties.Settings.Default.GiftUser;
                txtPasswordGiftHulk.Password = Properties.Settings.Default.GiftPass;
            }
            if (Properties.Settings.Default.InboxUser != string.Empty)
            {
                txtUsernameInbox.Text = Properties.Settings.Default.InboxUser;
                txtPasswordInbox.Password = Properties.Settings.Default.InboxPass;
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                zoomBw.CancelAsync();
            }
            catch { }
            try
            {
                swagBw.CancelAsync();
            }
            catch { }
            try
            {
                rebelBw.CancelAsync();
            }
            catch { }
            try
            {
                giftHulkBw.CancelAsync();
            }
            catch { }
            try
            {
                InboxBw.CancelAsync();
            }
            catch { }
            try
            {
                grindBw.CancelAsync();
            }
            catch { }
            settings.Close();
            Close();
        }

        private void btnStopInbox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_openGrind_Click(object sender, RoutedEventArgs e)
        {
            //Grindabuck
            string username = txtUsernameGrind.Text, password = txtPasswordGrind.Password;
            bool openGrind = true;

            grindBw = new BackgroundWorker();
            grindBw.WorkerSupportsCancellation = true;
            grindBw.DoWork += delegate (object o, DoWorkEventArgs args) { new GrindaModel(username, password, grindBw, openGrind); };
            grindBw.RunWorkerAsync();
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

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                zoomBw.CancelAsync();
            }
            catch { }
            try
            {
                swagBw.CancelAsync();
            }
            catch { }
            try
            {
                rebelBw.CancelAsync();
            }
            catch { }
            try
            {
                giftHulkBw.CancelAsync();
            }
            catch { }
            try
            {
                InboxBw.CancelAsync();
            }
            catch { }
            try
            {
                grindBw.CancelAsync();
            }
            catch { }
            settings.Close();

            base.OnClosing(e);
        }
    }
}
