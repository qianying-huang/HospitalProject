using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    [RoutePrefix("api/author")]

    public class AuthorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all authors
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        [ResponseType(typeof(AuthorsDto))]
        [HttpGet]
        public IHttpActionResult ListAuthors()
        {
            List<Author> authors = db.Authors.Include(s => s.AuthorFname).ToList();
            List<AuthorsDto> authorsDtos = new List<AuthorsDto>();

            authors.ForEach(s => authorsDtos.Add(new AuthorsDto()
            {
                AuthorID = s.AuthorID,
                AuthorFname = s.AuthorFname,
                AuthorLname = s.AuthorLname

            }));

            return Ok(authorsDtos);
        }

        /// <summary>
        /// Returns author by primary key
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
            return Created($"/api/author/{author.AuthorID}", author);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteAuthor([FromUri] int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Updates a author
        /// </summary>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateAuthor(int id, Author author)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.AuthorID)
            {
                return BadRequest();
            }

            db.Entry(author).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return StatusCode(System.Net.HttpStatusCode.NoContent);

        }

    }

}
