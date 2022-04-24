using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string AuthorFname { get; set; }
        public string AuthorLname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
    public class AuthorsDto
    {
        public int AuthorID { get; set; }
        public string AuthorFname { get; set; }
        public string AuthorLname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}