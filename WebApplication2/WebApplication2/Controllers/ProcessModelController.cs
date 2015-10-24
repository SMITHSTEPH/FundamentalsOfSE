using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class ProcessModelController : Controller
    {
        // GET: ProcessModel
        public ActionResult RequirementQuestions()
        {
            return View();
        }
        public ActionResult ProcessModelResult()
        {
            return View();
        }
    }
}