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

        // GET: Account
        public ActionResult Index()
        {
            ViewData["isValid"] = 0;
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(String DropChoice)
        {
            return View(DropChoice);
        }

        [HttpPost]
        public ActionResult SignIn(Account user)
        {

            user.MemberClass = user.login(user);
            ViewData["isValid"] = user.MemberClass;

            if (user.MemberClass == "Leader")
            {
                return account.Create(user) ? View("Index") : View();
            }
            else if(user.MemberClass == "Admin")
            {
                return account.Create(user) ? View("Index") : View();
            }
            else if(user.MemberClass == "Member")
            {
                return account.Create(user) ? View("Index") : View();
            }
            else
            {
                ViewData["isValid"] = "Fail";
                return View("Index");
            }
        }
        
        [HttpPost]
        public ActionResult CreateMember(Member Table)
        {

            string print = Table.isValid(Table);

            if (print != "Valid")
            {
                ViewData["isValid"] = print;
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
            string print = Table.isValid(Table);

            if (print != "Valid")
            {
                ViewData["isValid"] = print;
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
            string print = Table.isValid(Table);

            if (print != "Valid")
            {
                ViewData["isValid"] = print;
                return View("SignUpLeader");
            }
            else
            {
                Table.Init(Table);
                return View("Index");
            }
        }


    }
}