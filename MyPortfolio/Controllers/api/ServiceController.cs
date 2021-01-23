using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPortfolio.Controllers.api
{
    public class ServiceController : ApiController
    {
        public IHttpActionResult Get()
        {
            IList<ServiceViewModel> model = null;

            using(var db = new MyPortfolioEntities())
            {
                model = db.services.Select(x => new ServiceViewModel()
                {
                    id_service = x.id_service,
                    service_name = x.service_name,
                    service_content = x.service_content
                }).ToList<ServiceViewModel>();
            }

            if (model.Count == 0)
            {
                return NotFound();
            }

            return Ok(model);
        }
        public IHttpActionResult Post(ServiceViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Data Not Valid");
            }

            using (var db = new MyPortfolioEntities())
            {
                db.services.Add(new service()
                {
                    id_service = model.id_service,
                    service_name = model.service_name,
                    service_content = model.service_content,
                });

                db.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult Put(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data Put");
            }
            using(var db = new MyPortfolioEntities())
            {
                var data = db.services.Where(x => x.id_service == model.id_service).FirstOrDefault<service>();
                if(data != null)
                {
                    data.service_name = model.service_name;
                    data.service_content = model.service_content;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id not found");
            }

            using(var db = new MyPortfolioEntities())
            {
                var data = db.services.Where(x => x.id_service == id).FirstOrDefault();

                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return Ok();
        }
    }
}
