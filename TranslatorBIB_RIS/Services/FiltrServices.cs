using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class FiltrServices
    {
        private FiltrServices _filtrServices;
        private List<RecordFiltr> _recordsFiltr=new List<RecordFiltr>();
        private static FiltrServices _instance;
       

        public static FiltrServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FiltrServices();
                    
                }
                return _instance;
            }
        }
        public void setRecordsFiltr(List<RecordFiltr> r)
        {
            this._recordsFiltr.Clear();
            this._recordsFiltr = r;
        }
        public bool isIntalizeList()
        {
            if (_recordsFiltr == null)
                return false;
            else return true;
        }
        public void clearRecordsMark()
        {
            foreach(var r in _recordsFiltr)
            {
                r.IsChecked = false;
            }
        }
        public List<Record> GetAllAuthorRecords(string author)
        {
            List<Record> foundRecords = new List<Record>();
            foreach (var r in _recordsFiltr)
            {
                for (int i = 0; i < r.record.Authors.Count; i++)
                {
                    if (r.record.Authors[i].Contains(author)&&r.IsChecked==false)
                    {
                        r.IsChecked = true;
                        foundRecords.Add(r);
                    }
                        
                }
            }
            return foundRecords;
        }
        public List<AuthorFiltr> GetAllAuthorsFiltr()
        {
            List<String> authors = new List<String>();
            List<AuthorFiltr> authorsFitr = new List<AuthorFiltr>();
            foreach (var r in _recordsFiltr)
            {
                for (int i = 0; i < r.Authors.Count; i++)
                {
                    if (!authors.Contains(r.Authors[i]))
                    {
                        authors.Add(r.Authors[i]);
                        authorsFitr.Add(new AuthorFiltr(r.Authors[i], r.authorFiltr[i].check));
                    }
                }
            }
           
            return authorsFitr;
        }

        public List<TittleFiltr> GetAllTittleFiltr()
        {
            List<String> tittle = new List<String>();
            List<TittleFiltr> tittleF = new List<TittleFiltr>();
            foreach (var r in _recordsFiltr)
            {

                if (!tittle.Contains(r.Title)) {
                    tittle.Add(r.Title);
                    tittleF.Add(new TittleFiltr(r.Title, r.tittleFiltr.check));
                }
                    
            }
            return tittleF;
        }
        public void markAuthor(string autor)
        {
            foreach(var r in _recordsFiltr)
            {
                foreach(var a in r.authorFiltr)
                {
                    if (a.autor == autor)
                    {
                        a.check = true;
                    }
                }
            }
        }
        public void markTittle(string tittle)
        {
            foreach (var r in _recordsFiltr)
            {
                if (r.tittleFiltr.tittle==tittle)
                     r.tittleFiltr.check = true;
            }
        }
        public void clearMarks()
        {
            foreach(var r in _recordsFiltr)
            {
                foreach (var a in r.authorFiltr)
                {
                    a.check = false;
                }
                r.tittleFiltr.check = false;
            }
        }
       

    }
}