using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NurseryApplication1.Models;

namespace NurseryApplication1.Controllers
{
    public class TreeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TreeData/ListTrees
        [HttpGet]
        public IEnumerable<TreeDto> ListTrees()
        {
            List<Tree> Trees = db.Trees.ToList();
            List<TreeDto> TreeDtos = new List<TreeDto>();

            Trees.ForEach(t => TreeDtos.Add(new TreeDto()
            {
                TreeId = t.TreeId,
                TreeName = t.TreeName,
                TreeHeight = t.TreeHeight,
                CategoryName = t.Category.CategoryName

            }));


            return TreeDtos;
        }

        // GET: api/TreeData/FindTree/5
        [HttpGet]
        [ResponseType(typeof(Tree))]
        public IHttpActionResult FindTree(int id)
        {
            Tree Tree = db.Trees.Find(id);
            TreeDto TreeDto = new TreeDto()
            {
                TreeId = Tree.TreeId,
                TreeName = Tree.TreeName,
                TreeHeight = Tree.TreeHeight,
                CategoryName = Tree.Category.CategoryName
            };


            return Ok(TreeDto);
        }

        // POST: api/TreeData/UpdateTree/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTree(int id, Tree tree)
        {
            Debug.WriteLine("I have reached the update tree method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is valid");
                return BadRequest(ModelState);
            }

            if (id != tree.TreeId)
            {
                Debug.WriteLine("Id mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + tree.TreeId);
                Debug.WriteLine("POST parameter" + tree.TreeName);
                Debug.WriteLine("POST parameter" + tree.TreeHeight);
                return BadRequest();
            }

            db.Entry(tree).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreeExists(id))
                {
                    Debug.WriteLine("Tree not Found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggers");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TreeData/AddTree
        [ResponseType(typeof(Tree))]
        [HttpPost]
        public IHttpActionResult AddTree(Tree tree)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trees.Add(tree);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tree.TreeId }, tree);
        }

        // POST: api/TreeData/DeleteTree/5
        [ResponseType(typeof(Tree))]
        [HttpPost]
        public IHttpActionResult DeleteTree(int id)
        {
            Tree tree = db.Trees.Find(id);
            if (tree == null)
            {
                return NotFound();
            }

            db.Trees.Remove(tree);
            db.SaveChanges();

            return Ok(tree);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreeExists(int id)
        {
            return db.Trees.Count(e => e.TreeId == id) > 0;
        }
    }
}