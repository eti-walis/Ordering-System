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
    /// Interaction logic for addCommentxaml.xaml
    /// </summary>
    public partial class addComment: Window
    {
        public static IBL bl = BLFactory.getBL();
        public addComment()
        {
            InitializeComponent();
        }

        private void Btn_next_Click(object sender, RoutedEventArgs e)
        {
        //    try
        //    {
        //        bl.addComment(long.Parse(hostingUnitKey.Text), comment.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }
            Successful suc = new Successful("comment");
            suc.Show();
            this.Close();
        }
    }
}
