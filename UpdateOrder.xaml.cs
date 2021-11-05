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
using BL;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        public static IBL bl = BLFactory.getBL();
        Order or = new Order();
        public UpdateOrder()
        {
            InitializeComponent();
            newStatus.ItemsSource = bl.OrderStatusList();
            currentStatus.ItemsSource = bl.OrderStatusList();
            DataContext = or;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentStatus.SelectedIndex == -1 || newStatus.SelectedIndex == -1 || numberOfOrder.Text == "")
            {
                MessageBox.Show("יש למלא את כל השדות", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    or = bl.FindOrder(long.Parse(numberOfOrder.Text));
                    bl.updateOrderStatus(or, newStatus.SelectedItem.ToString());
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                addGuestRequest ags = new addGuestRequest();
                ags.Show();
                this.Close();
            }
        }
    }
}

