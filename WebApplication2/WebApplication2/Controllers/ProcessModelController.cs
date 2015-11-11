using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
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
        //for training
        public ActionResult TrainingData()
        {
            ViewData["questions"] = PModel.Questions;
            ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
            ViewData["isValid"] = "true";
            ViewData["answers"] = new string[92]{"0True", "1True", "2True", "3True", "4True", "5False", "test", "7False", "8False", "9very little", "10False", "11False", "12at the end", "13very little to no time", "14False", "15False", "16Fast", "17False", "18Less than a Year", "19False", "20Every 1-3 weeks", "21Every 1-3 weeks", "22False", "23False", "24False", "25very", "26True", "27very", "28False", "29False", "30little", "31little", "32average amount", "33love it", "34False", "35spread out across country", "36low - Medium", "37High", "38False", "39False", "40True", "41False", "42False", "43low", "44True", "45One", "46False", "47True", "48True", "49True", "50None", "51True", "52True", "53False", "54Medium", "55True", "56True", "57False", "58False", "59False", "60Updated Often", "61False", "62False", "63False", "64Low Budget", "65Low Budget", "66Low Budget", "67Low Budget", "68False", "69False", "70False", "71Small", "72True", "73False", "74Flexable", "75True", "76True", "77True", "78True", "79True", "80True", "81True", "82True", "83True", "84False", "85False", "86False", "87False", "88True", "89False", "90False", "91False"};
            ViewData["result"] = "";
            return View();
        }
        //for training
        public ActionResult TrainData(FormCollection form)
        {
            Debug.Print("Train COUNT IN CONTROLLER");
            string[] Answers = new string[form.Count]; //for now hard code it
            
            form.CopyTo(Answers, 0);
            Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());
            /*for (int i = 0; i < Answers.Length; i++)
            {
                Debug.Write(Answers[i] + ",");
            }*/
            if (Answers.Length==93) //if all the questions were answered
            {
                string winner = form[form.Count - 1];
                Debug.Print("winner: " + winner);
              
                string key= form.GetKey(form.Count-1);
                Debug.Print("key is: " + key);
                form.Remove(form.GetKey(form.Count - 1));//hopefully removing the winner from the form 
                Answers = new string[form.Count];
                form.CopyTo(Answers, 0);
                Debug.Print("Length of form is: ");
                Debug.Print(Answers.Length.ToString());
                string[] OldAnswers = Answers;
                PModel.Answers = PModel.RemoveQuestionIDS(Answers);

                Debug.Print("Answers Again: ");
                for (int i = 0; i < Answers.Length; i++) //test
                {
                    Debug.Write(Answers[i] + ",");
                }
             

                int score =PModel.TrainData(winner);
                //ViewData["questions"] = PModel.Questions;
                //ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
                ViewData["isValid"] = "true";
                ViewData["answers"] = OldAnswers;
                //ViewData["result"] = "";
                ViewData["questions"] = PModel.Questions;
                ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
                ViewData["result"] = winner + " has been added to the database with a score of " + score.ToString();
                return View("TrainingData");
            }
            else
            {

                ViewData["questions"] = PModel.Questions;
                ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
                ViewData["isValid"] = "false";
                ViewData["answers"] = Answers;
                ViewData["result"] = "";
                return View("TrainingData");
            }

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