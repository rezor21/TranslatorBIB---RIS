using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Controllers
{
    public class TreeController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            List<TreeTag> model = new List<TreeTag>();

            TreeTag firstrecord = new TreeTag
            {
                Tag = "Autorzy",
                Value = {"Autor1", "Autor2"}
            };

            TreeTag secondrecord = new TreeTag
            {
                Tag = "Tytuły",
                Value = { "Tytuł1", "Tytuł2" }
            };
            model.Add(firstrecord);
            model.Add(secondrecord);
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(List<TreeTag> model)
        {
            List<TreeTag> selectedRecords = model.Where(a => a.IsChecked).ToList();

            return View("~/Views/Home/Angular.cshtml");
        }
    }
}
