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
    public class AboutController : ApiController
    {
        MyPortfolioEntities db = new MyPortfolioEntities();

        public IHttpActionResult Get()
        {
            AboutViewModel model = new AboutViewModel();

            using (var data =  new MyPortfolioEntities())
            {
                model = data.abouts.Select(x => new AboutViewModel()
                {
                    id_about = x.id_about,
                    about_me = x.about_me,
                    current_position = x.current_position,
                    birthday = x.birthday,
                    city = x.city,
                    country = x.country,
                    age = x.age,
                    degree = x.degree
                }).FirstOrDefault<AboutViewModel>();
            }

            if (model == null)
            {
                return NotFound();
            }

                return Ok(model);
        }
        [Route("api/about/putresume")]
        public IHttpActionResult PutResume(AboutViewModel model)
        {
            using (var context = new MyPortfolioEntities())
            {
                var data = context.abouts
                    .Where(x => x.id_about == model.id_about)
                    .FirstOrDefault<about>();

                if (data != null)
                {
                    data.id_about = model.id_about;
                    data.about_me = model.about_me;

                    context.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        [Route("api/about/putabout")]
        public IHttpActionResult PutAbout(AboutViewModel model)
        {
            using (var context = new MyPortfolioEntities())
            {
                var data = context.abouts
                    .Where(x => x.id_about == model.id_about)
                    .FirstOrDefault<about>();

                if (data != null)
                {
                    data.id_about = model.id_about;
                    data.current_position = model.current_position;
                    data.birthday = model.birthday;
                    data.city = model.city;
                    data.country = model.country;
                    data.age = model.age;
                    data.degree = model.degree;

                    context.SaveChanges();
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
