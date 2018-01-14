using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TranslatorBIB_RIS.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            ViewBag.Title = "Help Page";

            return View();
        }
        
        [HttpGet, Route("Help/DownloadD")]
        public ActionResult DownloadD()
        {
            
            string directoryPath = Server.MapPath("~/Dokumentacja/Dokumentacja.pdf");
                return File(directoryPath, "application/octet-stream", "Dokumentacja.pdf");

        }
        [HttpGet, Route("Help/DownloadP")]
        public ActionResult DownloadP()
        {

            string directoryPath = Server.MapPath("~/Dokumentacja/Podręcznik.pdf");
            return File(directoryPath, "application/octet-stream", "Podręcznik.pdf");

        }
    }
}