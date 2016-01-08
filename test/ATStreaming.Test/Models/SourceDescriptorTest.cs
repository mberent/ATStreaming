using ATStreaming.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Test.Models
{
    [TestClass]
    public class SourceDescriptorTest
    {
        [TestMethod]
        public void Constructor_UseFileNameConvention_SetMarketAndCompany()
        {
            //Arrange
            var market = "";
            var company = "";
            var fileName = String.Format("{0}_{1}.csv", market, company);

            //Act
            var descriptor = new SourceDescriptor(fileName);

            //Asert
            Assert.AreEqual(market, descriptor.Market);
            Assert.AreEqual(company, descriptor.Company);
        }

        [TestMethod]
        public void Constructor_UseFileNameConventionAndOtherDirectoryLevel_SetMarketAndCompany()
        {
            //Arrange
            var market = "";
            var company = "";
            var fileName = String.Format("SomeDirectory\\{0}_{1}.csv", market, company);

            //Act
            var descriptor = new SourceDescriptor(fileName);

            //Asert
            Assert.AreEqual(market, descriptor.Market);
            Assert.AreEqual(company, descriptor.Company);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFilePath_TrowArgumentNullException()
        {
            //Arrange
            string fileName = null;

            //Act
            var descriptor = new SourceDescriptor(fileName);

            //Asert
        }
    }
}
