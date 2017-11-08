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
        private List<RisRecord> _records;

        public static RisServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RisServices();
                    _instance._records = new List<RisRecord>();
                }
                return _instance;
            }
        }

        public void parseRis(string ris)
        {
            for (int i = 0; i < ris.Length; i++)
            {
                if (CheckSeparator(ris, i))
                {
                    string tag = getTag(ris, i);
                    i = i + 3;
                    _records.Add(new RisRecord(tag, getValue(ris, i)));
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