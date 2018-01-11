using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class RecordsServices
    {
        private List<Record> _records;
        private static RecordsServices _instance;
        private List<HeadLine> _headLines;

        public static RecordsServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecordsServices();
                    _instance._records = new List<Record>();
                    _instance._headLines = new List<HeadLine>();
                }
                return _instance;
            }
        }

       

        public void AddNewRecord(Record record)
        {
            _records.Add(record);
        }

        public List<Record> GetAll()
        {
            return _records;
        }
        public void SetHeadLines()
        {
            
            _headLines.Clear();
            foreach (var record in _records)
            {
                string values="";
                foreach(var headline in _headLines)
                {
                    values += headline.Value;
                }

                    if (record.Type != null) { if (!values.Contains("Type")) { _headLines.Add(new HeadLine("Typ", "Type")); }  }
                if (record.Authors != null && record.Authors.Count > 0) { if (!values.Contains("Authors")) { _headLines.Add(new HeadLine("Autorzy", "Authors")); } }
                    if (record.Title != null) { if (!values.Contains("Title")) { _headLines.Add(new HeadLine("Tytuł", "Title")); } }
                    if (record.BookTitle != null) { if (!values.Contains("BookTitle")) { _headLines.Add(new HeadLine("Tytuł serii/książki", "BookTitle")); } }
                    if (record.Start_page != 0) { if (!values.Contains("Start_page")) { _headLines.Add(new HeadLine("Strona początkowa", "Start_page")); } }
                    if (record.End_page != 0) { if (!values.Contains("End_page")) { _headLines.Add(new HeadLine("Strona końcowa", "End_page")); } }
                    if (record.Release_date != null) { if (!values.Contains("Release_date")) { _headLines.Add(new HeadLine("Data wydania", "Release_date")); } }
                    if (record.Editors != null && record.Editors.Count > 0) { if (!values.Contains("Editors")) { _headLines.Add(new HeadLine("Edytorzy", "Editors")); } }
                    if (record.Edition != null) { if (!values.Contains("Edition")) { _headLines.Add(new HeadLine("Edycja/Wydanie", "Edition")); } }
                    if (record.Volume != 0) { if (!values.Contains("Volume")) { _headLines.Add(new HeadLine("Wolumin/Część", "Volume")); } }
                    if (record.Adress != null) { if (!values.Contains("Adress")) { _headLines.Add(new HeadLine("Miejsce wydania", "Adress")); } }
                    if (record.Publisher != null) { if (!values.Contains("Publisher")) { _headLines.Add(new HeadLine("Opublikowane przez", "Publisher")); } }

                
                
            }
        }

        public List<HeadLine> GetHeadLines()
        {
            return _headLines;
        }

        public List<Record> GetByAuthor(List<string> authors)
        {
            List<Record> foundRecords = new List<Record>();
            foreach (var i in _records)
            {
                int x = 0;
                foreach (var j in authors)
                {
                    int y = 0;
                    foundRecords = _records
                    .Where(record => record.Authors[x] == authors[y])
                .ToList();
                    y++;
                }
                x++;
            }
            return foundRecords;
        }
        public List<Record> GetByEditor(List<string> editors)
        {
            List<Record> foundRecords = new List<Record>();
            foreach (var i in _records)
            {
                int x = 0;
                foreach (var j in editors)
                {
                    int y = 0;
                    foundRecords = _records
                    .Where(record => record.Editors[x] == editors[y])
                .ToList();
                    y++;
                }
                x++;
            }
            return foundRecords;
        }
        public List<Record> GetByTitle(string title)
        {
            List<Record> foundRecords = new List<Record>();
            foreach (var i in _records)
            {
                    foundRecords = _records
                    .Where(record => record.Title.Contains(title))
                .ToList();
            }
            return foundRecords;
        }
        public List<Record> GetByPublisher(string publisher)
        {
            List<Record> foundRecords = new List<Record>();
            foreach (var i in _records)
            {
                foundRecords = _records
                .Where(record => record.Publisher.Contains(publisher))
            .ToList();
            }
            return foundRecords;
        }
        public List<Record> GetByAdress(string country)
        {
            List<Record> foundRecords = new List<Record>();
            foreach (var i in _records)
            {
                foundRecords = _records
                .Where(record => record.Adress.Contains(country))
            .ToList();
            }
            return foundRecords;
        }

        public List<String> GetAllAuthors()
        {
            List<String> authors = new List<String>();
            foreach(var r in _records)
            {
                for(int i = 0; i < r.Authors.Count; i++)
                {
                    if (!authors.Contains(r.Authors[i]))
                    {
                        authors.Add(r.Authors[i]);
                    }
                }
            }
            return authors;
        }
        public List<String> GetAllTittle()
        {
            List<String> tittle = new List<String>();
            foreach(var r in _records)
            {
                if(!tittle.Contains(r.Title))
                    tittle.Add(r.Title);
            }
            return tittle;
        }
        public List<Record> GetAllAuthorRecords(string author)
        {
            List<Record> foundRecords = new List<Record>();
            foreach (var r in _records)
            {
               for(int i=0;i<r.Authors.Count;i++)
                {
                    if (r.Authors[i].Contains(author))
                        foundRecords.Add(r);
                }
            }
            return foundRecords;
        }


        public void Clear()
        {
            _records.Clear();
        }
       

        
    }
}