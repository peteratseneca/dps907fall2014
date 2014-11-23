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

    public class AddressesController : ApiController
    {
        // Unit-of-work
        Worker m = new Worker();

        // GET: api/Addresses
        public AddressesLinked Get()
        {
            // Get all
            var fetchedObjects = m.Addresses.GetAll();

            // Create an object to be returned
            AddressesLinked adds = new AddressesLinked(Mapper.Map<IEnumerable<AddressWithLink>>(fetchedObjects), Request.RequestUri.AbsolutePath);

            // Tell the user what can be done with this collection
            adds.Links[0].Method = "GET,POST";

            // Return the results
            return adds;
        }

        // GET: api/Addresses/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Addresses
        public IHttpActionResult Post([FromBody]AddressAdd newItem)
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
                var addedItem = m.Addresses.AddNew(newItem);

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
                    AddressLinked emp = new AddressLinked(Mapper.Map<AddressWithLink>(addedItem), uri.AbsolutePath);

                    // Return the object
                    return Created(uri, emp);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Addresses/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Addresses/5
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

    public class AddressList
    {
        public int Id { get; set; }
    }

    public class AddressAdd
    {
        [Required, StringLength(100)]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required, StringLength(100)]
        public string CityAndProvince { get; set; }
        [Required, StringLength(20)]
        public string PostalCode { get; set; }

        public int EmployeeId { get; set; }
        [Required, StringLength(20)]
        public string AddressType { get; set; }
    }

    public class AddressAddTemplate
    {
        public string AddressLine1 { get { return "Address line 1, required, string, up to 100 characters"; } }
        public string AddressLine2 { get { return "Address line 2, string, up to 100 characters"; } }
        public string CityAndProvince { get { return "City and Province, required, string, up to 100 characters"; } }
        public string PostalCode { get { return "Postal code, required, string, up to 20 characters"; } }
        public string EmployeeId { get { return "Employee identifier, required, integer"; } }
        public string AddressType { get { return "Address type (work or home), required, string, up to 20 characters"; } }
    }

    public class AddressBase : AddressAdd
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }
    }

    public class AddressWithLink : AddressBase
    {
        public Link Link { get; set; }
    }

    public class AddressLinked : LinkedItem<AddressWithLink>
    {
        // A refactoring experiment...
        public AddressLinked(AddressWithLink item, string absolutePath)
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

    public class AddressesLinked : LinkedCollection<AddressWithLink>
    {
        public AddressesLinked()
        {
            this.Template = new AddressAddTemplate();
        }

        // A refactoring experiment...
        public AddressesLinked(IEnumerable<AddressWithLink> collection, string absolutePath)
        {
            this.Template = new AddressAddTemplate();

            this.Collection = collection;

            // Link relation for 'self'
            this.Links.Add(new Link() { Rel = "self", Href = absolutePath });

            // Add 'item' links for each item in the collection
            foreach (var item in this.Collection)
            {
                item.Link = new Link() { Rel = "item", Href = string.Format("{0}/{1}", absolutePath, item.Id) };
            }
        }

        public AddressAddTemplate Template { get; set; }
    }

}

// ############################################################
// Repository

namespace Associations.ServiceLayer
{
    using AutoMapper;
    using Associations.Models;
    using Associations.Controllers;

    public class Address_repo : Repository<Address>
    {
        // Constructor
        public Address_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers...

        // Get all
        public IEnumerable<AddressBase> GetAll()
        {
            var fetchedObjects = RGetAll();

            return Mapper.Map<IEnumerable<AddressBase>>(fetchedObjects.OrderBy(pc => pc.PostalCode));
        }

        // Add new
        public AddressBase AddNew(AddressAdd newItem)
        {
            // Validate the address type
            string at = newItem.AddressType.Trim().ToLower();
            if (at == "home" | at == "work")
            {
                // Can continue

                // Validate the employee identifier
                var emp = _ds.Employees.Find(newItem.EmployeeId);

                if (emp == null)
                {
                    return null;
                }
                else
                {
                    // Can continue

                    // Add the new object
                    var addedItem = _dbset.Add(Mapper.Map<Address>(newItem));
                    // Configure the properties
                    addedItem.Employee = emp;
                    addedItem.EmployeeId = emp.Id;

                    if (at == "home") { emp.HomeAddress = addedItem; }
                    if (at == "work") { emp.WorkAddress = addedItem; }

                    SaveChanges();

                    // Return the object
                    return Mapper.Map<AddressBase>(addedItem);
                }
            }
            else
            {
                return null;
            }
        }

    }

}