using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PagedListDemo.Common.PagedList.Tests
{
    [TestClass]
    public class PagedListExtensionTests
    {
        private List<Models.BooksModel.BooksModel> data;

        [TestInitialize]
        public void Initialize()
        {
            data = new List<Models.BooksModel.BooksModel>
            {
                new Models.BooksModel.BooksModel
                {
                    Author = "author 1",
                    Title = "title",
                    Description = "description"
                },
                new Models.BooksModel.BooksModel
                {
                    Author = "author 2",
                    Title = "title",
                    Description = "description"
                },
                new Models.BooksModel.BooksModel
                {
                    Author = "author 3",
                    Title = "title",
                    Description = "description"
                }
            };
        }

        [TestMethod]
        public void GetOneEntry()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                PerPage = 1,
                SortBy = "Author"
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Data.Count();
            var expected = 1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void EntriesEqualsTwo()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                CurrentEntries = 2,
                PerPage = 2
            };

            var actual = pagedListQueryOptions.Entries;
            var expected = 2;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void EntriesEqualsTwoWhenCurrentEntriesEqualsZero()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                CurrentEntries = 0,
                PerPage = 2
            };

            var actual = pagedListQueryOptions.Entries;
            var expected = 2;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void EntriesEqualsTwoWhenCurrentEntriesGreaterThanZeroAndSortOnlyEqualsFalse()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                CurrentEntries = 1,
                PerPage = 2
            };

            var actual = pagedListQueryOptions.Entries;
            var expected = 2;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void EntriesEqualsOneWhenCurrentEntriesGreaterThanZeroAndSortOnlyEqualsTrue()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                CurrentEntries = 1,
                SortOnly = true,
                PerPage = 2
            };

            var actual = pagedListQueryOptions.Entries;
            var expected = 1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void StartEqualsTwo()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                CurrentEntries = 2,
                Page = 3,
                PerPage = 1
            };

            var actual = pagedListQueryOptions.Start;
            var expected = 2;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void StartEqualsZeroWhenSortOnly()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                CurrentEntries = 2,
                Page = 2,
                SortOnly = true
            };

            var actual = pagedListQueryOptions.Start;
            var expected = 0;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TotalDataEqualsZero()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                Page = 5,
                PerPage = 1
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Data.Count();
            var expected = 0;

            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void TotalDataEqualsTwo()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                SortOnly = true,
                CurrentEntries = 2
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Data.Count();
            var expected = 2;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void OrderByEqualsAuthorAscending()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
            };

            var actual = pagedListQueryOptions.OrderBy;
            var expected = "Author ASC";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void OrderByEqualsAuthorDescending()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                SortAsc = false
            };

            var actual = pagedListQueryOptions.OrderBy;
            var expected = "Author DESC";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void OrderByEqualsNull()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortAsc = false
            };

            var actual = pagedListQueryOptions.OrderBy;

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void OrderByEqualsMultipleOrderBy()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortAsc = false,
                IsMultipleSort = true,
                SortBy = "Test ASC, Test2 DESC"
            };

            var actual = pagedListQueryOptions.OrderBy;
            var expected = pagedListQueryOptions.SortBy;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void OrderByEqualsSortByPlusDesc()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortAsc = false,
                IsMultipleSort = false,
                SortBy = "Test ASC, Test2 DESC"
            };

            var actual = pagedListQueryOptions.OrderBy;
            var expected = pagedListQueryOptions.SortBy + " DESC";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetAllEntries()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                SortBy = "Author",
                ShowAll = true,
                PerPage = 1
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Data.Count();
            var expected = 3;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TotalEntriesEqualsThree()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                PerPage = 1,
                SortBy = "Author"
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Details.TotalEntries;
            var expected = 3;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TotalEntriesEqualsThreeWhenSortOnly()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                PerPage = 1,
                SortBy = "Author",
                SortOnly = true
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Details.TotalEntries;
            var expected = 3;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void AscendingOrderByAuthor()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                PerPage = 1,
                SortBy = "Author"
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Data.Select(x => x.Author).First();
            var expected = data.Select(x => x.Author).First();

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void DescendingOrderByAuthor()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                PerPage = 1,
                SortBy = "Author",
                SortAsc = false
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);
            var actual = result.Data.Select(x => x.Author).First();
            var expected = data.Select(x => x.Author).Last();

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void NullResult()
        {
            var pagedListQueryOptions = new PagedList.PagedListOptions
            {
                PerPage = 1
            };

            var result = data.AsQueryable().ToPagedListResult(pagedListQueryOptions);

            Assert.IsNull(result);
        }
    }
}