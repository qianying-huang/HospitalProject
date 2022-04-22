using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class AppointmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();


        static AppointmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }




        // GET: Appointment/List
        public ActionResult List()
        {
            AppointmentList ViewModel = new AppointmentList();
            string url = "AppointmentData/ListAppointments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<AppointmentsDto> appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentsDto>>().Result;
            ViewModel.Appointments = appointments;
            return View(ViewModel);
            
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppointmentsDto selectedAppointmentDto = response.Content.ReadAsAsync<AppointmentsDto>().Result;
            return View(selectedAppointmentDto);
        }

       
        // GET: Appointment/Error
        public ActionResult Error()
        {
            return View();
        }

        

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Appointment/New
        public ActionResult New()
        {
            
            //need patient information for patient dropdown
            string url = "PatientData/ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> patientOptions = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            return View(patientOptions);
        }



        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(Appointments appointments)
        {
            string url = "AppointmentData/AddAppointment";

            string jsonpayload = jss.Serialize(appointments);

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

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateAppointment ViewModel = new UpdateAppointment();
            //selected appointment information
            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentsDto selectedAppointmentDto = response.Content.ReadAsAsync<AppointmentsDto>().Result;
            ViewModel.SelectedAppointment = selectedAppointmentDto;

            return View(ViewModel);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Appointments appointments)
        {
            string url = "AppointmentData/UpdateAppointment/" + id;

            string jsonpayload = jss.Serialize(appointments);

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



        // GET: Appointment/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            
            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppointmentsDto selectedAppointmentDto = response.Content.ReadAsAsync<AppointmentsDto>().Result;
            return View(selectedAppointmentDto);
        }



        // POST: Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "AppointmentData/DeleteAppointment/" + id;
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
