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
            int[] arr ={ 0, 1, 3 };
            Assert.AreEqual(pmMock.ComputeMedian(arr), 1);
        }
    }
}