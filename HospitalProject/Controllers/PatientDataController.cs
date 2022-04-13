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
    public class PatientDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PatientData/ListPatients
        [HttpGet]
        [Route("api/PatientData/ListPatients")]
        public IEnumerable<PatientDto> ListPatients()
        {
            List<Patient> patients = db.Patients.OrderBy(p => p.PatientID).ToList();
            List<PatientDto> patientDtos = new List<PatientDto>();
            patients.ForEach(p => patientDtos.Add(new PatientDto()
            {
                PatientID = p.PatientID,
                PatientFName = p.PatientFName,
                PatientLName = p.PatientLName,
                PatientGender = p.PatientGender,
                DOB = p.DOB,
                PatientPhone = p.PatientPhone,
                AppointmentNo = p.AppointmentNo
            }));
            return patientDtos;
        }

        // GET: api/PatientData/FindPatient/5
        [ResponseType(typeof(Patient))]
        [HttpGet]
        [Route("api/PatientData/FindPatient/{id}")]
        public IHttpActionResult FindPatient(int id)
        {
            Patient Patient = db.Patients.Find(id);
            PatientDto PatientsDto = new PatientDto()
            {
                PatientID = Patient.PatientID,
                PatientFName = Patient.PatientFName,
                PatientLName = Patient.PatientLName,
                PatientGender = Patient.PatientGender,
                DOB = Patient.DOB,
                PatientPhone = Patient.PatientPhone,
                AppointmentNo = Patient.AppointmentNo
            };
            if (Patient == null)
            {
                return NotFound();
            }

            return Ok(PatientsDto);
        }

        // POST: api/PatientData/UpdatePatient/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.PatientID)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/PatientData/AddPatient
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.PatientID }, patient);
        }

        // POST: api/PatientData/DeletePatient/5
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
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

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.PatientID == id) > 0;
        }
    }
}