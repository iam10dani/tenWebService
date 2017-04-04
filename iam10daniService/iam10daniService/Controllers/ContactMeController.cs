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
using iam10daniService.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace iam10daniService.Controllers
{
    public class ContactMeController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ContactMe
        public IQueryable<ContactMe> GetContactMes()
        {
            return db.ContactMes;
        }

        // GET: api/ContactMe/5
        [ResponseType(typeof(ContactMe))]
        public IHttpActionResult GetContactMe(Guid id)
        {
            ContactMe contactMe = db.ContactMes.Find(id);
            if (contactMe == null)
            {
                return NotFound();
            }

            return Ok(contactMe);
        }

        // PUT: api/ContactMe/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactMe(Guid id, ContactMe contactMe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactMe.Id)
            {
                return BadRequest();
            }

            db.Entry(contactMe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactMeExists(id))
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

        // the url is http://localhost:61943/Help/Api/GET-api-ContactMe

        // POST: api/ContactMe
        [ResponseType(typeof(ContactMe))]
        public IHttpActionResult PostContactMe(ContactMe contactMe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string msg = "Message from : " + contactMe.name + " \n Phone number : " +
                contactMe.PhoneNumber + " \n" + " Email : " + contactMe.email + "\n" +
                contactMe.massage;
        
                // Your Account SID from twilio.com/console
                var accountSid = "ACd2272839129a568c29f7ab12b6b61b09";
                // Your Auth Token from twilio.com/console
                var authToken = "ac788f96bd6ebc88ce4fd9b3b33e4755";

                TwilioClient.Init(accountSid, authToken);



                var message = MessageResource.Create(
                     to: new PhoneNumber("+27837443209"),
                     from: new PhoneNumber("(909) 570-1488"),
                     body: msg);

                //Console.WriteLine(message.Sid);
                //Console.Write("Press any key to continue.");
                //Console.ReadKey();
            

            //db.ContactMes.Add(contactMe);

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (ContactMeExists(contactMe.Id))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return CreatedAtRoute("DefaultApi", new { id = contactMe.Id }, contactMe);
        }

        // DELETE: api/ContactMe/5
        [ResponseType(typeof(ContactMe))]
        public IHttpActionResult DeleteContactMe(Guid id)
        {
            ContactMe contactMe = db.ContactMes.Find(id);
            if (contactMe == null)
            {
                return NotFound();
            }

            db.ContactMes.Remove(contactMe);
            db.SaveChanges();

            return Ok(contactMe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactMeExists(Guid id)
        {
            return db.ContactMes.Count(e => e.Id == id) > 0;
        }
    }
}