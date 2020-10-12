using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Kimo.Models;

namespace Kimo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuoteController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();

        public void createQuote(int id, double price)
        {
            Quotation quote = db.Quotations.Where(x => x.Quotation_ID == id).FirstOrDefault();
            Quotation_Line line = db.Quotation_Line.Where(x => x.Quotation_ID == quote.Quotation_ID).FirstOrDefault();
            line.Price = Convert.ToDecimal(price);
            db.SaveChanges();
        }
    }
}
