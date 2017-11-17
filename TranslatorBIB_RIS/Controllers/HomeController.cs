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
        private List<BibContein> ParseBib(string bib)
        {
            List<BibContein> bibModel = new List<BibContein>();
            
            for (int i = 0; i < bib.Length; i++)
            {

                if (bib[i] == '@')
                {
                    BibContein newBib = new BibContein();
                    String nowy = "";
                    int j = i + 1;
                    do//nazwa np conferenc
                    {
                        nowy += bib[j];
                        j += 1;
                    } while (bib[j] != '{');
                    // nazwy.Add(nowy);
                    newBib.bibType = nowy;
                  
                   
                    j += 1;
                    if (nowy != "Comment")
                    {
                        nowy = "";
                        do//klucz
                        {
                            nowy += bib[j];
                            j += 1;
                        } while (bib[j] != ',');
                        newBib.bibKey = nowy;

                        nowy = "";
                    }
                    else break;
                  
                    nowy = "";
                    bool end = false;
                    do
                    {

                        if (bib[j] == '=')
                        {
                            int jL = j - 1;//tag
                            int jK = j + 3;//value
                            do
                            {


                                nowy = bib[jL] + nowy;
                                jL -= 1;
                            } while (bib[jL] != ',');
                            newBib.bibTags.Add(nowy);
                            nowy = "";
                            do
                            {
                                
                                nowy += bib[jK];
                                jK += 1;
                                
                            } while (bib[jK] != '}');

                          
                            newBib.bibValues.Add(nowy);
                            nowy = "";

                            if (bib[jK + 4]=='}')
                                end = true;
                        }
                        
                        j += 1;

                    } while (end!=true);
                    i = j;
                    bibModel.Add(newBib);
                    

                }
                
             
            }
            return bibModel;
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
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] binData = b.ReadBytes(file.ContentLength);
            string result = System.Text.Encoding.UTF8.GetString(binData);

           
            ViewData["Text"] = result;

        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string bib = "@Pusto{},}";
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
                        bib=fileToString(file);
                        
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

            return View(ParseBib(bib));
        }

        
    }
}
