using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    { 
        // GET: Account
        public ActionResult Index()
        {
            ViewData["isValid"] = true;
            return View();
        }
        //[HttpPost]
        public ActionResult SignUpMember(FormCollection Form)
        {
            
           // Debug.Print(Form.ToString());
            Debug.Print("In Sign Up controller");
            return View();
        }
        public ActionResult SignUpAdmin()
        {
            return View();
        }
        public ActionResult SignUpLeader()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(String Username, String Password)
        {
            Models.Account account = new Models.Account();
            if(account.isUserValid(Username, Password)){return View("SuccessTest");}
            else
            {
                ViewData["isValid"] = false;
                return View("Index");
            }
        }
        
    }
}