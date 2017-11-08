using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class RisServices
    {
        private static RisServices _instance;
        private List<RisRecord> _RISrecords;
        public List<Record> _records;

        public static RisServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RisServices();
                    _instance._RISrecords = new List<RisRecord>();
                    _instance._records = new List<Record>();
                }
                return _instance;
            }
        }

        public void parseRis(string ris)
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
                        record.Type = risrecord.Value;
                        break;
                    case "PB":
                        record.Publisher = risrecord.Value;
                        break;
                    case "TI":
                        record.Title = risrecord.Value;
                        break;
                    case "PY":
                        //record.Release_date = risrecord.Value;
                        break;
                    case "JF":
                        
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
            foreach (var re in _records)
            {
                Console.WriteLine(re.Type);
            }

            Console.WriteLine(_records.Count);
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

        
    }
}