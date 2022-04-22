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
    public class AppointmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppointmentData/ListAppointments
        [HttpGet]
        [Route("api/AppointmentData/ListAppointments")]
        public IEnumerable<AppointmentsDto> ListAppointments()
        {
            List<Appointments> appointments = db.Appointments.OrderBy(a => a.Date).ToList();
            List<AppointmentsDto> appointmentsDtos = new List<AppointmentsDto>();
            appointmentsDtos.ForEach(a => appointmentsDtos.Add(new AppointmentsDto()
            {
                AppointID = a.AppointID,
                Date = a.Date,
                Time = a.Time,
                Reason = a.Reason,
                PatientFName=a.Patient.PatientFName,
                PatientLName = a.Patient.PatientLName,
                PatientID = a.PatientID

            }));
            return appointmentsDtos;
        }
  // GET: api/AppointmentData//ListAppointmentsByPatient/5
        [HttpGet]
        [Route("api/AppointmentData/ListAppointmentsByPatient/{id}")]
        public IEnumerable<AppointmentsDto> ListAppointmentsByPatient(int id)
        {
            List<Appointments> appointments = db.Appointments.Where(
                a =>a.Patient.PatientID == id).OrderBy(a =>a.Date).ToList();
            List<AppointmentsDto> appointmentsDtos = new List<AppointmentsDto>();
            appointments.ForEach(a => appointmentsDtos.Add(new AppointmentsDto()
            {
                AppointID = a.AppointID,
                Date = a.Date,
                Time = a.Time,
                Reason = a.Reason,
                PatientFName = a.Patient.PatientFName,
                PatientLName = a.Patient.PatientLName,
                PatientID=a.PatientID
            }));
            return appointmentsDtos;
        }
      

        // GET: api/AppointmentData/FindAppointment/5
        [ResponseType(typeof(Appointments))]
        [HttpGet]
        [Route("api/AppointmentData/FindAppointment/{id}")]
        public IHttpActionResult FindAppointment(int id)
        {
            Appointments appointments = db.Appointments.Find(id);
            AppointmentsDto appointmentsDto = new AppointmentsDto()
            {
                AppointID = appointments.AppointID,
                Date = appointments.Date,
                Time = appointments.Time,
                Reason = appointments.Reason,
                PatientFName = appointments.Patient.PatientFName,
                PatientLName = appointments.Patient.PatientLName,
                PatientID = appointments.PatientID,
            };
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointmentsDto);
        }

        // POST: api/AppointmentData/UpdateAppointment/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(int id,Appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointments.AppointID)
            {
                return BadRequest();
            }

            db.Entry(appointments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentsExists(id))
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

     
        // POST: api/AppointmentData/AddAppointment
        [ResponseType(typeof(Appointments))]
        [HttpPost]
        public IHttpActionResult AddAppointment(Appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointments.AppointID }, appointments);
        }

        // DELETE: api/AppointmentData/DeleteAppointment/5
        [ResponseType(typeof(Appointments))]
        [HttpPost]
        public IHttpActionResult DeleteAppointments(int id)
        {
            Appointments appointments = db.Appointments.Find(id);
            if (appointments == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointments);
            db.SaveChanges();

            return Ok(appointments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentsExists(int id)
        {
            return db.Appointments.Count(e => e.AppointID == id) > 0;
        }
    }
}