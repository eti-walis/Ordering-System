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
using BE;
using BL;
namespace UI
{
    /// <summary>
    /// Interaction logic for AddDetails.xaml
    /// </summary>
    public partial class AddDetails : Window
    {
        Guest guest = new Guest();
        public static IBL bl = BLFactory.getBL();
        public AddDetails(string userName, string userPassword)
        {
            InitializeComponent();
            DataContext = guest;
            guest.userName = userName;
            guest.password = userPassword;
        }
        private void המשך(object sender, RoutedEventArgs e)
        {

            if (textFirstName.Text == "" || textLastName.Text == "" || textEmail.Text == "")
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!(bl.IsMail(textEmail.Text)))
            {
                MessageBox.Show("!!כתובת המייל אינה חוקית", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
               try
                {
                   
                    bl.addGuest(guest);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                addGuestRequest ags = new addGuestRequest();
                ags.Show();
                this.Close();
            }
        }

        private void TextFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-z]+").IsMatch(e.Text);
        }

        private void TextLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-z]+").IsMatch(e.Text);
        }
    }
}
