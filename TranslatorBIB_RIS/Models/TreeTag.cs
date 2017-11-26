using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class TreeTag
    {
        public string Category { get; set; }
        public string Tag { get; set; }
        public List<string> Value { get; set; }

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