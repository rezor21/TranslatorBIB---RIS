using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class RisRecord
    {
        public RisRecord(string tag, string value)
        {
            Tag = tag;
            Value = value;
        }

        public string Tag { get; set; }
        public string Value { get; set; }
    }
}