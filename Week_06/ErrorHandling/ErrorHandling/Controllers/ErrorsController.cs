using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// more...
using ErrorHandling.ServiceLayer;

namespace ErrorHandling.Controllers
{
    public class ErrorsController : ApiController
    {
        // Reference to the worker object
        private Worker m = new Worker();

        // GET: api/Errors
        public IEnumerable<LoggedExceptionBase> Get()
        {
            // By default, delivers only the ten most recent exceptions
            return m.Exceptions.GetRecent();
        }

        // GET: api/errors/all
        [Route("api/errors/all")]
        public IEnumerable<LoggedExceptionBase> GetAll()
        {
            return m.Exceptions.GetAll();
        }

        // GET: api/errors/cause
        [Route("api/errors/cause")]
        public string GetError()
        {
            // Cause an error, with the following invalid code
            string name = null;
            return name.Length.ToString();
        }

        // GET: api/Errors/5
        public IHttpActionResult Get(int id)
        {
            var fetchedObject = m.Exceptions.GetById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fetchedObject);
            }
        }

        /*
        // POST: api/Errors
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Errors/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Errors/5
        public void Delete(int id)
        {
        }
        */
    }
}
