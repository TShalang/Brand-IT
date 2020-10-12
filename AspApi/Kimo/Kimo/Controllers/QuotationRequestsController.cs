using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kimo.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using System.Dynamic;
using System.IO;
using System.Web;
using System.Data;
using System.Web.Http.Cors;
using System.ComponentModel;

namespace Kimo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuotationRequestsController : ApiController
    {
        KIMODBEntities db = new KIMODBEntities();
        [System.Web.Http.Route("api/QuotationRequests/getQuotationRequests")]
        [HttpGet]

        public System.Object GetQuotationRequests()
        {
            var result = (from a in db.Quotation_Request
                          join b in db.Quotation_Request_Line on a.Request_ID equals b.Request_ID
                          select new
                          {
                              a.Request_ID,
                              a.Customer.First_Name,
                              a.Date,
                              b.Product_Size.Description,
                              b.Product_Code,
                              b.Quantity,
                              b.Colour,
                              b.Web_Link.Web_Address,
                              b.Design_ID
                          }).ToList();
            return result;
        }
        [System.Web.Http.Route("api/QuotationRequests/getQuotationResults/{id}")]
        [HttpGet]

        public System.Object getQuotationResults(int id)
        {
            var quotationrequest = (from a in db.Quotation_Request
                                    join b in db.Quotation_Request_Line on a.Request_ID equals b.Request_ID
                                    where a.Request_ID == id
                                    select new
                                    {
                                        a.Request_ID,
                                        a.Customer.First_Name,
                                        a.Date,
                                        b.Product_Size.Description,
                                        b.Product_Code,
                                        b.Quantity,
                                        b.Colour,
                                        b.Web_Link.Web_Address,
                                        b.Design_ID
                                    }).ToList();
            return quotationrequest;
        }

    }
}