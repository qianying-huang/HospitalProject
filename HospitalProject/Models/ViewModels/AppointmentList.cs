using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class AppointmentList
    {
        public IEnumerable<AppointmentsDto> Appointments { get; set; }

    }
}