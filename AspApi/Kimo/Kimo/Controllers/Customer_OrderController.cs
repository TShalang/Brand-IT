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
    public class Customer_OrderController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();

        // GET: api/Customer_Order
        public IQueryable<Customer_Order> GetCustomer_Order()
        {
            return db.Customer_Order;
        }

        // GET: api/Customer_Order/5
        [ResponseType(typeof(Customer_Order))]
        public IHttpActionResult GetCustomer_Order(int id)
        {
            Customer_Order customer_Order = db.Customer_Order.Find(id);
            if (customer_Order == null)
            {
                return NotFound();
            }

            return Ok(customer_Order);
        }

        // PUT: api/Customer_Order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer_Order(int id, Customer_Order customer_Order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer_Order.Customer_Order_ID)
            {
                return BadRequest();
            }

            db.Entry(customer_Order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer_OrderExists(id))
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

        // POST: api/Customer_Order
        [ResponseType(typeof(Customer_Order))]
        public IHttpActionResult PostCustomer_Order(Customer_Order customer_Order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer_Order.Add(customer_Order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer_Order.Customer_Order_ID }, customer_Order);
        }

        // DELETE: api/Customer_Order/5
        [ResponseType(typeof(Customer_Order))]
        public IHttpActionResult DeleteCustomer_Order(int id)
        {
            Customer_Order customer_Order = db.Customer_Order.Find(id);
            if (customer_Order == null)
            {
                return NotFound();
            }

            db.Customer_Order.Remove(customer_Order);
            db.SaveChanges();

            return Ok(customer_Order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Customer_OrderExists(int id)
        {
            return db.Customer_Order.Count(e => e.Customer_Order_ID == id) > 0;
        }
    }
}