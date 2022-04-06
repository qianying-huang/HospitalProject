using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateJob
    {
        //This viewmodel is a class which stores information that we need to present to /Job/Update/{}

        //the existing job information

        public JobDto SelectedJob { get; set; }

        // all departments to choose from when updating this job

        public IEnumerable<DepartmentDto> DepartmentOptions { get; set; }
    }
}