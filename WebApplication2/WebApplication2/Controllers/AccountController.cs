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
        private RegistrationEntities1 db = new RegistrationEntities1();

        // GET: Account
        public ActionResult Index()
        {
            ViewData["isValid"] = false;
            return View();
        }

        [HttpPost]
        public ActionResult SignUpMember(FormCollection form)
        {
            Models.Member model = new Models.Member();
            return account.Create(model) ? View("Index") : View();
        }
        public ActionResult SignUpAdmin()
        {
            Models.Member model = new Models.Administrator();
            return account.Create(model) ? View("Index") : View();
        }
        public ActionResult SignUpLeader()
        {
            ViewData["LeaderKey"] = true;
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
                return View("SignUpLeader");
            }
            if (AT != null)
            {
                return View("SignUpAdmin");
            }
            if (MT != null)
            {
                return View("SignUpMember");
            }


            ViewData["isValid"] = false;
            return View("Index");

        }
        
        [HttpPost]
        public ActionResult CreateMember(Member Table)
        {

            Table.Password

            db.memberTables.Add(new memberTable {
                UserName = Table.UserName,
                Email = Table.Email,
                Password = Encrypt(Table.Password),
                Address = Table.Address,
                BirthDate = Table.Birthdate,
                Gender = Table.Gender,
                FirstName = Table.FirstName,
                LastName = Table.LastName,
                PhoneNumber= Table.PhoneNumber
            });
            db.SaveChanges();


            return View("Index");
        }

        [HttpPost]
        public ActionResult CreateLeader(Leader Table)
        {
            if(Table.LeaderKey == "5") {
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
            else
            {
                ViewData["LeaderKey"] = false;
                return View("SignUpLeader");
            }
        }

        [HttpPost]
        public ActionResult CreateAdmin(Administrator Table)
        {
            if (Table.AdminKey == "5")
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
            else
            {
                ViewData["AdminKey"] = false;
                return View("SignUpAdmin");
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