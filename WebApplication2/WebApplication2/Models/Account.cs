using System.Linq;
using System.Net.Mail;
using System;
using System.Data.Entity;

namespace WebApplication2.Models
{
    public class Account
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Rank { get; set;}
        public bool ConfirmEmail { get; set; }
        public string Email { get; set; }
        public int AccountId { get; set; }
        
        protected Verifcation vf = new Verifcation();
        protected RegistrationEntities1 db = new RegistrationEntities1();

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
                memberTableV2 MT = db.memberTableV2.SqlQuery(queryM).SingleOrDefault();
                string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
                leaderTableV2 LT = db.leaderTableV2.SqlQuery(queryL).SingleOrDefault();
                string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + UserName + "' AND Password='" + vf.Encrypt(Password) + "'";
                administrationV2 AT = db.administrationV2.SqlQuery(queryA).SingleOrDefault();

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

        public string sendEmail(String Emailto, string User)
        {
            try
            {
                SmtpClient Sender = new SmtpClient();
                Sender.Port = 587;
                Sender.Host = "smtp.gmail.com";
                Sender.Credentials = new System.Net.NetworkCredential("bradbergeron90@gmail.com", "allhailgizmo1234");
                Sender.EnableSsl = true;

                MailMessage mail = new MailMessage("bradbergeron90@gmail.com", Emailto);
                mail.Subject = "This mail is sent from asp.net application";
                mail.Body = "Please Click <a href=\"http://localhost:49882/Account/EmailVerificationPage?UserName=" + User + "\">Here</a> to Confirm your email";
                mail.IsBodyHtml = true;

                Sender.Send(mail);
                return "Sent";
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                return "Fail";
            }
        }


        public string UpdateConfirmation(string UserName)
        {
            string queryM = "SELECT * FROM memberTableV2 WHERE UserName='" + UserName + "'";
            memberTableV2 MT = db.memberTableV2.SqlQuery(queryM).SingleOrDefault();
            string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + UserName + "'";
            leaderTableV2 LT = db.leaderTableV2.SqlQuery(queryL).SingleOrDefault();
            string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + UserName + "'";
            administrationV2 AT = db.administrationV2.SqlQuery(queryA).SingleOrDefault();

            if (MT != null)
            {
                MT.ConfirmEmail = true;
                db.Entry(MT).State = EntityState.Modified;
            }
            else if (LT != null)
            {
                LT.ConfirmEmail = true;
                db.Entry(LT).State = EntityState.Modified;
            }
            else if (AT != null)
            {
                AT.ConfirmEmail = true;
                db.Entry(AT).State = EntityState.Modified;
            }
            else
            {
                return "Error";
            }

            db.SaveChanges();
            return "Verified";
        }
    }
}