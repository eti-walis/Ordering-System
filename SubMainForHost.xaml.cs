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
    /// Interaction logic for SubMainForHost.xaml
    /// </summary>
    public partial class SubMainForHost : Window
    {
        Host hos = new Host();
      
        public SubMainForHost(Host ho)
        {
            InitializeComponent();
            hos = ho;
            DataContext = hos;
            hostKey.Content = ho.HostKey;
        }

        private void DeleteHostingUnit_Click_1(object sender, RoutedEventArgs e)
        {
            deleteHostingUnit del = new deleteHostingUnit();
            del.Show();
            this.Close();
        }

        private void AddHostingUnit_Click_1(object sender, RoutedEventArgs e)
        {
            AddHostingUnit add = new AddHostingUnit(hos);
            add.Show();
            this.Close();
        }

        private void UpdateHostingUnit_Click_1(object sender, RoutedEventArgs e)
        {
            updateHostingUnit up = new updateHostingUnit();
            up.Show();
            this.Close();
        }

        private void AddOrder_Click_1(object sender, RoutedEventArgs e)
        {
            addOrder or = new addOrder(hos);
            or.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            showHostingUnit sh = new showHostingUnit(hos);
            sh.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            showOrder or = new showOrder(hos);
            or.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GuestRequestList grl = new GuestRequestList();
            grl.Show();
            this.Close();
        }
    }
}
