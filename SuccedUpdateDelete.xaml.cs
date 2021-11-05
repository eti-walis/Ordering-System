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

namespace UI
{
    /// <summary>
    /// Interaction logic for SuccedUpdateDelete.xaml
    /// </summary>
    public partial class SuccedUpdateDelete : Window
    {
        public SuccedUpdateDelete(string str)
        {
            InitializeComponent();
            if (str == "delete")
                delete.Visibility = Visibility.Visible;
            else
                update.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
