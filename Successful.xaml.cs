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
    /// Interaction logic for SuccessfulAddingHostingUnit.xaml
    /// </summary>
    public partial class Successful : Window
    {
        Host hos = new Host();
        public Successful(string str, Host ho)
        {
            InitializeComponent();
            hos = ho;
            if (str == "HostingUnit")
            {
                lableForHostingUnit.Visibility = Visibility.Visible;
                ButtonForHostingUnit.Visibility = Visibility.Visible;
            }
            if (str == "GuestRequest")
            {
                lableForGuestRequst.Visibility = Visibility.Visible;
                labelForGuestRequest2.Visibility = Visibility.Visible;
            }
            if (str == "Order")
            {
                labelForOrder1.Visibility = Visibility.Visible;
                labelForOrder2.Visibility = Visibility.Visible;
                ////////////////send an email with ordedetails
            }
        }
         public Successful(string str)
            {
                InitializeComponent();

                if (str == "HostingUnit")
                {

                    lableForHostingUnit.Visibility = Visibility.Visible;
                    ButtonForHostingUnit.Visibility = Visibility.Visible;
                }
                if (str == "GuestRequest")
                {
                    lableForGuestRequst.Visibility = Visibility.Visible;
                    labelForGuestRequest2.Visibility = Visibility.Visible;
                }
                if (str == "Order")
                {
                    labelForOrder1.Visibility = Visibility.Visible;
                    labelForOrder2.Visibility = Visibility.Visible;
                    ////////////////send an email with ordedetails
                }
                if (str == "comment")
                {
                    labelForComment.Visibility = Visibility.Visible;
                }

             }
        
        private void לתפריט_מארח_Click(object sender, RoutedEventArgs e)
        {
            SubMainForHost smfo = new SubMainForHost(hos);
            smfo.Show();
            this.Close();
        }

        private void לתפריט_הראשי_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
    
}
