using System;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Data.Entity;

namespace WebApplication2.Controllers
{
    /**
    **/
    public class AccountController : Controller
    {
        Account ConfirmedUser = new Account();
        protected RegistrationEntities1 db = new RegistrationEntities1(); //instance of our database 'RegistrationEntities1'

        // GET: Account
        /**
        Called: when the user navigated to the web application
        Routes to: the login page (called Index)
        **/
        public ActionResult Index()
        {
            return View("Index");
        }
        /**
        Called: when the user clicks the 'confirm email' link in their email
        **/
        public ActionResult EmailVerificationPage()
        {
            return View("EmailVerificationPage");
        }
        /**
            
        **/
        public ActionResult EmailChangePassword()
        {
            return View("EmailChangePassword");
        }
        /**
        Called: when the user clicks the 'forgot password' link on the login page
        **/
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        public ActionResult EmailForgotPage() //no idea when this is called
        {
            return View("EmailForgotPage");
        }
        /**
        Called: after the user hits the 'Sign Up' button
        Routes to: the view that correspond to the dropdown selection
        **/
        [HttpPost]
        public ActionResult SignUp(String dropChoice)
        {
            return View(dropChoice);
        }
        /**
        Called: after the user hits the 'Sign In' button
        Routes to: Index if invalid or Project if valid
        **/
        //[HttpPost]
        public ActionResult SignIn(Account user)
        {
            ConfirmedUser = user.Verify(user); 
            ViewData["isValid"] = ConfirmedUser.Rank;
            ViewData["Email"] = ConfirmedUser.ConfirmEmail;
            if (ConfirmedUser.Rank != "Fail" && ConfirmedUser.ConfirmEmail == true)
            {
                return RedirectToAction("ExistingProjects", "Project", user);
            }
            else{return View("Index");}
        }
        /**
        Called: after the user hits the 'Sign Up' button at the end of the sign up form
        Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
        **/
        [HttpPost]
        public ActionResult CreateMember(Member possibleMem)
        {
            possibleMem.ConfirmEmail = false;
            string Member = possibleMem.isValid(possibleMem);

            if (Member != "Valid")
            {
                ViewData["isValid"] = Member;
                return View("SignUpMember"); 
            }
            else
            {
                Member ConfirmedMem = possibleMem;
                ViewData["Email"] = ConfirmedUser.sendEmail(ConfirmedMem.Email, ConfirmedMem.UserName);
                ConfirmedMem.Init(possibleMem);
                return View("EmailConfirmationPage");
            }
        }
       /**
       Called: after the user hits the 'Sign Up' button at the end of the sign up form
       Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
       **/
        [HttpPost]
        public ActionResult CreateLeader(Leader possibleLead)
        {
            possibleLead.ConfirmEmail = false;
            string leader = possibleLead.isValid(possibleLead);

            if (leader != "Valid")
            {
                ViewData["isValid"] = leader;
                return View("SignUpLeader");
            }
            else
            {
                Leader ConfirmedLead = possibleLead;
                ViewData["Email"] = ConfirmedUser.sendEmail(ConfirmedLead.Email, ConfirmedLead.UserName);
                ConfirmedLead.Init(possibleLead);
                return View("EmailConfirmationPage");
            }
        }
        /**
        Called: after the user hits the 'Sign Up' button at the end of the sign up form
        Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
        **/
        [HttpPost]
        public ActionResult CreateAdmin(Administrator possibleAdmin)
        {
            possibleAdmin.ConfirmEmail = false;
            string admin = possibleAdmin.isValid(possibleAdmin);

            if (admin != "Valid")
            {
                ViewData["isValid"] = admin;
                return View("SignUpAdmin");
            }
            else
            {
                Administrator ConfirmedAdmin = possibleAdmin;
                ViewData["Email"] = ConfirmedUser.sendEmail(ConfirmedUser.Email, ConfirmedUser.UserName);
                ConfirmedAdmin.Init(possibleAdmin);
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

        public ActionResult ResendPass(Account PossibleUser)
        {
            SendForgotEmail(PossibleUser);
            return View("EmailForgotPage");
        }
        /**
       Called: after the user hits the 'Sign Up' button at the end of the sign up form
       Routes to: itself if form was filled out incorrectly, emailconfirationpage if was filled out correctly
       **/
        public ActionResult Verify()
        {
            string UserName = (string) RouteData.Values["id"];
            ViewData["User"] = UserName;
            ViewData["Email"] = ConfirmedUser.UpdateConfirmation(UserName);
            return View("EmailVerificationPage");
        }
        /**
        Called: after inputting email and clicking 'reset password'
        
        **/
        public ActionResult SendForgotEmail(Account user)
        {
            if (user.Rank == "Member" || user.Rank == "Leader" || user.Rank == "Admin")
            {
                if(user.Rank == "Member")
                {
                    string queryM = "SELECT * FROM memberTableV2 WHERE UserName='" + user.UserName + "'";
                    memberTableV2 MT = db.memberTableV2.SqlQuery(queryM).SingleOrDefault();
                    if (MT == null)
                    {

                        string queryM2 = "SELECT * FROM memberTableV2 WHERE Email='" + user.Email + "'";
                        memberTableV2 MT2 = db.memberTableV2.SqlQuery(queryM2).SingleOrDefault();

                        if (MT2 == null)
                        {
                            ViewData["UserName"] = "Fail";
                            return View("ForgotPassword");
                        }
                        else
                        {
                            ViewData["Email"] = user.sendEmailPassword(MT2.Email, MT2.UserName);
                            return View("EmailForgotPage");
                        }
                    }
                    else
                    {
                        ViewData["Email"] = user.sendEmailPassword(MT.Email, MT.UserName);
                        return View("EmailForgotPage");
                    }
                    
                    
                }
                else if(user.Rank == "Leader")
                {
                    string queryL = "SELECT * FROM leaderTableV2 WHERE UserName='" + user.UserName + "'";
                    leaderTableV2 LT = db.leaderTableV2.SqlQuery(queryL).SingleOrDefault();

                    if (LT == null)
                    {

                        string queryL2 = "SELECT * FROM leaderTableV2 WHERE Email='" + user.Email + "'";
                        leaderTableV2 LT2 = db.leaderTableV2.SqlQuery(queryL2).SingleOrDefault();

                        if (LT2 == null)
                        {
                            ViewData["UserName"] = "Fail";
                            return View("ForgotPassword");
                        }
                        else
                        {
                            ViewData["Email"] = user.sendEmailPassword(LT2.Email, LT2.UserName);
                            return View("EmailForgotPage");
                        }
                    }
                    else
                    {
                        ViewData["Email"] = user.sendEmailPassword(LT.Email, LT.UserName);
                        return View("EmailForgotPage");
                    }
                }
                else
                {
                    string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + user.UserName + "'";
                    administrationV2 AT = db.administrationV2.SqlQuery(queryA).SingleOrDefault();

                    if (AT == null)
                    {

                        string queryA2 = "SELECT * FROM administrationV2 WHERE Email='" + user.Email + "'";
                        administrationV2 AT2 = db.administrationV2.SqlQuery(queryA2).SingleOrDefault();

                        if (AT2 == null)
                        {
                            ViewData["UserName"] = "Fail";
                            return View("ForgotPassword");
                        }
                        else
                        {
                            ViewData["Email"] = user.sendEmailPassword(AT2.Email, AT2.UserName);
                            return View("EmailForgotPage");
                        }
                    }
                    else
                    {
                        ViewData["Email"] = user.sendEmailPassword(AT.Email, AT.UserName);
                        return View("EmailForgotPage");
                    }
                }
            }
            else
            {
                ViewData["Rank"] = "Fail";
                return View("ForgotPassword");
            }
        }

        public ActionResult VerifyPassword()
        {
            string UserName = (string)RouteData.Values["id"];
            Account User = new Account();
            User.UserName = UserName;
            ViewData["Password"] = "Input";
            return View("InputPassword", User);
            
        }

        public ActionResult InputPassword(Account User)
        {
            ViewData["Password"] = "Input";
            return View("InputPassword", User);
        }

        public ActionResult ChangedPassword(Account User)
        {
                ViewData["Password"] = ConfirmedUser.UpdatePassword(User.UserName, User.Password);
                return View("InputPassword", User);
        }
    }
}