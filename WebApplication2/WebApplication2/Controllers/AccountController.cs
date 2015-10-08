﻿using System;
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
        
        public ActionResult SignUpMember(FormCollection Form)
        {
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
        public ActionResult SignUp(String DropChoice)
        {
            return View(DropChoice);
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

        [HttpPost]
        public ActionResult CreateMember(String Username)
        {
            return View("SuccessTest");
        }
        
    }
}