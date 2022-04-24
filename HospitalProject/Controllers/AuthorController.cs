using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using HospitalProject.Models;
using System.Linq;

namespace HospitalProject.Controllers
{
    public class AuthorController : Controller
    {
        private static readonly HttpClient client;
        private ApplicationDbContext db = new ApplicationDbContext();


        static AuthorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/");
        }

        public ActionResult Index(string search)
        {
            //string url = "api/author";
            //HttpResponseMessage response = client.GetAsync(url).Result;

            //IEnumerable<Author> Authors = response.Content.ReadAsAsync<IEnumerable<Author>>().Result;

            List<Author> authors = db.Authors.ToList();

            return View(authors);


        }

        public ActionResult Details(int id)
        {
            string url = "api/author/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Author author = response.Content.ReadAsAsync<Author>().Result;

            return View(author);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            string url = "api/author";
            HttpResponseMessage response = client.PostAsJsonAsync(url, author).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            string url = "api/author/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Author selectedAuthor = response.Content.ReadAsAsync<Author>().Result;

            return View(selectedAuthor);
        }

        public ActionResult Update(Author author)
        {
            string url = "api/author/" + author.AuthorID;
            HttpResponseMessage response = client.PutAsJsonAsync(url, author).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            string url = "api/author/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
