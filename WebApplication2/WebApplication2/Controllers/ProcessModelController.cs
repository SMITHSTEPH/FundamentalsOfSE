using System;
using System.Collections;
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
            string[,] Questions=PModel.GetProcessModelQuestions();
            string[,] MultAnswers = PModel.GetMultipleChoiceResponses();
            for (int i = 0; i < Questions.GetLength(0); i++) //testing Questions Table
            {
              //(Questions[i, 0].ToString().Trim());
            }
            for (int i = 0; i < MultAnswers.GetLength(0); i++) //testing Questions Table
            {
                for (int j = 0; j < MultAnswers.GetLength(1); j++)
                {
                    Debug.Print(MultAnswers[i, j]);
                }
            }
            ViewData["questions"] = Questions;
            ViewData["multAnswers"] = MultAnswers;
            ViewData["isValid"] = "true";
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