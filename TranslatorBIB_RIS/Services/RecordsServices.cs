using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class RecordsServices
    {
        private static RecordsServices _instance;
        private List<Record> _records;
        private string text_front;

        private RecordsServices()
        {
            _records = new List<Record>
            {
                new Record
                {
                    Authors = new List<string> {"Moshkov" },
                    Title = "Dynamic Programming Approach for Study of Decision Trees",
                    Release_date = new DateTime(2015, 8, 1),
                    Publisher = "CEUR Workshop Proceedings",
                    Editors = new List<string> {"Czaja", "Suraj"},
                    Start_page = 1,
                    End_page = 1,
                    Country = "Poland",
                    City = "Rzeszów",
                    Volume = 1
                },
                new Record
                {
                    Authors = new List<string> {"Moshkov2" },
                    Title = "a",
                    Release_date = new DateTime(2015, 8, 1),
                    Publisher = "CEUR Workshop Proceedings",
                    Editors = new List<string> {"Czaja", "Suraj"},
                    Start_page = 1,
                    End_page = 25,
                    Country = "Poland",
                    City = "Rzeszów",
                    Volume = 2
                },
                new Record
                {
                    Authors = new List<string> {"Moshkov3" },
                    Title = "v",
                    Release_date = new DateTime(2015, 8, 1),
                    Publisher = "CEUR Workshop Proceedings",
                    Editors = new List<string> {"Czaja", "Suraj"},
                    Start_page = 1,
                    End_page = 12,
                    Country = "Poland",
                    City = "Rzeszów",
                    Volume = 1
                }

            };

        }

        public static RecordsServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecordsServices();
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

        public Record GetByAuthor(List<string> authors)
        {
            Record foundRecord = null;
            foreach (var i in _records)
            {
                int x = 0;
                foreach (var j in authors)
                {
                    int y = 0;
                    foundRecord = _records
                    .Where(record => record.Authors[x] == authors[y])
                .SingleOrDefault();
                    y++;
                }
                x++;
            }
            return foundRecord;
        }
    }
}