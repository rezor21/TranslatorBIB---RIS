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

        [HttpGet, Route("records/{authors}")]
        public IHttpActionResult Get(string authors)
        {
            List<string> authorsList = new List<string>();
            authorsList.Add(authors);
            Record record = _recordsServices.GetByAuthor(authorsList);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
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
