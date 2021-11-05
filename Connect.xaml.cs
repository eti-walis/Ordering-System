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
using System.Text.RegularExpressions;
using BL;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for Connectxaml.xaml
    /// </summary>
    public partial class Connect : Window
    {
        public static IBL bl = BLFactory.getBL();
        Host ho = new Host();
        public Connect()
        {
            InitializeComponent();
            signStatus.DataContext = bl.HostClientList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addGuestRequest gs = new addGuestRequest();
            gs.ShowDialog();
        }



        private void אישור_Click_1(object sender, RoutedEventArgs e)
        {
            if (logUserNsame.Text == "" || logpassword.Password == "")
            {
                MessageBox.Show("יש למלא את כל השדות", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                bl.isExistGuest(logUserNsame.Text, logpassword.Password.ToString());
            }
            catch (Exception ex)
            {
                try
                {
                    bl.isExistHost(logUserNsame.Text, logpassword.Password.ToString());                    
                }
                catch(Exception x)
                {
                    MessageBox.Show("שם משתמש או סיסמא לא תקינים", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }              
            ho = bl.hostByUserName(logUserNsame.Text);
            if (bl.userStatus(logUserNsame.Text, logpassword.Password.ToString()) == "Host")
            {
                SubMainForHost smfo = new SubMainForHost(ho);
                smfo.ShowDialog();
            }
            else
            {
                addGuestRequest agr = new addGuestRequest();
                agr.ShowDialog();

            }

        }

        private void המשך_Click(object sender, RoutedEventArgs e)
        {
            if (signUserName.Text == "" || signPasswordBox.Password == "" || signPasswordBox1.Password == "" || signStatus.SelectedIndex == -1)
            {
                MessageBox.Show("יש למלא את כל השדות", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (signPasswordBox.Password != signPasswordBox1.Password)
            {
                MessageBox.Show("הסיסמאות שהזנת אינן תואמות", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                bl.checkUserNameAndPassword(signUserName.Text, signPasswordBox.Password.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (signStatus.SelectedItem.ToString() == "Host")
            {
                HostConnect h = new HostConnect(signUserName.Text, signPasswordBox.Password.ToString());
                h.Show();
                this.Close();
            }
            else
            {
                AddDetails ad = new AddDetails(signUserName.Text, signPasswordBox.Password.ToString());
                ad.Show();
                this.Close();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lab_password_create.Visibility = Visibility.Visible;
        }

        private void LogUserNsame_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            lab_password_connect.Visibility = Visibility.Visible;
        }

        private void SignUserName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }
    }
}