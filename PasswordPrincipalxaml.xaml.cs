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

namespace UI
{
    /// <summary>
    /// Interaction logic for PasswordPrincipalxaml.xaml
    /// </summary>
    public partial class PasswordPrincipalxaml : Window
    {
        public PasswordPrincipalxaml()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(txb_password.Password.ToString() == "*****"))
            {
                MessageBox.Show("הסיסמא שגויה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                principal p = new principal();
                p.Show();
                this.Close();
            }
        }
    }

}
