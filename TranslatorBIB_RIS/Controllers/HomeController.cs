using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TranslatorBIB_RIS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Angular()
        {

            ViewBag.Title = "Angular Page";

            return View("Angular");
        }
    }
}
