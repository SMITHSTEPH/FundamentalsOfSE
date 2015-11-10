using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProcessModelController : Controller
    {
        ProcessModel PModel = new ProcessModel();
        public ActionResult Questions()
        {
            ViewData["questions"] = PModel.Questions;
            ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
            ViewData["isValid"] = "true";
            ViewData["answers"] = new string[1] { "test" };

            return View();
        }
        [HttpPost]
        public ActionResult Result(FormCollection form) 
        {
          
            Debug.Print("ANS COUNT IN CONTROLLER");
            string[] Answers = new string[form.Count]; //for now hard code it
            form.CopyTo(Answers, 0);
            Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());
            for (int i = 0; i < Answers.Length; i++)
            {
               Debug.Write(Answers[i] +",");
            }
            if (PModel.IsValid(Answers))
            {
                for (int i = 0; i < Answers.Length; i++) 
                {
                    Answers[i].Remove(0, 1);  //removing the first character from the string
                }
                for (int i = 0; i < Answers.Length; i++) //test
                {
                    Debug.Write(Answers[i] + ",");
                }
                PModel.Answers = Answers;
                PModel.EliminateProcessModels();
                PModel.ChooseProcessModels();
                ViewData["result"] = PModel.Result;
                return View();
            }
            else
            {
              
                ViewData["questions"] = PModel.Questions;
                ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
                ViewData["isValid"] = "false";
                ViewData["answers"] = Answers;
                return View("Questions");
            }
          }
        
    }
}