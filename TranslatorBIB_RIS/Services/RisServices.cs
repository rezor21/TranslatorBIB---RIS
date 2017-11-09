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

        
    }
}