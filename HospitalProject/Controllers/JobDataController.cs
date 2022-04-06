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
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class JobDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(JobDto))]
        public IHttpActionResult ListJobs()
        {
            List<Job> Jobs = db.Jobs.ToList();
            List<JobDto> JobDtos = new List<JobDto>();

            Jobs.ForEach(j => JobDtos.Add(new JobDto()
            {
                JobId = j.JobId,
                JobTitle = j.JobTitle,
                Responsibility = j.Responsibility,
                Qualification = j.Qualification,
                Offer = j.Offer,
                DeptId = j.Department.DeptId,
                DeptName = j.Department.DeptName
            }));
            return Ok(JobDtos);
        }




        /// <summary>
        /// Gathers information about all jobs related to a particular department ID
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Department ID</param>
        /// <example>
        /// GET: api/JobData/ListJobsForDepartment/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(JobDto))]
        public IHttpActionResult ListJobsForDepartment(int id)
        {
            List<Job> Jobs = db.Jobs.Where(j=>j.DeptId==id).ToList();
            List<JobDto> JobDtos = new List<JobDto>();

            Jobs.ForEach(j => JobDtos.Add(new JobDto()
            {
                JobId = j.JobId,
                JobTitle = j.JobTitle,
                Responsibility = j.Responsibility,
                Qualification = j.Qualification,
                Offer = j.Offer,
                DeptId = j.Department.DeptId,
                DeptName = j.Department.DeptName
            }));
            return Ok(JobDtos);
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
                Responsibility = Job.Responsibility,
                Qualification = Job.Qualification,
                Offer = Job.Offer,
                DeptId = Job.Department.DeptId,
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
            Debug.WriteLine("Reached the update job method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != job.JobId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter: " + id);
                Debug.WriteLine("POST parameter: " + job.JobId);
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
                    Debug.WriteLine("Job not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the condition triggered");
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