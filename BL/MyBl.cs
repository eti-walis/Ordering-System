using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

using DAL;
using BE;

namespace BL
{
    public class MyBL : IBL
    {
        IDal dal = DalFactory.getDal();



        public GuestRequest FindGuestRequest(Order order)
        {
            try
            {
                if (dal.allGuestRequest().ToList().Exists(item => item.GuestRequestKey == order.GuestRequestKey))
                {
                    GuestRequest v = dal.allGuestRequest().FirstOrDefault(item => item.GuestRequestKey == order.GuestRequestKey);
                    return v;
                }
                throw new Exception("הבקשה אינה קיימת");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public bool IsMail(string m)
        {

            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return (regex.IsMatch(m));

        }

        public void deleteHostingUnit(long hostingUnitKey, string UName, string pass)
        {
            try
            {
                HostingUnit ho = new HostingUnit();
                ho = ifHostingUnit(hostingUnitKey, UName, pass, false);
                dal.deleteHostingUnit(ho);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateHostingUnit(HostingUnit hos)
        {
            try
            {
                dal.updateHostingUnit(hos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Order FindOrder(long orderKey)
        {
            Order or;
            or = dal.allOrder().FirstOrDefault(item => item.OrderKey == orderKey);
            try
            {
                if (or == null)
                    throw new Exception("מספר ההזמנה שגוי");
                else
                    return or;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ApproveRequest(Order or)
        {
            var v = dal.allGuestRequest().FirstOrDefault(item => item.GuestRequestKey == or.GuestRequestKey);
            var t = dal.allHostingUnits().FirstOrDefault(item => item.HostingUnitKey == or.HostingUnitKey);
            bool flag = true;
            for (DateTime d = v.EntryDate; d < v.ReleaseDate; d = d.AddDays(1))
            {
                if (t.Diary[(d.Month) - 1, (d.Day) - 1] == true)
                    flag = false;
            }

            if (flag == false)//The request was not accepted
                return false;
            else//The request was accepted 
                return true;
        }
        public void addOrder(Order or)
        {
            try
            {
                if ((dal.allGuestRequest().ToList().Exists(item => item.GuestRequestKey == or.GuestRequestKey)) && (dal.allHostingUnits().ToList().Exists(item => item.HostingUnitKey == or.HostingUnitKey)))
                {
                    var v = dal.allHostingUnits().FirstOrDefault(item => item.HostingUnitKey == or.HostingUnitKey);
                    Host h = v.Owner;
                    if (h.CollectionClearance.ToString() != "Yes")
                        throw new Exception("ההרשמה אינה אפשרית ללא הרשאה מחשבון הבנק");
                    else
                    {
                        if (this.ApproveRequest(or))
                        {
                            //Checks whether the price of the unit offered to the customer is indeed within the price range requested
                            ////////////////בדיקה שהמחיר תקין
                                updateOrderStatus(or, OrderStatus.EmailWasSent.ToString());//we called function that update the order status.
                                dal.AddOrder(or);
                                TotalPrice(or);
                        }
                        else
                            throw new Exception("הימים תפוסים");
                    }
                }
                else
                    throw new Exception("לא נמצאו בקשת אירוח או יחידת אירוח מתאימות ");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Thread StartTheThread(Order ord)
        {
            var t = new Thread(() => mail(ord));
            return t;
        }
        public void updateOrderStatus(Order or, string orderStatus)
        {
            try
            {
                if (orderStatus == OrderStatus.EmailWasSent.ToString())
                {
                    //Thread t1 = StartTheThread(or);
                    //t1.Start();
                    or.Status = OrderStatus.EmailWasSent;
                }
                if (orderStatus == OrderStatus.NoAddressed.ToString())
                {
                    or.Status = OrderStatus.NoAddressed;
                }
                if (orderStatus == OrderStatus.closedWithcustomerResponsetatus.ToString())
                {
                    or.Status = OrderStatus.closedWithcustomerResponsetatus;
                    closingDeal(or);
                }

                if (orderStatus == OrderStatus.closedforUnansweredCustomer.ToString())
                {
                    or.Status = OrderStatus.closedforUnansweredCustomer;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void addGuestRequest(GuestRequest gr)
        {
            try
            {
                if (gr.EntryDate.Date < gr.ReleaseDate.Date)
                    dal.addClientRequest(gr);
                else
                    throw new Exception("אין מספיק ימים");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void addHost(Host host)
        {
            try
            {
                dal.addHost(host);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public void AddHostingUnit(HostingUnit ho)
        {
            try
            {
                dal.AddHostingUnit(ho);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateClientRequestStatus(GuestRequest gr)
        {
            try
            {
                dal.updateClientRequestStatus(gr);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void updateHostingUnit(HostingUnit ho)
        //{
        //    try
        //    {
        //        dal.updateHostingUnit(ho);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public void UpdateOrder(Order or)
        {
            try
            {
                dal.UpdateOrder(or);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void closingDeal(Order or)
        {
            //check if the days are available.
            if (!ApproveRequest(or))
                throw new Exception("הימים תפוסים");
            else
            {
                //Calculate commission
                GuestRequest guestRequest = FindGuestRequest(or);
                int amla = ((guestRequest.ReleaseDate - guestRequest.EntryDate).Days) * Configuration.commission;
                //Marking in the matrix
                var v = dal.allGuestRequest().FirstOrDefault(item => item.GuestRequestKey == or.GuestRequestKey);
                var t = dal.allHostingUnits().FirstOrDefault(item => item.HostingUnitKey == or.HostingUnitKey);
                for (DateTime d = v.EntryDate; d < v.ReleaseDate; d = d.AddDays(1))
                    t.Diary[d.Month - 1, (d.Day) - 1] = true;
                guestRequest.Status = RequestStatus.SiteClose;
                List<Order> orderList = dal.allOrder().ToList();
                var orderListSelected = from item in orderList
                                        where item.GuestRequestKey == or.GuestRequestKey
                                        select item;
                foreach (var item in orderListSelected)
                    updateOrderStatus(item, OrderStatus.closedforUnansweredCustomer.ToString());
            }


        }
        public HostingUnit ifHostingUnit(long hostingUnitKey, string UName, string pass, bool flag)
        {
            try
            {
                HostingUnit hostingUnit = dal.allHostingUnits().FirstOrDefault(item => item.HostingUnitKey == hostingUnitKey);
                //למקרה שהיחידה פתוחה בהזמנה כלשהי
                if (dal.allOrder().ToList().Exists(item => item.HostingUnitKey == hostingUnitKey) && flag == false)
                    throw new Exception("לא ניתן למחוק את יחידת האירוח כיוון שקיימות לה הזמנות פתוחות");
                else
                {
                    if (dal.allOrder().ToList().Exists(item => item.HostingUnitKey == hostingUnitKey) && flag == true)
                        throw new Exception("לא ניתן לעדכן את יחידת האירוח כיוון שקיימות לה הזמנות פתוחות");
                }
                //למקרה שהשם משתמש והסיסמא אינם מתאימים למארח בעל היחידת אירוח הזאת
                if (((hostingUnit.Owner.userName != UName) || (hostingUnit.Owner.password != pass)) && flag == false)
                    throw new Exception("לא ניתן למחוק את יחידת האירוח כיוון ששם המשתמש או הסיסמא שגויים");
                else
                {
                    if (((hostingUnit.Owner.userName != UName) || (hostingUnit.Owner.password != pass)) && flag == true)
                        throw new Exception("לא ניתן לעדכן את יחידת האירוח כיוון ששם המשתמש או הסיסמא שגויים");
                }

                return hostingUnit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RevokeAuthorization(Host h)
        {
            try
            {
                HostingUnit hostingUnit = dal.allHostingUnits().FirstOrDefault(item => item.Owner.HostKey == h.HostKey);
                if (dal.allOrder().ToList().Exists(item => item.HostingUnitKey == hostingUnit.HostingUnitKey))
                    throw new Exception("לא ניתן לבטל את ההרשאה מכיוון שקשורה בה הצעה פתוחה");
                else
                    h.CollectionClearance = YesONo.No;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public List<HostingUnit> AvailableHostingUnits(DateTime t, int numDays)
        {
            List<HostingUnit> hostingUnitSelected = new List<HostingUnit>();//new empty list
            DateTime last = t.AddDays(numDays);
            foreach (HostingUnit item in dal.allHostingUnits())//We went through all the hosting units and checked on the dates we got available
            {
                bool flag = true;
                for (DateTime first = t; t < last; t = t.AddDays(1))
                {
                    if (item.Diary[(t.Month) - 1, (t.Day) - 1] == true)
                        flag = false;
                }
                if (flag == true)
                    hostingUnitSelected.Add(item);//If this hosting unit has free dates, we have added it to the list
            }
            return hostingUnitSelected;//We have returned the list of hosting units that meet the requirement.
        }
        public int numOfDays(DateTime t, DateTime d)
        {
            return (d - t).Days;
        }
        public int numOfDays(DateTime t)
        {
            return (t - DateTime.Today).Days;
        }
        public List<Order> numOfOrders(int num)
        {
            DateTime date = DateTime.Now.AddDays(-num);
            var ordersSE = from item in dal.allOrder()
                           where item.CreateDate <= date
                           select item;
            return ordersSE.ToList();
        }
        public List<GuestRequest> Deleg(conditionDelegate cd)
        {
            var v = from item in dal.allGuestRequest()
                    where cd(item)
                    select item;
            return v.ToList();
        }
        public int numOfOrdersForGuestRequest(GuestRequest gr)
        {
            int i = 0;
            foreach (var item in dal.allOrder()) { if (item.GuestRequestKey == gr.GuestRequestKey) { i++; } }
            return i;
        }
        public int numOfSucceedOrders(HostingUnit ho)
        {
            var v = from item in dal.allOrder()
                    where item.HostingUnitKey == ho.HostingUnitKey
                    select item;
            int i = 0;
            foreach (var item in v) { if (item.Status == OrderStatus.closedWithcustomerResponsetatus || item.Status == OrderStatus.EmailWasSent) { i++; } }
            return i;
        }
        public IEnumerable<IGrouping<DesiredResortArea, GuestRequest>> GuestRequesGrouptByArea()
        {
            var guestrequests = from item in dal.allGuestRequest()
                                group item by item.Area;
            //into g1
            // select g1).ToList();
            return guestrequests.ToList();
        }
        public IEnumerable<IGrouping<int, GuestRequest>> GuestRequesGrouptByNumOfVacationers()
        {
            var guestrequests = (from item in dal.allGuestRequest()
                                 group item by (item.Couples + item.Singles) into g1
                                 select g1).ToList();
            return guestrequests;
        }
        public IEnumerable<IGrouping<int, Host>> GuestRequesGrouptByNumOfHostingUnit()
        {

            var hosts = (from item in dal.allHost()
                         group item by item.numOfHostingUnits into g1
                         select g1).ToList();
            return hosts;
        }
        public IEnumerable<IGrouping<DesiredResortArea, HostingUnit>> HostingUnitGrouptByArea()
        {

            var hostingUnits = (from item in dal.allHostingUnits()
                                group item by item.Area into g1
                                select g1).ToList();
            return hostingUnits.ToList();
        }
        //OUR 6 CREATIVE COOL FUNCTIONS WOW!!!!!!!!!!!!!!!!!!!!
        //public void addComment(long hostingUnitKey, string str)
        //{
        //    try
        //    {
        //        HostingUnit ho = null;
        //        ho = dal.allHostingUnits().FirstOrDefault(item => item.HostingUnitKey == hostingUnitKey);
        //        if (ho == null)
        //            throw new Exception("מספר יחידת האירוח שגוי");
        //        else
        //            dal.addComment(ho,str);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void TotalPrice(Order or)
        {
            GuestRequest g = dal.allGuestRequest().FirstOrDefault(item => item.GuestRequestKey == or.GuestRequestKey);
            var h = dal.allHostingUnits().FirstOrDefault(item => item.HostingUnitKey == or.HostingUnitKey);
            if ((g.ReleaseDate - g.EntryDate).Days > 4)
            {
                or.totalPrice = (g.ReleaseDate - g.EntryDate).Days * h.priceForNight * 0.3;
                Console.WriteLine(or.totalPrice);
            }
            else
            {
                if (dal.allHostingUnits().ToList().Exists(item => item.HostingUnitKey == or.HostingUnitKey))
                {
                    or.totalPrice = (g.ReleaseDate - g.EntryDate).Days * h.priceForNight;
                }
            }
        }
        public IEnumerable<GuestRequest> lessDetails()
        {
            IEnumerable<GuestRequest> gr = (from request1 in dal.allGuestRequest()
                                            where request1.Status == RequestStatus.Open
                                            select new GuestRequest { MailAddress = request1.MailAddress, GuestRequestKey = request1.GuestRequestKey });
            return gr;
        }
        //public List<Attraction> getAttraction(HostingUnit ho)
        //{
        //    return dal.getAttraction(ho);
        //}
        public List<Order> getClosedforUnansweredCustomerOrder()
        {
            return (from item in dal.allOrder()
                    where item.Status == OrderStatus.closedforUnansweredCustomer
                    select item).ToList();
        }
        public List<BankBranch> GetBankBranches()
        {
            return dal.allBankBranch();
        }
        public List<DesiredResortArea> DesiredResortAreaList()
        {
            return Enum.GetValues(typeof(DesiredResortArea)).Cast<DesiredResortArea>().ToList();
        }
        public List<TheAccommodationTypeUnit> TheAccommodationTypeUnitList()
        {
            return Enum.GetValues(typeof(TheAccommodationTypeUnit)).Cast<TheAccommodationTypeUnit>().ToList();
        }
        public List<IsInterested> IsInterestedList()
        {
            return Enum.GetValues(typeof(IsInterested)).Cast<IsInterested>().ToList();
        }
        public List<YesONo> YesONoList()
        {
            return Enum.GetValues(typeof(YesONo)).Cast<YesONo>().ToList();

        }
        public List<OrderStatus> OrderStatusList()
        {
            return Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList();
        }
        public List<HostClient> HostClientList()
        {
            return Enum.GetValues(typeof(HostClient)).Cast<HostClient>().ToList();
        }
        //public void updateGuest(Guest g, string userName, string password)
        //{
        //    try
        //    {


        //        if (dal.allGuests().Exists(item => item.userName == g.userName))
        //            throw new Exception("שם משתמש זה כבר קיים");
        //        else if (dal.allGuests().Exists(item => item.password == g.password))
        //            throw new Exception("סיסמא זו כבר קיימת");
        //        else
        //            dal.UpdateGuest(g, userName, password);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void addGuest(Guest gs)
        {
            try
            {
                if (dal.allGuests().ToList().Exists(item => item.userName == gs.userName))
                    throw new Exception("שם משתמש זה כבר קיים");
                else if (dal.allGuests().ToList().Exists(item => item.password == gs.password))
                    throw new Exception("סיסמא זו כבר קיימת");
                else
                    dal.addGuest(gs);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Guest> allGuests()
        {
            return dal.allGuests().ToList();
        }
        public void isExistGuest(string userName, string password)
        {
            try
            {
                if ((dal.allGuests().ToList().Exists(item => item.userName == userName) == false) || (dal.allGuests().ToList().Exists(item => item.password == password) == false))
                    throw new Exception("שם משתמש או סיסמא שגויים");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void isExistHost(string userName, string password)
        {
            try
            {
                if ((dal.allHost().ToList().Exists(item => item.userName == userName) == false) || (dal.allHost().ToList().Exists(item => item.password == password) == false))
                    throw new Exception("שם משתמש או סיסמא שגויים");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string userStatus(string userName, string password)
        {
            if ((dal.allHost().ToList().Exists(item => item.userName == userName) == true) && (dal.allHost().ToList().Exists(item => item.password == password) == true))
                return "Host";
            return "Guest";
        }
        public List<HostingUnit> hostingUnitFromHost(Host h)
        {
            try
            {
                List<HostingUnit> v = null;
                v = (from item in dal.allHostingUnits()
                     where item.Owner.HostKey == h.HostKey
                     select item).ToList();
                if (v == null)
                    throw new Exception("לא קיימת עבורך יחידת אירוח");
                else
                    return v;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Order> orderFromHost(Host h)
        {
            try
            {
                List<Order> v = null;
                v = (from item in dal.allOrder()
                     where item.hostKey == h.HostKey
                     select item).ToList();
                if (v == null)
                    throw new Exception("לא קיימת עבורך יחידת אירוח");
                else
                    return v;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Host hostByUserName(string UName)
        {
            return (dal.allHost().FirstOrDefault(item => item.userName == UName));
        }
        public void checkUserNameAndPassword(string userName, string password)
        {
            try
            {
                if ((dal.allGuests().ToList().Exists(item => item.userName == userName) == true) && (dal.allGuests().ToList().Exists(item => item.password == password) == true))
                    throw new Exception("שם המשתמש והסיסמא כבר בשימוש");
                if ((dal.allGuests().ToList().Exists(item => item.userName == userName) == true))
                    throw new Exception("שם המשתמש כבר בשימוש");
                if (dal.allGuests().ToList().Exists(item => item.password == password) == true)
                    throw new Exception("הסיסמא כבר בשימוש");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GuestRequest> allGuestRequest()
        {
            return dal.allGuestRequest().ToList();
        }
        public List<Host> allHost()
        {
            return dal.allHost().ToList();
        }
       public List<Order> allOrder()
        {
            return dal.allOrder().ToList();
        }
        public List<HostingUnit> allHostingUnits()
        {
            return dal.allHostingUnits().ToList();
        }
        public HostingUnit hos(Order order)
        {
            try
            {
                var v = dal.allHostingUnits().FirstOrDefault(item => item.HostingUnitKey == order.HostingUnitKey);
                if (v == null)
                    throw new Exception("לא קיימת הזמנה עם יחידת האירוח הזאת");
                return v;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void mail(Order or)
        {
            HostingUnit ho = hos(or);
            or.Status = OrderStatus.EmailWasSent;
            MailMessage mail = new MailMessage();
            mail.To.Add(FindGuestRequest(or).MailAddress);
            mail.From = new MailAddress("TripNet100@gmail.com");
            mail.Subject = "הזמנה לנופש";
            mail.Body = "לקוח יקר! מצאנו עבורך יחידת אירוח המתאימה לדרישותיך" + "\n" + "שם יחידת האירוח:" + "  " + ho.HostingUnitName.ToString() + "\n" + "מיקום:" + "  " + ho.city.ToString() + "  " + ho.address.ToString() + "\n" + "מחיר ללילה:" + "  " + ho.priceForNight.ToString() + "\n" + "תגובות:" + "  " + ho.CommentList + "\n" + "להזמנת הנופש השב במייל לכתובת הבאה:" + "  " + ho.Owner.MailAddress;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("TripNet100@gmail.com", "tripnet123");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //or.Status = OrderStatus.EmailWasSent;

        }
        static bool bContinue = true;
        public void closeOrder()//להחזיר לרגיללללללללללללללללללל
        {
        //    Thread closeO = new Thread(cancleAllOldOrdets);
        //    closeO.Start();
        //    closeO.Abort();
        }
        public void cancleAllOldOrdets()//להחזיר לרגיללללללללללללללללללל
        {
        //    while (bContinue)
        //    {
        //        //close Orders... 
        //        var list = from x in dal.allOrder()
        //                   where x.Status == OrderStatus.EmailWasSent && (DateTime.Today - x.CreateDate).Days >= 30
        //                   select x;
        //        foreach (var item in list)
        //        {
        //            updateOrderStatus(item, OrderStatus.closedforUnansweredCustomer.ToString());
        //        }
        //        Thread.Sleep(500);
        //    }
        }
       //public double garden()
       // {
       //     try
       //     {
       //         double sum = 0;
       //         double allsum = 0;
       //         var v = dal.allHostingUnits();
       //         if (v.Count==0)
       //             throw new Exception("no hosting unit");
       //         else
       //         {
       //             foreach (var item in v)
       //             {
       //                 allsum++;
       //                 if (item.Garden == YesONo.Yes)
       //                     sum++;
       //             }
       //             return (sum / allsum) * 100;
       //         }
       //     }
       //     catch(Exception ex)
       //     {
       //         throw ex;
       //     }
        

    }


}
