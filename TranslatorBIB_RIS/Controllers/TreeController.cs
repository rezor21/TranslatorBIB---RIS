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
        private static FiltrServices _filtrServices;
      
       
        private List<AuthorFiltr> authors;
        private List<TittleFiltr> tittle;
      
        public TreeController()
        {
            _recordServices = RecordsServices.Instance;
            _filtrServices = FiltrServices.Instance;
            //if (_filtrServices.isIntalizeList() == false)
            //{
            //     List<RecordFiltr> recordsFiltr;
            //     recordsFiltr = _recordServices.GetAll().Select(i => new RecordFiltr(i)).ToList();
            //    _filtrServices.setRecordsFiltr(recordsFiltr);
                
            //}
           
                 
        }
        [HttpGet]
        public ActionResult Index()
        {
            TreeTag model=new TreeTag();

            List<TreeTagCheckValue> checkValue = new List<TreeTagCheckValue>();
            TreeTagPages pages = new TreeTagPages();
            authors = _filtrServices.GetAllAuthorsFiltr();
            tittle = _filtrServices.GetAllTittleFiltr();
            
            
            

            TreeTagCheckValue firstrecord = new TreeTagCheckValue
            {
                Tag = "Autorzy",
                Value = authors.Select(c => c.autor).ToList(),
                checkValue = authors.Select(c => c.check).ToList()


            };
            firstrecord.IsChecked = true;

            TreeTagCheckValue secondrecord = new TreeTagCheckValue
            {
                Tag = "Tytuły",
                Value = tittle.Select(c => c.tittle).ToList(),
                checkValue = tittle.Select(c => c.check).ToList(),
            };
            checkValue.Add(firstrecord);
            checkValue.Add(secondrecord);
            pages.StartPage = null;
            pages.EndPage= null;
            model.Pages = pages;
            model.CheckValue = checkValue;
            return View("Index", model);
        }
      
        [HttpPost]
        public ActionResult Index(TreeTag model)
        {
            TreeTagPages pages = model.Pages;

            List<TreeTagCheckValue> selectedRecords = model.CheckValue;
            List<Record> records = new List<Record>();          
            List<string> title = new List<string>();
            _filtrServices.clearMarks();
            _filtrServices.clearRecordsMark();
            foreach (var t in selectedRecords)
            {
                if (t.Tag == "Autorzy")
                {
                    List<string> value=new List<string>();
                    bool addAll = true;
                    for(int i = 0; i < t.checkValue.Count; i++)
                    {
                        if (t.checkValue[i])
                        {
                            addAll = false;
                            _filtrServices.markAuthor(t.Value[i]);

                            records.AddRange(_filtrServices.GetAllAuthorRecords(t.Value[i]));
                        }
                       
                    }
                    if (addAll == true)
                    {
                        records.AddRange(_filtrServices.GetAllAuthorRecords());

                    }


                }
                else if (t.Tag == "Tytuły")
                {
                    bool addAll = true;
                    for(int i = 0; i < t.checkValue.Count; i++)
                    {
                        if (t.checkValue[i])
                        {
                            addAll = false;
                            title.Add(t.Value[i]);
                            _filtrServices.markTittle(t.Value[i]);
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
                if (title.Contains(r.Title))
                {
                    if(r.End_page<=pages.EndPage && r.Start_page>=pages.StartPage)

                    _recordServices.AddNewRecord(r);
                }
                    
            }
            
           
            
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
