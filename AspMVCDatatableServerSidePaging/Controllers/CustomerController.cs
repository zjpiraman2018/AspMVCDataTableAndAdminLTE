using AspMVCDatatableServerSidePaging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Linq.Dynamic;

namespace AspMVCDatatableServerSidePaging.Controllers
{
    public class CustomerController : ApiController
    {
        private ApplicationDbContext context;
        public CustomerController()
        {
            context = new ApplicationDbContext();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult LoadData()
        {
            var Request = new HttpRequestWrapper(HttpContext.Current.Request);
            // get Start (paging start index) and length (page size for paging)
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            //Get sort columns valu
            var sortColumn = Request.Form.GetValues("columns[" +
                Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();

            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;


            // get search string
            string searchStr = Request.Form.GetValues("search[value]").FirstOrDefault();


            var v = (from a in context.Customers
                     where (a.ContactName.Contains(searchStr) ||
                 a.CompanyName.Contains(searchStr) ||
                 a.Phone.Contains(searchStr) ||
                 a.Country.Contains(searchStr) ||
                 a.City.Contains(searchStr) ||
                 a.PostalCode.Contains(searchStr))
                     select a);



            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                v = v.OrderBy(sortColumn + " " + sortColumnDir);
            }
            totalRecords = v.Count();
            var data = v.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data });
        }

    }
}
