using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scrap
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (txtZoomUser.Text != "")
            {
                Properties.Settings.Default.ZoomUser = txtZoomUser.Text;
                Properties.Settings.Default.ZoomPass = txtZoomPass.Password;
            }
            if (txtSwagUser.Text != "")
            {
                Properties.Settings.Default.SwagUser = txtSwagUser.Text;
                Properties.Settings.Default.SwagPass = txtSwagPass.Password;
            }
            if (txtPrizeUser.Text != "")
            {
                Properties.Settings.Default.RebelUser = txtPrizeUser.Text;
                Properties.Settings.Default.RebelPass = txtPrizePass.Password;
            }
            if (txtGiftUser.Text != "")
            {

                Properties.Settings.Default.GiftUser = txtGiftUser.Text;
                Properties.Settings.Default.GiftPass = txtGiftPass.Password;
            }
            if (txtInboxUser.Text != "")
            {
                Properties.Settings.Default.InboxUser = txtInboxUser.Text;
                Properties.Settings.Default.InboxPass = txtInboxPass.Password;
            }
            Properties.Settings.Default.Save();

            MessageBox.Show("Your Settings Have Been Saved!!", "Save Settings", MessageBoxButton.OK);

            Close();
        }
    }
}
