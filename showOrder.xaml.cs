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
    /// Interaction logic for showOrder.xaml
    /// </summary>
    public partial class showOrder : Window
    {
        public static IBL bl = BLFactory.getBL();
        List <Order> or = new List<Order>();
        public showOrder(Host hos)
        {
            InitializeComponent();
            this.or = bl.orderFromHost(hos);
            ListView.ItemsSource = or;
        }

    }
}
