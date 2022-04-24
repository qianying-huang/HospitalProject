using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateArticle
    {
        public ArticlesDto SelectedArticle { get; set; }

        public IEnumerable<AuthorsDto> AuthorsOptions { get; set; }

    }
}