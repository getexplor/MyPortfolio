using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPortfolio.Controllers.api
{
    [AuthCustom]
    public class SkillController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            IList<SkillViewModel> model = null;

            using (var db = new MyPortfolioEntities())

                model = db.skills.Select(x => new SkillViewModel()
                {
                    id_skill = x.id_skill,
                    skill_name = x.skill_name,
                    skill_value = x.skill_value
                }).ToList();

            if (model.Count == 0)
            {
                return NotFound();
            }
            return Ok(model);
        }

        public IHttpActionResult Post(SkillViewModel model)
        {
            using(var data = new MyPortfolioEntities())
            {
                data.skills.Add(new skill()
                {
                    id_skill = model.id_skill,
                    skill_name = model.skill_name,
                    skill_value = model.skill_value
                });

                data.SaveChanges();
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(SkillViewModel model)
        {
            using (var db = new MyPortfolioEntities())
            {
                var data = db.skills.Where(x => x.id_skill == model.id_skill).FirstOrDefault<skill>();

                if (data != null)
                {
                    data.skill_name = model.skill_name;
                    data.skill_value = model.skill_value;

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
                return BadRequest("Id Not found");
            }

            using (var db = new MyPortfolioEntities())
            {
                var data = db.skills.Where(x => x.id_skill == id).FirstOrDefault();

                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok();
        }
    }
}
