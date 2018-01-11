using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class TittleFiltr
    {
        public string tittle { get; set; }
        public bool check { get; set; }

        public TittleFiltr(string a, bool c)
        {
            this.tittle = a;
            this.check = c;

        }
    }
}