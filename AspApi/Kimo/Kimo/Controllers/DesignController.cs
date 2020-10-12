using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
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
    public class DesignController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();

        // GET: api/Design
        public IHttpActionResult GetDesignsLines()
        {
            List<Design> designs = db.Designs.ToList();
            return Ok(designs);
        }

        // GET: api/Design/5
        [System.Web.Http.Route("api/Design/GetDesignID/{id}")]
        [HttpGet]
        public IHttpActionResult GetDesignID(int id)
        {
            List<dynamic> list = new List<dynamic>();

            List<Design_Line> design_Lines = db.Design_Line.Where(x => x.Design_ID == id).ToList();
            foreach(var design in design_Lines)
            {
                dynamic d = new ExpandoObject();
                Job_Type job = db.Job_Type.Where(x => x.Job_Type_ID == design.Job_Type_ID).FirstOrDefault();

                d.Template_Position_ID = design.Template_Position_ID;
                d.Job_Type_ID = job.Description;
                d.Width = design.Width;
                d.Height = design.Height;
                d.Image = design.Image;
                d.Text = design.Text;
                list.Add(d);

            }
            return Ok(list);
        }

        //var design = (from a in db.Designs
        //              where a.Design_ID == id
        //              select new
        //              {
        //                  a.Design_ID,
        //                  a.Template_ID,
        //                  a.Design_Name,
        //                  a.Additional_Instructions,
        //                  a.Customer_ID,
        //                  a.Date_Created
        //              }).FirstOrDefault();
        //var designLine = (from a in db.Design_Line
        //                  join b in db.Template_Position on a.Template_Position_ID equals b.Template_Position_ID
        //                  join c in db.Job_Type on a.Job_Type_ID equals c.Job_Type_ID
        //                  where a.Design_ID == id

        //                  select new
        //                  {
        //                      a.Design_ID,
        //                      a.Design_Line_ID,
        //                      a.Template_Position_ID,
        //                      Job_Type_ID = c.Description,
        //                      a.Width,
        //                      a.Height,
        //                      a.Image,
        //                      a.Text
        //                  }).ToList();
        //return Ok(new { design, designLine });


        //var design = (from a in db.Designs
        //                  join b in db.Design_Line on a.Design_ID equals b.Design_ID                             
        //                  where a.Design_ID == id

        //                  select new
        //                  {
        //                      a.Design_ID,
        //                      a.Product_Type_ID,
        //                      a.Design_Name,
        //                      a.Additional_Instructions,
        //                      a.Customer_ID,
        //                      a.Date_Created,
        //                      b.Design_Line_ID,
        //                      b.Template_Position_ID,
        //                      b.Job_Type.Description,
        //                      b.Width,
        //                      b.Height,
        //                      b.Image,
        //                      b.Text
        //                  }).ToList();

        //return Ok(new { design });



        // PUT: api/Design/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDesign(int id, Design design)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != design.Design_ID)
            {
                return BadRequest();
            }

            db.Entry(design).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignExists(id))
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

        // POST: api/Design
        [ResponseType(typeof(Design))]
        public IHttpActionResult PostDesign(Design design)
        {
            try
            {
                //Design table            
                db.Designs.Add(design);

                //DesignLine table
                foreach (var item in design.Design_Line)
                {
                    
                    db.Design_Line.Add(item);
                }
                db.SaveChanges();
                

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
   
        }

        // DELETE: api/Design/5
        [ResponseType(typeof(Design))]
        public IHttpActionResult DeleteDesign(int id)
        {
            Design design = db.Designs.Find(id);
            if (design == null)
            {
                return NotFound();
            }

            db.Designs.Remove(design);
            db.SaveChanges();

            return Ok(design);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DesignExists(int id)
        {
            return db.Designs.Count(e => e.Design_ID == id) > 0;
        }

      
    }


}