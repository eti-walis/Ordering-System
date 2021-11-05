using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest

    {
        public YesONo Breakfast { get; set; }
        public long GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DesiredResortArea Area { get; set; }
        public TheAccommodationTypeUnit Type { get; set; }
        public int Couples { get; set; }
        public int Singles { get; set; }
        public IsInterested Pool { get; set; }
        public IsInterested Jacuzzi { get; set; }
        public IsInterested Garden { get; set; }
        public IsInterested ChildrensAttractions { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }
        //for debugging
        public override string ToString()
        {
            return GuestRequestKey.ToString();
        }
    }
}
