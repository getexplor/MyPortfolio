using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortfolio.Models;
using System.Net.Http;

namespace MyPortfolio.Controllers
{
    public class ContactController : Controller
    {
        ContactViewModel model = new ContactViewModel();

        // GET: Contact
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");
                var responseData = client.GetAsync("contact");
                responseData.Wait();

                var result = responseData.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<ContactViewModel>();
                    readData.Wait();

                    model = readData.Result;
                }
                else
                {
                    ModelState.AddModelError(null, "Server error, not found data");
                }
            }

            return View(model);
        }
        public ActionResult UpdateContact(ContactViewModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/contact/putcontact");
                var putData = client.PutAsJsonAsync<ContactViewModel>("putcontact", model);
                putData.Wait();

                var result = putData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(null,"Server Error");
                }

            }
            return View(model);
        }
        public ActionResult UpdateSocial(ContactViewModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/contact/putsocial");
                var putData = client.PutAsJsonAsync<ContactViewModel>("putsocial", model);
                putData.Wait();

                var result = putData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(null, "Server Error");
                }

            }
            return View(model);
        }
    }
}