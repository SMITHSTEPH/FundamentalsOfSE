using System;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Controllers;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    { 
        Account ConfirmedUser = new Account();
        ProjectController pc = new ProjectController();

        // GET: Account
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult EmailVerificationPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(String DropChoice)
        {

            return View(DropChoice);
        }

        [HttpPost]
        public ActionResult SignIn(Account PossibleUser)
        {
            ConfirmedUser = PossibleUser.Create(PossibleUser);
            ViewData["isValid"] = ConfirmedUser.Rank;
            ViewData["Email"] = ConfirmedUser.ConfirmEmail;

            return RedirectToAction("ExistingProjects", "Project");
            // return View("Index", ConfirmedUser);
        }
        
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

        public ActionResult Resend(Account PossibleUser)
        {
            ConfirmedUser = PossibleUser.Create(PossibleUser);
            ConfirmedUser.sendEmail(ConfirmedUser.Email,ConfirmedUser.UserName);
            return View("EmailConfirmationPage");
        }

        public ActionResult Verify( )
        {
            string UserName = (string) RouteData.Values["id"];
            ViewData["User"] = UserName;
            ViewData["Email"] = ConfirmedUser.UpdateConfirmation(UserName);
            return View("EmailVerificationPage");
        }


    }
}