using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class DashboardsController : Controller
    {
        // GET: Dashboards
        public ActionResult Index()
        {
            return View();
        }
    }
}