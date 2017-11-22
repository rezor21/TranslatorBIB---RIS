using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class BibContein
    {
        public List<string> bibTags = new List<string>();
        public List<string> bibValues = new List<string>();
        public string bibType { get; set; }//np. Conference
        public string bibKey { get; set; } //np. Moshkov2015
    }
}