using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// In this code example, we're trying something new...
// This source code file will include the 'resource model' classes and the 'repository' class

namespace Associations.Controllers
{
    using AutoMapper;
    using Associations.ServiceLayer;

    public class JobDutiesController : ApiController
    {
        // Unit-of-work
        Worker m = new Worker();

        // GET: api/JobDuties
        public JobDutiesLinked Get()
        {
            // Get all
            var fetchedObjects = m.JobDuties.GetAll();

            // Create an object to be returned
            JobDutiesLinked duts = new JobDutiesLinked(Mapper.Map<IEnumerable<JobDutyWithLink>>(fetchedObjects), Request.RequestUri.AbsolutePath);

            // Tell the user what can be done with this collection
            duts.Links[0].Method = "GET,POST";

            // Return the results
            return duts;
        }

        // GET: api/JobDuties/5
        public IHttpActionResult Get(int id)
        {
            // Get by identifier
            var fetchedObject = m.JobDuties.GetById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                // Create an object to be returned
                JobDutyLinked dut = new JobDutyLinked(Mapper.Map<JobDutyWithLink>(fetchedObject), Request.RequestUri.AbsolutePath);

                // Tell the user what can be done with this item and collection
                dut.Links[0].Method = "GET,DELETE";
                dut.Links[1].Method = "GET,POST";
                // TODO maybe refactor this, use the API explorer to discover
                // (the same API explorer that's used in the HTTP OPTIONS handler

                /*
                // Add another link to tell the user that they can set the supervisor value
                Link supervisor = new Link()
                {
                    Rel = "self",
                    Title = "Set the supervisor identifier",
                    Href = dut.Links[0].Href,
                    Method = "PUT",
                    ContentType = "application/json"
                };
                dut.Links.Add(supervisor);
                */

                // Return the results
                return Ok(dut);
            }

        }

        // POST: api/JobDuties
        public IHttpActionResult Post([FromBody]JobDutyAdd newItem)
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
                // Attempt to add the new item
                var addedItem = m.JobDuties.AddNew(newItem);

                if (addedItem == null)
                {
                    return BadRequest("Cannot add the object");
                }
                else
                {
                    // HTTP 201 with the new object in the entity body

                    // Build the URI to the new object
                    Uri uri = new Uri(Url.Link("DefaultApi", new { id = addedItem.Id }));

                    // Create an object to be returned
                    JobDutyLinked dut = new JobDutyLinked(Mapper.Map<JobDutyWithLink>(addedItem), uri.AbsolutePath);

                    // Return the object
                    return Created(uri, dut);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // PUT: api/JobDuties/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/JobDuties/5
        public void Delete(int id)
        {
        }
    }
}

// ############################################################
// Resource model classes

namespace Associations.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class JobDutyList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class JobDutyAdd
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(1000)]
        public string Description { get; set; }
    }

    public class JobDutyAddTemplate
    {
        public string Name { get { return "Name of job duty, required, string, up to 100 characters"; } }
        public string Description { get { return "Description of job duty, required, string, up to 1000 characters"; } }
    }

    public class JobDutyBase : JobDutyAdd
    {
        public int Id { get; set; }

        public ICollection<EmployeeList> Employees { get; set; }
    }

    public class JobDutyWithLink : JobDutyBase
    {
        public Link Link { get; set; }
    }

    public class JobDutyLinked : LinkedItem<JobDutyWithLink>
    {
        // A refactoring experiment...
        public JobDutyLinked(JobDutyWithLink item, string absolutePath)
        {
            this.Item = item;

            // Link relation for 'self' in the item
            this.Item.Link = new Link() { Rel = "self", Href = absolutePath };

            // Link relation for 'self'
            this.Links.Add(new Link() { Rel = "self", Href = absolutePath });
            //this.Links.Add(this.Item.Link);

            // Link relation for 'collection'
            string[] u = absolutePath.Split(new char[] { '/' });
            this.Links.Add(new Link() { Rel = "collection", Href = string.Format("/{0}/{1}", u[1], u[2]) });
        }
    }

    public class JobDutiesLinked : LinkedCollection<JobDutyWithLink>
    {
        public JobDutiesLinked()
        {
            this.Template = new JobDutyAddTemplate();
        }

        // A refactoring experiment...
        public JobDutiesLinked(IEnumerable<JobDutyWithLink> collection, string absolutePath)
        {
            this.Template = new JobDutyAddTemplate();

            this.Collection = collection;

            // Link relation for 'self'
            this.Links.Add(new Link() { Rel = "self", Href = absolutePath });

            // Add 'item' links for each item in the collection
            foreach (var item in this.Collection)
            {
                item.Link = new Link() { Rel = "item", Href = string.Format("{0}/{1}", absolutePath, item.Id) };
            }
        }

        public JobDutyAddTemplate Template { get; set; }
    }

}

// ############################################################
// Repository

namespace Associations.ServiceLayer
{
    using AutoMapper;
    using Associations.Models;
    using Associations.Controllers;

    public class JobDuty_repo : Repository<JobDuty>
    {
        // Constructor
        public JobDuty_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers...

        // Get all
        public IEnumerable<JobDutyBase> GetAll()
        {
            var fetchedObjects = RGetAll();
            return Mapper.Map<IEnumerable<JobDutyBase>>(fetchedObjects.OrderBy(ln => ln.Name));
            // This seems to lazily fetch the related employees - why?
        }

        // Get all, filtered

        // Get one, by its identifier
        public JobDutyBase GetById(int id)
        {
            var fetchedObject = _ds.JobDuties
                .Include("Employees")
                .SingleOrDefault(i => i.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<JobDutyBase>(fetchedObject);
        }

        // Add new
        public JobDutyBase AddNew(JobDutyAdd newItem)
        {
            // Add the new object
            var addedItem = RAdd(Mapper.Map<JobDuty>(newItem));

            // Return the object
            return Mapper.Map<JobDutyBase>(addedItem);
        }

    }

}
