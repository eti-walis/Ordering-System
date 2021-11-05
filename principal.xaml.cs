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
    /// Interaction logic for principal.xaml
    /// </summary>
    public partial class principal : Window
    {
        public static IBL bl = BLFactory.getBL();
        public principal()
        {
            InitializeComponent();
           
        }

        private void Host_Click(object sender, RoutedEventArgs e)
        {
            HostList h = new HostList();
            h.Show();
        }

        private void guest_Click(object sender, RoutedEventArgs e)
        {
            GuestList g = new GuestList();
            g.Show();
        }

        private void HostingUnit_Click(object sender, RoutedEventArgs e)
        {
            HostingUnitList ho = new HostingUnitList();
                ho.Show();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            OrderList o = new OrderList();
            o.Show();
        }

        private void GuestRequest_Click(object sender, RoutedEventArgs e)
        {
            GuestRequestList g = new GuestRequestList();
            g.Show();
        }
    }
}
