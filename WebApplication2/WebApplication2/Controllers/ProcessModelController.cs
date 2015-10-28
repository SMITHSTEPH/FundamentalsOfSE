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
            /*for (int i = 0; i < Questions.GetLength(0); i++) //testing Questions Table
            {
              //(Questions[i, 0].ToString().Trim());
            }
            for (int i = 0; i < MultAnswers.GetLength(0); i++) //testing Questions Table
            {
                for (int j = 0; j < MultAnswers.GetLength(1); j++)
                {
                    Debug.Print(MultAnswers[i, j]);
                }
            }*/
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
            Debug.Print("Form Count is: "+ form.Count.ToString());
            Debug.Print("Question Size: " + PModel.QuestionSize); //this is always 0 and I don't know why
            string[] Answers = new string[form.Count]; //for now hard code it
            form.CopyTo(Answers, 0);
            Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());
            /*for (int i = 0; i < Answers.Length; i++)
            {
                Debug.Print(i + ": " + Answers[i]);
            }*/
            if (PModel.IsValid(Answers))
            {
                
                PModel.EliminateProcessModels();
                PModel.ChooseProcessModels();
                return View();
            }
            else
            {
                string[,] Questions = PModel.GetProcessModelQuestions();
                string[,] MultAnswers = PModel.GetMultipleChoiceResponses();
                ViewData["questions"] = Questions;
                ViewData["multAnswers"] = MultAnswers;
                Debug.Print("In else");
                ModelState.AddModelError("Name", "Name is required");
                ViewData["isValid"] = "false";
                return View("Questions");
            }
           
        }
        
    }
}