using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class SkillController : Controller
    {

        MyPortfolioEntities db = new MyPortfolioEntities();

        // GET: Skill
        public ActionResult Index()
        {
            IEnumerable<SkillViewModel> model = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                
                var responseData = client.GetAsync("skill");
                responseData.Wait();

                var result = responseData.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<IList<SkillViewModel>>();
                    readData.Wait();

                    model = readData.Result;
                    ViewBag.SkillList = model;
                }
                else
                {
                    model = Enumerable.Empty<SkillViewModel>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

            return View();
        }

        public ActionResult Create(SkillViewModel model)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["Token"].ToString());

                var postData = client.PostAsJsonAsync<SkillViewModel>("skill", model);

                var result = postData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error");

            return View(model);
        }

        public ActionResult Update(SkillViewModel model)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/skill");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["Token"].ToString());

                var putData = client.PutAsJsonAsync <SkillViewModel>("skill", model);
                putData.Wait();

                var result = putData.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult Delete(int id_skill)
        {
            var response = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61977/api/");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["Token"].ToString());

                var deleteData = client.DeleteAsync("skill/" + id_skill.ToString());
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