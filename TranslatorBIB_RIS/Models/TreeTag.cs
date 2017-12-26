using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Services;

namespace TranslatorBIB_RIS.Models
{
    public class TreeTag
    {
        public List<TreeTagCheckValue> CheckValue { get; set; }
        public TreeTagPages Pages { get; set; }
        public TreeTag()
        {
           CheckValue = new List<TreeTagCheckValue>();

        }
    }
}