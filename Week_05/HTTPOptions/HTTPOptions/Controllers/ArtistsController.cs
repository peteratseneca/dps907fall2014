using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// more...
using HTTPOptions.ServiceLayer;
using AutoMapper;

namespace HTTPOptions.Controllers
{
    public class ArtistsController : ApiController
    {
        // Reference to the worker object
        private Worker m = new Worker();

        // GET: api/Artists
        public IEnumerable<ArtistBase> Get()
        {
            return m.Artists.GetAll();
        }

        // GET: api/Artists?withlinks
        // Notice the new return result, which includes link relations
        // Also, try this by changing the "ArtistsLinked" data type to
        // the "ArtistsLinkedWithTemplateInfo" data type - this action
        // will return a 'new item template' that is useful to the user
        public ArtistsLinked Get(string withlinks)
        {
            // Get all
            var fetchedObjects = m.Artists.GetAll();

            // Create an object to be returned
            ArtistsLinked artists = new ArtistsLinked();

            // Set its collection property
            artists.Collection = Mapper.Map<IEnumerable<ArtistWithLink>>(fetchedObjects);

            // Set the URI request path
            string self = Request.RequestUri.AbsolutePath;

            // Add a link relation for 'self'
            artists.Links.Add(new Link() { Rel = "self", Href = self });

            // Add a link relation for each item in the collection
            foreach (var item in artists.Collection)
            {
                item.Link = new Link() { Rel = "item", Href = string.Format("{0}/{1}", self, item.Id) };
            }

            // Return the results
            return artists;
        }

        // GET: api/Artists/5
        public IHttpActionResult Get(int id)
        {
            var fetchedObject = m.Artists.GetById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fetchedObject);
            }
        }

        // GET: api/Artists/5?withlinks
        // Notice that the result includes link relations
        public IHttpActionResult Get(int id, string withlinks)
        {
            var fetchedObject = m.Artists.GetById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                // Create an object to be returned
                ArtistLinked artist = new ArtistLinked();

                // Set its item property
                artist.Item = Mapper.Map<ArtistWithLink>(fetchedObject);

                // Get the request URI path
                string self = Request.RequestUri.AbsolutePath;

                // Add a link relation for 'self'
                artist.Links.Add(new Link() { Rel = "self", Href = self });

                // Add a link relation for the parent 'collection'
                List<string> u = Request.RequestUri.Segments.ToList();
                artist.Links.Add(new Link() { Rel = "collection", Href = u[0] + u[1] + u[2] });

                // Add a link relation for 'self' in the item 
                artist.Item.Link = new Link() { Rel = "self", Href = self };

                // Return the result
                return Ok(artist);
            }
        }

        // POST: api/Artists
        public IHttpActionResult Post([FromBody]ArtistAdd newItem)
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
                var addedItem = m.Artists.AddNew(newItem);

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
                    return Created<ArtistBase>(uri, addedItem);
                }
            }
            else
            {
                // HTTP 400
                return BadRequest(ModelState);
            }

        }

        // PUT: api/Artists/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Artists/5
        public void Delete(int id)
        {
        }
    }
}
