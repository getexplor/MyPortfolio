using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        IEnumerable<PorfolioViewModel> model = null;
        IList<CategoryViewModel> CategoryModel = null;
        // GET: Portfolio
        public ActionResult Index()
        {
            using (var categclient = new HttpClient())
            {
                categclient.BaseAddress = new Uri("http://localhost:61977/api/");
                var responseData = categclient.GetAsync("category");
                responseData.Wait();

                var result = responseData.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<IList<CategoryViewModel>>();
                    readData.Wait();
                    CategoryModel = readData.Result;

                    ViewBag.CategoryList = new SelectList(CategoryModel, "id_category", "category_name");
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/portfolio");
                var getData = client.GetAsync("portfolio");
                getData.Wait();

                var result = getData.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<IList<PorfolioViewModel>>();
                    readData.Wait();

                    model = readData.Result;
                    ViewBag.PortfoList = model;
                }
            }

            return View();
        }
        
        public ActionResult CreateCateg(PorfolioViewModel model)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");
                var postData = client.PostAsJsonAsync<PorfolioViewModel>("category", model);
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
        
        public ActionResult Update(PorfolioViewModel model)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/portfolio");
                var putData = client.PutAsJsonAsync<PorfolioViewModel>("portfolio", model);
                putData.Wait();

                var result = putData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id_portfolio)
        {
            var response = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");
                var deleteData = client.DeleteAsync("portfolio/" + id_portfolio.ToString());
                deleteData.Wait();

                var result = deleteData.Result;
                if (result.IsSuccessStatusCode)
                {
                    response = true;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            response = false;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}