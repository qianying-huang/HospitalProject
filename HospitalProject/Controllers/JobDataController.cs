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
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class JobDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/JobData/ListJobs
        [HttpGet]
        public IEnumerable<JobDto> ListJobs()
        {
            List<Job> Jobs = db.Jobs.ToList();
            List<JobDto> JobDtos = new List<JobDto>();

            Jobs.ForEach(j => JobDtos.Add(new JobDto()
            {
                JobId = j.JobId,
                JobTitle = j.JobTitle,
                JobDate = j.JobDate,
                Responsibility = j.Responsibility,
                Qualification = j.Qualification,
                Offer = j.Offer,
                DeptName = j.Department.DeptName
            }));
            return JobDtos;
        }

        // GET: api/JobData/FindJob/5
        [ResponseType(typeof(Job))]
        [HttpGet]
        public IHttpActionResult FindJob(int id)
        {
            Job Job = db.Jobs.Find(id);
            JobDto JobDto = new JobDto()
            {
                JobId = Job.JobId,
                JobTitle = Job.JobTitle,
                JobDate = Job.JobDate,
                Responsibility = Job.Responsibility,
                Qualification = Job.Qualification,
                Offer = Job.Offer,
                DeptName = Job.Department.DeptName
            };
            if (Job == null)
            {
                return NotFound();
            }

            return Ok(JobDto);
        }

        // POST: api/JobData/UpdateJob/7
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateJob(int id, Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.JobId)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/JobData/AddJob
        [ResponseType(typeof(Job))]
        [HttpPost]
        public IHttpActionResult AddJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jobs.Add(job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job.JobId }, job);
        }

        // DELETE: api/JobData/DeleteJob/5
        [ResponseType(typeof(Job))]
        [HttpPost]
        public IHttpActionResult DeleteJob(int id)
        {
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            db.Jobs.Remove(job);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(int id)
        {
            return db.Jobs.Count(e => e.JobId == id) > 0;
        }
    }
}