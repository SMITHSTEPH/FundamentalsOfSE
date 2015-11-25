//http://www.asp.net/mvc/overview/older-versions/getting-started-with-aspnet-mvc4/adding-a-model
//http://www.codeproject.com/Articles/639709/Getting-Data-From-View-to-Controller-in-MVC

using System.Linq;
using System;

namespace WebApplication2.Models
{
    public class Member : Account
    {
        public string FirstName { get; set; } //define properties for a Member
        public string LastName { get; set; }
        public string OptionalPhoneNumber { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }

        /****
        Adding a row to the Memeber table in the Registration DB
        ****/
        public virtual void Init(Member Table)
        {
                db.memberTableV2.Add(new memberTableV2
                {
                    UserName = Table.UserName,
                    Email = Table.Email,
                    ConfirmEmail = Table.ConfirmEmail,
                    OptionalPhoneNumber = Table.OptionalPhoneNumber,
                    Password = vf.Encrypt(Table.Password),
                    Address = Table.Address,
                    BirthDate = Table.Birthdate,
                    Gender = Table.Gender,
                    FirstName = Table.FirstName,
                    MiddleName = Table.MiddleName,
                    LastName = Table.LastName,
                    PhoneNumber = Table.PhoneNumber
                });
                db.SaveChanges();

        }

        public string isValid(Member Self)
        {
            if (FindMember(Self.UserName, Self.Email))
               return "Exist";
            else if (IsNullMember(Self))
               return "NullFields";
            else
               return vf.PasswordCheck(Self.Password);
        }

        public Boolean IsNullMember(Member Table)
        {
            if (vf.IsNull(Table.UserName) ||
                     vf.IsNull(Table.Email) ||
                     vf.IsNull(Table.Password) ||
                     vf.IsNull(Table.Address) ||
                     vf.IsNull(Table.Birthdate) ||
                     vf.IsNull(Table.Gender) ||
                     vf.IsNull(Table.FirstName) ||
                     vf.IsNull(Table.LastName) ||
                     vf.IsNull(Table.PhoneNumber))
            {
                return true;
            }
            else return false;
        }

        public bool FindMember(string UserName, string Email)
        {
            try
            {
                string queryM = "SELECT * FROM memberTableV2 WHERE UserName='" + UserName + "'";
                memberTableV2 MT = db.memberTableV2.SqlQuery(queryM).SingleOrDefault();

                string queryM2 = "SELECT * FROM memberTableV2 WHERE Email='" + Email + "'";
                memberTableV2 MT2 = db.memberTableV2.SqlQuery(queryM2).SingleOrDefault();

                if (MT != null)return true;
                if (MT2 != null)return true;
                return false;
            }
            catch{return true;}
        }

    }
}