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
            string[,] Questions=PModel.Questions;
            string[,] MultAnswers = PModel.MultipleChoiceAnswers;
            //do model-view binding instead
            ViewData["questions"] = Questions;
            ViewData["multAnswers"] = MultAnswers;
            ViewData["isValid"] = "true";
           
            return View();
        }
        [HttpPost]
        public ActionResult Result(FormCollection form)
        {
           
            string[] Answers = new string[form.Count]; //for now hard code it
            form.CopyTo(Answers, 0);
            Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());
            for (int i = 0; i < Answers.Length; i++)
            {
                //Debug.Print("Test");
                //System.Console.Write(Answers[i] + ",");
                Debug.Write(Answers[i] +",");
            }
            Debug.Print("Test length: " + PModel.AnswersTest.Length);
            if (PModel.IsValid(PModel.AnswersTest))
            {
                PModel.Answers = Answers;
                PModel.EliminateProcessModels();
                //PModel.ChooseProcessModels();
                return View();
            }
            else
            {
                string[,] Questions = PModel.Questions; //goal is to replace this with model/view
                string[,] MultAnswers = PModel.MultipleChoiceAnswers;
                ViewData["questions"] = Questions;
                ViewData["multAnswers"] = MultAnswers;
                ViewData["isValid"] = "false";

                return View("Questions");
            }
           
        }
        
    }
}