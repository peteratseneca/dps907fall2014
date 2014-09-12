using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
            var fetchedObject = m.GetColourById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fetchedObject);
            }
        }

        // POST: api/Colours
        // Notice the IHttpActionResult return type
        public IHttpActionResult Post(ColourAdd newItem)
        {
            if (ModelState.IsValid)
            {
                var addedItem = m.AddColour(newItem);

                if (addedItem==null)
                {
                    return BadRequest("Cannot add the object");
                }
                else
                {
                    return Created<ColourBase>(Request.RequestUri + addedItem.Id.ToString(), addedItem);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Colours/5
        public void Put(int id, [FromBody]string value)
        {
            // coming soon
        }

        // DELETE: api/Colours/5
        public void Delete(int id)
        {
            // coming soon
        }

    }

}
