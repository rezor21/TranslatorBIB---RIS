using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class AuthorFiltr
    {
        public string autor { get; set; }
        public bool check { get; set; }

        public AuthorFiltr(string a,bool c)
        {
            this.autor = a;
            this.check = c;

        }
    }
}