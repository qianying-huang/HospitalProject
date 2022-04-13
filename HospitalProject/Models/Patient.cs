using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        public string PatientFName { get; set; }
        public string PatientLName { get; set; }
        public string PatientGender { get; set; }
        public string PatientPhone { get; set; }
        public DateTime DOB { get; set; }
        public string AppointmentNo { get; set; }
    }

    public class PatientDto
    {
        public int PatientID { get; set; }
        public string PatientFName { get; set; }
        public string PatientLName { get; set; }
        public string PatientGender { get; set; }
        public string PatientPhone { get; set; }
        public DateTime DOB { get; set; }
        public string AppointmentNo { get; set; }
    }
}