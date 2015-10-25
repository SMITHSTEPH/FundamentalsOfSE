using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProcessModelController : Controller
    {
        ProcessModel PModel = new ProcessModel();
        // GET: ProcessModel
        public ActionResult Questions()
        {
            ViewData["isValid"] = "true";
            Debug.Print("HEEELOOOO");
            return View();
        }
        [HttpPost]
        public ActionResult Result(FormCollection form)
        {
            Debug.Print("Trying to print form");
            Debug.Print(form["Answer"]);
            string[] Answers = {null};
            form.CopyTo(Answers, 1);
            Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());
            if (PModel.IsValid())
            {

                PModel.ProcessAnswers(Answers);
                PModel.EliminateProcessModels();
                PModel.ChooseProcessModels();
                return View();
            }
            else
            {
                ViewData["isValid"] = "false";
                return View("Questions");
            }
        }
    }
}