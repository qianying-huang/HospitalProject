using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsDepartment
    {
        public DepartmentDto SelectedDepartment { get; set; }
        public IEnumerable<JobDto> RelatedJobs { get; set; }
    }
}