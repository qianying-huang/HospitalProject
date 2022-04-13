using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsPatient
    {
        public PatientDto SelectedPatient { get; set; }
        public IEnumerable<AppointmentsDto> RelatedAppointments { get; set; }
    }
}