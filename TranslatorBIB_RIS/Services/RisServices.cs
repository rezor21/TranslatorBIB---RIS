using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class RisServices
    {
        private static RisServices _instance;
        private static RecordsServices _recordServices;
        private List<RisRecord> _RISrecords;
        private List<Record> _records;

        public static RisServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RisServices();
                    _instance._RISrecords = new List<RisRecord>();
                    _recordServices = RecordsServices.Instance;
                    _instance._records = _recordServices.GetAll();
                    
                }
                return _instance;
            }
        }

        public void parseFromRis(string ris)
        {
            _records.Clear();
            _RISrecords.Clear();
            readRis(ris);
            Record record = new Record();
            foreach (var risrecord in _RISrecords)
            {
                
                int x = 0;
                switch (risrecord.Tag.ToString())
                {
                    case "TY":
                        record.Type = typeTranslator(risrecord);
                        break;
                    case "PB":
                        record.Publisher = risrecord.Value;
                        break;
                    case "TI":
                        record.Title = risrecord.Value;
                        break;
                    case "PY":
                        DateTime dataValue = parseDate(risrecord.Value);
                        record.Release_date = dataValue;
                        break;
                    case "JF":
                        //to nie wiem czy ma być
                        break;
                    case "VL":
                        x = 0;

                        if (Int32.TryParse(risrecord.Value, out x))
                        {
                            record.Volume = x;
                        }
                        break;
                    case "SP":
                        x = 0;

                        if (Int32.TryParse(risrecord.Value, out x))
                        {
                            record.Start_page = x;
                        }
                        break;
                    case "EP":
                        x = 0;

                        if (Int32.TryParse(risrecord.Value, out x))
                        {
                            record.End_page = x;
                        }
                        break;
                    case "AU":
                        record.Authors.Add(risrecord.Value);
                        break;
                    case "ED":
                        record.Editors.Add(risrecord.Value);
                        break;
                    case "CY":
                        record.Adress = risrecord.Value;
                        break;
                    case "ER":
                        if(risrecord.Value=="0" || risrecord.Value == "")
                        {
                            _records.Add(record);
                            record = new Record();
                        }
                        break;

                }

            }
            
            
        }

        public void readRis(string ris)
        {
            for (int i = 0; i < ris.Length; i++)
            {
                if (CheckSeparator(ris, i))
                {
                    string tag = getTag(ris, i);
                    i = i + 3;
                    _RISrecords.Add(new RisRecord(tag, getValue(ris, i)));
                }
            }
        }

        public DateTime parseDate(string dataString)
        {
            DateTime data = new DateTime();
            string text = "";
            int number=0;
            int year = 0;
            int month = 0;
            int day = 0;
            for (int i = 0; i < dataString.Length; i++)
            {
                text += dataString[i];
                i++;
                text += dataString[i];
                i++;
                text += dataString[i];
                i++;
                text += dataString[i];

                if (Int32.TryParse(text, out number))
                {
                    year = number;
                }
                else { break; }
                try
                {
                    i++;
                    if (dataString[i] != '/')
                    {
                        
                    }
                    else { try { i++; } catch { break; } }
                }
                catch
                {
                    break;
                } 
                
                
                try
                {
                    text = "";
                    text += dataString[i];
                    i++;
                    text += dataString[i];
                    
                    if (text != "00")
                    {
                        if (Int32.TryParse(text, out number))
                        {
                            month = number;
                        }
                        else { break; }
                    }
                    else { break; }
                }
                catch
                {
                    break;
                }
                if (month != 0 && month != 1)
                {
                    data = new DateTime(year, month, 1);
                }
                try
                {
                    i++;
                    if (dataString[i] != '/')
                    {

                    }
                    else { try { i++; } catch { break; } }
                }
                catch
                {
                    break;
                }
                try
                {
                    text = "";
                    text += dataString[i];
                    i++;
                    text += dataString[i];
                    
                    if (text != "00")
                    {
                        if (Int32.TryParse(text, out number))
                        {
                            day = number;
                        }
                        else { break; }
                    }
                    else { try { i++; } catch { break; } }
                }
                catch
                {
                    break;
                }
                
               
                if (month != 0 && day != 0 && month != 1 && day != 1)
                {

                    data = new DateTime(year, month, day);
                }

            }
            
            return data;
        }

        public bool CheckSeparator(string ris, int i)
        {
            String separator = "";
            separator += ris[i];
            i++;
            separator += ris[i];
            i++;
            separator += ris[i];
            if (separator == " - ")
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        
        



        public string getValue(string ris, int i)
        {
            string value = "";
            try
            {
                while (ris[i] != '\r' && ris[i+1] != '\n')
                {
                    value += ris[i];
                    i++;
                }
            }
            catch
            {
                value = "";
                value += ris[ris.Length-1];
            }
            return value;
        }

        public string getTag(string ris, int i)
        {
            string tag = "";
            tag += ris[i-2];
            tag += ris[i-1];
            return tag;
        }

        public List<RisRecord> recordsToRisRecords(List<Record> recordsList)
        {
            List<RisRecord> risRecords = new List<RisRecord>();
            foreach (var record in recordsList)
            {

                int x = 0;
                RisRecord risrecord;

                if (record.Type != "" && record.Type != " ")
                {
                    risrecord = new RisRecord("TY", typeTranslator(record));
                    risRecords.Add(risrecord);
                }
                if (record.Publisher != "" && record.Publisher != " ")
                {
                    risrecord = new RisRecord("PB", record.Publisher);
                    risRecords.Add(risrecord);
                }
                if (record.Title != "" && record.Title != " ")
                {
                    risrecord = new RisRecord("TI", record.Title);
                    risRecords.Add(risrecord);
                }
                if (record.Release_date.Month == 0 && record.Release_date.Day == 0 && record.Release_date.Year == 0 )
                {
                }
                else
                {
                    if (record.Release_date.Month != 0 && record.Release_date.Day != 0 && record.Release_date.Day != 1)
                    {
                        risrecord = new RisRecord("PY", record.Release_date.Year.ToString() + "/" + record.Release_date.Month.ToString() + "/" + record.Release_date.Day.ToString());
                    }
                    else
                    {
                        if (record.Release_date.Month != 0)
                        {
                            risrecord = new RisRecord("PY", record.Release_date.Year.ToString() + "/" + record.Release_date.Month.ToString());
                        }
                        else
                        {
                            risrecord = new RisRecord("PY", record.Release_date.Year.ToString());
                        }
                    }
                    risRecords.Add(risrecord);
                }
                if (record.Volume.ToString() != "" && record.Volume != 0)
                {
                    risrecord = new RisRecord("VL", record.Volume.ToString());
                    risRecords.Add(risrecord);
                }
                if (record.Start_page.ToString() != "")
                {
                    risrecord = new RisRecord("SP", record.Start_page.ToString());
                    risRecords.Add(risrecord);
                }
                if (record.End_page.ToString() != "" && record.End_page != 0)
                {
                    risrecord = new RisRecord("EP", record.End_page.ToString());
                    risRecords.Add(risrecord);
                }
                if (record.Authors.Count > 0)
                {
                    x = 0;
                    foreach (var author in record.Authors)
                    {
                        if (author != "" && author != " ")
                        {
                            risrecord = new RisRecord("AU", record.Authors[x]);
                            risRecords.Add(risrecord);
                            x++;
                        }
                    }

                }
                if (record.Authors.Count > 0)
                {
                    x = 0;
                    foreach (var editor in record.Editors)
                    {
                        if (editor != "" && editor != " ")
                        {
                            risrecord = new RisRecord("ED", record.Editors[x]);
                            risRecords.Add(risrecord);
                            x++;
                        }
                    }

                }
                if (record.Adress != "" && record.Adress != " ")
                {
                   risrecord = new RisRecord("CY", record.Adress);
                    risRecords.Add(risrecord);
                }

                risrecord = new RisRecord("ER", "0");
                risRecords.Add(risrecord);
            }
            return risRecords;
        }

        public List<RisRecord> parseToRis(List<Record> records)
        {
            List<RisRecord> RISrecords = new List<RisRecord>();
            RISrecords = recordsToRisRecords(records);

            string risstring="";
            string separator = " - ";
            string endofline = "\r\n";

            foreach(var risrecord in RISrecords)
            {
                risstring += risrecord.Tag;
                risstring += separator;
                risstring += risrecord.Value;
                risstring += endofline;
            }

            saveAsRISfile(risstring);

            return RISrecords;
        }

        public void saveAsRISfile(string text)
        {

            var path = Path.GetTempPath() + "ris.ris";
            
            try
            {
                System.IO.File.WriteAllText(@path, text);
            }
            catch
            {
                Exception ex = new OutOfMemoryException();
            }
        }

        private string typeTranslator(RisRecord typerecord)
        {
            string type = "";

            switch (typerecord.Value.ToString())
            {
                case "CONF":
                    type = "Conference";
                    break;
                case "JOUR":
                    type = "Article";
                    break;
                case "EJOUR":
                    type = "Article";
                    break;
                case "JFULL":
                    type = "Article";
                    break;
                case "MGZN":
                    type = "Article";
                    break;
                case "NEWS":
                    type = "Article";
                    break;
                case "BOOK":
                    type = "Book";
                    break;
                case "EBOOK":
                    type = "Book";
                    break;
                case "EDBOOK":
                    type = "Booklet";
                    break;
                case "CHAP":
                    type = "Inbook";
                    break;
                case "ECHAP":
                    type = "Inbook";
                    break;
                case "CPAPER":
                    type = "Proceedings";
                    break;
                case "RPRT":
                    type = "Techreport";
                    break;
                case "UNPB":
                    type = "Unpublished";
                    break;

                default:
                    type = "Misc";
                    break;
            }

            return type;
        }

        private string typeTranslator(Record typerecord)
        {
            string type = "";

            switch (typerecord.Type.ToString())
            {
                case "Conference":
                    type = "CONF";
                    break;
                case "Article":
                    type = "JOUR";
                    break;
                case "Book":
                    type = "BOOK";
                    break;
                case "Inbook":
                    type = "CHAP";
                    break;
                case "Proceedings":
                    type = "CPAPER";
                    break;
                case "Techreport":
                    type = "RPRT";
                    break;
                case "Unpublished":
                    type = "UNPB";
                    break;

                default:
                    type = "STAND";
                    break;
            }

            return type;
        }



    }
}