using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AssociationOneToMany.Controllers
{
    public class ManufacturersController : ApiController
    {
        // Reference to the manager object
        private Manager m = new Manager();

        // GET: api/Manufacturers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Manufacturers?withvehicles
        public IEnumerable<ManufacturerWithVehicles> Get(string withvehicles)
        {
            return m.GetAllMfrWithVehicles();
        }

        // GET: api/Manufacturers/5
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/Manufacturers/5?withVehicles
        public IHttpActionResult Get(int id, string withvehicles)
        {
            // Attempt to get the matching object
            var fetchedObject = m.GetOneMfrWithVehicles(id);

            if (fetchedObject==null)
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
