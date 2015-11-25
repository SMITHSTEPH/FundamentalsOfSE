using System.Linq;
using System;

namespace WebApplication2.Models
{
    public class Leader : Account
	{
        public string FirstName { get; set; } //define properties
        public string LastName { get; set; }
        public string OptionalPhoneNumber { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string LeaderKey { get; set; }

        public void Init(Leader Table)
        {
                db.leaderTableV2.Add(new leaderTableV2
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

        public string isValid(Leader Self)
        {
            if (Self.LeaderKey != "5")
                return "LeaderKey";
            else if (IsNullLeader(Self))
                return "NullFields";
            else if (FindLeader(Self.UserName, Self.Email))
                return "Exist";
            else return vf.PasswordCheck(Self.Password);
        }

        public Boolean IsNullLeader(Leader Table)
        {
            if (vf.IsNull(Table.UserName) ||
                     vf.IsNull(Table.Email) ||
                     vf.IsNull(Table.Password) ||
                     vf.IsNull(Table.Address) ||
                     vf.IsNull(Table.Birthdate) ||
                     vf.IsNull(Table.Gender) ||
                     vf.IsNull(Table.FirstName) ||
                     vf.IsNull(Table.LastName) ||
                     vf.IsNull(Table.PhoneNumber) ||
                     vf.IsNull(Table.LeaderKey))
            {
                return true;
            }
            else return false;
        }

        public bool FindLeader(string UserName, string Email)
        {
            try
            {
                string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + UserName + "'";
                leaderTableV2 LT = db.leaderTableV2.SqlQuery(queryL).SingleOrDefault();

                string queryL2 = "SELECT * FROM leaderTableV2 WHERE Email='" + Email + "'";
                leaderTableV2 LT2 = db.leaderTableV2.SqlQuery(queryL2).SingleOrDefault();


                if (LT != null)return true;
                if (LT2 != null) return true;
                return false;
            }
            catch{return true;}
        }
    }
}