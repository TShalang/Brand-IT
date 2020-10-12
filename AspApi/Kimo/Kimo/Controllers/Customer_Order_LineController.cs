using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Kimo.Models;

namespace Kimo.Controllers
{
    public class Customer_Order_LineController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();

        // GET: api/Customer_Order_Line
        public IQueryable<Customer_Order_Line> GetCustomer_Order_Line()
        {
            return db.Customer_Order_Line;
        }

        // GET: api/Customer_Order_Line/5
        [ResponseType(typeof(Customer_Order_Line))]
        public IHttpActionResult GetCustomer_Order_Line(int id)
        {
            Customer_Order_Line customer_Order_Line = db.Customer_Order_Line.Find(id);
            if (customer_Order_Line == null)
            {
                return NotFound();
            }

            return Ok(customer_Order_Line);
        }

        // PUT: api/Customer_Order_Line/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer_Order_Line(int id, Customer_Order_Line customer_Order_Line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer_Order_Line.Customer_Order_ID)
            {
                return BadRequest();
            }

            db.Entry(customer_Order_Line).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer_Order_LineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customer_Order_Line
        [ResponseType(typeof(Customer_Order_Line))]
        public IHttpActionResult PostCustomer_Order_Line(Customer_Order_Line customer_Order_Line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer_Order_Line.Add(customer_Order_Line);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer_Order_Line.Customer_Order_ID }, customer_Order_Line);
        }

        // DELETE: api/Customer_Order_Line/5
        [ResponseType(typeof(Customer_Order_Line))]
        public IHttpActionResult DeleteCustomer_Order_Line(int id)
        {
            Customer_Order_Line customer_Order_Line = db.Customer_Order_Line.Find(id);
            if (customer_Order_Line == null)
            {
                return NotFound();
            }

            db.Customer_Order_Line.Remove(customer_Order_Line);
            db.SaveChanges();

            return Ok(customer_Order_Line);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Customer_Order_LineExists(int id)
        {
            return db.Customer_Order_Line.Count(e => e.Customer_Order_ID == id) > 0;
        }
    }
}