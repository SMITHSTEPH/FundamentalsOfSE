using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models.Tests
{
    [TestClass()]
    public class ProcessModelTests
    {
        [TestMethod()]
        public void ComputeMedianTest()
        {
            Models.ProcessModel ProcessModelTest = new ProcessModel();
            int[] pmArr = { 0, 1, 2 };
            var Result = ProcessModelTest.ComputeMedian(pmArr);
            Assert.AreEqual(Result, 1);
        }
    }
}