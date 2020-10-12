using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Kimo.Models;

namespace Kimo.Controllers
{
    public class Product_CategoryController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();


        // GET: api/Product_Category
        public IQueryable<Product_Category> GetProduct_Category()
        {
            return db.Product_Category;
        }


        [Route("api/Product_Category/getProductTypes/{id}/{custID}")]
        [HttpGet]
        public List<Design> getProductTypes(int id, int custID)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Design> designline = db.Designs.Where(x => x.Product_Type_ID == id && x.Customer_ID == custID).ToList();

            return designline;
        }

        [Route("api/Product_Category/getProductTypeImage/{id}")]
        [HttpGet]
        public List<Product_Type> getProductTypeImage(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Product_Type> types = db.Product_Type.Where(x => x.Name == id).ToList();
            return types;
        }

        [Route("api/Product_Category/getDesignLines/{id}")]
        [HttpGet]
        public List<dynamic> getDesignLines(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Design_Line> lines = db.Design_Line.Where(x => x.Design_ID == id).ToList();

            List<dynamic> list = new List<dynamic>();
            foreach(var line in lines)
            {
                Template_Position template = db.Template_Position.Where(x => x.Template_Position_ID == line.Template_Position_ID).FirstOrDefault();
                Job_Type job = db.Job_Type.Where(x => x.Job_Type_ID == line.Job_Type_ID).FirstOrDefault();
                dynamic newObj = new ExpandoObject();
                newObj.Position = template.Position_ID;
                newObj.JobType = job.Description;
                newObj.Text = line.Text;
                newObj.Width = line.Width;
                newObj.Height = line.Height;
                list.Add(newObj);
            }

            return list;
        }

        [Route("api/Product_Category/createRequest/{custID}/{designID}/{sizeID}/{color}/{webID}/{prodCode}/{quantity}")]
        [HttpGet]
        public List<dynamic> createRequest(int custID, int designID, int sizeID, string color, int webID, string prodCode,int quantity)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<dynamic> list = new List<dynamic>();

            Quotation_Request request = new Quotation_Request();
            request.Customer_ID = custID;
            request.Date = DateTime.Now;
            db.Quotation_Request.Add(request);
            db.SaveChanges();
            list.Add(request);

            if(request != null)
            {
                Quotation_Request_Line quotation_Request_Line = new Quotation_Request_Line();
                quotation_Request_Line.Request_ID = request.Request_ID;
                quotation_Request_Line.Design_ID = designID;
                quotation_Request_Line.Product_Size_ID = sizeID;
                quotation_Request_Line.Colour = color;
                quotation_Request_Line.Web_Link_ID = webID;
                quotation_Request_Line.Product_Code = prodCode;
                quotation_Request_Line.Quantity = quantity;
                db.Quotation_Request_Line.Add(quotation_Request_Line);
                db.SaveChanges();
                list.Add(quotation_Request_Line);
            }
            return list;
        }

        // GET: api/Product_Category/5
        [ResponseType(typeof(Product_Category))]
        public IHttpActionResult GetProduct_Category(int id)
        {
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return NotFound();
            }

            return Ok(product_Category);
        }

        // PUT: api/Product_Category/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct_Category(int id, Product_Category product_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_Category.Product_Category_ID)
            {
                return BadRequest();
            }

            db.Entry(product_Category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_CategoryExists(id))
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

        // POST: api/Product_Category
        [ResponseType(typeof(Product_Category))]
        public IHttpActionResult PostProduct_Category(Product_Category product_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product_Category.Add(product_Category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_Category.Product_Category_ID }, product_Category);
        }

        // DELETE: api/Product_Category/5
        [ResponseType(typeof(Product_Category))]
        public IHttpActionResult DeleteProduct_Category(int id)
        {
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return NotFound();
            }

            db.Product_Category.Remove(product_Category);
            db.SaveChanges();

            return Ok(product_Category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Product_CategoryExists(int id)
        {
            return db.Product_Category.Count(e => e.Product_Category_ID == id) > 0;
        }
    }
}