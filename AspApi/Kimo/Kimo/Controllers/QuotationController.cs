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
    public class QuotationController : ApiController
    {
        KIMODBEntities db = new KIMODBEntities();
        [System.Web.Http.Route("api/Quotation/getQuotations")]
        [HttpGet]

        public System.Object GetQuotations()
        {
            var result = (from a in db.Quotations
                          join b in db.Quotation_Line on a.Quotation_ID equals b.Quotation_ID
                          select new
                          {
                              a.Quotation_ID,
                              a.Customer.First_Name,
                              a.Quotation_Status,
                              a.Amount,
                              b.Description,
                              b.Quantity,
                              b.Price,
                              b.Total
                          }).ToList();
            return result;
        }
        [System.Web.Http.Route("api/Quotation/getQuotationDetails/{id}")]
        [HttpGet]

        public System.Object getQuotationDetails(int id)
        {
            var quotationline = (from a in db.Quotations
                                 join b in db.Quotation_Line on a.Quotation_ID equals b.Quotation_ID
                                 select new
                                 {
                                     a.Quotation_ID,
                                     a.Customer.First_Name,
                                     a.Quotation_Status,
                                     a.Amount,
                                     b.Description,
                                     b.Quantity,
                                     b.Price,
                                     b.Total
                                 }).ToList();
            return quotationline;
        }

    }
}