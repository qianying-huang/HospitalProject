using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string JobTitle { get; set; }

        public string Responsibility { get; set; }
        public string Qualification { get; set; }
        public string Offer { get; set; }
        /*  
          TODO: AdminId (fk)
         */
        //A job is created by one admin
        //An admin can create many jobs


        //A Job belongs to one department
        //A department can have many jobs
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public virtual Department Department { get; set; }
    }
    public class JobDto
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string Responsibility { get; set; }
        public string Qualification { get; set; }
        public string Offer { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
    }
}