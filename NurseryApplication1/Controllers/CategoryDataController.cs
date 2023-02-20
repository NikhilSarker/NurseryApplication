using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NurseryApplication1.Models;

namespace NurseryApplication1.Controllers
{
    public class CategoryDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CategoryData/ListCategories
        [HttpGet]
        public IEnumerable<CategoryDto> ListCategories()
        {
            List<Category> Categories = db.Categories.ToList();
            List<CategoryDto> CategoryDtos = new List<CategoryDto>();

            Categories.ForEach(c => CategoryDtos.Add(new CategoryDto()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
                //TreeHeight = t.TreeHeight,
                //CategoryName = t.Category.CategoryName

            }));


            return CategoryDtos;
        }

        // GET: api/CategoryData/FindCategory/5
        [HttpGet]
        [ResponseType(typeof(Category))]
        public IHttpActionResult FindCategory(int id)
        {
            Category Category = db.Categories.Find(id);
            CategoryDto CategoryDto = new CategoryDto()
            {
                CategoryId = Category.CategoryId,
                CategoryName = Category.CategoryName,
                //TreeHeight = Tree.TreeHeight,
                //CategoryName = Tree.Category.CategoryName
            };


            return Ok(CategoryDto);
        }

        // PUT: api/CategoryData/UpdateCategory/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/CategoryData/AddCategory
        [ResponseType(typeof(Category))]
        [HttpPost]
        public IHttpActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryId }, category);
        }

        // DELETE: api/CategoryData/DeleteCategory/5
        [ResponseType(typeof(Category))]
        [HttpPost]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CategoryId == id) > 0;
        }
    }
}