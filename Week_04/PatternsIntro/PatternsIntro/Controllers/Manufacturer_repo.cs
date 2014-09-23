using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using PatternsIntro.Models;
using AutoMapper;

namespace PatternsIntro.Controllers
{
    public class Manufacturer_repo : Repository<Manufacturer>
    {
        // Constructor
        public Manufacturer_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers

        public IEnumerable<ManufacturerBase> GetAll()
        {
            var fetchedObjects = RGetAll();
            return Mapper.Map<IEnumerable<ManufacturerBase>>(fetchedObjects);
        }

        public ManufacturerBase GetById(int id)
        {
            var fetchedObject = RGetById(id);
            return (fetchedObject == null) ? null : Mapper.Map<ManufacturerBase>(fetchedObject);
        }

        // Add other methods here...

    }

}
