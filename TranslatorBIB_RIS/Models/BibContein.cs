using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class BibContein
    {
        List<string> bibTags = new List<string>();
        List<string> bibValues = new List<string>();
        string bibType; //np. Conference
        string bibKey; //np. Moshkov2015
    }
}