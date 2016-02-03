using PagedListDemo.Common.PagedList;
using PagedListDemo.Models;
using PagedListDemo.Models.BooksModel;
using PagedListDemo.Repositories.BooksRepository;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace PagedListDemo.Controllers.Api
{
    public class ValuesController : ApiController
    {
        private readonly IBooksRepository _iBooksRepository;

        public ValuesController(IBooksRepository iBooksRepository)
        {
            _iBooksRepository = iBooksRepository;
        }

        public PagedListResult<BooksModel> GetList([FromUri]BooksFilterOptions filters, [FromUri]PagedListOptions pagedListOptions)
        {
            try
            {
                return _iBooksRepository.GetList(filters, pagedListOptions);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}
