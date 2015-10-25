using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Controllers;
//using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Tests
{
    [TestClass]
    public class Iteration1
    {
        AccountController TestController = new AccountController();
        Verifcation TestVerifiation = new Verifcation();

        [TestMethod]
        public void Views()
        {
            /*var IndexResult = TestController.Index() as ViewResult;
            var SignUpResultMember = TestController.SignUp("SignUpMember") as ViewResult;
            var SignUpResultLeader = TestController.SignUp("SignUpLeader") as ViewResult;
            var SignUpResultAdmin = TestController.SignUp("SignUpAdmin") as ViewResult;
 
            Assert.AreEqual("Index", IndexResult.ViewName);
            Assert.AreEqual("SignUpMember", SignUpResultMember.ViewName);
            Assert.AreEqual("SignUpLeader", SignUpResultLeader.ViewName);
            Assert.AreEqual("SignUpAdmin", SignUpResultAdmin.ViewName);*/
        }

        [TestMethod]
        public void Member()
        {

            Member GoodMember = new Member();
            GoodMember.UserName = "HFSKS";
            GoodMember.FirstName = "Taylor";
            GoodMember.LastName = "Swift";
            GoodMember.MiddleName = "Megan";
            GoodMember.Password = "Password1*";
            GoodMember.Address = "555 Hollywood Ave";
            GoodMember.PhoneNumber = "708-899-2222";
            GoodMember.Gender = "Female";
            GoodMember.Birthdate = "2/23/19";
            GoodMember.Email = "bradbergeron90@me.com";

            Member NullMember = new Member();
            NullMember.UserName = "TSwift";
            NullMember.FirstName = "Taylor";
            NullMember.LastName = "Swift";
            NullMember.MiddleName = "Megan";
            NullMember.Password = "Password1*";
            NullMember.PhoneNumber = "708-899-2222";
            NullMember.Gender = "Female";
            NullMember.Birthdate = "2/23/19";

            Member BadPasswordMember = new Member();
            BadPasswordMember.UserName = "TSwift2";
            BadPasswordMember.FirstName = "Taylor";
            BadPasswordMember.LastName = "Swift";
            BadPasswordMember.MiddleName = "Megan";
            BadPasswordMember.Password = "notvalid";
            BadPasswordMember.Address = "555 Hollywood Ave";
            BadPasswordMember.PhoneNumber = "708-899-2222";
            BadPasswordMember.Gender = "Female";
            BadPasswordMember.Birthdate = "2/23/19";
            BadPasswordMember.Email = "bradbergeron90@me.com";

         /*   var CreateMemberResult1 = TestVerifiation.Check(GoodMember);
            var CreateMemberResult2 = TestVerifiation.Check(NullMember);
            var CreateMemberResult3 = TestVerifiation.Check(BadPasswordMember);

            Assert.AreEqual("Valid", CreateMemberResult1);
            Assert.AreEqual("NullFields", CreateMemberResult2);
            Assert.AreEqual("Char", CreateMemberResult3);*/

        }

        [TestMethod]
        public void Leader()
        {

            Leader GoodLeader = new Leader();
            GoodLeader.UserName = "HFSKS";
            GoodLeader.FirstName = "Taylor";
            GoodLeader.LastName = "Swift";
            GoodLeader.MiddleName = "Megan";
            GoodLeader.Password = "Password1*";
            GoodLeader.Address = "555 Hollywood Ave";
            GoodLeader.PhoneNumber = "708-899-2222";
            GoodLeader.Gender = "Female";
            GoodLeader.Birthdate = "2/23/19";
            GoodLeader.Email = "bradbergeron90@me.com";
            GoodLeader.LeaderKey = "5";

            /*Leader BadLeaderKey = new Leader();
            BadLeaderKey.UserName = "HFSKF";
            BadLeaderKey.FirstName = "Taylor";
            BadLeaderKey.LastName = "Swift";
            BadLeaderKey.MiddleName = "Megan";
            BadLeaderKey.Password = "Password1*";
            BadLeaderKey.Address = "555 Hollywood Ave";
            BadLeaderKey.PhoneNumber = "708-899-2222";
            BadLeaderKey.Gender = "Female";
            BadLeaderKey.Birthdate = "2/23/19";
            BadLeaderKey.Email = "bradbergeron90@me.com";
            BadLeaderKey.LeaderKey = "6";*/

            Leader NullLeader = new Leader();
            NullLeader.UserName = "TSwift2";
            NullLeader.FirstName = "Taylor";
            NullLeader.LastName = "Swift";
            NullLeader.MiddleName = "Megan";
            NullLeader.Password = "Password1*";
            NullLeader.PhoneNumber = "708-899-2222";
            NullLeader.Gender = "Female";
            NullLeader.Birthdate = "2/23/19";
            NullLeader.LeaderKey = "5";

            Leader BadPasswordLeader = new Leader();
            BadPasswordLeader.UserName = "TSwift";
            BadPasswordLeader.FirstName = "Taylor";
            BadPasswordLeader.LastName = "Swift";
            BadPasswordLeader.MiddleName = "Megan";
            BadPasswordLeader.Password = "notvalid";
            BadPasswordLeader.Address = "555 Hollywood Ave";
            BadPasswordLeader.PhoneNumber = "708-899-2222";
            BadPasswordLeader.Gender = "Female";
            BadPasswordLeader.Birthdate = "2/23/19";
            BadPasswordLeader.Email = "bradbergeron90@me.com";
            BadPasswordLeader.LeaderKey = "5";

          /*  var CreateLeaderResult1 = TestVerifiation.Check(GoodLeader);
            var CreateLeaderResult2 = TestVerifiation.Check(NullLeader);
            var CreateLeaderResult3 = TestVerifiation.Check(BadPasswordLeader);
            //var CreateLeaderResult4 = BadLeaderKey.isValid(BadLeaderKey);

            Assert.AreEqual("Valid", CreateLeaderResult1);
            Assert.AreEqual("NullFields", CreateLeaderResult2);
            Assert.AreEqual("Char", CreateLeaderResult3);
            //Assert.AreEqual("LeaderKey", CreateLeaderResult4);*/

        }

        [TestMethod]
        public void Admin()
        {
            Administrator GoodAdmin = new Administrator();
            GoodAdmin.UserName = "HFSKS";
            GoodAdmin.FirstName = "Taylor";
            GoodAdmin.LastName = "Swift";
            GoodAdmin.MiddleName = "Megan";
            GoodAdmin.Password = "Password1*";
            GoodAdmin.Address = "555 Hollywood Ave";
            GoodAdmin.PhoneNumber = "708-899-2222";
            GoodAdmin.Gender = "Female";
            GoodAdmin.Birthdate = "2/23/19";
            GoodAdmin.Email = "bradbergeron90@me.com";
            GoodAdmin.AdminKey = "5";

           /* Administrator BadAdminKey = new Administrator();
            BadAdminKey.UserName = "HFSKS2";
            BadAdminKey.FirstName = "Taylor";
            BadAdminKey.LastName = "Swift";
            BadAdminKey.MiddleName = "Megan";
            BadAdminKey.Password = "Password1*";
            BadAdminKey.Address = "555 Hollywood Ave";
            BadAdminKey.PhoneNumber = "708-899-2222";
            BadAdminKey.Gender = "Female";
            BadAdminKey.Birthdate = "2/23/19";
            BadAdminKey.Email = "bradbergeron90@me.com";
            BadAdminKey.AdminKey = "6";*/

            Administrator NullAdmin = new Administrator();
            NullAdmin.UserName = "TSwift";
            NullAdmin.FirstName = "Taylor";
            NullAdmin.LastName = "Swift";
            NullAdmin.MiddleName = "Megan";
            NullAdmin.Password = "Password1*";
            NullAdmin.PhoneNumber = "708-899-2222";
            NullAdmin.Gender = "Female";
            NullAdmin.Birthdate = "2/23/19";
            NullAdmin.AdminKey = "5";

            Administrator BadPasswordAdmin = new Administrator();
            BadPasswordAdmin.UserName = "TSwift2";
            BadPasswordAdmin.FirstName = "Taylor";
            BadPasswordAdmin.LastName = "Swift";
            BadPasswordAdmin.MiddleName = "Megan";
            BadPasswordAdmin.Password = "notvalid";
            BadPasswordAdmin.Address = "555 Hollywood Ave";
            BadPasswordAdmin.PhoneNumber = "708-899-2222";
            BadPasswordAdmin.Gender = "Female";
            BadPasswordAdmin.Birthdate = "2/23/19";
            BadPasswordAdmin.Email = "bradbergeron90@me.com";
            BadPasswordAdmin.AdminKey = "5";

           /* var CreateAdminResult1 = TestVerifiation.Check(GoodAdmin);
            var CreateAdminResult2 = TestVerifiation.Check(NullAdmin);
            var CreateAdminResult3 = TestVerifiation.Check(BadPasswordAdmin);
            //var CreateLeaderResult4 = TestVerifiation.Check(BadAdminKey);

            Assert.AreEqual("Valid", CreateAdminResult1);
            Assert.AreEqual("NullFields", CreateAdminResult2);
            Assert.AreEqual("Char", CreateAdminResult3);*/
        }

        //[TestMethod]
        /*public void Email()
        {
            EmailConfirmation ef = new EmailConfirmation();
            Account GoodEmail = new Account();
            GoodEmail.ConfirmEmail = false;
            GoodEmail.Email = "bradbergeron90@me.com";
            GoodEmail.Password = "Password1*";
            GoodEmail.UserName = "TSwift";
            GoodEmail.Rank = "Member";

            Account BadEmail = new Account();
            BadEmail.ConfirmEmail = false;
            BadEmail.Email = "BadEmail";
            BadEmail.Password = "Passsword1*";
            BadEmail.UserName = "TSwift";
            BadEmail.Rank = "Member";

            var CreateGoodEmailResult1 = ef.sendEmail(GoodEmail.Email);
            var CreateBadEmailResult2 = ef.sendEmail(BadEmail.Email);

            Assert.AreEqual("Sent", CreateGoodEmailResult1);
            Assert.AreEqual("Fail", CreateBadEmailResult2);

        }*/
    }
}
