using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class ArticleController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ArticleController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }

        /// <summary>
        /// Gets the authentication cookie sent to this controller.
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            client.DefaultRequestHeaders.Remove("Cookie");

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }

        // GET: Article/List
        public ActionResult List()
        {
            //Objective: retrieve a list of articles
            //curl https://localhost:44338/api/articledata/listarticles

            string url = "articledata/listarticles";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<ArticlesDto> Articles = response.Content.ReadAsAsync<IEnumerable<ArticlesDto>>().Result;

            return View(Articles);
        }

        // GET: Articles/Details/2
        public ActionResult Details(int id)
        {
            DetailsArticle ViewModel = new DetailsArticle();

            //objective: retrieve an article from API
            //curl https://localhost:44338/api/articledata/findarticle/{id}

            string url = "articledata/findarticle/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            ArticlesDto SelectedArticle = response.Content.ReadAsAsync<ArticlesDto>().Result;

            ViewModel.SelectedArticle = SelectedArticle;

            return View(ViewModel);

        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Article/New
        [Authorize]
        public ActionResult New()
        {
            //GET: api/author

            string url = "author/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<AuthorsDto> AuthorsOptions = response.Content.ReadAsAsync<IEnumerable<AuthorsDto>>().Result;

            return View(AuthorsOptions);
        }


        //Post: Article/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Article Article)
        {
            GetApplicationCookie();
            //new Article using the API
            //curl -H "Content-Type:application/json" -d @Article.json https://localhost:44338/api/Articledata/addArticle 
            string url = "articledata/addarticle";

            string jsonpayload = jss.Serialize(Article);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Article/Edit/2
        [Authorize]
        public ActionResult Edit(int id)
        {
            UpdateArticle ViewModel = new UpdateArticle();

            //existing Article information
            string url = "Articledata/findarticle/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArticlesDto SelectedArticle = response.Content.ReadAsAsync<ArticlesDto>().Result;
            ViewModel.SelectedArticle = SelectedArticle;

            // all authors to choose from when updating this article
            url = "author/";
            response = client.GetAsync(url).Result;
            IEnumerable<AuthorsDto> AuthorsOptions = response.Content.ReadAsAsync<IEnumerable<AuthorsDto>>().Result;

            ViewModel.AuthorsOptions = AuthorsOptions;

            return View(ViewModel);
        }

        // POST: Article/Update/2
        [HttpPost]
        [Authorize]
        public ActionResult Update(int id, Article Article)
        {
            GetApplicationCookie();
            string url = "articledata/updatearticle/" + id;
            string jsonpayload = jss.Serialize(Article);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Article/Delete/2
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "articledata/findarticle/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArticlesDto SelectedArticles = response.Content.ReadAsAsync<ArticlesDto>().Result;
            return View(SelectedArticles);
        }

        // POST: Article/Delete/2
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();
            string url = "articledata/deletearticle/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}

