using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class BibServices
    {
        private static BibServices _instance;
        private List<BibContein> _BIBrecords;
        public List<Record> _records;
        private static RecordsServices _recordServices;
        private FiltrServices _filtrServices;

        public static BibServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BibServices();
                    _instance._BIBrecords = new List<BibContein>();
                    _recordServices = RecordsServices.Instance;
                    _instance._records = _recordServices.GetAll();
                   
                }
                return _instance;
            }
        }
        public void saveToRecords()
        {
           
            foreach (var b in _BIBrecords)
            {
                Record rec = new Record();
               
                int month=1;
                int year=1;
                rec.Type = b.bibType;
                for (int i = 0; i < b.bibTags.Count; i++)
                {
                                 
                    if (b.bibTags[i].Contains("author"))
                    {
                        rec.Authors.Clear();
                        string[] values = b.bibValues[i].Split(new[] { " and " }, StringSplitOptions.None);
                       foreach(var v in values)
                       {
                            rec.Authors.Add(v);
                       }
                        
                        
                    }
                    if (b.bibTags[i].Contains("publisher"))
                    {

                        rec.Publisher = b.bibValues[i];

                    }
                    if (b.bibTags[i].Contains("volume"))
                    {

                        rec.Volume = Int32.Parse(b.bibValues[i]);

                    }
                    if (b.bibTags[i].Contains("address"))
                    {

                        rec.Adress = b.bibValues[i];

                    }
                    if (b.bibTags[i].Contains("edition"))
                    {
                       
                        rec.Edition = b.bibValues[i];

                    }
                    if (b.bibTags[i].Contains("editor"))
                    {


                        rec.Editors.Clear();
                        string[] values = b.bibValues[i].Split(new[] { " and " }, StringSplitOptions.None);
                        foreach (var v in values)
                        {
                            rec.Editors.Add(v);
                        }

                    }
                    if (b.bibTags[i].Contains("title")&& !b.bibTags[i].Contains("booktitle"))
                    {

                        rec.Title = b.bibValues[i];

                    }
                    if (b.bibTags[i].Contains("booktitle"))
                    {

                        rec.BookTitle = b.bibValues[i];

                    }
                    if (b.bibTags[i].Contains("pages"))
                    {
                        if (b.bibValues[i].Contains("-"))
                        {
                            string[] values = b.bibValues[i].Split('-');
                        
                            rec.Start_page = Int32.Parse(values[0]);
                            rec.End_page = Int32.Parse(values[1]);
                        }
                        else
                        {
                            rec.Start_page = Int32.Parse( b.bibValues[i]);
                            rec.End_page = Int32.Parse(b.bibValues[i]);
                        }
                       

                    }
                    if (b.bibTags[i].Contains("month"))
                    {
                        switch(b.bibValues[i]){
                            case "jan":
                                month = 1;
                                break;
                            case "feb":
                                month = 2;
                                break;
                            case "mar":
                                month = 3;
                                break;
                            case "apr":
                                month = 4;
                                break;
                            case "may":
                                month = 5;
                                break;
                            case "jun":
                                month = 6;
                                break;
                            case "jul":
                                month = 7;
                                break;
                            case "aug":
                                month = 8;
                                break;
                            case "sep":
                                month = 9;
                                break;
                            case "oct":
                                month = 10;
                                break;
                            case "nov":
                                month = 11;
                                break;
                            case "dec":
                                month = 12;
                                break;
                        }
                        


                    }
                    if (b.bibTags[i].Contains("year"))
                    {
                        year = Int32.Parse(b.bibValues[i]); 

                    }
                }
                DateTime data = new DateTime(year,month,1);
                rec.Release_date = data;
                _records.Add(rec);
               
               
            }
            _filtrServices = FiltrServices.Instance;
            List<RecordFiltr> recordsFiltr;
            recordsFiltr = _recordServices.GetAll().Select(i => new RecordFiltr(i)).ToList();
            _filtrServices.setRecordsFiltr(recordsFiltr);


        }
        public void parseToBib(List<Record> records)
        {
            string endofline = "\r\n";
            string returnedBib = "% Encoding: UTF-8"+endofline;
            foreach (var r in records)
            {
                returnedBib += "@" + r.Type + "{";
                string keyString = "";
                string authorString = "";
                string editorString = "";
                string pagesString = "";
                string monthString = "";
                for (int i = 0; i < r.Authors.Count; i++)
                {
                    if (i + 1 == r.Authors.Count)
                    {
                        string[] values = r.Authors[i].Split(' ');
                        keyString += values[1] + r.Release_date.Year;
                        authorString += r.Authors[i];
                    }
                    else
                    {
                        string[] values = r.Authors[i].Split(' ');
                        keyString += values[1] + "&";
                        authorString += r.Authors[i] + " and ";
                    }
                }
                for (int i = 0; i < r.Editors.Count; i++)
                {
                    if (i + 1 == r.Editors.Count)                                         
                        editorString += r.Editors[i];                  
                    else                 
                        editorString += r.Editors[i] + " and ";
                    
                }
                if (r.Start_page != r.End_page)
                {
                    pagesString += r.Start_page + "-" + r.End_page;
                }
                else pagesString += r.Start_page;
               
                switch (r.Release_date.Month)
                {
                    case 1:
                        monthString = "jan";
                        break;
                    case 2:                    
                        monthString = "feb";
                        break;
                    case 3:
                        monthString = "mar";
                        break;
                    case 4:
                        monthString = "apr";
                        break;
                    case 5:
                        monthString = "may";
                        break;
                    case 6:
                        monthString = "jun";
                        break;
                    case 7:
                        monthString = "jul";
                        break;
                    case 8:
                        monthString = "aug";
                        break;
                    case 9:
                        monthString = "sep";
                        break;
                    case 10:
                        monthString = "oct";
                        break;
                    case 11:
                        monthString = "nov";
                        break;
                    case 12:
                        monthString = "dec";
                        break;
                }



                
                returnedBib += keyString + ",";
                returnedBib += endofline;

                returnedBib += "author = {" + authorString + " },"+ endofline;
                returnedBib += "title = {" + r.Title + " }," + endofline;
                returnedBib += "booktitle = {" + r.BookTitle + " }," + endofline;
                returnedBib += "edition = {" + r.Edition + " }," + endofline;
                returnedBib += "year = {" + r.Release_date.Year + " }," + endofline;
                returnedBib += "editor = {" + editorString + " }," + endofline;
                returnedBib += "volume = {" + r.Volume + " }," + endofline;
                returnedBib += "pages = {" + pagesString + " }," + endofline;
                returnedBib += "address = {" + r.Adress + " }," + endofline;
                returnedBib += "month = {" + monthString + " }," + endofline;
                returnedBib += "publisher = {" + r.Publisher + " }," + endofline+"}"+endofline;
                
            }
            returnedBib += "@Comment{ jabref - meta: databaseType: bibtex; }";
            saveAsBibFile(returnedBib);
        }
        public void saveAsBibFile(string text)
        {

            var path = Path.GetTempPath() + "bib.bib";

            try
            {
                System.IO.File.WriteAllText(@path, text);
            }
            catch
            {
                Exception ex = new OutOfMemoryException();
            }
        }
        public void ParseBib(string bib)
        {
            _records.Clear();
           
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

                            if (bib[jK + 4] == '}')
                                end = true;
                        }

                        j += 1;

                    } while (end != true);
                    i = j;
                    bibModel.Add(newBib);


                }


            }
            _BIBrecords= bibModel;
        }
    }
}