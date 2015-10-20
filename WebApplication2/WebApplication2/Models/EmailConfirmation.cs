using System;
using System.Net.Mail;
using System.Linq;
using System.Data.Entity;

namespace WebApplication2.Models
{
    public class EmailConfirmation
    {
        private RegistrationEntities1 activeDb = new RegistrationEntities1();

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
                mail.Body = "Please Click <a href=\"http://localhost:49882/Account/EmailVerificationPage?UserName="+User+"\">Here</a> to Confirm your email";
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
            memberTableV2 MT = activeDb.memberTableV2.SqlQuery(queryM).SingleOrDefault();
            string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + UserName + "'";
            leaderTableV2 LT = activeDb.leaderTableV2.SqlQuery(queryL).SingleOrDefault();
            string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + UserName + "'";
            administrationV2 AT = activeDb.administrationV2.SqlQuery(queryA).SingleOrDefault();

            if (MT != null)
            {
                MT.ConfirmEmail = true;
                activeDb.Entry(MT).State = EntityState.Modified;
            }
            else if (LT != null)
            {
                LT.ConfirmEmail = true;
                activeDb.Entry(LT).State = EntityState.Modified;
            }
            else if (AT != null)
            {
                AT.ConfirmEmail = true;
                activeDb.Entry(AT).State = EntityState.Modified;
            }
            else
            {
                return "Error";   
            }

            activeDb.SaveChanges();
            return "Verified";
        }
    }
}