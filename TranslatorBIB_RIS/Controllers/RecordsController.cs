using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TranslatorBIB_RIS.Services;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Controllers
{
    public class RecordsController : ApiController
    {
        private RecordsServices _recordsServices;

        public RecordsController()
        {
            _recordsServices = RecordsServices.Instance;
        }

        // GET records
        public IHttpActionResult GetAll()
        {

            List<Record> records = _recordsServices.GetAll();
            return Ok(records);
        }

        [HttpGet, Route("records/author/{authors}")]
        public IHttpActionResult GetAuthor(string authors)
        {
            List<string> authorsList = new List<string>();
            authorsList.Add(authors);
            List<Record> records = _recordsServices.GetByAuthor(authorsList);
            if (records == null)
            {
                return NotFound();
            }

            return Ok(records);
        }

        [HttpGet, Route("records/title/{title}")]
        public IHttpActionResult GetTitle(string title)
        {

            List<Record> records = _recordsServices.GetByTitle(title);
            if (records == null)
            {
                return NotFound();
            }

            return Ok(records);
        }

        // POST records
        public IHttpActionResult Post([FromBody]Record record)
        {
            _recordsServices.AddNewRecord(record);
            return Ok();
        }

        

        //// PUT records/5
        //public IHttpActionResult Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE records/5
        //public IHttpActionResult Delete(int id)
        //{
        //}
    }
}
