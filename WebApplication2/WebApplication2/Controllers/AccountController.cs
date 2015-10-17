using System;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    { 
        Account ConfirmedUser = new Account();
        EmailConfirmation ef = new EmailConfirmation();

        // GET: Account
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult EmailVerificationPage()
        {
            ViewData["Email"] = "Verified";

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
            return View("Index", ConfirmedUser);
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
                ViewData["Email"] = ef.sendEmail(ConfirmedMem.Email);
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
                ConfirmedLead.Init(PossibleLead);
                return View("Index");
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
                ConfirmedAdmin.Init(PossibleAdmin);
                return View("Index");
            }
        }

        public ActionResult Resend(Account User)
        {
            //User.UpdateUsersEmail();
            ViewData["Email"] = ef.sendEmail(User.Email);
            return View("EmailConfirmationPage");
        }


    }
}