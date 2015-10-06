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
            Debug.Print("In Index controller");
            return View();
        }
        public ActionResult SignUp()
        {
            Debug.Print("In Sign Up controller");
            return View();
        }
    }
}