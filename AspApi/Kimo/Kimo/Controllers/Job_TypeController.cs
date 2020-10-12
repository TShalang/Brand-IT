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
    public class Job_TypeController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();

        // GET: api/Job_Type
        public IQueryable<Job_Type> GetJob_Type()
        {
            return db.Job_Type;
        }

        // GET: api/Job_Type/5
        [ResponseType(typeof(Job_Type))]
        public IHttpActionResult GetJob_Type(int id)
        {
            Job_Type job_Type = db.Job_Type.Find(id);
            if (job_Type == null)
            {
                return NotFound();
            }

            return Ok(job_Type);
        }

        // PUT: api/Job_Type/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJob_Type(int id, Job_Type job_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job_Type.Job_Type_ID)
            {
                return BadRequest();
            }

            db.Entry(job_Type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Job_TypeExists(id))
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

        // POST: api/Job_Type
        [ResponseType(typeof(Job_Type))]
        public IHttpActionResult PostJob_Type(Job_Type job_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Job_Type.Add(job_Type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job_Type.Job_Type_ID }, job_Type);
        }

        // DELETE: api/Job_Type/5
        [ResponseType(typeof(Job_Type))]
        public IHttpActionResult DeleteJob_Type(int id)
        {
            Job_Type job_Type = db.Job_Type.Find(id);
            if (job_Type == null)
            {
                return NotFound();
            }

            db.Job_Type.Remove(job_Type);
            db.SaveChanges();

            return Ok(job_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Job_TypeExists(int id)
        {
            return db.Job_Type.Count(e => e.Job_Type_ID == id) > 0;
        }
    }
}