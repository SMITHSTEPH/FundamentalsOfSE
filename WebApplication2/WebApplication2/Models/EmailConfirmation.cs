using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace WebApplication2.Models
{
    public class EmailConfirmation
    {

        public string sendEmail(String Emailto)
        {
            SmtpClient Sender = new SmtpClient();
            Sender.Port = 587;
            Sender.Host = "smtp.gmail.com";
            Sender.Credentials = new System.Net.NetworkCredential("bradbergeron90@gmail.com", "allhailgizmo1234");
            Sender.EnableSsl = true;

            MailMessage mail = new MailMessage("bradbergeron90@gmail.com", Emailto);
            mail.Subject = "This mail is sent from asp.net application";
            mail.Body = "Please Click <a href=\"http://localhost:49882/Account/EmailVerificationPage\">Here</a> to Confirm your email";
            mail.IsBodyHtml = true;
            try
            {
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
                return errorMessage;
            }
        }
    }
}