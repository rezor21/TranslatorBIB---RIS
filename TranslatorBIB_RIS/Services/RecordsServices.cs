using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class RecordsServices
    {
        private RisServices _risServices;
        private List<Record> _records;
        private static RecordsServices _instance;

        public static RecordsServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecordsServices();
                    _instance._records = new List<Record>();
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
       

        
    }
}