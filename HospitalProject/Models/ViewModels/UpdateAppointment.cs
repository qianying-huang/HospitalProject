using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateAppointment
    {
        public AppointmentsDto SelectedAppointment { get; set; }
        public IEnumerable<PatientDto> PatientOptions { get; set; }
    }
}