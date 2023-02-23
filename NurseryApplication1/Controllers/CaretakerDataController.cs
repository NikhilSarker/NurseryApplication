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
    public class CaretakerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CaretakerData/ListCaretakers
        [HttpGet]
        public IEnumerable<CaretakerDto> ListCaretakers()
        {
            List<Caretaker> Caretakers = db.Caretakers.ToList();
            List<CaretakerDto> CaretakerDtos = new List<CaretakerDto>();

            Caretakers.ForEach(c => CaretakerDtos.Add(new CaretakerDto()
            {
                CaretakerId = c.CaretakerId,
                CaretakerLastName = c.CaretakerLastName,
                CaretakerFirstName = c.CaretakerFirstName
                //CategoryName = t.Category.CategoryName

            }));


            return CaretakerDtos;
        }
        // GET: api/CaretakerData/ListCaretakersForTree/1
        [HttpGet]
        public IEnumerable<CaretakerDto> ListCaretakersForTree(int id)
        {
            List<Caretaker> Caretakers = db.Caretakers.Where(
                c=>c.Trees.Any(
                    t=>t.TreeId==id)
                
                ).ToList();
            List<CaretakerDto> CaretakerDtos = new List<CaretakerDto>();

            Caretakers.ForEach(c => CaretakerDtos.Add(new CaretakerDto()
            {
                CaretakerId = c.CaretakerId,
                CaretakerLastName = c.CaretakerLastName,
                CaretakerFirstName = c.CaretakerFirstName
                //CategoryName = t.Category.CategoryName

            }));


            return CaretakerDtos;
        }

        // GET: api/CaretakerData/ListCaretakersNotCaringForTree/1
        [HttpGet]
        public IEnumerable<CaretakerDto> ListCaretakersNotCaringForTree(int id)
        {
            List<Caretaker> Caretakers = db.Caretakers.Where(
                c => !c.Trees.Any(
                    t => t.TreeId == id)

                ).ToList();
            List<CaretakerDto> CaretakerDtos = new List<CaretakerDto>();

            Caretakers.ForEach(c => CaretakerDtos.Add(new CaretakerDto()
            {
                CaretakerId = c.CaretakerId,
                CaretakerLastName = c.CaretakerLastName,
                CaretakerFirstName = c.CaretakerFirstName
                //CategoryName = t.Category.CategoryName

            }));


            return CaretakerDtos;
        }


        // GET: api/CaretakerData/FindCaretaker/5
        [HttpGet]
        [ResponseType(typeof(Caretaker))]
        public IHttpActionResult FindCaretaker(int id)
        {
            Caretaker Caretaker = db.Caretakers.Find(id);
            CaretakerDto CaretakerDto = new CaretakerDto()
            {
                CaretakerId = Caretaker.CaretakerId,
                CaretakerLastName = Caretaker.CaretakerLastName,
                CaretakerFirstName = Caretaker.CaretakerFirstName

                //TreeHeight = Tree.TreeHeight,
                //CategoryName = Tree.Category.CategoryName
            };


            return Ok(CaretakerDto);
        }

        // POST: api/CaretakerData/UpdateCaretaker/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCaretaker(int id, Caretaker caretaker)
        {
            Debug.WriteLine("I have reached the update caretaker method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is valid");
                return BadRequest(ModelState);
            }

            if (id != caretaker.CaretakerId)
            {
                Debug.WriteLine("Id mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + caretaker.CaretakerId);
                Debug.WriteLine("POST parameter" + caretaker.CaretakerLastName);
                Debug.WriteLine("POST parameter" + caretaker.CaretakerFirstName);
                return BadRequest();
            }

            db.Entry(caretaker).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaretakerExists(id))
                {
                    Debug.WriteLine("Caretaker not Found");
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


        // POST: api/CaretakerData/AddCaretaker
        [ResponseType(typeof(Caretaker))]
        [HttpPost]
        public IHttpActionResult AddCaretaker(Caretaker caretaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Caretakers.Add(caretaker);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = caretaker.CaretakerId }, caretaker);
        }


        // POST: api/CaretakerData/DeleteCaretaker/5
        [ResponseType(typeof(Caretaker))]
        [HttpPost]
        public IHttpActionResult DeleteCaretaker(int id)
        {
            Caretaker caretaker = db.Caretakers.Find(id);
            if (caretaker == null)
            {
                return NotFound();
            }

            db.Caretakers.Remove(caretaker);
            db.SaveChanges();

            return Ok(caretaker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaretakerExists(int id)
        {
            return db.Caretakers.Count(e => e.CaretakerId == id) > 0;
        }
    }
}