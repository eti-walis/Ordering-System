using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum RequestStatus { Open, SiteClose, Expired };
    public enum DesiredResortArea { All, North, South, Center };
    public enum TheAccommodationTypeUnit { Zimmer, Hotel, Camping, Etc };
    public enum IsInterested { Must, Possible, NotIntrested };
    public enum YesONo { Yes, No };
    public enum HostClient { Host, client };
    public enum OrderStatus { NoAddressed, EmailWasSent, closedforUnansweredCustomer, closedWithcustomerResponsetatus };

}
