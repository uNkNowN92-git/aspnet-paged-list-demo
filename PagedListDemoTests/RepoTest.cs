using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagedListDemoTests
{
    [TestClass]
    public class RepoTest
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var b = new PagedListDemo.Repositories.BooksRepository.BooksRepository();

            b.Test();
        }
    }
}
