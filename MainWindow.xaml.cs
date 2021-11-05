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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IBL bl = BLFactory.getBL();
        public MainWindow()
        {
            InitializeComponent();
            bl.closeOrder();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connect c = new Connect();
            c.Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow smw = new MainWindow();
            smw.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Connect co = new Connect();
            co.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            addGuestRequest gs = new addGuestRequest();
            gs.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            addComment ac=new addComment();
            ac.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PasswordPrincipalxaml pp = new PasswordPrincipalxaml();
            pp.Show();

        } 
    }
}
