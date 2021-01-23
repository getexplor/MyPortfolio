using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPortfolio.Models;

namespace MyPortfolio.Controllers.api
{
    public class CategoryController : ApiController
    {
        private MyPortfolioEntities db = new MyPortfolioEntities();

        public IHttpActionResult Get()
        {
            IList<CategoryViewModel> categoryList = db.categorys.Select(x => new CategoryViewModel()
            {
                id_category = x.id_category,
                category_name = x.category_name
            }).ToList<CategoryViewModel>();

            return Ok(categoryList);
        }   
        public IHttpActionResult Post(PorfolioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not data entry");
            }

            using (db)
            {
                db.categorys.Add(new category()
                {
                    category_name = model.category_name,
                });
                db.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult Put(PorfolioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Update Data");
            }
            using (db)
            {
                var data = db.categorys.Where(x => x.id_category == model.id_category).FirstOrDefault<category>();
                if (data != null)
                {
                    data.id_category = model.id_category;
                    data.category_name = model.category_name;

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
                return BadRequest("Fail delete category");
            }

            using (db)
            {
                var data = db.categorys.Where(x => x.id_category == id).FirstOrDefault();
                if (data != null)
                {
                    db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
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
