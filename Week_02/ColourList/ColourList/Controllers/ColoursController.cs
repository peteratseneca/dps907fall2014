using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// This controller was created with the
// "Web API 2 Controller with read/write actions" template

namespace ColourList.Controllers
{
    public class ColoursController : ApiController
    {
        // Reference to the data service operations manager class
        private Manager m = new Manager();

        // GET: api/Colours
        public IEnumerable<ColourBase> Get()
        {
            return m.GetAllColours();
        }

        // GET: api/Colours/5
        // Notice the IHttpActionResult return type
        public IHttpActionResult Get(int id)
        {
            // Attempt to get the matching object
            var fetchedObject = m.GetColourById(id);

            // Notice the ApiController convenience methods
            if (fetchedObject == null)
            {
                // HTTP 404
                return NotFound();
            }
            else
            {
                // HTTP 200 with the object in the HTTP message entity body
                return Ok(fetchedObject);
            }
        }

        // POST: api/Colours
        // Notice the IHttpActionResult return type and the FromBody attribute
        public IHttpActionResult Post([FromBody]ColourAdd newItem)
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
                var addedItem = m.AddColour(newItem);

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
                    return Created<ColourBase>(uri, addedItem);
                }
            }
            else
            {
                // HTTP 400
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Colours/5
        public void Put(int id, [FromBody]string value)
        {
            // Will be implemented in a future code example
        }

        // DELETE: api/Colours/5
        public void Delete(int id)
        {
            // Will be implemented in a future code example
        }

    }

}
