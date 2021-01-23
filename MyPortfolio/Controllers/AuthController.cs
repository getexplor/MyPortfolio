using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyPortfolio.Models;
using Newtonsoft.Json;

namespace MyPortfolio.Controllers
{
    public class AuthController : Controller
    {

        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Signin(ProfileViewModel model)
        {
            var token = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri("http://localhost:61977/api/auth/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = await client.GetAsync("signin?username=" +  model.username + "&" +"password=" + model.password);

                if (response.IsSuccessStatusCode)
                {
                    var resultMessage = response.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<string>(resultMessage);
                    Session["Token"] = token;
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index","Dashboards");
        }

        public ActionResult signout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}