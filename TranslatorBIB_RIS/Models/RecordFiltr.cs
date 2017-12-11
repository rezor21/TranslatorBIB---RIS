using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslatorBIB_RIS.Models
{
    public class RecordFiltr : Record
    {
        public bool check = false;
        public List<AuthorFiltr> authorFiltr=new List<AuthorFiltr>();
        public Record record;
        public TittleFiltr tittleFiltr;

       
        public RecordFiltr(Record r)
        {


            record = r;
            this.Adress = r.Adress;
            this.Authors = r.Authors;         
            this.Editors = r.Editors;
            this.End_page = r.End_page;
            this.IsChecked = false;
            this.Publisher = r.Publisher;
            this.Release_date = r.Release_date;
            this.Release_date = r.Release_date;
            this.Start_page = r.Start_page;
            this.Title = r.Title;
            this.Type = r.Type;
            this.Volume = r.Volume;

            this.check = false;
            for (int i = 0; i < Authors.Count; i++)
            {
                AuthorFiltr a=new AuthorFiltr(Authors[i], false);
              
               authorFiltr.Add(a);
            }
            tittleFiltr = new TittleFiltr(r.Title, false);


        }
    }
}