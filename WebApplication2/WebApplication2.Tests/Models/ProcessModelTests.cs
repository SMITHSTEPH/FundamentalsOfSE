using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;

namespace WebApplication2.Models.Tests
{
    [TestClass()]
    public class ProcessModelTests
    {
        [TestMethod()]
        public void ComputeMedianTest1()
        {
            var pmMock = Mock.Create<ProcessModel>();
            int[] arr = { 5 };
            Assert.AreEqual(pmMock.ComputeMedian(arr), 0); //this is screwed up somehow
        }

        [TestMethod()]
        public void IsWithinRangeTest()
        {
            var pmMock = Mock.Create<ProcessModel>();
            int[] arr = { 0, 1, 3 };
            Assert.AreEqual(pmMock.IsWithinRange(arr, 1), false);
        }

        [TestMethod()]
        public void ComputeDistanceFromMedianTest()
        {
            var pmMock = Mock.Create<ProcessModel>();
            int[] arr = { 0, 1, 3 };
            Assert.AreEqual(pmMock.ComputeDistanceFromMedian(1, arr), 0);
        }

        [TestMethod()]
        public void IsWithinRangeTestNotValid()
        {
            var pmMock = Mock.Create<ProcessModel>();
            int[] arr = { 0, 1, 3 };
            Assert.AreEqual(pmMock.IsWithinRange(arr, -5), false);
        }

        [TestMethod()]
        public void IsValidTest()
        {
            //Assert.IsTrue(Mock.IsProfilerEnabled);
            var pmMock = Mock.Create<ProcessModel>();
            Mock.Arrange(() => pmMock.Questions).Returns(new string[92, 3]);
            string[] arr = new string[92];
            Assert.AreEqual(pmMock.IsValid(arr), false);
        }

        [TestMethod()]
        public void IsValidTest1()
        {
            var pmMock = Mock.Create<ProcessModel>();
            Mock.Arrange(() => pmMock.Questions).Returns(new string[92, 3]);
            string[] arr = new string[4];
            Assert.AreEqual(pmMock.IsValid(arr), false);
        }
    }
}