using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            IEnumerable<ServiceViewModel> model = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");
                var responseData = client.GetAsync("service");
                responseData.Wait();

                var result = responseData.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<IEnumerable<ServiceViewModel>>();
                    readData.Wait();

                    model = readData.Result;
                    ViewBag.ServiceList = model;
                }
                else
                {
                    model = Enumerable.Empty<ServiceViewModel>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

            return View();
        }
        public ActionResult Create(ServiceViewModel model)
        {
            using(var client  = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/service");

                var postData = client.PostAsJsonAsync<ServiceViewModel>("service", model);
                postData.Wait();

                var result = postData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error");
            return View(model);
        }
        public ActionResult Update(ServiceViewModel model)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/service");
                var putData = client.PutAsJsonAsync<ServiceViewModel>("service", model);
                putData.Wait();

                var result = putData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public ActionResult Delete(int id_service)
        {
            var response = false;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");
                var deleteData = client.DeleteAsync("service/" + id_service.ToString());
                deleteData.Wait();

                var result = deleteData.Result;
                if (result.IsSuccessStatusCode)
                {
                    response = true;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            response = true;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
    
}