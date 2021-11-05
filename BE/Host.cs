using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BE
{
    public class Host
    {
        public string userName { get; set; }
        public string password { get; set; }
        public long HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public long FhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public int numOfHostingUnits { get; set; }//we added it
        public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public YesONo CollectionClearance { get; set; }
        // for debugging
        public override string ToString()
        {
            return HostKey.ToString();
        }
    }
}
