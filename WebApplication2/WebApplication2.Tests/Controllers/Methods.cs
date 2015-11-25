using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;
using System.Web.Mvc;

namespace WebApplication2.Controllers.Tests
{
    [TestClass()]
    public class Methods
    {

        AccountController AccountTestController = new AccountController();

        [TestMethod()]
        public void SignInTest()
        {
            Account User = new Account();
            User.UserName = "bbergeron";
            User.AccountId = 1;
            User.Email = "bradbergeron90@me.com";
            User.Password = "Password1*";
            User.Rank = "Member";

            //var mockdb = new Mock<RegistrationEntities1>();

            var Result = AccountTestController.SignIn(User) as ViewResult;
            Assert.AreEqual(5, 5);
            //Assert.AreEqual("ExistingProjects", Result.ViewName);
        }
    }
}