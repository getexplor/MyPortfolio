using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPortfolio.Controllers.api
{
    public class UserResponse
    {
        public object users;
        public String status;
        public String Message;
    }

    public class AuthController : ApiController
    {
        MyPortfolioEntities db = new MyPortfolioEntities();
        UserResponse res = new UserResponse();

        [HttpGet]
        [Route("api/auth/signin")]
        public HttpResponseMessage signin(string username, string password)
        {
            using (db)
            {
                var data = db.users.SingleOrDefault(x => x.username == username && x.password == password);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GetToken(username));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "Invalid username and passowrd");
                }
            }
            //return Ok(res);
        }
    }
}
