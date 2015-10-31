using System;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Diagnostics;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    { 
        Account ConfirmedUser = new Account();

        // GET: Account
        /**
        Called: when the user navigated to the web application
        Routes to: the login page (called Index)
        **/
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult EmailVerificationPage()
        {
            return View();
        }
        /**
        Called: after the user hits the 'Sign Up' button
        Routes to: the view that correspond to the dropdown selection
        **/
        [HttpPost]
        public ActionResult SignUp(String DropChoice)
        {
            return View(DropChoice);
        }
        /**
        Called: after the user hits the 'Sign In' button
        Routes to: Index if invalid or Project if valid
        **/
        [HttpPost]
        public ActionResult SignIn(Account User)
        {
            //Debug.Print(User.UserName);
            ConfirmedUser = User.Verify(User);
            ViewData["isValid"] = ConfirmedUser.Rank;
            ViewData["Email"] = ConfirmedUser.ConfirmEmail;

            //if(ConfirmedUser.UserName == "bbergeron") //Password: Password1*
            //{
            //    return View("Input");
            //}
           
            if (ConfirmedUser.Rank != "Fail")
            {
                return RedirectToAction("ExistingProjects", "Project", User);
            }
            else
            {
                return View("Index");
            }

           
           
        }
        /**
        Called: after the user hits the 'Sign Up' button at the end of the sign up form
        Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
        **/
        [HttpPost]
        public ActionResult CreateMember(Member PossibleMem)
        {
            PossibleMem.ConfirmEmail = false;
            string print = PossibleMem.isValid(PossibleMem);

            if (print != "Valid")
            {
                ViewData["isValid"] = print;
                return View("SignUpMember");
            }
            else
            {
                Member ConfirmedMem = PossibleMem;
                ViewData["Email"] = ConfirmedUser.sendEmail(ConfirmedMem.Email, ConfirmedMem.UserName);
                ConfirmedMem.Init(PossibleMem);
                return View("EmailConfirmationPage");
            }
        }
       /**
       Called: after the user hits the 'Sign Up' button at the end of the sign up form
       Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
       **/
        [HttpPost]
        public ActionResult CreateLeader(Leader PossibleLead)
        {
            PossibleLead.ConfirmEmail = false;
            string print = PossibleLead.isValid(PossibleLead);

            if (print != "Valid")
            {
                ViewData["isValid"] = print;
                return View("SignUpLeader");
            }
            else
            {
                Leader ConfirmedLead = PossibleLead;
                ViewData["Email"] = ConfirmedUser.sendEmail(ConfirmedLead.Email, ConfirmedLead.UserName);
                ConfirmedLead.Init(PossibleLead);
                return View("EmailConfirmationPage");
            }
        }
        /**
        Called: after the user hits the 'Sign Up' button at the end of the sign up form
        Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
        **/
        [HttpPost]
        public ActionResult CreateAdmin(Administrator PossibleAdmin)
        {
            PossibleAdmin.ConfirmEmail = false;
            string print = PossibleAdmin.isValid(PossibleAdmin);

            if (print != "Valid")
            {
                ViewData["isValid"] = print;
                return View("SignUpLeader");
            }
            else
            {
                Administrator ConfirmedAdmin = PossibleAdmin;
                ViewData["Email"] = ConfirmedUser.sendEmail(ConfirmedUser.Email, ConfirmedUser.UserName);
                ConfirmedAdmin.Init(PossibleAdmin);
                return View("EmailConfirmationPage");
            }
        }
        /**
        Called: after the user hits the 'Sign Up' button at the end of the sign up form
        Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
       **/
        public ActionResult Resend(Account PossibleUser)
        {
            ConfirmedUser = PossibleUser.Verify(PossibleUser);
            ConfirmedUser.sendEmail(ConfirmedUser.Email,ConfirmedUser.UserName);
            return View("EmailConfirmationPage");
        }
         /**
        Called: after the user hits the 'Sign Up' button at the end of the sign up form
        Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
        **/
        public ActionResult Verify( )
        {
            string UserName = (string) RouteData.Values["id"];
            ViewData["User"] = UserName;
            ViewData["Email"] = ConfirmedUser.UpdateConfirmation(UserName);
            return View("EmailVerificationPage");
        }


       [HttpPost]
        public ActionResult Upload(HttpPostedFileBase FileUpload)
        {
            //check we have a file
            if (FileUpload.ContentLength > 0)
            {
                //Workout our file path
                string fileName = Path.GetFileName(FileUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
     
                //Try and upload
                try
                {
                    FileUpload.SaveAs(path);
                    //Process the CSV file and capture the results to our DataTable place holder
                    ProcessCSV(path);
                }
                catch (Exception ex)
                {
                    //Catch errors
                    ViewData["Feedback"] = ex.Message;
                }
            }
            else
            {
                //Catch errors
                ViewData["Feedback"] = "Please select a file";
            }

            return View("Input", ViewData["Feedback"]);
        }

        private static void ProcessCSV(string fileName)
        {
            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;

            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            StreamReader sr = new StreamReader(fileName);

            //Get rid of Headers
            line = sr.ReadLine();

            //CHANGE TO WHICH TABLE YOU NEED
            Questions2Table result = new Questions2Table();
    
            RegistrationEntities1 db = new RegistrationEntities1();
            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                //row = dt.NewRow();

                strArray = r.Split(line);

                //for multiple choice table
                //result.QuestionId = Int32.Parse(strArray[0]);

               /* result.Response1 = strArray[1];
                result.Response2 = strArray[2];
                result.Response3 = strArray[3];
                result.Response4 = strArray[4];
                result.Response5 = strArray[5];
                result.Response6 = strArray[6];*/

                //for Process tables
                 //result.Answer = strArray[1];
                 //result.Priority = Int32.Parse(strArray[2]);

                result.QuestionId = Int32.Parse(strArray[0]);
                result.Question = strArray[1];
                result.Category = strArray[2];
                result.QuestionType = strArray[3];


                string query = "INSERT INTO Questions2Table(QuestionId,Question,Category,QuestionType)  VALUES (" + result.QuestionId + ",'" + result.Question + "','" + result.Category + "','" + result.QuestionType + "')";
                //string query = "INSERT INTO MultipleChoiceTable(QuestionId,Response1,Response2,Response3,Response4,Response5,Response6) VALUES (" + result.QuestionId + ",'" + result.Response1 + "','" + result.Response2 + "','" + result.Response3 + "','" + result.Response4 + "','" + result.Response5 + "','" + result.Response6 + "')";
                //string query = "INSERT INTO COTSTable(QuestionId,Answer,Priority) VALUES (" + result.QuestionId + ",'" + result.Answer + "'," + result.Priority + ")";
                //string query = "INSERT INTO WaterfallIterationTable (QuestionId,Answer,Priority) VALUES (" + result.QuestionId + ",'" + result.Answer + "'," + result.Priority + ")";
                //string query = "INSERT INTO RADTable(QuestionId,Answer,Priority) VALUES (" + result.QuestionId + ",'" + result.Answer + "'," + result.Priority + ")";
                //string query = "INSERT INTO WaterfallTable2 (QuestionId,Answer,Priority) VALUES (" + result.QuestionId + ",'" + result.Answer + "'," + result.Priority + ")";
                //Run Query 
                db.Database.ExecuteSqlCommand(query);
            }

            //Tidy Streameader up
            //sr.Dispose();

        }
    }
}