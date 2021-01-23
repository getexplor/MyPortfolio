using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class ProfileController : Controller
    {
        HttpClient profileClient = new HttpClient();
        ProfileViewModel model = null;

        // GET: Profile
        public ActionResult Index()
        {
            var id = 1;

            using (profileClient)
            {
                profileClient.BaseAddress = new Uri("http://localhost:61977/api/");
                var responseData = profileClient.GetAsync("profiles/" + id);
                responseData.Wait();

                var result = responseData.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<ProfileViewModel>();
                    readData.Wait();

                    model = readData.Result;
                    ViewBag.ProfileData = model;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View(model);
        }

        public ActionResult updateProfile(ProfileViewModel model)
        {
            using (profileClient)
            {
                profileClient.BaseAddress = new Uri("http://localhost:61977/api/");
                var putData = profileClient.PutAsJsonAsync("profile", model);

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