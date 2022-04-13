using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;

namespace HospitalProject.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        private static readonly HttpClient client;

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }

        // GET: Patient/List
        public ActionResult List()
        {
            //objective: communicate with our patient data api to retrieve a list of patients
            //curl https://localhost:44338/api/patientdata/listpatients
            PatientList ViewModel = new PatientList();
            string url = "patientdata/ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientDto> patient = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            ViewModel.Patients = patient;
            return View(ViewModel);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            DetailsPatient ViewModel = new DetailsPatient();
            //curl https://localhost:44338/api/patientdata/findPatient/{id}
            string url = "patientdata/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;
            ViewModel.SelectedPatient = selectedPatient;

            url = "AppointmentsData/ListAppointmentsByPatient/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<AppointmentsDto> relatedAppointments = response.Content.ReadAsAsync<IEnumerable<AppointmentsDto>>().Result;

            ViewModel.RelatedAppointments = relatedAppointments;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }


        // GET: Patient/New
        public ActionResult New()
        {
            return View();
        }


        // GET: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            string url = "patientdata/AddPatient";

            string jsonpayload = jss.Serialize(patient);
            System.Diagnostics.Debug.WriteLine(jsonpayload);

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


        // POST: Patient/Update/5
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult Update(int id, Patient patient)
        {
  
            string url = "patientdata/UpdatePatient/" + id;

            string jsonpayload = jss.Serialize(patient);
            //System.Diagnostics.Debug.WriteLine(jsonpayload);

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


        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
  
            string url = "patientdata/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;

            return View(selectedPatient);
        }

        // GET: Patient/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {

            string url = "patientdata/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;

            return View(selectedPatient);
        }

        // POST: Patient/Delete/5
        [HttpPost]

        public ActionResult Delete(int id)
        {
         
            string url = "patientdata/DeletePatient/" + id;

            HttpContent content = new StringContent("");
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
    }
}
