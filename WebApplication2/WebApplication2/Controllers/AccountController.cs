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
        private Models.Account account = new Models.Account();
        private Models.Member member = new Models.Member();
        private Models.Leader leader = new Models.Leader();
        private Models.Administrator admin = new Models.Administrator();
        // GET: Account
        public ActionResult Index()
        {
            Debug.Print("In Index controller");
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
            if(account.isUserValid(Username, Password))
            {
                return View("SuccessTest");
            }
            else
            {
                return Redirect("Index");
            }
        }
        
    }
}