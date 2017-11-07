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
            string directory = @"D:\Temp\";

            if (file != null && file.ContentLength > 0)
                try
                {
                    var supportedTypes = new[] { "bib", "ris"};

                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("file", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");
                        return View();
                    }
                    if (file.ContentType == "application/octet-stream" && file.FileName.Contains(".bib") || file.FileName.Contains(".ris") && file.ContentType == "application/x-Research-Info-Systems")
                    {
                        showFile(file);
                        var fileName = Path.GetFileName(file.FileName);
                        file.SaveAs(Path.Combine(directory, fileName));
                        ViewBag.Message = "File uploaded successfully ";
                    }
                    else
                    {
                        ViewBag.Message = "Nieprawidłowy plik";
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
