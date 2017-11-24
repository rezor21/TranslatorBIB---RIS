using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranslatorBIB_RIS.Models;
using TranslatorBIB_RIS.Services;
namespace TranslatorBIB_RIS.Controllers
{
    public class HomeController : Controller
    {
        private RecordsServices _recordServices;
        private RisServices _risServices;
        private BibServices _bibServices;
        public HomeController()
        {
            _recordServices = RecordsServices.Instance;
            _risServices = RisServices.Instance;
            _bibServices = BibServices.Instance;
        }
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
        
        private string fileToString(HttpPostedFileBase file)
        {
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] binData = b.ReadBytes(file.ContentLength);
            string result = System.Text.Encoding.UTF8.GetString(binData);
            return result;
        }
        private void showFile(HttpPostedFileBase file)
        {
            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] binData = b.ReadBytes(file.ContentLength);
            string result = System.Text.Encoding.UTF8.GetString(binData);


            ViewData["Text"] = result;
            if(fileExt == "ris")
            {
                _risServices.parseFromRis(result);
            }
            
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
                        
                        //showFile(file);
                        
                        if (fileExt == "bib")
                        {
                            string bib = "@Pusto{},}";
                            bib = fileToString(file);
                            return View(_bibServices.ParseBib(bib));
                        }
                        if (fileExt == "ris")
                        {
                            string ris = fileToString(file);
                            _risServices.parseFromRis(ris);
                        }

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

        [HttpGet, Route("Home/DownloadRIS")]
        public ActionResult DownloadRIS()
        {
            if (_recordServices.GetAll().Count != 0)
            {
                _risServices.parseToRis(_recordServices.GetAll());

                var path = System.IO.Path.GetTempPath() + "ris.ris";

                //string directoryPath = Server.MapPath(path);
                return File(path, "application/octet-stream", "ris.ris");
            }
            else
            {
                return View("Index");
            }
            
        }



    }
}
