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
    /// Interaction logic for deleteHostingUnit.xaml
    /// </summary>
    public partial class deleteHostingUnit : Window
    {
        public static IBL bl = BLFactory.getBL();
        public deleteHostingUnit()
        {

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (numberOfHostingUnit.Text == "" || userName.Text == "" || password.Password == "")
            {
                MessageBox.Show("יש למלא את כל השדות", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }         
            else
            {
                try
                {
                     bl.deleteHostingUnit(long.Parse(numberOfHostingUnit.Text), userName.Text, password.Password.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } 
            }
        }

        private void succedDelete_Click(object sender, RoutedEventArgs e)
        {
            SuccedUpdateDelete sud = new SuccedUpdateDelete("delete");
            sud.Show();
            this.Close();
        }
    }
}
