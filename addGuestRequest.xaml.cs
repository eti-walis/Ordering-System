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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class addGuestRequest : Window
    {
        GuestRequest guestRequest;
        public static IBL bl = BLFactory.getBL();
        public addGuestRequest()
        {
            InitializeComponent();
            guestRequest = new GuestRequest();
            DataContext = guestRequest;
            namei.Content = DateTime.Today.ToShortDateString();
            pool.DataContext = bl.IsInterestedList();
            jacuzzi.DataContext = bl.IsInterestedList();
            garden.DataContext = bl.IsInterestedList();
            breakfast.DataContext = bl.IsInterestedList();
            childrensAttractions.DataContext = bl.IsInterestedList();
            typeOfHostungUnit.DataContext = bl.TheAccommodationTypeUnitList();
            area.DataContext = bl.DesiredResortAreaList();
            entryDay.DisplayDateStart = DateTime.Today;
            releaseDay.DisplayDateStart = DateTime.Today;
            GuestRequestKey.Content = Configuration.guestRequestKeySeq;
        }       
        private void addGuestRequets_button(object sender, RoutedEventArgs e)
        {
            //להוסיף באיף בדיקה אם בחר תאריך כניסה ויציאה
            if (Name.Text == "" || familyName.Text == "" || email.Text == "" || area.SelectedIndex == -1 || couples.Text == "" || signals.Text == "" || pool.SelectedIndex == -1 || area.SelectedIndex == -1 || jacuzzi.SelectedIndex == -1 || garden.SelectedIndex == -1 || childrensAttractions.SelectedIndex == -1 || typeOfHostungUnit.SelectedIndex == -1 || minPrice.Text == "" || maxPrice.Text == ""||entryDay.SelectedDate==null||releaseDay.SelectedDate==null)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!(bl.IsMail(email.Text)))
            {
                MessageBox.Show("!!כתובת המייל אינה חוקית", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    guestRequest.EntryDate = entryDay.SelectedDate.Value;
                    guestRequest.ReleaseDate =releaseDay.SelectedDate.Value;
                    guestRequest.RegistrationDate = DateTime.Today;//.ToShortDateString();
                    guestRequest.GuestRequestKey = (long)GuestRequestKey.Content;
                    bl.addGuestRequest(guestRequest);
                }
                catch(Exception ex)//if there is any problem MessageBox show it up
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //After everything has worked and added successfully
                Successful sar = new Successful("GuestRequest");
                sar.Show();
                this.Close();
            }
        }
        private void Name_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void FamilyName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void Couples_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Signals_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void MinPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void MaxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
