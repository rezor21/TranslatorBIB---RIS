using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class Record
    {
        private bool isChecked = true;
        public string Type { get; set; }
        public List<string> Authors { get; set; }
        public string Title { get; set; }
        public string BookTitle { get; set; }
        public DateTime Release_date { get; set; }
        public List<string> Editors { get; set; }
        public string Edition { get; set; }
        public int Volume { get; set; }
        public int Start_page { get; set; }
        public int End_page { get; set; }
        public string Adress { get; set; }
        public string Publisher { get; set; }
        public bool IsChecked {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
            }
        }



        public Record()
        {
            Authors = new List<string>();
            Editors = new List<string>();
        }
    }
}