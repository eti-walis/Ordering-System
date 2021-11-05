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
    /// Interaction logic for updateHostingUnit1.xaml
    /// </summary>
    public partial class updateHostingUnit1 : Window
    {
        public static IBL bl = BLFactory.getBL();
        HostingUnit hos = new HostingUnit();
        public updateHostingUnit1(HostingUnit hos)
        {
            this.hos = hos;
            InitializeComponent();
            DataContext = hos;
            type.ItemsSource = bl.TheAccommodationTypeUnitList();
            area.ItemsSource = bl.DesiredResortAreaList();
            childrenAttraction.ItemsSource = bl.YesONoList();
            jacuzzi.ItemsSource = bl.YesONoList();
            garden.ItemsSource = bl.YesONoList();
            pool.ItemsSource= bl.YesONoList();
            breakfast.ItemsSource = bl.YesONoList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bl.updateHostingUnit(hos);
            SuccedUpdateDelete sud = new SuccedUpdateDelete("update");
            sud.Show();
        }
        private void numOfBeds_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void numOfRoomsFor2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void priceForNight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}

