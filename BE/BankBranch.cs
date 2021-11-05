using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace BE
{
    public class BankBranch
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        //for debugging
        public override string ToString()
        {
            if(BankName!=null)
            return BankName.ToString()+" סניף "+ BranchAddress.ToString();
            return BankNumber.ToString();
        }
    }

}
