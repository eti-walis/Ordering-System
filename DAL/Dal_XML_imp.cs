using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using BE;
using System.Xml.Serialization;
using System.Net;
namespace DAL
{
    class Dal_XML_imp : IDal
    {
        //Roots and paths of the files

        XElement GuestRequestRoot;
        XElement HostingUnitRoot;
        XElement OrderRoot;
        XElement ConfigurationRoot;
        XElement GuestRoot;
        XElement HostRoot;
        

        string GuestRequestRootPath = @"XMLGuestRequest.xml";
        string HostingUnitRootPath = @"XMLHostingUnit.xml";
        string OrderRootPath = @"XMLOrder.xml";
        string ConfigurationRootPath = @"XMLConfiguration.xml";
        string GuestRootPath = @"XMLGuest.xml";
        string HostRootPath = @"XMLHost.xml";


        XElement banksRoot;
        string banksPath = @"atm.xml";

        public Dal_XML_imp()
        {
            if (!File.Exists(banksPath))
            {
                // WebClient wc = new WebClient();
                try
                {
                    downloadAtm();
                    new Thread(downloadAtm).Start();
                    while (!File.Exists(banksPath))
                    {
                        Thread.Sleep(1000 * 10);
                    }
                    banksRoot = XElement.Load(banksPath);
                }
                catch
                {
                    throw new FileLoadException("file upload problem");
                }
            }
            else
            {
                banksRoot = XElement.Load(banksPath);
            }
            //check if files are 
            if (!File.Exists(banksPath))
            {
                WebClient wc = new WebClient();
                try
                {
                    string xmlServerPath =
                   @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                    wc.DownloadFile(xmlServerPath, banksPath);
                }
                catch (Exception)
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, banksPath);
                }
                finally
                {
                    wc.Dispose();
                }
            }
            else
            {
                banksRoot = XElement.Load(banksPath);
            }
            if (!File.Exists(GuestRequestRootPath))
            {
                GuestRequestRoot = new XElement("GuestRequests");
                GuestRequestRoot.Save(GuestRequestRootPath);

            }
            else
            {
                try {
                    GuestRequestRoot = XElement.Load(GuestRequestRootPath);
                }
                catch { Console.WriteLine("File upload problem"); }
            }

            if (!File.Exists(HostingUnitRootPath))
            {
                HostingUnitRoot = new XElement("HostingUnits");
                HostingUnitRoot.Save(HostingUnitRootPath);
            }
            else
            {
                try { HostingUnitRoot = XElement.Load(HostingUnitRootPath); }
                catch { Console.WriteLine("File upload problem"); }
            }
            if (!File.Exists(GuestRootPath))
            {
                GuestRoot = new XElement("Guest");
                GuestRoot.Save(GuestRootPath);
            }
            else
            {
                try { GuestRoot = XElement.Load(GuestRootPath); }
                catch { Console.WriteLine("File upload problem"); }
            }

            if (!File.Exists(OrderRootPath))
            {
                OrderRoot = new XElement("Orders");
                OrderRoot.Save(OrderRootPath);
            }
            else
            {
                try { OrderRoot = XElement.Load(OrderRootPath); }
                catch { Console.WriteLine("File upload problem"); }
            }
            if (!File.Exists(HostRootPath))
            {
                HostRoot = new XElement("Host");
                HostRoot.Save(HostRootPath);
            }
            else
            {
                try { HostRoot = XElement.Load(HostRootPath); }
                catch { Console.WriteLine("File upload problem"); }
            }

            if (!File.Exists(ConfigurationRootPath))
            {
                ConfigurationRoot = new XElement("Configurations");
                ConfigurationRoot.Add(new XElement("GuestRequestKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("HostingUnitKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("OrderKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("hostKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("Fee", 10));
                ConfigurationRoot.Save(ConfigurationRootPath);
            }
            else
            {
                try { ConfigurationRoot = XElement.Load(ConfigurationRootPath); }
                catch { Console.WriteLine("File upload problem"); }
            }

        }

        XElement ConverGRToXElement(GuestRequest gr)
        {
            XElement convered = new XElement("GuestRequest");

            convered.Add(new XElement("PrivetName", gr.PrivateName));
            convered.Add(new XElement("Adults", gr.Couples));
            convered.Add(new XElement("Area", gr.Area));
            convered.Add(new XElement("Children", gr.Singles));
            convered.Add(new XElement("ChildrensAttractions", gr.ChildrensAttractions));
            convered.Add(new XElement("EntryDate", gr.EntryDate));
            convered.Add(new XElement("FamilyName", gr.FamilyName));
            convered.Add(new XElement("Garden", gr.Garden));
            convered.Add(new XElement("GuestRequestKey", gr.GuestRequestKey));
            convered.Add(new XElement("Jacuzzi", gr.Jacuzzi));
            convered.Add(new XElement("MailAddress", gr.MailAddress));
            convered.Add(new XElement("Pool", gr.Pool));
            convered.Add(new XElement("RegistrationDate", gr.RegistrationDate));
            convered.Add(new XElement("ReleaseDate", gr.ReleaseDate));
            convered.Add(new XElement("StartingPrice", gr.minPrice));
            convered.Add(new XElement("maxPrice", gr.maxPrice));
            convered.Add(new XElement("Status", gr.Status));
            convered.Add(new XElement("Type", gr.Type));
            convered.Add(new XElement("breakfast", gr.Breakfast));

            return convered;
        }
        XElement ConverGToXElement(Guest gr)
        {
            XElement convered = new XElement("Guest");

            convered.Add(new XElement("PrivetName", gr.firstName));
            convered.Add(new XElement("lastName", gr.lastName));
            convered.Add(new XElement("password", gr.password));
            convered.Add(new XElement("userName", gr.userName));
            convered.Add(new XElement("email", gr.email));
            return convered;
        }
        


        XElement ConverHostingUnitToXElement(HostingUnit hu)
        {
            XElement convered = new XElement("HostingUnit");

            convered.Add(new XElement("HostingUnitKey", hu.HostingUnitKey));
            convered.Add(new XElement("HostingUnitName", hu.HostingUnitName));
            //convered.Add(new XElement("Diary", hu.DiaryDto));
            convered.Add(hu.DiaryDto);
            convered.Add(new XElement("address", hu.address));
            convered.Add(new XElement("priceForNight", hu.priceForNight));
            convered.Add(new XElement("Area", hu.Area));
            convered.Add(new XElement("Type", hu.type));
            convered.Add(new XElement("NumOfPeople", hu.numOfBeds));
            convered.Add(new XElement("numOfRoomsForTwo", hu.numOfRoomsForTwo));
            convered.Add(new XElement("Pool", hu.pool));
            convered.Add(new XElement("Garden", hu.Garden));
            convered.Add(new XElement("Jacuzzi", hu.Jacuzzi));
            convered.Add(new XElement("city", hu.city));
            convered.Add(new XElement("Breakfast", hu.Breakfast));
            convered.Add(new XElement("ChildrensAttractions", hu.ChildrensAttractions));
            convered.Add(new XElement("CommentList", hu.CommentList));


            XElement hostXelement = new XElement("Owner");
            XElement BankBranchXelement = new XElement("BankBranchDetails");
            BankBranchXelement.Add(new XElement("BankNumber", hu.Owner.BankBranchDetails.BankNumber));
            BankBranchXelement.Add(new XElement("BankName", hu.Owner.BankBranchDetails.BankName));
            BankBranchXelement.Add(new XElement("BranchNumber", hu.Owner.BankBranchDetails.BranchNumber));
            BankBranchXelement.Add(new XElement("BranchAddress", hu.Owner.BankBranchDetails.BranchAddress));
            BankBranchXelement.Add(new XElement("BranchCity", hu.Owner.BankBranchDetails.BranchCity));


            hostXelement.Add(new XElement("HostKey", hu.Owner.HostKey));
            hostXelement.Add(new XElement("userName", hu.Owner.userName));
            hostXelement.Add(new XElement("password", hu.Owner.password));
            hostXelement.Add(new XElement("PrivateName", hu.Owner.PrivateName));
            hostXelement.Add(new XElement("FamilyName", hu.Owner.FamilyName));
            hostXelement.Add(new XElement("PhoneNumber", hu.Owner.FhoneNumber));
            hostXelement.Add(new XElement("MailAddress", hu.Owner.MailAddress));
            hostXelement.Add(new XElement("numOfHostingUnits", hu.Owner.numOfHostingUnits));
            hostXelement.Add(BankBranchXelement);
            hostXelement.Add(new XElement("BankAccountNumber", hu.Owner.BankAccountNumber));
            hostXelement.Add(new XElement("CollectionClearance", hu.Owner.CollectionClearance));

            convered.Add(hostXelement);

            return convered;
        }

        XElement ConverOrderToXElement(Order o)
        {
            XElement convered = new XElement("Order");

            convered.Add(new XElement("HostingUnitKey", o.HostingUnitKey));
            convered.Add(new XElement("hostKey", o.hostKey));
            convered.Add(new XElement("GuestRequestKey", o.GuestRequestKey));
            convered.Add(new XElement("OrderKey", o.OrderKey));
            convered.Add(new XElement("Status", o.Status));
            convered.Add(new XElement("CreateDate", o.CreateDate));
            convered.Add(new XElement("totalPrice", o.totalPrice));

            return convered;
        }
        XElement ConverHToXElement(Host o)
        {
            XElement convered = new XElement("host");

            convered.Add(new XElement("userName", o.userName));
            convered.Add(new XElement("password", o.password));
            convered.Add(new XElement("HostKey", o.HostKey));
            convered.Add(new XElement("PrivateName", o.PrivateName));
            convered.Add(new XElement("FamilyName", o.FamilyName));
            convered.Add(new XElement("FhoneNumber", o.FhoneNumber));
            convered.Add(new XElement("MailAddress", o.MailAddress));
            convered.Add(new XElement("numOfHostingUnits", o.numOfHostingUnits));
            convered.Add(new XElement("BankBranchDetails", o.BankBranchDetails));

            XElement BankBranchXelement = new XElement("BankBranchDetails");
            BankBranchXelement.Add(new XElement("BankNumber", o.BankBranchDetails.BankNumber));
            BankBranchXelement.Add(new XElement("BankName", o.BankBranchDetails.BankName));
            BankBranchXelement.Add(new XElement("BranchNumber", o.BankBranchDetails.BranchNumber));
            BankBranchXelement.Add(new XElement("BranchAddress", o.BankBranchDetails.BranchAddress));
            BankBranchXelement.Add(new XElement("BranchCity", o.BankBranchDetails.BranchCity));

            convered.Add(new XElement("CollectionClearance", o.CollectionClearance));
            convered.Add(new XElement("BankAccountNumber", o.BankAccountNumber));
            return convered;
        }

        BankBranch ConvertBankBranch(XElement element)
        {
            return new BankBranch()
            {
                BankNumber = int.Parse(element.Element("קוד_בנק").Value),
                BankName = element.Element("שם_בנק").Value,
                BranchNumber = int.Parse(element.Element("קוד_סניף").Value),
                BranchAddress = element.Element("כתובת_ה-ATM").Value,
                BranchCity = element.Element("ישוב").Value
            };
        }

        //public List<BankBranch> allBankBranch()
        //{
        //    //return (from item in banksRoot.Elements()
        //    //        let a = ConvertBankBranch(item)
        //    //        select a).GroupBy(x => (x.BranchAddress + x.BranchCity)).Select(x => x.First()).ToList();

        //}

        public List<BankBranch> allBankBranch()
        {
            return (from item in banksRoot.Elements()
                    let a = ConvertBankBranch(item)
                    select a).ToList();
        }

            public void addClientRequest(GuestRequest gr)
        {
            try
            {

                //var x = ConverGRToXElement(gr); 
                XElement GuestRequestElement = (from p in GuestRequestRoot.Elements()
                                                where Convert.ToInt64(p.Element("GuestRequestKey").Value) == gr.GuestRequestKey
                                                select p).FirstOrDefault();

                if (GuestRequestElement != null)
                    throw new Exception("Request is already exist");

                else
                {
                    int config = Convert.ToInt32(ConfigurationRoot.Element("GuestRequestKeySeq").Value);
                    config++;
                    ConfigurationRoot.Element("GuestRequestKeySeq").Value = Convert.ToString(config);
                    ConfigurationRoot.Save(ConfigurationRootPath);

                    gr.GuestRequestKey = config;
                    gr.RegistrationDate = DateTime.Now;
                    gr.Status = RequestStatus.Open;

                    //GuestRequestRoot.Add(ConverGRToXElement(gr));
                    GuestRequestRoot.Save(GuestRequestRootPath);
                }
            }
            catch (Exception )
            {
                throw;
            }
        }
        public void addHost(Host gr)
        {
            try
            {
                XElement hostElement = (from p in HostRoot.Elements()
                                        where Convert.ToInt32(p.Element("HostKey").Value) == gr.HostKey
                                        select p).FirstOrDefault();

                if (hostElement != null)
                    throw new Exception("host is already exist");

                else
                {
                    int config = Convert.ToInt32(ConfigurationRoot.Element("hostKeySeq").Value);
                    config++;
                    ConfigurationRoot.Element("hostKeySeq").Value = Convert.ToString(config);
                    ConfigurationRoot.Save(ConfigurationRootPath);

                    gr.HostKey = config;
                    HostRoot.Add(ConverHToXElement(gr));
                    HostRoot.Save(GuestRequestRootPath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void addGuest(Guest gr)
        {
            try
            {
                XElement guestElement = (from p in GuestRoot.Elements()
                                         where Convert.ToInt32(p.Element("password").Value) == int.Parse(gr.password)
                                         select p).FirstOrDefault();

                if (guestElement != null)
                    throw new Exception("guest is already exist");

                else
                {
                    //int config = Convert.ToInt32(ConfigurationRoot.Element("guestKeySeq").Value);
                    //config++;
                    //ConfigurationRoot.Element("gustKeySeq").Value = Convert.ToString(config);
                    //ConfigurationRoot.Save(ConfigurationRootPath);

                    //gr.GuestKey = config;
                    GuestRoot.Add(ConverGToXElement(gr));
                    GuestRoot.Save(GuestRequestRootPath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddHostingUnit(HostingUnit hu)
        {
            try
            {
                XElement HostingUnitElement = (from p in HostingUnitRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
                                               select p).FirstOrDefault();

                if (HostingUnitElement != null)
                    throw new Exception("HostingUnit is already exist");
                else
                {
                    int config = Convert.ToInt32(ConfigurationRoot.Element("HostingUnitKeySeq").Value);
                    config++;
                    ConfigurationRoot.Element("HostingUnitKeySeq").Value = Convert.ToString(config);
                    ConfigurationRoot.Save(ConfigurationRootPath);
                    hu.HostingUnitKey = config;
                    hu.Diary = new bool[13, 32];
                    for (int i = 1; i < 13; i++)
                    {
                        for (int j = 1; j < 32; j++)
                        {
                            hu.Diary[i, j] = false;
                        }
                    }

                    HostingUnitRoot.Add(ConverHostingUnitToXElement(hu));
                    HostingUnitRoot.Save(HostingUnitRootPath);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void AddOrder(Order o)
        {
            try
            {

                XElement OrderElement = (from p in OrderRoot.Elements()
                                         where Convert.ToInt32(p.Element("OrderKey").Value) == o.OrderKey
                                         select p).FirstOrDefault();

                if (OrderElement != null)
                    throw new Exception("Order is already exist");
                else
                {
                    int config = Convert.ToInt32(ConfigurationRoot.Element("OrderKeySeq").Value);
                    config++;
                    ConfigurationRoot.Element("OrderKeySeq").Value = Convert.ToString(config);
                    ConfigurationRoot.Save(ConfigurationRootPath);

                    o.OrderKey = config;
                    o.CreateDate = DateTime.Now;
                    o.Status = OrderStatus.NoAddressed;

                    OrderRoot.Add(ConverOrderToXElement(o));
                    OrderRoot.Save(OrderRootPath);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteHostingUnit(HostingUnit hu)
        {
            try
            {

                XElement HostingUnitElement = (from p in HostingUnitRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement == null)
                    throw new Exception("this element is not exist for deleting");

                HostingUnitElement.Remove();

                HostingUnitRoot.Save(HostingUnitRootPath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BankBranch> ReturnBankBranchList()
        {
            throw new NotImplementedException();
        }
        //public RequestStatus ConverFromString(string os)
        // {
        //     if (os==" Open")
        //     {
        //         return RequestStatus.Open;
        //     }
        //     if (os== "SiteClose")
        //     {
        //         return RequestStatus.SiteClose;
        //     }
        //     if (os == "Expired")
        //     {
        //         return RequestStatus.Expired;
        //     }
        //     else
        //     {
        //         throw new Exception ("wrong status input");
        //     }


        //}
        public List<Guest> allGuests()
        {
            List<Guest> list = new List<Guest>();
            list = (from p in GuestRoot.Elements()
                    select new Guest()
                    {
                        firstName = (p.Element("firstname").Value),
                        lastName = (p.Element("FamilyName").Value),
                        email = (p.Element("MailAddress").Value),
                        userName = (p.Element("username").Value),
                        password = (p.Element("password").Value),
                    }).ToList();
            return list;
        }
        public List<GuestRequest> allGuestRequest()
        {
            List<GuestRequest> list = new List<GuestRequest>();
            list = (from p in GuestRequestRoot.Elements()
                    select new GuestRequest()
                    {
                        GuestRequestKey = int.Parse(p.Element("GuestRequestKey").Value),
                        PrivateName = (p.Element("PrivetName").Value),
                        FamilyName = (p.Element("FamilyName").Value),
                        MailAddress = (p.Element("MailAddress").Value),
                        Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), p.Element("Status").Value),
                        RegistrationDate = DateTime.Parse(p.Element("RegistrationDate").Value),
                        EntryDate = DateTime.Parse(p.Element("EntryDate").Value),
                        ReleaseDate = DateTime.Parse(p.Element("ReleaseDate").Value),
                        Area = (DesiredResortArea)Enum.Parse(typeof(DesiredResortArea), p.Element("Area").Value),
                        Type = (TheAccommodationTypeUnit)Enum.Parse(typeof(TheAccommodationTypeUnit), p.Element("Type").Value),
                        Couples = int.Parse(p.Element("Adults").Value),
                        Singles = int.Parse(p.Element("Children").Value),
                        Pool = (IsInterested)Enum.Parse(typeof(IsInterested), p.Element("Pool").Value),
                        Jacuzzi = (IsInterested)Enum.Parse(typeof(IsInterested), p.Element("Jacuzzi").Value),
                        Breakfast = (YesONo)Enum.Parse(typeof(YesONo), p.Element("breakfast").Value),
                        Garden = (IsInterested)Enum.Parse(typeof(IsInterested), p.Element("Garden").Value),
                        ChildrensAttractions = (IsInterested)Enum.Parse(typeof(IsInterested), p.Element("ChildrensAttractions").Value),
                        minPrice = int.Parse(p.Element("StartingPrice").Value),
                        maxPrice = int.Parse(p.Element("maxPrice").Value),
                    }).ToList<GuestRequest>();
            return list;
        }
        public List<HostingUnit> allHostingUnits()
        {
            return (from p in HostingUnitRoot.Elements()
                    select new HostingUnit()
                    {

                        HostingUnitKey = int.Parse(p.Element("HostingUnitKey").Value),
                        HostingUnitName = (p.Element("HostingUnitName").Value),
                        address = (p.Element("address").Value),
                        DiaryDto = (p.Element("diary")),
                        city = (p.Element("city").Value),
                        Area = (DesiredResortArea)Enum.Parse(typeof(DesiredResortArea), p.Element("Area").Value),
                        type = (TheAccommodationTypeUnit)Enum.Parse(typeof(TheAccommodationTypeUnit), p.Element("Type").Value),
                        numOfBeds = int.Parse(p.Element("NumOfPeople").Value),
                        numOfRoomsForTwo = int.Parse(p.Element("NumOf2beds").Value),
                        pool = (YesONo)Enum.Parse(typeof(YesONo), p.Element("Pool").Value),
                        Jacuzzi = (YesONo)Enum.Parse(typeof(YesONo), p.Element("jacuzzi").Value),
                        Garden = (YesONo)Enum.Parse(typeof(YesONo), p.Element("garden").Value),
                        ChildrensAttractions = (YesONo)Enum.Parse(typeof(YesONo), p.Element("garden").Value),
                        Breakfast = (YesONo)Enum.Parse(typeof(YesONo), p.Element("Jacuzzi").Value),
                        priceForNight = Double.Parse(p.Element("price").Value),
                        Owner = new Host()
                        {
                            HostKey = long.Parse(p.Element("Owner").Element("HostKey").Value),
                            PrivateName = (p.Element("Owner").Element("PrivateName").Value),
                            FamilyName = (p.Element("Owner").Element("FamilyName").Value),

                            FhoneNumber = long.Parse(p.Element("Owner").Element("PhoneNumber").Value),
                            MailAddress = (p.Element("Owner").Element("MailAddress").Value),
                            BankAccountNumber = int.Parse(p.Element("Owner").Element("BankAccountNumber").Value),
                            CollectionClearance = (YesONo)Enum.Parse(typeof(YesONo), p.Element("CollectionClearance").Value),
                            BankBranchDetails = new BankBranch()
                            {
                                BankNumber = int.Parse(p.Element("Owner").Element("BankBranchDetails").Element("BankNumber").Value),
                                BankName = (p.Element("Owner").Element("BankBranchDetails").Element("BankName").Value),
                                BranchNumber = int.Parse(p.Element("Owner").Element("BankBranchDetails").Element("BranchNumber").Value),
                                BranchAddress = (p.Element("Owner").Element("BankBranchDetails").Element("BranchAddress").Value),
                                BranchCity = (p.Element("Owner").Element("BankBranchDetails").Element("BranchCity").Value),

                            }
                        }
                    }).ToList<HostingUnit>();
        }

        public List<Order> allOrder()
        {
            return (from p in OrderRoot.Elements()
                    select new Order()
                    {
                        HostingUnitKey = long.Parse(p.Element("HostingUnitKey").Value),
                        GuestRequestKey = long.Parse(p.Element("GuestRequestKey").Value),
                        OrderKey = long.Parse(p.Element("OrderKey").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), p.Element("Status").Value),
                        CreateDate = DateTime.Parse(p.Element("CreateDate").Value),
                    }).ToList<Order>();
        }
        public List<Host> allHost()
        {
            return (from p in HostRoot.Elements()
                    select new Host()
                    {
                        userName = (p.Element("userName").Value),
                        password = (p.Element("password").Value),
                        HostKey = long.Parse(p.Element("OrderKey").Value),
                        CollectionClearance = (YesONo)Enum.Parse(typeof(YesONo), p.Element("CollectionClearance").Value),
                        PrivateName = (p.Element("PrivateName").Value),
                        FamilyName = (p.Element("FamilyName").Value),
                        FhoneNumber = long.Parse(p.Element("FhoneNumber").Value),
                        MailAddress = (p.Element("FamilyName").Value),
                        numOfHostingUnits = int.Parse(p.Element("numOfHostingUnits").Value),
                        BankAccountNumber = int.Parse(p.Element("BankAccountNumber").Value),
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = int.Parse(p.Element("BankBranchDetails").Element("BankNumber").Value),
                            BankName = (p.Element("BankBranchDetails").Element("BankName").Value),
                            BranchNumber = int.Parse(p.Element("BankBranchDetails").Element("BranchNumber").Value),
                            BranchAddress = (p.Element("BankBranchDetails").Element("BranchAddress").Value),
                            BranchCity = (p.Element("BankBranchDetails").Element("BranchCity").Value),

                        }
                    }).ToList<Host>();
        }

        public void updateClientRequestStatus(GuestRequest gr)
        {
            try
            {
                var x = ConverGRToXElement(gr);
                XElement GuestRequestElement = (from p in GuestRequestRoot.Elements()
                                                where Convert.ToInt32(p.Element("GuestRequestKey").Value) == gr.GuestRequestKey
                                                select p).FirstOrDefault();
                if (GuestRequestElement == null)
                    throw new Exception("this element is not exist");

                GuestRequestElement.Remove();
                GuestRequestRoot.Add(x);
                GuestRequestRoot.Save(GuestRequestRootPath);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateGuest(Guest gr)
        {
            try
            {

                XElement GuestElement = (from p in GuestRoot.Elements()
                                         where Convert.ToInt32(p.Element("password").Value) == int.Parse(gr.password)
                                         select p).FirstOrDefault();
                if (GuestElement == null)
                    throw new Exception("this element is not exist");

                GuestElement.Remove();
                GuestRoot.Add(ConverGToXElement(gr));
                GuestRoot.Save(GuestRootPath);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public void updateHostingUnit(HostingUnit hu)
        {
            try
            {
                XElement HostingUnitElement = (from p in HostingUnitRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement == null)
                    throw new Exception("this element is not exist");
                HostingUnitElement.Remove();
                HostingUnitRoot.Add(ConverHostingUnitToXElement(hu));
                HostingUnitRoot.Save(HostingUnitRootPath);
            }
            catch (Exception)
            {

                throw;
            }

        }
        //public void UpdateHostingUnit(HostingUnit hu, string str)
        //{
        //    try
        //    {
        //        XElement HostingUnitElement = (from p in HostingUnitRoot.Elements()
        //                                       where Convert.ToInt32(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
        //                                       select p).FirstOrDefault();
        //        if (HostingUnitElement == null)
        //            throw new Exception("this element is not exist");
        //        HostingUnitElement.Remove();
        //        HostingUnitRoot.Add(ConverHostingUnitToXElement(hu));
        //        HostingUnitRoot.Save(HostingUnitRootPath);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        public void UpdateOrder(Order o)
        {
            try
            {
                XElement OrderElement = (from p in OrderRoot.Elements()
                                         where Convert.ToInt32(p.Element("OrderKey").Value) == o.OrderKey
                                         select p).FirstOrDefault();

                if (OrderElement == null)
                    throw new Exception("this element is not exist");
                OrderElement.Remove();
                OrderRoot.Add(ConverOrderToXElement(o));
                OrderRoot.Save(OrderRootPath);

            }
            catch (Exception)
            {

                throw;
            }
        }



            //Bank
            void downloadAtm()
        {
            XElement atm;
            try
            {
                atm = new XElement("ATMs");
                atm = XElement.Load("https://drive.google.com/u/0/uc?id=1FpcqslnRD6naLHOjrCvKArCg3Ihkb9hR&export=download");
                atm.Save(banksPath);
            }
            catch (Exception)
            {
                atm = new XElement("ATMs");
                atm = XElement.Load("http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml");
                atm.Save(banksPath);
            }
        }


    }
}

