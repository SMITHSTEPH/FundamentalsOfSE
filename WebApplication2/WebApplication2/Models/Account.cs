using System.Linq;
using System.Net.Mail;
using System;
using System.Data.Entity;

namespace WebApplication2.Models
{
    public class Account
    {
        /*These properties specifiy all the information a given user must have in order
        to have an account with this application*/
        public string UserName { get; set; } 
        public string Password { get; set; }
        public string Rank { get; set;} //0=Member; 1=Leader, 2=Admin
        public bool ConfirmEmail { get; set; } //True if the user have validated through email, false otherwise
        public string Email { get; set; }
        public int AccountId { get; set; }
        
        protected Verifcation vf = new Verifcation();
        protected RegistrationEntities1 db = new RegistrationEntities1(); //instance of the registration DB
        /****
        Verifies the possible user
        If the user filled out the username and password fields the try to find the user
        in the registration DB
        ****/
        public Account Verify(Account PossibleUser) 
        {
            if (PossibleUser.Password != null && PossibleUser.UserName != null) { return PossibleUser.Find(PossibleUser); }
            else
            {
                PossibleUser.Rank = "Fail";
                return PossibleUser;
            }
        }
        /**
        Method forms queries to look in the member, admin and leader DB. If the query returns
        anything other than null it is in the DB.
        **/
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
                    PossibleUser.AccountId = MT.Id;
                }
                else if (LT != null)
                {
                    PossibleUser.Rank = "Leader";
                    PossibleUser.ConfirmEmail = LT.ConfirmEmail;
                    PossibleUser.Email = LT.Email;
                    PossibleUser.AccountId = LT.Id;
                }
                else if (AT != null)
                {
                    PossibleUser.Rank = "Admin";
                    PossibleUser.ConfirmEmail = AT.ConfirmEmail;
                    PossibleUser.Email = AT.Email;
                    PossibleUser.AccountId = AT.Id;
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

        public string UpdatePassword(string UserName, string newPassword)
        {

            if(vf.PasswordCheck(newPassword) == "Valid")
            {
                string queryM = "SELECT * FROM memberTableV2 WHERE UserName='" + UserName + "'";
                memberTableV2 MT = db.memberTableV2.SqlQuery(queryM).SingleOrDefault();
                string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + UserName + "'";
                leaderTableV2 LT = db.leaderTableV2.SqlQuery(queryL).SingleOrDefault();
                string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + UserName + "'";
                administrationV2 AT = db.administrationV2.SqlQuery(queryA).SingleOrDefault();

                if (MT != null)
                {
                    MT.Password = vf.Encrypt(newPassword);
                    db.Entry(MT).State = EntityState.Modified;
                }
                else if (LT != null)
                {
                    LT.Password = vf.Encrypt(newPassword);
                    db.Entry(LT).State = EntityState.Modified;
                }
                else
                {
                    AT.Password = vf.Encrypt(newPassword);
                    db.Entry(AT).State = EntityState.Modified;
                }

                db.SaveChanges();
                return "Valid";
            }
            else
            {
                return "Fail";
            }

           
        }


        public string sendEmailPassword(String Emailto, string User)
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
                mail.Body = "Please Click <a href=\"http://localhost:49882/Account/EmailChangePassword?UserName=" + User + "\">Here</a> to change your password";
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