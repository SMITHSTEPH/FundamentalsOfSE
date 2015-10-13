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
        public string MemberClass { get; set;}

        private RegistrationEntities1 db = new RegistrationEntities1();
        private Verifcation vf = new Verifcation();

        public Boolean Create(Account user)
        {
            if(user.UserName != null && user.Password != null)
            {
                user.Password = vf.Decrypt(user.Password);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string login(Account user)
        {
            string queryL = "SELECT * FROM leaderTable WHERE UserName='" + user.UserName + "' AND Password='" + vf.Encrypt(user.Password) + "'";
            leaderTable LT = db.leaderTables.SqlQuery(queryL).SingleOrDefault();
            string queryA = "SELECT * FROM administration WHERE UserName='" + user.UserName + "' AND Password='" + vf.Encrypt(user.Password) + "'";
            administration AT = db.administrations.SqlQuery(queryA).SingleOrDefault();
            string queryM = "SELECT * FROM memberTable WHERE UserName='" + user.UserName + "' AND Password='" + vf.Encrypt(user.Password) + "'";
            memberTable MT = db.memberTables.SqlQuery(queryM).SingleOrDefault();

            if (LT != null)
            {
                return "Leader";
            }
            if (AT != null)
            {
                return "Admin";
            }
            if (MT != null)
            {
                return "Member";
            }

            return "Fail";
        }
        
    }
}