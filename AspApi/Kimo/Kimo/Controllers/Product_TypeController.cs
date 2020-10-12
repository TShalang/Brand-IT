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
    public class Product_TypeController : ApiController
    {   
        private KIMODBEntities db = new KIMODBEntities();

        [Route("api/Product_Type/getProductTypes")]
        [HttpGet]
        public List<Product_Type> getProductTypes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Product_Type.ToList();
        }


        [Route("api/Product_Type/getDesigns/{prodTypeID}/{custID}")]
        [HttpGet]
        public List<Design> getProductTypes(int prodTypeID, int custID)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.Designs.Where(x => x.Product_Type_ID == prodTypeID && x.Customer_ID == custID).ToList();
        }

        [Route("api/Product_Type/getProductType/{id}")]
        [HttpGet]
        public List<Product_Type> getProductType(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Product_Category product_Category = db.Product_Category.Where(x => x.Description == id).FirstOrDefault();
            List<Product_Type> types = db.Product_Type.Where(x => x.Product_Category_ID == product_Category.Product_Category_ID).ToList();
            return types;
        }

        // GET: api/Product_Type
        public IQueryable<Product_Type> GetProduct_Type()
        {
            return db.Product_Type;
        }

        // GET: api/Product_Type/5
        [ResponseType(typeof(Product_Type))]
        public IHttpActionResult GetProduct_Type(int id)
        {
            Product_Type product_Type = db.Product_Type.Find(id);
            if (product_Type == null)
            {
                return NotFound();
            }

            return Ok(product_Type);
        }

        // PUT: api/Product_Type/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct_Type(int id, Product_Type product_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_Type.Product_Type_ID)
            {
                return BadRequest();
            }

            db.Entry(product_Type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_TypeExists(id))
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

        // POST: api/Product_Type
        [ResponseType(typeof(Product_Type))]
        public IHttpActionResult PostProduct_Type(Product_Type product_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product_Type.Add(product_Type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_Type.Product_Type_ID }, product_Type);
        }

        // DELETE: api/Product_Type/5
        [ResponseType(typeof(Product_Type))]
        public IHttpActionResult DeleteProduct_Type(int id)
        {
            Product_Type product_Type = db.Product_Type.Find(id);
            if (product_Type == null)
            {
                return NotFound();
            }

            db.Product_Type.Remove(product_Type);
            db.SaveChanges();

            return Ok(product_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Product_TypeExists(int id)
        {
            return db.Product_Type.Count(e => e.Product_Type_ID == id) > 0;
        }
    }
}