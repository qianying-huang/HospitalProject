using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;
using System.Diagnostics;


namespace HospitalProject.Controllers
{
    public class ArticleDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Article in the system.
        /// </summary>
        /// <example>
        /// GET: api/ArticleData/ListArticles
        /// </example>
        [ResponseType(typeof(ArticlesDto))]
        [HttpGet]
        public IHttpActionResult ListArticles()
        {
            List<Article> Articles = db.Articles.Include(S => S.Authors).ToList();
            List<ArticlesDto> ArticlesDtos = new List<ArticlesDto>();

            Articles.ForEach(s => ArticlesDtos.Add(new ArticlesDto()
            {
                ArticleID = s.ArticleID,
                ArticleTitle = s.ArticleTitle,
                ArticleBody = s.ArticleBody,
                Published = s.Published,
                AuthorID = s.AuthorID,
                Author = s.Authors
            }));
            return Ok(ArticlesDtos);
        }

        /// <summary>
        /// Returns all Article in the system.
        /// </summary>
        /// <example>
        /// GET: api/ArticleData/FindArticle/2
        /// </example>
        [ResponseType(typeof(Article))]
        [HttpGet]
        public IHttpActionResult FindArticle(int id)
        {
            Article Articles = db.Articles.Find(id);
            Author Author = db.Authors.Find(Articles.AuthorID);
            ArticlesDto ArticlesDto = new ArticlesDto()
            {
                ArticleID = Articles.ArticleID,
                ArticleTitle = Articles.ArticleTitle,
                ArticleBody = Articles.ArticleBody,
                Published = Articles.Published,
                AuthorID = Articles.AuthorID,
                Author = Articles.Authors
            };
            if (Articles == null)
            {
                return NotFound();
            }

            return Ok(ArticlesDto);
        }

        /// <summary>
        /// Updates a Article in the system with POST Data input
        /// </summary>


        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateArticle(int id, Article Article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Article.ArticleID)
            {

                return BadRequest();
            }

            db.Entry(Article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a Article to the system
        /// </summary>
        /// <example>
        /// POST: api/ArticlesData/AddArticle
        /// </example>
        [ResponseType(typeof(Article))]
        [HttpPost]
        public IHttpActionResult AddArticle(Article Article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articles.Add(Article);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Article.ArticleID }, Article);
        }

        /// <summary>
        /// Deletes a Article by its id.
        /// </summary>
        /// <param name="id">The primary key of the Article</param>
        /// <example>
        /// POST: api/ArticleData/DeleteArticle/2
        /// </example>
        [ResponseType(typeof(Article))]
        [HttpPost]
        public IHttpActionResult DeleteArticle(int id)
        {
            Article Article = db.Articles.Find(id);
            if (Article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(Article);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.ArticleID == id) > 0;
        }
    }
}