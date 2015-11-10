using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProcessModelController : Controller
    {
        ProcessModel PModel = new ProcessModel();
        Answers answers = new Answers();
        Dictionary<int, string> answers2 = new Dictionary<int, string>();

        // GET: ProcessModel

        public ActionResult Questions()
        {
            string[,] Questions=PModel.Questions;
            string[,] MultAnswers = PModel.MultipleChoiceAnswers;
            Dictionary<int, string> UserAnswers = PModel.UserForm;
           //do model-view binding instead
           ViewData["questions"] = Questions;
           ViewData["multAnswers"] = MultAnswers;
           ViewData["isValid"] = "true";
           ViewData["userAnswers"] = UserAnswers;

            ViewData["answers"] = new string[1] { "test" };

            return View(answers2);
        }
        [HttpPost]
        public ActionResult Result(FormCollection form) 
        {
            //Debug.Print(form.Count.ToString());
            //Debug.Print(PModel.Answers.Length.ToString());
            Debug.Print("ANS COUNT IN CONTROLLER");
            //Debug.Print(form.Test.Count.ToString());
            //Debug.Print(form.ToString());
            //Debug.Print(answers2.Count.ToString());

            //Debug.Print(form.UserForm[2]);
            //Debug.Print(answers.Ans[100]);
            //string[] Answers = new string[92];
           
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
            //Debug.Print("Test length: " + PModel.AnswersTest.Length);
            if (PModel.IsValid(PModel.AnswersTest))
            {
                /*PModel.Answers = Answers;
                //PModel.EliminateProcessModels();
                PModel.ChooseProcessModels();
                ViewData["result"] = PModel.Result;
                return View();*/
            }
            else
            {
                /*string[,] Questions = PModel.Questions; //goal is to replace this with model/view
                string[,] MultAnswers = PModel.MultipleChoiceAnswers;
                ViewData["questions"] = Questions;
                ViewData["multAnswers"] = MultAnswers;
                ViewData["isValid"] = "false";

                return View("Questions");*/
            }
            string[,] Questions = PModel.Questions; //goal is to replace this with model/view
            string[,] MultAnswers = PModel.MultipleChoiceAnswers;
            ViewData["questions"] = Questions;
            ViewData["multAnswers"] = MultAnswers;
            ViewData["isValid"] = "false";
            ViewData["answers"] = Answers;
            return View("Questions");

        }
        
    }
}