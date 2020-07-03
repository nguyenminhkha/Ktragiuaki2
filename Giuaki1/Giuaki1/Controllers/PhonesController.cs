using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Giuaki1.Models;

namespace Giuaki1.Controllers
{
    public class PhonesController : ApiController
    {
        private PhoneAPIEntities db = new PhoneAPIEntities();

        // GET: api/Phones
        public IQueryable<Phone> GetPhones()
        {
            return db.Phones;
        }

        // GET: api/Phones/5
        [ResponseType(typeof(Phone))]
        public async Task<IHttpActionResult> GetPhone(int id)
        {
            Phone phone = await db.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        // PUT: api/Phones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPhone(int id, Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phone.Id)
            {
                return BadRequest();
            }

            db.Entry(phone).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(id))
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

        // POST: api/Phones
        [ResponseType(typeof(Phone))]
        public async Task<IHttpActionResult> PostPhone(Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Phones.Add(phone);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = phone.Id }, phone);
        }

        // DELETE: api/Phones/5
        [ResponseType(typeof(Phone))]
        public async Task<IHttpActionResult> DeletePhone(int id)
        {
            Phone phone = await db.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            db.Phones.Remove(phone);
            await db.SaveChangesAsync();

            return Ok(phone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneExists(int id)
        {
            return db.Phones.Count(e => e.Id == id) > 0;
        }
    }
}