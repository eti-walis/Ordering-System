using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace BE
{
    public class HostingUnit
    {
        public string address { get; set; }
        public string city { get; set; }
        public YesONo Breakfast { get; set; }
        public double priceForNight { get; set; }
        public YesONo pool { get; set; }
        public YesONo Jacuzzi { get; set; }
        public TheAccommodationTypeUnit type { get; set; }
        public YesONo Garden { get; set; }
        public YesONo ChildrensAttractions { get; set; }
        public int numOfRoomsForTwo { get; set; }
        public int numOfBeds { get; set; }//Number of beds apart from the double-child rooms.
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }

        public bool[,] Diary { get; set; }
        public XElement DiaryDto
        {
            get { return Diary.Flatten<XElement>(); }
            set
            {
                Diary = value.Expand<XElement>(13);
            }
        }

        public DesiredResortArea Area { get; set; }//we added it
        public string CommentList { get; set; }
     
        public override string ToString()
        {
            return HostingUnitKey.ToString();
        }
    }
}
