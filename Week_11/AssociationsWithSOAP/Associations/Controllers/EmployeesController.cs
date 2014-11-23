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

    //[Authorize]
    public class EmployeesController : ApiController
    {
        // Unit-of-work
        Worker m = new Worker();

        // GET: api/Employees
        public EmployeesLinked Get()
        {
            // Get all
            var fetchedObjects = m.Employees.GetAll();

            // Create an object to be returned
            EmployeesLinked emps = new EmployeesLinked(Mapper.Map<IEnumerable<EmployeeWithLink>>(fetchedObjects), Request.RequestUri.AbsolutePath);

            // Tell the user what can be done with this collection
            emps.Links[0].Method = "GET,POST";

            // Return the results
            return emps;
        }

        // GET: api/Employees/5
        public IHttpActionResult Get(int id)
        {
            // Get by identifier
            var fetchedObject = m.Employees.GetById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                // Create an object to be returned
                EmployeeLinked emp = new EmployeeLinked(Mapper.Map<EmployeeWithLink>(fetchedObject), Request.RequestUri.AbsolutePath);

                // Tell the user what can be done with this item and collection
                emp.Links[0].Method = "GET,DELETE";
                emp.Links[1].Method = "GET,POST";
                // TODO maybe refactor this, use the API explorer to discover
                // (the same API explorer that's used in the HTTP OPTIONS handler

                // Add another link to tell the user that they can set the supervisor value
                Link supervisor = new Link()
                {
                    Rel = "self",
                    Title = "Set the supervisor identifier",
                    Href = emp.Links[0].Href,
                    Method = "PUT",
                    ContentType = "application/json"
                };
                emp.Links.Add(supervisor);

                // Return the results
                return Ok(emp);
            }
        }

        // POST: api/Employees
        public IHttpActionResult Post([FromBody]EmployeeAdd newItem)
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
                var addedItem = m.Employees.AddNew(newItem);

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
                    EmployeeLinked emp = new EmployeeLinked(Mapper.Map<EmployeeWithLink>(addedItem), uri.AbsolutePath);

                    // Return the object
                    return Created(uri, emp);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // PUT: api/Employees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // PUT: api/Employees/5/set-supervisor
        [Route("api/employees/{id}/set-supervisor")]
        public IHttpActionResult PutSupervisor(int id, [FromBody]int supervisorId)
        {
            // Attempt to save
            if (m.Employees.SetSupervisor(id, supervisorId))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                //return BadRequest("Unable to set the supervisor for this employee");
                return BadRequest(string.Format("Unable to set supervisor {0} to employee {1}", supervisorId, id));
            }
        }

        // PUT: api/Employees/5/add-job-duty
        [Route("api/employees/{id}/add-job-duty")]
        public IHttpActionResult PutJobDuty(int id, [FromBody]int jobDutyId)
        {
            // Attempt to save
            if (m.Employees.AddJobDuty(id, jobDutyId))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest(string.Format("Unable to add job duty {0} to employee {1}", jobDutyId, id));
            }
        }

        // DELETE: api/Employees/5
        public void Delete(int id)
        {
            m.Employees.DeleteExisting(id);
        }
    }
}

// ############################################################
// Resource model classes

namespace Associations.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeList
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

    public class EmployeeAdd
    {
        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required, StringLength(100)]
        public string FirstName { get; set; }
    }

    public class EmployeeAddTemplate
    {
        public string LastName { get { return "Last name, required, string, up to 100 characters"; } }
        public string FirstName { get { return "First name, required, string, up to 100 characters"; } }
    }

    public class EmployeeBase : EmployeeAdd
    {
        public int Id { get; set; }

        public int? ReportsToEmployeeId { get; set; }
        public int? HomeAddressId { get; set; }
        public int? WorkAddressId { get; set; }
        public ICollection<EmployeeList> EmployeesSupervised { get; set; }
        public ICollection<JobDutyList> JobDuties { get; set; }
    }

    public class EmployeeWithLink : EmployeeBase
    {
        public Link Link { get; set; }
    }

    public class EmployeeLinked : LinkedItem<EmployeeWithLink>
    {
        // A refactoring experiment...
        public EmployeeLinked(EmployeeWithLink item, string absolutePath)
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

    public class EmployeesLinked : LinkedCollection<EmployeeWithLink>
    {
        public EmployeesLinked()
        {
            this.Template = new EmployeeAddTemplate();
        }

        // A refactoring experiment...
        public EmployeesLinked(IEnumerable<EmployeeWithLink> collection, string absolutePath)
        {
            this.Template = new EmployeeAddTemplate();

            this.Collection = collection;

            // Link relation for 'self'
            this.Links.Add(new Link() { Rel = "self", Href = absolutePath });

            // Add 'item' links for each item in the collection
            foreach (var item in this.Collection)
            {
                item.Link = new Link() { Rel = "item", Href = string.Format("{0}/{1}", absolutePath, item.Id) };
            }
        }

        public EmployeeAddTemplate Template { get; set; }
    }

}

// ############################################################
// Repository

namespace Associations.ServiceLayer
{
    using AutoMapper;
    using Associations.Models;
    using Associations.Controllers;

    public class Employee_repo : Repository<Employee>
    {
        // Constructor
        public Employee_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers...

        // Get all
        public IEnumerable<EmployeeBase> GetAll()
        {
            var fetchedObjects = RGetAll();
            return Mapper.Map<IEnumerable<EmployeeBase>>(fetchedObjects.OrderBy(ln => ln.LastName).ThenBy(fn => fn.FirstName));
            // This seems to lazily fetch the related employees - why?
        }

        // Get all, filtered

        // Get one, by its identifier
        public EmployeeBase GetById(int id)
        {
            //var fetchedObject = RGetById(id);
            var fetchedObject = _ds.Employees
                .Include("EmployeesSupervised")
                .Include("JobDuties")
                .Include("HomeAddress")
                .Include("WorkAddress")
                .SingleOrDefault(i => i.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<EmployeeBase>(fetchedObject);
        }

        // Add new
        public EmployeeBase AddNew(EmployeeAdd newItem)
        {
            // Add the new object
            var addedItem = RAdd(Mapper.Map<Employee>(newItem));

            // Return the object
            return Mapper.Map<EmployeeBase>(addedItem);
        }

        // Edit existing

        // Delete existing

        // Set supervisor
        public bool SetSupervisor(int employeeId, int supervisorId)
        {
            // Validate the employee
            var emp = _dbset.Find(employeeId);

            if (emp == null)
            {
                return false;
            }
            else
            {
                // Validate the supervisor
                var sup = _dbset.Find(supervisorId);

                if (sup == null)
                {
                    return false;
                }
                else
                {
                    emp.ReportsToEmployee = sup;
                    emp.ReportsToEmployeeId = sup.Id;
                    SaveChanges();

                    return true;
                }
            }
        }

        // Add job duty
        public bool AddJobDuty(int employeeId, int jobDutyId)
        {
            // Validate the employee
            var emp = _dbset.Find(employeeId);

            if (emp == null)
            {
                return false;
            }
            else
            {
                // Validate the job duty
                var dut = _ds.JobDuties.Find(jobDutyId);

                if (dut == null)
                {
                    return false;
                }
                else
                {
                    emp.JobDuties.Add(dut);
                    SaveChanges();

                    return true;
                }
            }
        }

    }

}
