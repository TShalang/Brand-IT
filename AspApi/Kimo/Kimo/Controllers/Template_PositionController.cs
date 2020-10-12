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
    public class Template_PositionController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();

        // GET: api/Template_Position
        public IQueryable<Template_Position> GetTemplate_Position()
        {
            return db.Template_Position;
        }

        // GET: api/Template_Position/5
        [ResponseType(typeof(Template_Position))]
        public IHttpActionResult GetTemplate_Position(int id)
        {
            Template_Position template_Position = db.Template_Position.Find(id);
            if (template_Position == null)
            {
                return NotFound();
            }

            return Ok(template_Position);
        }

        // PUT: api/Template_Position/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTemplate_Position(int id, Template_Position template_Position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != template_Position.Template_Position_ID)
            {
                return BadRequest();
            }

            db.Entry(template_Position).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Template_PositionExists(id))
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

        // POST: api/Template_Position
        [ResponseType(typeof(Template_Position))]
        public IHttpActionResult PostTemplate_Position(Template_Position template_Position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Template_Position.Add(template_Position);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = template_Position.Template_Position_ID }, template_Position);
        }

        // DELETE: api/Template_Position/5
        [ResponseType(typeof(Template_Position))]
        public IHttpActionResult DeleteTemplate_Position(int id)
        {
            Template_Position template_Position = db.Template_Position.Find(id);
            if (template_Position == null)
            {
                return NotFound();
            }

            db.Template_Position.Remove(template_Position);
            db.SaveChanges();

            return Ok(template_Position);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Template_PositionExists(int id)
        {
            return db.Template_Position.Count(e => e.Template_Position_ID == id) > 0;
        }
    }
}