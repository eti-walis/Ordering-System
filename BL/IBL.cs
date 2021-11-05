using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using BE;
using DAL;
namespace BL
{
    public delegate bool conditionDelegate(GuestRequest gr);
    public interface IBL
    {
        GuestRequest FindGuestRequest(Order order);//Auxiliary function Find guest request from order.
        void deleteHostingUnit(long hostingUnitKey, string UName, string pass);
        void updateHostingUnit(HostingUnit ho);
        Order FindOrder(long orderKey);
        bool IsMail(string m);
        bool ApproveRequest(Order or);
        void addOrder(Order or);
        void addHost(Host host);
        void AddHostingUnit(HostingUnit ho);
        void addGuestRequest(GuestRequest gr);
        void updateOrderStatus(Order or, string newStatus);
        void closingDeal(Order or);// closing deal
        HostingUnit ifHostingUnit (long hostingUnitKey,string UName,string pass,bool flag);
        void RevokeAuthorization(Host h);//revoke authorization
        List<HostingUnit> AvailableHostingUnits(DateTime t, int numDays);//return list of HostingUnit that available between the dates he wants.
        int numOfDays(DateTime t, DateTime d);//return the num of days between 2 dates.
        int numOfDays(DateTime t);//return the num of days between now and the date that the user sent.
        List<Order> numOfOrders(int num);//all orders created a certain number of days ago
        List<GuestRequest> Deleg(conditionDelegate cd);//return all the GuestRequest that meets a certain condition
        int numOfOrdersForGuestRequest(GuestRequest gr);//num Of Orders For GuestRequest
        int numOfSucceedOrders(HostingUnit ho); //num Of Succeed Orders for specific HostingUnit
        //*Grouping
        IEnumerable<IGrouping<DesiredResortArea, GuestRequest>> GuestRequesGrouptByArea();
        IEnumerable<IGrouping<int, GuestRequest>> GuestRequesGrouptByNumOfVacationers();
        IEnumerable<IGrouping<int, Host>> GuestRequesGrouptByNumOfHostingUnit();
        IEnumerable<IGrouping<DesiredResortArea, HostingUnit>> HostingUnitGrouptByArea();
        //Grouping*
       // void addComment(long hostingUnitKey, string str);//add comment for specific HostingUnit
        void TotalPrice(Order or);//For more then 4 nights of vecation you get a 30% discount
        IEnumerable<GuestRequest> lessDetails();//select new
       // List<Attraction> getAttraction(HostingUnit ho);
        List<Order> getClosedforUnansweredCustomerOrder();// return all orders that Closed for Unanswered Customer.
        List<BankBranch> GetBankBranches();
        //conversion from enum to list for the combobox in the UI.
        List<DesiredResortArea> DesiredResortAreaList();
        List<TheAccommodationTypeUnit> TheAccommodationTypeUnitList();
        List<IsInterested> IsInterestedList();
        List<YesONo> YesONoList();
        List<OrderStatus> OrderStatusList();
        List<HostClient> HostClientList();
       // void updateGuest(Guest g, string userNme, string password);
        void addGuest(Guest gs);
        List<Guest> allGuests();
        void isExistGuest(string userName, string password);
        void isExistHost(string userName, string password);
        string userStatus(string userName, string password);
        List<HostingUnit> hostingUnitFromHost(Host h);
        List <Order> orderFromHost(Host h);
        Host hostByUserName(string UName);
        void checkUserNameAndPassword(string userName, string password);
        List<GuestRequest> allGuestRequest();
        List<Host> allHost();
        List<Order> allOrder();
        List<HostingUnit> allHostingUnits();
        HostingUnit hos(Order order);
        void mail(Order or);
        Thread StartTheThread(Order ord);
        void closeOrder();
        //double garden();
        void cancleAllOldOrdets();
    }

}
