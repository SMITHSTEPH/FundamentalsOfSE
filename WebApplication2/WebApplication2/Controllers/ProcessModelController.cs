using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProcessModelController : Controller
    {
        ProcessModel PModel = new ProcessModel();
        public ActionResult Questions(Account User)
        {
            ViewData["id"] = User.AccountId;
            ViewData["questions"] = PModel.Questions;
            ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
            ViewData["isValid"] = "true";
            ViewData["answers"] = ViewData["answers"] = new string[92] { "0True", "1True", "2True", "3True", "4True", "5False", "test", "7False", "8False", "9very little", "10False", "11False", "12at the end", "13very little to no time", "14False", "15False", "16Fast", "17False", "18Less than a Year", "19False", "20Every 1-3 weeks", "21Every 1-3 weeks", "22False", "23False", "24False", "25very", "26True", "27very", "28False", "29False", "30little", "31little", "32average amount", "33love it", "34False", "35spread out across country", "36low - Medium", "37High", "38False", "39False", "40True", "41False", "42False", "43low", "44True", "45One", "46False", "47True", "48True", "49True", "50None", "51True", "52True", "53False", "54Medium", "55True", "56True", "57False", "58False", "59False", "60Updated Often", "61False", "62False", "63False", "64Low Budget", "65Low Budget", "66Low Budget", "67Low Budget", "68False", "69False", "70False", "71Small", "72True", "73False", "74Flexable", "75True", "76True", "77True", "78True", "79True", "80True", "81True", "82True", "83True", "84False", "85False", "86False", "87False", "88True", "89False", "90False", "91False" };

            return View("Questions",User);
        }
        [HttpPost]
        public ActionResult Test(FormCollection form)
        {
            string[] info = new string[form.Count];
            form.CopyTo(info, 0);
            int id = Convert.ToInt16(info[0]);
            string model = info[1];

           //RegistrationEntities1Entities1 db = new RegistrationEntities1Entities1();
			RegistrationEntities1 db = new RegistrationEntities1();

            db.ProjectTables.Add(new ProjectTable
            {
                ProcessModelChosen = model
            });

            db.SaveChanges();

            List<ProjectTable> projectTables = db.ProjectTables.ToList();
            ProjectTable justAdded = projectTables.Last();

            db.JunctionTableProjectAndAccountV2.Add(new JunctionTableProjectAndAccountV2
            {
                AID = id,
                PId = justAdded.ProjectId,
                Responsibilities = "Leader",
                Role = "Leader"
            });

            db.SaveChanges();

            leaderTableV2 leader = db.leaderTableV2.Find(id);
            Account User = new Account();
            User.AccountId = leader.Id;
            User.Rank = "Leader";
            User.UserName = leader.UserName;

            return RedirectToAction("ExistingProjects", "Project", User);
        }
        //for training
        public ActionResult TrainingData()
        {
            ViewData["questions"] = PModel.Questions;
            ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
            ViewData["isValid"] = "true";
            ViewData["answers"] =new string[1]{"test"};
            ViewData["answers"] = new string[92]{"0True", "1True", "2True", "3True", "4True", "5False", "test", "7False", "8False", "9very little", "10False", "11False", "12at the end", "13very little to no time", "14False", "15False", "16Fast", "17False", "18Less than a Year", "19False", "20Every 1-3 weeks", "21Every 1-3 weeks", "22False", "23False", "24False", "25very", "26True", "27very", "28False", "29False", "30little", "31little", "32average amount", "33love it", "34False", "35spread out across country", "36low - Medium", "37High", "38False", "39False", "40True", "41False", "42False", "43low", "44True", "45One", "46False", "47True", "48True", "49True", "50None", "51True", "52True", "53False", "54Medium", "55True", "56True", "57False", "58False", "59False", "60Updated Often", "61False", "62False", "63False", "64Low Budget", "65Low Budget", "66Low Budget", "67Low Budget", "68False", "69False", "70False", "71Small", "72True", "73False", "74Flexable", "75True", "76True", "77True", "78True", "79True", "80True", "81True", "82True", "83True", "84False", "85False", "86False", "87False", "88True", "89False", "90False", "91False"};
            ViewData["result"] = "";
            return View();
        }
        //for training
        public ActionResult TrainData(FormCollection form)
        {
            string[] Answers2= new string[92] { "0True", "1True", "2True", "3True", "4True", "5False", "test", "7False", "8False", "9very little", "10False", "11False", "12at the end", "13very little to no time", "14False", "15False", "16Fast", "17False", "18Less than a Year", "19False", "20Every 1-3 weeks", "21Every 1-3 weeks", "22False", "23False", "24False", "25very", "26True", "27very", "28False", "29False", "30little", "31little", "32average amount", "33love it", "34False", "35spread out across country", "36low - Medium", "37High", "38False", "39False", "40True", "41False", "42False", "43low", "44True", "45One", "46False", "47True", "48True", "49True", "50None", "51True", "52True", "53False", "54Medium", "55True", "56True", "57False", "58False", "59False", "60Updated Often", "61False", "62False", "63False", "64Low Budget", "65Low Budget", "66Low Budget", "67Low Budget", "68False", "69False", "70False", "71Small", "72True", "73False", "74Flexable", "75True", "76True", "77True", "78True", "79True", "80True", "81True", "82True", "83True", "84False", "85False", "86False", "87False", "88True", "89False", "90False", "91False" };
            Debug.Print("Answers2: " +Answers2.Length.ToString());
            Debug.Print("Train COUNT IN CONTROLLER");
            string[] Answers = new string[form.Count]; //for now hard code it
            Debug.Print("Form length is: " + form.Count.ToString());
            form.CopyTo(Answers, 0);
            for(int i=0; i<Answers.Length; i++)
            {
                Debug.Write(Answers[i]+", ");
            }
            Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());
            if (Answers.Length==93) //if all the questions were answered
            {
                string winner = form[form.Count - 1];
                //Debug.Print("winner: " + winner);
                string key= form.GetKey(form.Count-1);
                form.Remove(form.GetKey(form.Count - 1));//hopefully removing the winner from the form 
                Answers = new string[form.Count];
                form.CopyTo(Answers, 0);
                string[] OldAnswers = new string[form.Count];
                Answers.CopyTo(OldAnswers, 0);
                PModel.Answers = PModel.RemoveQuestionIDS(Answers);
                /*Debug.Print("Answers Again: ");
                for (int i = 0; i < Answers.Length; i++) //test
                {
                    Debug.Write(Answers[i] + ",");
                }*/
                int score =PModel.TrainData(winner);
     
                ViewData["isValid"] = "true";
                ViewData["answers"] = OldAnswers;
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
        public ActionResult Result(FormCollection form, Account User) 
        {
          
            Debug.Print("ANS COUNT IN CONTROLLER");

            //BRad Removed this
            //string[] Answers = new string[form.Count]; //for now hard code it
            //form.CopyTo(Answers, 0);
            //End Brads Remove

            /*Debug.Print("Length of form is: ");
            Debug.Print(Answers.Length.ToString());
            for (int i = 0; i < Answers.Length; i++)
            {
               Debug.Write(Answers[i] +",");
            }*/

            /**** Brads Code
            Adding the Account Id makes the form length 1 more larger, I had to fix it
            */
            string[] Answers = new string[form.Count - 1];
            string[] wrongAnswers = new string[form.Count];
            form.CopyTo(wrongAnswers, 0);
            for (int i = 0; i < wrongAnswers.Length - 1; i++)
            {
                Answers[i] = wrongAnswers[i + 1];
            }
            // BRads code ends

            if (PModel.IsValid(Answers))
            {
                Answers = PModel.RemoveQuestionIDS(Answers);
                PModel.Answers = Answers;
                PModel.EliminateProcessModels();
                string Result=PModel.ChooseProcessModels();
                ViewData["result"] = Result;
                return View("Result",User);
            }
            else
            {
                ViewData["questions"] = PModel.Questions;
                ViewData["multAnswers"] = PModel.MultipleChoiceAnswers;
                ViewData["isValid"] = "false";
                ViewData["answers"] = Answers;
                return View("Questions", User);
            }
          }
        
    }
}