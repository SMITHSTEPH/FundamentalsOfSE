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
        Account account = new Account();
        Verifcation Check = new Verifcation();

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

            int loggedIn = user.login(user);

            if(loggedIn == 2)
            {
                ViewData["isValid"] = 2;
                return View("Index");
            }
            if(loggedIn == 3)
            {
                ViewData["isValid"] = 3;
                return View("Index");
            }
            if(loggedIn == 4)
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

                Table.Init(Table);

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

                Table.Init(Table);
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
                Table.Init(Table);

                return View("Index");
            }
        }

    }
}