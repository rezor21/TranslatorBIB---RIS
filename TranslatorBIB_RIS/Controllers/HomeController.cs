using System;
using System.Collections.Generic;
using System.IO;
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
        private void parseBib(string bib)
        {
            List<String> nazwy = new List<string>();
            for (int i = 0; i < bib.Length; i++)
            {
                if (bib[i] == '@')
                {
                    String nowy = "";
                    int j = i + 1;
                    do//nazwa np conferenc
                    {
                        nowy += bib[j];
                        j += 1;
                    } while (bib[j] != '{');
                    i = j;
                    nazwy.Add(nowy);

                    j = i + 1;
                    nowy = "";
                    do//naza moskov
                    {
                        nowy += bib[j];
                        j += 1;
                    } while (bib[j] != ',');
                    i = j;
                    nazwy.Add(nowy);
                    nowy = "";
                    bool seqEnd = false;
                    do
                    {

                        if (bib[j] == ',')
                        {

                            j += 1;
                            do
                            {
                                nowy += bib[j];
                                j += 1;

                            } while (bib[j] != '=');
                            nazwy.Add(nowy);
                            nowy = "";
                        }
                        if (bib[j] == '=')
                        {
                            bool defFinded = false;
                            do
                            {
                                j += 1;
                                if (bib[j] == '{')
                                {
                                    defFinded = true;
                                    j += 1;
                                    do
                                    {
                                        nowy += bib[j];
                                        j += 1;
                                    } while (bib[j] != '}');
                                    nazwy.Add(nowy);
                                    nowy = "";
                                }
                            } while (defFinded == false);
                        }
                        j += 1;
                        if (bib[j] == ',' && bib[j + 1] == '}')
                            seqEnd = true;

                    } while (seqEnd == false);


                }


            }
            for (int i = 0; i < nazwy.Count; i++)
            {
                Console.WriteLine(nazwy[i]);
            }
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

                    showFile(file);
                    ViewBag.Message = "File uploaded successfully ";
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
