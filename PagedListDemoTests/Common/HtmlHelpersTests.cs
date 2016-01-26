using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagedListDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedListDemo.Models.NotificationMessage;

namespace PagedListDemo.Common.Tests
{
    [TestClass()]
    public class HtmlHelpersTests
    {
        private Controller controller;
        private NotificationMessageModel notificationMessageModel;

        [TestInitialize]
        public void Initialize()
        {
            controller = new Controllers.HomeController();
            notificationMessageModel = new NotificationMessageModel
            {
                Message = "message",
                Severity = Severity.Success
            };
        }

        [TestMethod]
        public void NotificationMessageEqualsMessage()
        {
            controller.SetNotificationMessage(notificationMessageModel.Message, notificationMessageModel.Severity);

            var result = controller.TempData["NotificationMessage"] as NotificationMessageModel;
            var actual = result.Message;
            var expected = notificationMessageModel.Message;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotificationMessageSeverityEqualsSeverity()
        {
            controller.SetNotificationMessage(notificationMessageModel.Message, notificationMessageModel.Severity);

            var result = controller.TempData["NotificationMessage"] as NotificationMessageModel;
            var actual = result.Severity;
            var expected = notificationMessageModel.Severity;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNotificationMessageEqualsMessage()
        {
            controller.SetNotificationMessage(notificationMessageModel.Message, notificationMessageModel.Severity);

            var result = HtmlHelpers.GetNotificationMessage(controller);
            var actual = result.Message;
            var expected = notificationMessageModel.Message;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNotificationMessageSeverityEqualsSeverity()
        {
            controller.SetNotificationMessage(notificationMessageModel.Message, notificationMessageModel.Severity);

            var result = HtmlHelpers.GetNotificationMessage(controller);
            var actual = result.Severity;
            var expected = notificationMessageModel.Severity;

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void RenderToastNotificationTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetJsonPropertyNameTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KoPagedListPagerTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void RadioButtonToggleForTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void RadioButtonToggleForTest1()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void RadioButtonToggleForTest2()
        //{
        //    Assert.Fail();
        //}
    }
}