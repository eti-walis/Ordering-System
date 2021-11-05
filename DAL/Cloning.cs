using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BE;
namespace DAL
{
    public static class Cloning
    {
        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest target = new GuestRequest();
            target.Breakfast = original.Breakfast;
            target.minPrice = original.minPrice;
            target.maxPrice = original.maxPrice;
            target.GuestRequestKey = original.GuestRequestKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.MailAddress = original.MailAddress;
            target.Status = original.Status;
            target.RegistrationDate = original.RegistrationDate;
            target.EntryDate = original.EntryDate;
            target.ReleaseDate = original.ReleaseDate;
            target.Area = original.Area;
            target.Type = original.Type;
            target.Couples = original.Couples;
            target.Singles = original.Singles;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            return target;
        }
        public static Host Clone(this Host original)
        {

            Host target = new Host();
            target.userName = original.userName;
            target.password = original.password;
            target.HostKey = original.HostKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.FhoneNumber = original.FhoneNumber;
            target.MailAddress = original.MailAddress;
            target.BankBranchDetails = original.BankBranchDetails;
            target.BankAccountNumber = original.BankAccountNumber;
            target.CollectionClearance = original.CollectionClearance;
            return target;

        }
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.address = original.address;
            target.city = original.city;
            target.Breakfast = original.Breakfast;
            target.priceForNight = original.priceForNight;
            target.pool = original.pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.numOfRoomsForTwo = original.numOfRoomsForTwo;
            target.numOfBeds = original.numOfBeds;
            target.Area = original.Area;
            target.CommentList = original.CommentList;
            target.type = original.type;
            target.HostingUnitKey = original.HostingUnitKey;
            target.Owner = original.Owner.Clone();
            target.HostingUnitName = original.HostingUnitName;
            target.Diary = original.Diary;
            return target;

        }
        public static Order Clone(this Order original)
        {

            Order target = new Order();
            target.hostKey = original.hostKey;
            target.HostingUnitKey = original.HostingUnitKey;
            target.GuestRequestKey = original.GuestRequestKey;
            target.OrderKey = original.OrderKey;
            target.Status = original.Status;
            target.CreateDate = original.CreateDate;
            target.totalPrice = original.totalPrice;
            return target;

        }
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.BankName = original.BankName;
            target.BankNumber = original.BankNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            target.BranchNumber = original.BranchNumber;
            return target;

        }
        //public static Attraction Clone(this Attraction original)
        //{
        //    Attraction target = new Attraction();
        //    target.name = original.name;
        //    target.address = original.address;
        //    target.phonenumber = original.phonenumber;
        //    return target;
        //}
        public static Guest Clone(this Guest original)
        {
            Guest target = new Guest();
            target.firstName = original.firstName;
            target.lastName = original.lastName;
            target.userName = original.userName;
            target.password = original.password;
            target.email = original.email;
            return target;
        }
    }
}
