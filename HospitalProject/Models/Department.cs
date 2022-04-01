using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; }
    }
}