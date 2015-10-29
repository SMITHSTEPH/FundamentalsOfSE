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
            /*Debug.Print("Trying to print form");
            Debug.Print(form["Answer"]);
            Debug.Print("Form Count is: "+ form.Count.ToString());
            //Debug.Print("Question Size: " + PModel.QuestionSize); //this is always 0 and I don't know why*/
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
                string[,] Questions = PModel.Questions;
                string[,] MultAnswers = PModel.MultipleChoiceAnswers;
                ViewData["questions"] = Questions;
                ViewData["multAnswers"] = MultAnswers;
                //Debug.Print("In else");
                //ModelState.AddModelError("Name", "Name is required");
                ViewData["isValid"] = "false";
                return View("Questions");
            }
           
        }
        
    }
}