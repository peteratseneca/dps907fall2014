using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// more...
using Images.ServiceLayer;
using AutoMapper;

namespace Images.Controllers
{
    public class BooksController : ApiController
    {
        // Reference to the worker object
        private Worker m = new Worker();

        // GET: api/Books
        public BooksLinked Get()
        {
            // Get all
            var fetchedObjects = m.Books.GetAll();

            // Create an object to be returned
            BooksLinked books = new BooksLinked();

            // Set its collection property
            books.Collection = Mapper.Map<IEnumerable<BookWithLink>>(fetchedObjects);

            // Set the URI request path
            string self = Request.RequestUri.AbsolutePath;

            // Add a link relation for 'self'
            books.Links.Add(new Link() { Rel = "self", Href = self });

            // Add a link relation for each item in the collection
            foreach (var item in books.Collection)
            {
                item.Link = new Link() { Rel = "item", Href = string.Format("{0}/{1}", self, item.Id) };
            }

            // Return the results
            return books;
        }

        // GET: api/Books/5
        public HttpResponseMessage Get(int id)
        {
            var fetchedObject = m.Books.GetById(id);

            if (fetchedObject == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // Create an object to be returned
                BookLinked book = new BookLinked();

                // Set its item property
                book.Item = Mapper.Map<BookWithLink>(fetchedObject);

                // Get the request URI path
                string self = Request.RequestUri.AbsolutePath;

                // Add a link relation for 'self'
                book.Links.Add(new Link() { Rel = "self", Href = self });

                // Add a link relation for the parent 'collection'
                List<string> u = Request.RequestUri.Segments.ToList();
                book.Links.Add(new Link() { Rel = "collection", Href = u[0] + u[1] + u[2] });

                // Add a link relation for 'self' in the item 
                book.Item.Link = new Link() { Rel = "self", Href = self };

                // Build a response object
                var response = Request.CreateResponse(HttpStatusCode.OK, book);

                // The repository method has returned a VehicleFull object 
                // We need to know if the request asked for an image to be returned.
                // If so, we need to configure the Content-Type header BEFORE the object
                // is passed on to the 'image' media formatter.
                // Unfortunately, the current design of the media formatter does not
                // allow us to dynamically set the response's Content-Type header.
                // See this post - http://stackoverflow.com/a/12565530 

                // Step 1 - look for an Accept header that starts with 'image'
                var imageHeader = Request.Headers.Accept
                    .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("image/"));
                // Step 2 - if found, set the Content-Type header value
                if (imageHeader != null)
                {
                    if (string.IsNullOrEmpty(book.Item.ContentType))
                    {
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                    else
                    {
                        response.Content.Headers.ContentType =
                            new System.Net.Http.Headers.MediaTypeHeaderValue(book.Item.ContentType);
                    }
                }

                // Return the result
                return response;
            }
        }

        // POST: api/Books
        public HttpResponseMessage Post([FromBody]BookAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null)
            {
                return Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, "Invalid request URI");
            }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null)
            {
                return Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, "Must send an entity body with the request");
            }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to add the new object
                var addedItem = m.Books.AddNew(newItem);

                if (addedItem == null)
                {
                    // HTTP 400
                    return Request.CreateErrorResponse
                        (HttpStatusCode.BadRequest, "Cannot add the object");
                }
                else
                {
                    // HTTP 201 with the new object in the entity body
                    // Notice how to create the URI for the Location header

                    // Create an object to be returned
                    BookLinked book = new BookLinked();

                    // Set its item property
                    book.Item = Mapper.Map<BookWithLink>(addedItem);

                    // Get the request URI path
                    Uri uri = new Uri(Url.Link("DefaultApi", new { id = book.Item.Id }));

                    // Add a link relation for 'self'
                    book.Links.Add(new Link() { Rel = "self", Href = uri.AbsolutePath });

                    // Add a link relation for the parent 'collection'
                    List<string> u = Request.RequestUri.Segments.ToList();
                    book.Links.Add(new Link() { Rel = "collection", Href = u[0] + u[1] + u[2] });

                    // Add a link relation for 'self' in the item 
                    book.Item.Link = new Link() { Rel = "self", Href = uri.AbsolutePath };

                    // Build the response object
                    var response = Request.CreateResponse<BookLinked>(HttpStatusCode.Created, book);

                    // Set the Location header
                    response.Headers.Location = uri;

                    // Return the response
                    return response;
                }
            }
            else
            {
                // HTTP 400
                return Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT: api/Books/5
        public IHttpActionResult Put(int id, [FromBody]BookEdit editedItem)
        {
            // Ensure that an "editedItem" is in the entity body
            if (editedItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != editedItem.Id)
            {
                return BadRequest("Invalid data in the entity body");
            }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                var changedItem = m.Books.EditExisting(editedItem);

                // Notice the ApiController convenience methods
                if (changedItem == null)
                {
                    // HTTP 400
                    return BadRequest("Cannot edit the object");
                }
                else
                {
                    // From RFC 7231...
                    // http://tools.ietf.org/html/rfc7231#section-4.3.4

                    // "the origin server MUST send either a 200 (OK) 
                    // or a 204 (No Content) response to indicate 
                    // successful completion of the request"

                    // So, we have a choice...

                    // To send HTTP 204...
                    // return StatusCode(HttpStatusCode.NoContent);

                    // To send HTTP 200...
                    // Include the changed item in the entity body, as seen below

                    // Create an object to be returned
                    BookLinked book = new BookLinked();

                    // Set its item property
                    book.Item = Mapper.Map<BookWithLink>(changedItem);

                    // Get the request URI path
                    Uri uri = new Uri(Url.Link("DefaultApi", new { id = book.Item.Id }));

                    // Add a link relation for 'self'
                    book.Links.Add(new Link() { Rel = "self", Href = uri.AbsolutePath });

                    // Add a link relation for the parent 'collection'
                    List<string> u = Request.RequestUri.Segments.ToList();
                    book.Links.Add(new Link() { Rel = "collection", Href = u[0] + u[1] + u[2] });

                    // Add a link relation for 'self' in the item 
                    book.Item.Link = new Link() { Rel = "self", Href = uri.AbsolutePath };

                    // Return the response
                    return Ok<BookLinked>(book);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Yikes - attribute routing!
        // PUT: api/Books/5/setphoto
        [Route("api/books/{id}/setphoto")]
        public IHttpActionResult PutPhoto(int id, [FromBody]byte[] photo)
        {
            // Get the Content-Type header from the request
            var contentType = Request.Content.Headers.ContentType.MediaType;

            // Attempt to save
            if (m.Books.SetPhoto(id, contentType, photo))
            {
                // By convention, we have decided to return HTTP 204
                // It's a 'success' code, but there's no content for a 'command' task
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                // Uh oh, some error happened, so tell the user
                return BadRequest("Unable to set the photo");
            }
        }

        // DELETE: api/Books/5
        public void Delete(int id)
        {
            m.Books.DeleteExisting(id);
        }

    }

}
