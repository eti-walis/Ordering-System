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
    /// Interaction logic for Host.xaml
    /// </summary>
    public partial class HostConnect : Window
    {
        public static IBL bl = BLFactory.getBL();
        public static Host host; 
        public HostConnect(string userName, string userPassword)
        {
            InitializeComponent();
            host = new Host();
            host.BankBranchDetails = new BankBranch();
            BankDetails.ItemsSource = bl.GetBankBranches();
            DataContext = host;
            bankcheck.DataContext = bl.YesONoList();
            host.userName = userName;
            host.password = userPassword;
            host.HostKey= Configuration.hostKeySeq;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || familyName.Text == "" || email.Text == "" || phoneNumber.Text==""|| numOfHostingUnit.Text==""|| bankAccount.Text==""|| bankcheck.SelectedIndex==-1)
            { 
                MessageBox.Show("יש למלא את כל השדות", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!(bl.IsMail(email.Text)))
            {
                MessageBox.Show("!!כתובת המייל אינה חוקית", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    if (bankcheck.SelectedItem.ToString() == "No")
                    {
                        MessageBox.Show("אין אפשרות להוסיף יחידת אירוח ללא הרשאה בנקאית", "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                        bl.addHost(host);
                }
               catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "אזהרה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                AddHostingUnit ho = new AddHostingUnit(host);
                ho.Show();
                this.Close();
            }
        }

        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void FamilyName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void NumOfHostingUnit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBankNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBankName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void TextBankBranchNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBankCity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void TextBankAdress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^א-ת]+").IsMatch(e.Text);
        }

        private void BankAccount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

       
    }
}