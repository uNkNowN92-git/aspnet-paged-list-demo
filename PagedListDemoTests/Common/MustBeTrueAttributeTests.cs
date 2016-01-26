using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagedListDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PagedListDemo.Common.Tests
{
    [TestClass()]
    public class MustBeTrueAttributeTests
    {
        private MustBeTrueAttribute mustBetrueAttribute;

        [TestInitialize]
        public void Initialize()
        {
            mustBetrueAttribute = new MustBeTrueAttribute();
        }

        [TestMethod]
        public void TrueIsValid()
        {
            var actual = mustBetrueAttribute.IsValid(true);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void NullIsInvalid()
        {
            var actual = mustBetrueAttribute.IsValid(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void StringIsInvalid()
        {
            var actual = mustBetrueAttribute.IsValid("string");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ObjectIsInvalid()
        {
            var actual = mustBetrueAttribute.IsValid(new object());

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void FalseIsInvalid()
        {
            var actual = mustBetrueAttribute.IsValid(false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void FormatErrorMessageTest()
        {
            var actual = mustBetrueAttribute.FormatErrorMessage("name");
            var expected = "The name field must be checked in order to continue.";

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void GetClientValidationRulesTest()
        //{
        //    //var result = mustBetrueAttribute.GetClientValidationRules()
        //    Assert.Fail();
        //}
    }
}