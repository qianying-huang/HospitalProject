using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using HospitalProject.Models;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class JobController : Controller
    {
        private static readonly HttpClient client;
        static JobController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }

        // GET: Job/List
        public ActionResult List()
        {
            //objective: communicate with our job data api to retrieve a list of jobs
            //curl https://localhost:44338/api/jobdata/listjobs

            string url = "jobdata/listjobs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<JobDto> jobs = response.Content.ReadAsAsync<IEnumerable<JobDto>>().Result;
            return View(jobs);
        }

        // GET: Job/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our job data api to retrieve one job
            //curl https://localhost:44338/api/jobdata/findjob/{id}

            string url = "jobdata/findjob/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            JobDto selectedJob = response.Content.ReadAsAsync<JobDto>().Result;
            return View(selectedJob);
        }

        // GET: Job/New
        public ActionResult New()
        {
           //information about all species in the system
           //GET api/departmentdata/listdepartment
            return View();
        }

        // POST: Job/Create
        [HttpPost]
        public ActionResult Create(Job job)
        {
            //objective: add a new job into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44338/api/jobdata/addjob
            string url = "jobdata/addjob";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(job);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            client.PostAsync(url, content);

            return RedirectToAction("List");         
        }

        // GET: Job/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Job/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Job/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
