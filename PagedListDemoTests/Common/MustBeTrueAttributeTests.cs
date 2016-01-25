using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagedListDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagedListDemo.Common.Tests
{
    [TestClass()]
    public class MustBeTrueAttributeTests
    {
        [TestMethod()]
        public void TrueIsValid()
        {
            var mustBetrueAttribute = new MustBeTrueAttribute();

            var actual = mustBetrueAttribute.IsValid(true);

            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void NullIsInvalid()
        {
            var mustBetrueAttribute = new MustBeTrueAttribute();

            var actual = mustBetrueAttribute.IsValid(null);
            //return value != null && value is bool && (bool)value;

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void StringIsInvalid()
        {
            var mustBetrueAttribute = new MustBeTrueAttribute();

            var actual = mustBetrueAttribute.IsValid("string");

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void FalseIsInvalid()
        {
            var mustBetrueAttribute = new MustBeTrueAttribute();

            var actual = mustBetrueAttribute.IsValid(false);

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void FormatErrorMessageTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetClientValidationRulesTest()
        {
            Assert.Fail();
        }
    }
}