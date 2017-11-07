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
        private  List<BibContein> ParseBib(string bib)
        {
            List <BibContein> bibModel = new List<BibContein>();
            List<String> nazwy = new List<string>();
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


                    if (nowy == "Comment")
                        break;
                    
                    if (bib[j] == '{')
                    {
                        nowy = "";
                        j += 1;
                        do//naza moskov
                        {


                            nowy += bib[j];
                            j += 1;

                        } while (bib[j] !=',');
                        i = j;
                        newBib.bibKey = nowy;
                        nowy = "";
                    }

                    bool seqEnd = false;
                    do
                    {

                        if (bib[j] == ',')
                        {

                            j += 1;
                            bool endTag = false;
                            do
                            {
                                nowy += bib[j];
                                if (bib[j] == '}')
                                {
                                    endTag = true;
                                    break;
                                }
                                j += 1;
                                if (j >= bib.Length-1)
                                    break;

                            } while (bib[j] != '=');
                            // nazwy.Add(nowy);
                            if (endTag == false)
                            {
                                newBib.bibTags.Add(nowy);
                            }
                          
                            nowy = "";
                            
                        }
                        if (bib[j] == '=')
                        {
                            bool defFinded = false;
                            do
                            {
                                
                                nowy += bib[j];
                                j += 1;
                                if (bib[j+1] == ',')
                                    break;
                                if (bib[j] == '{')
                                {
                                    nowy = "";
                                    defFinded = true;
                                    j += 1;
                                    do
                                    {
                                        nowy += bib[j];
                                        j += 1;
                                    } while (bib[j] != '}');
                                    //  nazwy.Add(nowy);
                                                                   
                                }
                            } while (defFinded == false);
                            newBib.bibValues.Add(nowy);
                            nowy = "";
                        }
                        j += 1;
                        if (j >= bib.Length)
                            break;
                        if (bib[j] == ',' && bib[j + 1] == '}')
                            seqEnd = true;

                    } while (seqEnd == false);

                    bibModel.Add(newBib);

                }
                

            }
            return bibModel;
            //for (int i = 0; i < nazwy.Count; i++)
            //{
            //    Console.WriteLine(nazwy[i]);
            //}
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
