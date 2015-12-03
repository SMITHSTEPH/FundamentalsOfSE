using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Web.Mvc;
using Telerik.JustMock;
namespace WebApplication2.Controllers.Tests
{
    [TestClass()]
    public class ProcessModelControllerTests
    {
        [TestMethod()]
        public void QuestionsTest()
        {
            ProcessModelController pmControllerMock = Mock.Create<ProcessModelController>();
            var pmMock = Mock.Create<Models.ProcessModel>();
            var accountMock = Mock.Create<Models.Account>();
            Mock.Arrange(() => pmMock.Questions).Returns(new string[92, 3]);
            Mock.Arrange(() => pmMock.MultipleChoiceAnswers).Returns(new string[10, 6]);
            Mock.Arrange(() => accountMock.AccountId).Returns(1);
            var Result = pmControllerMock.Questions(accountMock) as ViewResult;
            //Assert.AreEqual(Result.ViewName, "Questions");

        }

    }
}