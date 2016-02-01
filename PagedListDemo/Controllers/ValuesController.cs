using PagedListDemo.Common.PagedList;
using PagedListDemo.Models;
using PagedListDemo.Models.BooksModel;
using PagedListDemo.Repositories.BooksRepository;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace PagedListDemo.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IBooksRepository iBooksRepository;

        public ValuesController()
        {
            iBooksRepository = DependencyResolver.Current.GetService<IBooksRepository>();
        }

        public PagedListResult<BooksModel> GetList([FromUri]BooksFilterOptions filters, [FromUri]PagedListOptions pagedListOptions)
        {
            try
            {
                return iBooksRepository.GetList(filters, pagedListOptions);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}
