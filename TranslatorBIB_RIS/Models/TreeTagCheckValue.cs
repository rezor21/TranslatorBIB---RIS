using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class TreeTagCheckValue
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

        public TreeTagCheckValue()
        {
            Value = new List<string>();

        }
    }
}