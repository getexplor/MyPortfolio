using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http;
using System.IO;

namespace MyPortfolio.Controllers.api
{
    public class ProfileController : ApiController
    {
        [HttpGet]
        [Route("api/profiles/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            ProfileViewModel model = null;
            //BaseResponse baseResponse = new BaseResponse();
            UserResponse res = new UserResponse();

            using (var db = new MyPortfolioEntities())
            {
                model = db.users.Where(x => x.id_user == id).Select(x => new ProfileViewModel()
                {
                    id_user = x.id_user,
                    name = x.name,
                    image_path = x.image_path,
                    email = x.email,
                    password = x.password,
                    username = x.username,
                    id_role = x.role.id_role,
                    role_name = x.role.role_name
                }).FirstOrDefault<ProfileViewModel>();

            }

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
        [HttpPut]
        public IHttpActionResult Put(ProfileViewModel model)
        {
            var db = new MyPortfolioEntities();
            UserResponse resp = new UserResponse();


            using (db)
            {
                var data = db.users.Where(x => x.id_user == model.id_user).FirstOrDefault<user>();
                if (data != null)
                {
                    data.email = model.email;
                    data.name = model.name;
                    data.password = model.password;
                    
                    db.SaveChanges();      
                }
                else
                {
                    return BadRequest("Update Filed");
                }
            }
            return Ok();
        }
        [HttpPut]
        [Route("api/profile/updateimage")]
        public IHttpActionResult PutImage()
        {
            //Check if Request contains File.
            if (HttpContext.Current.Request.Files.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //Read the File data from Request.Form collection.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files["image_upload"];

            var id = HttpContext.Current.Request["id"];
            var iid = Convert.ToInt32(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Update Image Profile");
            }

            using (var db = new MyPortfolioEntities())
            {
                var data = db.users.Where(x => x.id_user == iid).FirstOrDefault<user>();
                var oldImage = HttpContext.Current.Request.MapPath(data.image_path);

                if (data != null)
                {

                    var file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    var extension = Path.GetExtension(postedFile.FileName);

                    var fileName = file + DateTime.Now.ToString("yymmssff") + extension;

                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("/Assets/Profile/") + fileName);

                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                    {
                        bytes = br.ReadBytes(postedFile.ContentLength);
                    }

                    data.image_byte = bytes;
                    data.image_title = fileName;
                    data.image_path = "/Assets/Profile/" + fileName;
                    
                    if (db.SaveChanges() > 0)
                    {
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                        return Ok();
                    }
                }
            }
            return Ok();
        }
    }
}
