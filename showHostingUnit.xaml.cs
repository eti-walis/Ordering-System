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
    /// Interaction logic for showHostingUnit.xaml
    /// </summary>
    public partial class showHostingUnit : Window
    {
       List <HostingUnit> ho = new List <HostingUnit>();
        public static IBL bl = BLFactory.getBL();
        public showHostingUnit(Host host)
        {
            InitializeComponent();
            this.ho = bl.hostingUnitFromHost(host);
            ListView.ItemsSource = ho;
        }       
    }
}
