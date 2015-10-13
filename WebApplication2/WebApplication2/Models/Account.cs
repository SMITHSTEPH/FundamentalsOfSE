using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Account
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Rank { get; set;}

        private RegistrationEntities1 db = new RegistrationEntities1();
        private Verifcation vf = new Verifcation();

        public Account Create(Account PossibleUser)
        {
            if(PossibleUser.Password != null)
            {
                if (PossibleUser.FindLeader(PossibleUser.UserName, PossibleUser.Password))
                {
                    PossibleUser.Rank = "Leader";
                    return PossibleUser;
                }
                else if (PossibleUser.FindAdmin(PossibleUser.UserName, PossibleUser.Password))
                {
                    PossibleUser.Rank = "Admin";
                    return PossibleUser;
                }
                else if (PossibleUser.FindMember(PossibleUser.UserName, PossibleUser.Password))
                {

                    PossibleUser.Rank = "Member";
                    return PossibleUser;
                }

            }

            PossibleUser.Rank = "Fail";
            return PossibleUser;
            
        }

        private Boolean FindMember(String UserName, String Password)
        {
            string queryM = "SELECT * FROM memberTable WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
            memberTable MT = db.memberTables.SqlQuery(queryM).SingleOrDefault();

            if (MT != null) return true;

            return false;
        }

        private Boolean FindLeader(String UserName, String Password)
        {
            string queryL = "SELECT * FROM leaderTable WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
            leaderTable LT = db.leaderTables.SqlQuery(queryL).SingleOrDefault();

            if (LT != null) return true;

            return false;
        }

        private Boolean FindAdmin(String UserName, String Password)
        {
            string queryA = "SELECT * FROM administration WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
            administration AT = db.administrations.SqlQuery(queryA).SingleOrDefault();

            if (AT != null) return true;

            return false;
        }

    }
}