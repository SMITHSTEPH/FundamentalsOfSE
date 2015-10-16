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
        public bool ConfirmEmail { get; set; }

        private RegistrationEntities1 db = new RegistrationEntities1();
        private Verifcation vf = new Verifcation();

        public Account Create(Account PossibleUser)
        {
            if(PossibleUser.Password != null)
            {

                return PossibleUser.Find(PossibleUser);

            }

            PossibleUser.Rank = "Fail";
            PossibleUser.ConfirmEmail = false;
            return PossibleUser;
            
        }

        private Account Find(Account PossibleUser)
        {
            string queryM = "SELECT * FROM memberTableV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
            memberTableV2 MT = db.memberTableV2.SqlQuery(queryM).SingleOrDefault();
            string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
            leaderTableV2 LT = db.leaderTableV2.SqlQuery(queryL).SingleOrDefault();
            string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
            administrationV2 AT = db.administrationV2.SqlQuery(queryA).SingleOrDefault();

            if (MT != null)
            {
                PossibleUser.Rank = "Member";
                PossibleUser.ConfirmEmail = MT.ConfirmEmail;
            }
            else if (LT != null)
            {
                PossibleUser.Rank = "Leader";
                PossibleUser.ConfirmEmail = MT.ConfirmEmail;
            }
            else if (AT != null)
            {
                PossibleUser.Rank = "Admin";
                PossibleUser.ConfirmEmail = MT.ConfirmEmail;
            }
            else
            {
                PossibleUser.Rank = "Fail";
                PossibleUser.ConfirmEmail = false;
            }

            return PossibleUser;
        }

    }
}