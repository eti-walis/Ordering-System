using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDal
    {
        void addHost(Host host);
        void addClientRequest(GuestRequest gr);
        void addGuest(Guest gs);
        void updateClientRequestStatus(GuestRequest gr);
        void AddHostingUnit(HostingUnit ho);
        void deleteHostingUnit(HostingUnit hu);
        void updateHostingUnit(HostingUnit ho);
        void AddOrder(Order or);
        void UpdateOrder(Order or);
       // void UpdateGuest(Guest gs, string userName1, string password1);
        List<HostingUnit> allHostingUnits();
        List<Host> allHost();
        List<Guest> allGuests();
        List<GuestRequest> allGuestRequest();
        List<Order> allOrder();
        List<BankBranch> allBankBranch();
      //  void addComment(HostingUnit ho, string str);// add comment
    }
}
