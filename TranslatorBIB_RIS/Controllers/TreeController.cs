using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TranslatorBIB_RIS.Models;
using TranslatorBIB_RIS.Services;

namespace TranslatorBIB_RIS.Controllers
{
    public class TreeController : Controller
    {
        private static RecordsServices _recordServices;
        private List<string> authors;
        private List<string> tittle;
        public TreeController()
        {
            _recordServices = RecordsServices.Instance;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<TreeTag> model = new List<TreeTag>();
            authors = _recordServices.GetAllAuthors();
            tittle = _recordServices.GetAllTittle();
            List<bool> authorsCheck = new List<bool>();
            for(int i = 0; i < authors.Count; i++)
            {
                authorsCheck.Add(false);
            }
            List<bool> tittleCheck = new List<bool>();
            for (int i = 0; i < tittle.Count; i++)
            {
                tittleCheck.Add(false);
            }

            TreeTag firstrecord = new TreeTag
            {
                Tag = "Autorzy",
                Value = authors,
                checkValue=authorsCheck
                

            };
            firstrecord.IsChecked = true;

            TreeTag secondrecord = new TreeTag
            {
                Tag = "Tytuły",
                Value = tittle,
                checkValue = tittleCheck
            };
            model.Add(firstrecord);
            model.Add(secondrecord);
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(List<TreeTag> model)
        {
            List<TreeTag> selectedRecords = model;
            List<Record> records = new List<Record>();          
            List<string> title = new List<string>();

            foreach(var t in selectedRecords)
            {
                if (t.Tag == "Autorzy")
                {
                    List<string> value=new List<string>();
                    for(int i = 0; i < t.checkValue.Count; i++)
                    {
                        if (t.checkValue[i])
                        {
                          
                            records.AddRange(_recordServices.GetAllAuthorRecords(t.Value[i]));
                        }
                    }
                    
                  
                }else if (t.Tag == "Tytuły")
                {
                    bool addAll = true;
                    for(int i = 0; i < t.checkValue.Count; i++)
                    {
                        if (t.checkValue[i])
                        {
                            addAll = false;
                            title.Add(t.Value[i]);
                        }
                           
                    }
                    if (addAll == true)
                    {
                        title.AddRange(t.Value);
                    }
                }
               
            }
            _recordServices.Clear();
            foreach(var r in records)
            {
                if(title.Contains(r.Title))
                    _recordServices.AddNewRecord(r);
            }
           
            
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
