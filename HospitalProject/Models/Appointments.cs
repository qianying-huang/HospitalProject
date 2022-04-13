using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models
{
    public class Appointments
    {
        [Key]
        public int AppointID { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Reason { get; set; }

        //An appointment belongs to one patient
        //A patient can have many appointments
        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }
    }

    public class AppointmentsDto
    {
        public int AppointID { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Reason { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PatientID { get; set; }
    }
}