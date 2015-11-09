using System.Diagnostics;
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
        public ActionResult Result(ProcessModel form)
        {
            //Debug.Print(form.Count.ToString());
            //Debug.Print(PModel.Answers.Length.ToString());
            
            Debug.Print(form.Ans.Count.ToString());
            //Debug.Print(form.UserForm[2]);
            //Debug.Print(answers.Ans[100]);
            /*string[] Answers = new string[92];
           
            //string[] Answers = new string[form.Count]; //for now hard code it
            //form.CopyTo(Answers, 0);
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
                //PModel.EliminateProcessModels();
                PModel.ChooseProcessModels();
                ViewData["result"] = PModel.Result;
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
            }*/
            string[,] Questions = PModel.Questions; //goal is to replace this with model/view
            string[,] MultAnswers = PModel.MultipleChoiceAnswers;
            ViewData["questions"] = Questions;
            ViewData["multAnswers"] = MultAnswers;
            ViewData["isValid"] = "false";
            return View("Questions", form);

        }
        
    }
}