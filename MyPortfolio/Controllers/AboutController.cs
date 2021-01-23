using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class AboutController : Controller
    {
        AboutViewModel model = new AboutViewModel();

        // GET: About
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["Token"].ToString());

                var responseData = client.GetAsync("about");
                responseData.Wait();

                var result = responseData.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<AboutViewModel>();
                    readData.Wait();

                    model = readData.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

                return View(model);
        }

        public ActionResult UpdateResume(AboutViewModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/about/putresume");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["Token"].ToString());

                var putData = client.PutAsJsonAsync<AboutViewModel>("PutResume", model);
                putData.Wait();

                var result = putData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult UpdateAbout(AboutViewModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/about/putabout");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["Token"].ToString());

                var putData = client.PutAsJsonAsync<AboutViewModel>("PutAbout", model);
                putData.Wait();

                var result = putData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}