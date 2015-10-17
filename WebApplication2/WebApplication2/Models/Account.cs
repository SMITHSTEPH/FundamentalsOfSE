using System.Linq;

namespace WebApplication2.Models
{
    public class Account
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Rank { get; set;}
        public bool ConfirmEmail { get; set; }
        public string Email { get; set; }
        
        private Verifcation vf = new Verifcation();
        private RegistrationEntities1 activeDb = new RegistrationEntities1();

        public Account Create(Account PossibleUser)
        {
            if(PossibleUser.Password != null)
            {

                return PossibleUser.Find(PossibleUser);

            }

            PossibleUser.Rank = "Fail";
            return PossibleUser;
            
        }

        private Account Find(Account PossibleUser)
        {
                string queryM = "SELECT * FROM memberTableV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
                memberTableV2 MT = activeDb.memberTableV2.SqlQuery(queryM).SingleOrDefault();
                string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
                leaderTableV2 LT = activeDb.leaderTableV2.SqlQuery(queryL).SingleOrDefault();
                string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
                administrationV2 AT = activeDb.administrationV2.SqlQuery(queryA).SingleOrDefault();

                if (MT != null)
                {
                    PossibleUser.Rank = "Member";
                    PossibleUser.ConfirmEmail = MT.ConfirmEmail;
                    PossibleUser.Email = MT.Email;
                }
                else if (LT != null)
                {
                    PossibleUser.Rank = "Leader";
                    PossibleUser.ConfirmEmail = LT.ConfirmEmail;
                    PossibleUser.Email = LT.Email;
                }
                else if (AT != null)
                {
                    PossibleUser.Rank = "Admin";
                    PossibleUser.ConfirmEmail = AT.ConfirmEmail;
                    PossibleUser.Email = AT.Email;
                }
                else
                {
                    PossibleUser.Rank = "Fail";
                }

            return PossibleUser;
        }

        /*public void UpdateUsersEmail()
        {

        }*/


    }
}