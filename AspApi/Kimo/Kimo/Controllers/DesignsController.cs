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
    public class DesignsController : ApiController
    {
        KIMODBEntities db = new KIMODBEntities();

        [System.Web.Http.Route("api/Designs/getDesigns")]
        [HttpGet]
        public IHttpActionResult GetDesigns()
        {

            List<dynamic> list = new List<dynamic>();

            List<Design> designs = db.Designs.ToList();
            foreach (var design in designs)
            {
                dynamic d = new ExpandoObject();
                Product_Type product_Type = db.Product_Type.Where(x => x.Product_Type_ID == design.Product_Type_ID).FirstOrDefault();

                d.Design_ID = design.Design_ID;
                d.Design_Name = design.Design_Name;
                d.Name = product_Type.Name;
                d.Date_Created = design.Date_Created;
                list.Add(d);

            }
            return Ok(list);
        }
        [System.Web.Http.Route("api/Designs/getDesignDetails/{id}")]
        [HttpGet]

        public System.Object getDesignDetails(int id)
        {
            var designline = (from a in db.Designs
                          join b in db.Design_Line on a.Design_ID equals b.Design_ID
                          select new
                          {
                              a.Design_ID,
                              a.Design_Name,
                              a.Customer_ID,
                              a.Additional_Instructions,
                              a.Date_Created,
                              a.Product_Type.Name,
                              a.Product_Type.ImageString,
                              b.Image,
                              b.Height,
                              b.Width,
                              b.Job_Type.Description,
                              b.Template_Position.Position,
                              b.Text
                          }).ToList();
            return designline;
        }

        [Route("api/Designs/upload")]
        [HttpPost]
        public IHttpActionResult upload([FromBody] dynamic file)
        {
            db.Configuration.ProxyCreationEnabled = false;

            string upload = file.file;

            Product_Type product_Type = new Product_Type();
            
            product_Type.ImageString = upload;
            db.Product_Type.Add(product_Type);
            db.SaveChanges();

            return Ok(product_Type);
        }

        [Route("api/Designs/getDesign/{id}")]
        [HttpGet]
        public List<Product_Type> getDesign(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Product_Type> product_Type = db.Product_Type.Where(x => x.Product_Type_ID == id).ToList();

            return product_Type;
        }

    }
}