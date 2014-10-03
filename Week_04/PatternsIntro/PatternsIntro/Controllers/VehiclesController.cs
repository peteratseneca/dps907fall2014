using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AssociationOneToMany.Controllers
{
    public class VehiclesController : ApiController
    {
        // Reference to the manager object
        private Manager m = new Manager();

        // GET: api/Vehicles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Vehicles?withmanufacturer
        public IEnumerable<VehicleWithManufacturer> Get(string withmanufacturer)
        {
            return m.GetAllVehWithManufacturer();
        }

        // GET: api/Vehicles/5
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/Vehicles/5?withmanufacturer
        public IHttpActionResult Get(int id, string withmanufacturer)
        {
            // Attempt to get the matching object
            var fetchedObject = m.GetOneVehWithManufacturer(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fetchedObject);
            }
        }

        // POST: api/Vehicles
        public IHttpActionResult Post([FromBody]VehicleAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null)
            {
                return BadRequest("Invalid request URI");
            }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to add the new object
                var addedItem = m.AddVehicle(newItem);

                // Notice the ApiController convenience methods
                if (addedItem == null)
                {
                    // HTTP 400
                    return BadRequest("Cannot add the object");
                }
                else
                {
                    // HTTP 201 with the new object in the entity body
                    // Notice how to create the URI for the Location header

                    var uri = Url.Link("DefaultApi", new { id = addedItem.Id });
                    return Created<VehicleBase>(uri, addedItem);
                }
            }
            else
            {
                // HTTP 400
                return BadRequest(ModelState);
            }

        }

        // PUT: api/Vehicles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Vehicles/5
        public void Delete(int id)
        {
        }
    }
}
