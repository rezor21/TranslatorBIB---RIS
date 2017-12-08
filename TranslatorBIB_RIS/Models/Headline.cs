using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class HeadLine
    {
        public string Title { get; }
        public string Value { get; }

        public HeadLine (string titile, string value)
        {
            this.Title = titile;
            this.Value = value;
        }
    }
}