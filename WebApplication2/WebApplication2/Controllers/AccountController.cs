using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Text;
using System.IO;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    { 
        Models.Account account = new Models.Account();
        Verifcation Check = new Verifcation();
        private RegistrationEntities1 db = new RegistrationEntities1();

        // GET: Account
        public ActionResult Index()
        {
            ViewData["isValid"] = 0;
            return View();
        }

        [HttpPost]
        public ActionResult SignUpMember(FormCollection form)
        {
            Models.Member model = new Models.Member();
            ViewData["isValidLength"] = 0;
            ViewData["isValidChar"] = 0;
            ViewData["isValidNull"] = 0;
            return account.Create(model) ? View("Index") : View();
        }
        public ActionResult SignUpAdmin()
        {
            Models.Member model = new Models.Administrator();
            ViewData["isValidLength"] = 0;
            ViewData["isValidChar"] = 0;
            ViewData["isValidNull"] = 0;
            ViewData["LeaderKey"] = 0;
            return account.Create(model) ? View("Index") : View();
        }
        public ActionResult SignUpLeader()
        {
            ViewData["LeaderKey"] = 0;
            ViewData["isValidLength"] = 0;
            ViewData["isValidChar"] = 0;
            ViewData["isValidNull"] = 0;
            Models.Member model = new Models.Leader();
            return account.Create(model) ? View("Index") : View();
        }

        [HttpPost]
        public ActionResult SignUp(String DropChoice)
        {
            return View(DropChoice);
        }

        [HttpPost]
        public ActionResult SignIn(Account user)
        {

            string queryL = "SELECT * FROM leaderTable WHERE UserName='" + user.UserName + "' AND Password='" + Encrypt(user.Password) + "'";
            leaderTable LT = db.leaderTables.SqlQuery(queryL).SingleOrDefault();
            string queryA = "SELECT * FROM administration WHERE UserName='" + user.UserName + "' AND Password='" + Encrypt(user.Password) + "'";
            administration AT = db.administrations.SqlQuery(queryA).SingleOrDefault();
            string queryM = "SELECT * FROM memberTable WHERE UserName='" + user.UserName + "' AND Password='" + Encrypt(user.Password) + "'";
            memberTable MT = db.memberTables.SqlQuery(queryM).SingleOrDefault();

            if (LT != null)
            {
                ViewData["isValid"] = 2;
                return View("Index");
            }
            if (AT != null)
            {
                ViewData["isValid"] = 3;
                return View("Index");
            }
            if (MT != null)
            {
                ViewData["isValid"] = 4;
                return View("Index");
            }


            ViewData["isValid"] = 1;
            return View("Index");

        }
        
        [HttpPost]
        public ActionResult CreateMember(Member Table)
        {
            if (Check.IsNullMember(Table))
            {
                ViewData["isValidNull"] = 1;
                return View("SignUpMember");
            }
            else if (Check.PasswordCheck(Table.Password) == 1)
            {
                ViewData["isValidLength"] = 1;
                return View("SignUpMember");
            }
            else if (Check.PasswordCheck(Table.Password) == 2)
            {
                ViewData["isValidChar"] = 1;
                return View("SignUpMember");
            }
            else
            {
                 db.memberTables.Add(new memberTable
                 {
                     UserName = Table.UserName,
                     Email = Table.Email,
                     Password = Encrypt(Table.Password),
                     Address = Table.Address,
                     BirthDate = Table.Birthdate,
                     Gender = Table.Gender,
                     FirstName = Table.FirstName,
                     LastName = Table.LastName,
                     PhoneNumber = Table.PhoneNumber
                 });
                 db.SaveChanges();


                 return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateLeader(Leader Table)
        {

            if (Check.IsNullMember(Table))
            {
                ViewData["isValidNull"] = 1;
                return View("SignUpMember");
            }
            else if (Check.PasswordCheck(Table.Password) == 1)
            {
                ViewData["isValidLength"] = 1;
                return View("SignUpMember");
            }
            else if (Check.PasswordCheck(Table.Password) == 2)
            {
                ViewData["isValidChar"] = 1;
                return View("SignUpMember");
            }
            else if(Table.LeaderKey != "5") {
               
                ViewData["LeaderKey"] = 1;
                return View("SignUpLeader");
            }
            else
            {
                db.leaderTables.Add(new leaderTable
                {
                    UserName = Table.UserName,
                    Email = Table.Email,
                    Password = Encrypt(Table.Password),
                    Address = Table.Address,
                    BirthDate = Table.Birthdate,
                    Gender = Table.Gender,
                    FirstName = Table.FirstName,
                    LastName = Table.LastName,
                    PhoneNumber = Table.PhoneNumber
                });
                db.SaveChanges();

                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateAdmin(Administrator Table)
        {
            if (Check.IsNullMember(Table))
            {
                ViewData["isValidNull"] = 1;
                return View("SignUpMember");
            }
            else if (Check.PasswordCheck(Table.Password) == 1)
            {
                ViewData["isValidLength"] = 1;
                return View("SignUpMember");
            }
            else if (Check.PasswordCheck(Table.Password) == 2)
            {
                ViewData["isValidChar"] = 1;
                return View("SignUpMember");
            } 
            else if (Table.AdminKey != "5")
            {
                ViewData["AdminKey"] = 1;
                return View("SignUpAdmin");
               
            }
            else
            {
                db.administrations.Add(new administration
                {
                    UserName = Table.UserName,
                    Email = Table.Email,
                    Password = Encrypt(Table.Password),
                    Address = Table.Address,
                    BirthDate = Table.Birthdate,
                    Gender = Table.Gender,
                    FirstName = Table.FirstName,
                    LastName = Table.LastName,
                    PhoneNumber = Table.PhoneNumber
                });
                db.SaveChanges();

                return View("Index");
            }
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}