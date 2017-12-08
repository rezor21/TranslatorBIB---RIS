using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Services;

namespace TranslatorBIB_RIS.Models
{
    public class TreeTag
    {
        public string Category { get; set; }
        public string Tag { get; set; }
        public List<string> Value { get; set; }
        public List<bool> checkValue { get; set; }

        private bool isChecked = false;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
            }
        }

        public TreeTag()
        {
            Value = new List<string>();
           
        }
    }
}