using WebApplication2.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebApplication2.Models;


namespace WebApplication2.Controllers.Tests
{
    [TestClass()]
    public class Views
    {
        AccountController AccountTestController = new AccountController();
        ProjectController ProjectTestController = new ProjectController();

        [TestMethod()]
        public void IndexTest()
        {
            var IndexResult = AccountTestController.Index() as ViewResult;
            Assert.AreEqual("Index", IndexResult.ViewName);
        }

        [TestMethod()]
        public void EmailVerificationPageTest()
        {
            var Result = AccountTestController.EmailVerificationPage() as ViewResult;
            Assert.AreEqual("EmailVerificationPage", Result.ViewName);
        }

        [TestMethod()]
        public void EmailChangePasswordTest()
        {
            var Result = AccountTestController.EmailChangePassword() as ViewResult;
            Assert.AreEqual("EmailChangePassword", Result.ViewName);
        }

        [TestMethod()]
        public void ForgotPasswordTest()
        {
            var Result = AccountTestController.ForgotPassword() as ViewResult;
            Assert.AreEqual("ForgotPassword", Result.ViewName);
        }

        [TestMethod()]
        public void SignUpTest()
        {
            var Result = AccountTestController.SignUp("SignUpMember") as ViewResult;
            var Result2 = AccountTestController.SignUp("SignUpLeader") as ViewResult;
            var Result3 = AccountTestController.SignUp("SignUpAdmin") as ViewResult;
            Assert.AreEqual("SignUpMember", Result.ViewName);
            Assert.AreEqual("SignUpLeader", Result2.ViewName);
            Assert.AreEqual("SignUpAdmin", Result3.ViewName);
        }

        [TestMethod()]
        public void AddPeopleTest()
        {
            Project ListofProjects = new Project();
            var Result = ProjectTestController.AddPeople(ListofProjects) as ViewResult;
            Assert.AreEqual("AddPeople", Result.ViewName);
        }

    }
}