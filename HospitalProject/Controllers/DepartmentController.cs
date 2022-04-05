using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HospitalProject.Models;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }
        // GET: Department/List
        public ActionResult List()
        {
            //objective: communicate with our Department data api to retrieve a list of Departments
            //curl https://localhost:44338/api/departmentdata/listdepartments

            string url = "departmentdata/listdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Department data api to retrieve one Department
            //curl https://localhost:44338/api/departmentdata/finddepartment/{id}

            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto selecteddepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(selecteddepartment);
        }

        // GET: Department/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            //objective: add a new Department into our system using the API
            //curl -H "Content-Type:application/json" -d @Department.json https://localhost:44338/api/departmentdata/adddepartment
            string url = "departmentdata/adddepartment";


            string jsonpayload = jss.Serialize(department);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
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

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
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
