using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatternsIntro.Controllers
{
    public class ManufacturersController : ApiController
    {
        // Reference to the worker object
        private Worker m = new Worker();

        // GET: api/Manufacturers
        public IEnumerable<ManufacturerBase> Get()
        {
            return m.Manufacturers.GetAll();
        }

        // GET: api/Manufacturers/5
        public IHttpActionResult Get(int id)
        {
            var fetchedObject = m.Manufacturers.GetById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fetchedObject);
            }
        }

        // POST: api/Manufacturers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Manufacturers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Manufacturers/5
        public void Delete(int id)
        {
        }
    }
}
