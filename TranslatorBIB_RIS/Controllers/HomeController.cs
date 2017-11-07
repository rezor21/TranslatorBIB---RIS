using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranslatorBIB_RIS.Services;

namespace TranslatorBIB_RIS.Controllers
{
    public class HomeController : Controller
    {
        private RecordsServices _recordsServices;
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Angular()
        {

            ViewBag.Title = "Angular";

            return View("Angular");
        }
        private void showFile(HttpPostedFileBase file)
        {
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] binData = b.ReadBytes(file.ContentLength);
            string result = System.Text.Encoding.UTF8.GetString(binData);


            ViewData["Text"] = result;

        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
                try
                {
                    var supportedTypes = new[] { "bib", "ris"};

                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("file", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");
                        ViewBag.Message = "Nieprawidłowy plik";
                        return View();
                    }
                    else
                    {
                        showFile(file);
                        ViewBag.Message = "File uploaded successfully ";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return View();
        }

        
    }
}
