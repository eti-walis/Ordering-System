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
using BE;
using BL;
namespace UI
{
    /// <summary>
    /// Interaction logic for UpdateHostingUnit.xaml
    /// </summary>
    public partial class updateHostingUnit : Window
    {
        HostingUnit ho = new HostingUnit();
        public static IBL bl = BLFactory.getBL();
        public updateHostingUnit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ho = bl.ifHostingUnit(long.Parse(numberOfHostingUnit.Text), userName.Text, password.Password.ToString(), true);
            }
            catch (Exception ex)//if there is any problem MessageBox show it up
            {
                MessageBox.Show(ex.Message, "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            updateHostingUnit1 uh = new updateHostingUnit1(ho);
            uh.Show();
            this.Close();
        }
    }
}
