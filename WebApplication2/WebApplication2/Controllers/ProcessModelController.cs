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
        public ActionResult RequirementQuestions()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProcessModelResult(FormCollection form)
        {
            int[] Answers = { };
            form.CopyTo(Answers, 0);
            Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());

            PModel.ProcessAnswers(Answers);
            return View();
        }
    }
}