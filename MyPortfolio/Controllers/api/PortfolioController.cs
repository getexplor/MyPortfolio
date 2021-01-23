using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPortfolio.Models;
using System.Web;
using System.IO;

namespace MyPortfolio.Controllers.api
{
    public class PortfolioController : ApiController
    {
        private MyPortfolioEntities db = new MyPortfolioEntities();
        
        public IHttpActionResult Get()
        {
            IList<PorfolioViewModel> model = null;

            using (db)
            {
                model = db.portfolios.Select(x => new PorfolioViewModel()
                {
                    id_portfolio = x.id_portfolio,
                    id_category = x.id_category,
                    portfo_name = x.portfo_name,
                    image_path = x.image_path,
                    image_title = x.image_title,
                    category_name = x.category.category_name
                }).ToList<PorfolioViewModel>();
            }

            if (model.Count == 0)
            {
                return NotFound();
            }

            return Ok(model);
        }
        [HttpPost]
        public IHttpActionResult Post()
        {
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Check if Request contains File.

            //Read the File data from Request.Form collection.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files["image_upload"];

            var category = HttpContext.Current.Request["id_category"];
            var title = HttpContext.Current.Request["portfo_name"];

            var file = Path.GetFileNameWithoutExtension(postedFile.FileName);
            var extension = Path.GetExtension(postedFile.FileName);
            
            var filename = file + DateTime.Now.ToString("yymmssff") + extension;

            postedFile.SaveAs(HttpContext.Current.Server.MapPath("/Assets/Images/") + filename);

            //Convert the File data to Byte Array.
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }

            //Insert the File to Database Table.
            MyPortfolioEntities db = new MyPortfolioEntities();
            portfolio porfo = new portfolio
            {
                portfo_name = title,
                id_category = Convert.ToInt32(category),
                image_title = filename,
                image_path = "/Assets/Images/" + filename,
                image_byte = bytes
            };
            db.portfolios.Add(porfo);
            db.SaveChanges();

            return Ok();
        }
        public IHttpActionResult Put(PorfolioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Update Portofolio");
            }

            using (db)
            {
                var data = db.portfolios.Where(x => x.id_portfolio == model.id_portfolio).FirstOrDefault<portfolio>();
                if (data != null)
                {
                    data.id_portfolio = model.id_portfolio;
                    data.id_category = model.id_category;
                    data.portfo_name = model.portfo_name;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        public IHttpActionResult Delete(int Id)
        {
            MyPortfolioEntities db = new MyPortfolioEntities();
            var porto = db.portfolios.Find(Id);

            string image = HttpContext.Current.Request.MapPath(porto.image_path);

            db.Entry(porto).State = System.Data.Entity.EntityState.Deleted;

            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(image))
                {
                    System.IO.File.Delete(image);
                }
                return Ok();
            }

            return Ok();
        }
    }
}
