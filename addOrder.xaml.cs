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
using System.ComponentModel;
using BE;
using BL;
namespace UI
{
    /// <summary>
    /// Interaction logic for addOrder.xaml
    /// </summary>
    public partial class addOrder : Window
    {
        
        public static IBL bl = BLFactory.getBL();
        Order order;
        public addOrder(Host host)
        {
            InitializeComponent();
            order = new Order();
            DataContext = order;
            date.Content = DateTime.Today.ToShortDateString();
            OrderKey.Content = Configuration.orderKeySeq;
            order.hostKey = host.HostKey;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //להוסיף לאיף בדיקה שיש תאריך יצירת הזמנה
            if (numOfHostingUnit.Text == "" || numOfGuestRequest.Text == ""  )
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    order.OrderKey = (long)OrderKey.Content;
                    order.CreateDate = DateTime.Today;
                    bl.addOrder(order);
                    CallMailSend(order);
                    //Loading b;
                    //b = new Loading();
                    //b.ShowDialog();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //if everything worked and the order was added successfully
                //Loading b;
                //b = new Loading();
                //b.ShowDialog();
                //b.Close();
                Successful ags = new Successful("Order");
                ags.Show();
                this.Close();
            }
        }
        Loading b;
        private void CallMailSend(Order or)
        {
            try
            {
                bl.updateOrderStatus(or, OrderStatus.EmailWasSent.ToString());
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();
                b = new Loading();
                b.ShowDialog();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "בדיקה", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None,
               MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                return;
            } 
        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            b.Close();
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (order.Status == OrderStatus.EmailWasSent)
            {

                bl.mail(order);//, bl.allGuestRequest().Find(x => x.GuestRequestKey == o.GuestRequestKey));
            }

        }

    }
}
