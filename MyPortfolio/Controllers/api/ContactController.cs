using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPortfolio.Models;

namespace MyPortfolio.Controllers.api
{
    public class ContactController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            ContactViewModel model = new ContactViewModel();

            using(var db = new MyPortfolioEntities())
            {
                model = db.contacts.Select(x => new ContactViewModel()
                {
                    id_contact = x.id_contact,
                    address = x.address,
                    phone = x.phone,
                    email = x.email,
                    instagram = x.instagram,
                    twitter = x.twitter,
                    facebook = x.facebook,
                    linkedin = x.linkedin,
                    github = x.github
                }).FirstOrDefault<ContactViewModel>();
            }

            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [Route("api/contact/putcontact")]
        [HttpPut]
        public IHttpActionResult PutContact(ContactViewModel model)
        {
            using (var db = new MyPortfolioEntities())
            {
                var data = db.contacts
                    .Where(x => x.id_contact == model.id_contact)
                    .FirstOrDefault<contact>();

                if (data != null)
                {
                    data.id_contact = model.id_contact;
                    data.address = model.address;
                    data.phone = model.phone;
                    data.email = model.email;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
                
            }
            return Ok();
        }

        [Route("api/contact/putsocial")]
        [HttpPut]
        public IHttpActionResult PutSocial(ContactViewModel model)
        {
            using (var db = new MyPortfolioEntities())
            {
                var data = db.contacts
                    .Where(x => x.id_contact == model.id_contact)
                    .FirstOrDefault<contact>();

                if (data != null)
                {
                    data.instagram = model.instagram;
                    data.twitter = model.twitter;
                    data.facebook = model.facebook;
                    data.linkedin = model.linkedin;
                    data.github = model.github;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

            }
            return Ok();
        }
    }
}
