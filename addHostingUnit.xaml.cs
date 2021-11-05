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
    /// Interaction logic for HostingUnit.xaml
    /// </summary>
    public partial class AddHostingUnit : Window
    {
        HostingUnit hostingUnit = new HostingUnit();
        public static IBL bl = BLFactory.getBL();
        Host hos = new Host();
        public AddHostingUnit(Host ho)
        {
            InitializeComponent();
            hos = ho;
            DataContext = hostingUnit;
            pool.ItemsSource = bl.YesONoList();
            jacuzzi.ItemsSource = bl.YesONoList();
            breakfast.ItemsSource = bl.YesONoList();
            garden.ItemsSource = bl.YesONoList();
            type.ItemsSource = bl.TheAccommodationTypeUnitList();
            childrenAttraction.ItemsSource = bl.YesONoList();
            area.ItemsSource = bl.DesiredResortAreaList();
            HostingUnitKey.Content = Configuration.hostingUnitKeySeq;
            hostingUnit.Owner = ho;
        }

        private void המשך_Click(object sender, RoutedEventArgs e)
        {

            if (name.Text == "" || numOfBeds.Text == "" || priceForNight.Text == "" || numOfRoomsFor2.Text == "" || childrenAttraction.SelectedIndex == -1 || pool.SelectedIndex == -1 || area.SelectedIndex == -1 || breakfast.SelectedIndex == -1 || jacuzzi.SelectedIndex == -1 || type.SelectedIndex == -1 || garden.SelectedIndex == -1)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    hostingUnit.HostingUnitKey = (long)HostingUnitKey.Content;
                    bl.AddHostingUnit(hostingUnit);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Successful sahu = new Successful("HostingUnit",hos);
                sahu.Show();
                this.Close();
            }
        }
        private void NumOfBeds_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void NumOfRoomsFor2_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void PriceForNight_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void City_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }
    }
}
