//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BE;
//using DS;
//namespace DAL
//{
//    class MyDalList : IDal
//    {
//        public void addHost(Host host)
//        {
//            if (DataSource.hostList.Exists(item => item.HostKey == host.HostKey) == true)
//                throw new Exception("המארח קיים");
//            else
//            {
//                DataSource.hostList.Add(host.Clone());
//                Configuration.hostKeySeq++;
//            }
//        }
//        public void addClientRequest(GuestRequest gr)
//        {
//            if (DataSource.guestRequestList.Exists(item => item.GuestRequestKey == gr.GuestRequestKey) == true)
//                throw new Exception("הבקשה קיימת");
//            else
//            {
//                DataSource.guestRequestList.Add(gr.Clone());
//                Configuration.guestRequestKeySeq++;
//            }
//        }
//        public void AddHostingUnit(HostingUnit ho)
//        {
//            if (DataSource.hostingUnitlist.Exists(item => item.HostingUnitKey == ho.HostingUnitKey) == true)
//                throw new Exception("יחידת האירוח קיימת");
//            else
//            {
//                ho.Diary = new bool[12, 31];
//                for (int i = 0; i < 12; i++)
//                    for (int j = 0; j < 31; j++)
//                        ho.Diary[i, j] = false;
//                DataSource.hostingUnitlist.Add(ho.Clone());
//                Configuration.hostingUnitKeySeq++;
//            }
//        }
//        public void AddOrder(Order or)
//        {
//            if (DataSource.orderList.Exists(item => item.GuestRequestKey == or.OrderKey) == true)
//                throw new Exception("ההזמנה קיימת");
//            else
//            {
//                DataSource.orderList.Add(or.Clone());
//                Configuration.orderKeySeq++;
//            }
//        }
//        public IEnumerable<BankBranch> allBankBranch()//only for part A
//        {

//            return (from x in DataSource.BankBranchList
//                    select x.Clone()).ToList();

//        }
//        public IEnumerable<GuestRequest> allGuestRequest()
//        {
//            return (from item in DataSource.guestRequestList
//                    select item.Clone()).ToList();
//        }
//        public IEnumerable<HostingUnit> allHostingUnits()
//        {
//            return (from item in DataSource.hostingUnitlist
//                    select item.Clone()).ToList();
//        }
//        public IEnumerable<Host> allHost()
//        {
//            return (from item in DataSource.hostList
//                    select item.Clone()).ToList();
//        }
//        public IEnumerable<Order> allOrder()
//        {
//            return (from item in DataSource.orderList
//                    select item.Clone()).ToList();

//        }
//        public void deleteHostingUnit(HostingUnit hostingUnitKey)
//        {

//            if (DataSource.hostingUnitlist.Exists(item => item.HostingUnitKey == hostingUnitKey.HostingUnitKey) == true)//*if the hosting unit exists delete it.
//            {
//                var v = DataSource.hostingUnitlist.FirstOrDefault(item => item.HostingUnitKey == hostingUnitKey.HostingUnitKey);
//                DataSource.hostingUnitlist.Remove(v);
//            }
//            else
//                throw new Exception("יחידת האירוח לא קיימת");//if the hosting unit doesn't exists  throw exception
//        }
//        public void updateClientRequestStatus(GuestRequest gr)
//        {
//            int count = DataSource.guestRequestList.RemoveAll(item => gr.GuestRequestKey == item.GuestRequestKey);
//            if (count == 0)
//                throw new Exception("לא צוינו שינויים");
//            DataSource.guestRequestList.Add(gr);
//        }
//        public void updateHostingUnit(HostingUnit ho)
//        {
//            int count = DataSource.hostingUnitlist.RemoveAll(item => ho.HostingUnitKey == item.HostingUnitKey);
//            if (count == 0)
//                throw new Exception("לא צוינו שינויים");
//            DataSource.hostingUnitlist.Add(ho);
//        }
//        public void UpdateOrder(Order or)
//        {
//            int count = DataSource.orderList.RemoveAll(item => or.OrderKey == item.OrderKey);
//            if (count == 0)
//                throw new Exception("לא צוינו שינויים");
//            DataSource.orderList.Add(or);
//        }
//        public void addComment(HostingUnit ho, string str)
//        {
//            we have a list for comment for evry HostingUnit and here we add a comment for this HostingUnit.
//            string s = " *" + str;
//            if (ho.CommentList == null)
//                            ho.CommentList = s;
//                        else
//                            ho.CommentList += s;
//            updateHostingUnit(ho);
//        }
//        public List<Attraction> getAttraction(HostingUnit ho)
//        {
//            return (from item in ho.attractions
//                    select item).ToList();
//        }
//        public void addGuest(Guest gs)
//        {
//            if (DataSource.guestList.Exists(item => item.password == gs.password) == true)
//                throw new Exception("האורח קיים");
//            else
//                DataSource.guestList.Add(gs.Clone());
//        }
//        public void UpdateGuest(Guest gs, string userName1, string password1)
//        {
//            gs.userName = userName1;
//            gs.password = password1;
//        }
//        public IEnumerable<Guest> allGuests()
//        {
//            return (from item in DataSource.guestList
//                    select item.Clone()).ToList();
//        }
//    }
//}