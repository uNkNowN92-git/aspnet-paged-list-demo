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
    public class HtmlHelpersTests
    {
        [TestMethod()]
        public void RadioButtonToggleForTest()
        {
            var books = new Models.BooksModel.BooksModel();
            //HtmlHelpers.RadioButtonToggleFor(x => x.YesOrNo, new { @class = "test-class", id = "test-id" });

            Assert.Fail();
        }
        [TestMethod]
        public void HtmlHelperActionRenderCMSObject_Execute_EnsureInvokeActionCalledWithExpectedControlerAndActionName()
        {
            //Arrange
            //var fakecmsObject = new CMSObject() { ActionName = "foo", ControllerName = "bar" };
            var books = new Models.BooksModel.BooksModel();

            var testableHtmlHelperAction = new TestableHtmlHelperAction();
            HtmlHelpers.HtmlHelperActionFunc = () => testableHtmlHelperAction;

            // Act
            //HtmlHelpers.RadioButtonToggleFor(books => books.YesOrNo, new { @class = "test-class", id = "test-id" });

            // Verify
            //Assert.AreEqual<string>(fakecmsObject.ActionName, testableHtmlHelperAction.Action);
            //Assert.AreEqual<string>(fakecmsObject.ControllerName, testableHtmlHelperAction.Controller);
        }
    }

    public static class MvcHelper
    {
        //public static HtmlHelper<TModel> GetHtmlHelper<TModel>(TModel inputDictionary)
        //{
        //    var viewData = new ViewDataDictionary<TModel>(inputDictionary);
        //    var mockViewContext = new Mock<ViewContext> { CallBase = true };
        //    mockViewContext.Setup(c => c.ViewData).Returns(viewData);
        //    mockViewContext.Setup(c => c.HttpContext.Items).Returns(new Hashtable());

        //    return new HtmlHelper<TModel>(mockViewContext.Object, GetViewDataContainer(viewData));
        //}

        //public static IViewDataContainer GetViewDataContainer(ViewDataDictionary viewData)
        //{
        //    var mockContainer = new Mock<IViewDataContainer>();
        //    mockContainer.Setup(c => c.ViewData).Returns(viewData);
        //    return mockContainer.Object;
        //}
    }

    public class TestableHtmlHelperAction : HtmlHelperActionInvoker
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public override MvcHtmlString InvokeAction(HtmlHelper helper, string action, string controller)
        {
            Action = action;
            Controller = controller;

            return new MvcHtmlString("");
        }
    }
}