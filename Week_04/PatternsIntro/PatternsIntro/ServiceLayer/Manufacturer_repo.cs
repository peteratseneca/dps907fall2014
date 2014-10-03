using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using PatternsIntro.Models;
using PatternsIntro.Controllers;
using AutoMapper;

namespace PatternsIntro.ServiceLayer
{
    public class Manufacturer_repo : Repository<Manufacturer>
    {
        // Constructor
        public Manufacturer_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers

        public IEnumerable<ManufacturerBase> GetAll()
        {
            // Call the base method; notice the sort order
            var fetchedObjects = RGetAll();
            return Mapper.Map<IEnumerable<ManufacturerBase>>(fetchedObjects.OrderBy(nm => nm.Name));
        }

        public ManufacturerBase GetById(int id)
        {
            // Call the base method
            var fetchedObject = RGetById(id);
            return (fetchedObject == null) ? null : Mapper.Map<ManufacturerBase>(fetchedObject);
        }

        public IEnumerable<ManufacturerBase> GetByManufacturerName(string name)
        {
            // Call the base method
            var fetchedObjects = RGetAllFiltered(nm => nm.Name.ToLower().StartsWith(name.Trim().ToLower()));
            return Mapper.Map<IEnumerable<ManufacturerBase>>(fetchedObjects.OrderBy(nm => nm.Name));
        }

        public ManufacturerBase AddNew(ManufacturerAdd newItem)
        {
            // Add the new object
            var addedItem = RAdd(Mapper.Map<Manufacturer>(newItem));

            // Return the object
            return Mapper.Map<ManufacturerBase>(addedItem);
        }

        public ManufacturerBase EditExisting(ManufacturerBase editedItem)
        {
            // Update the object
            var updatedItem = REdit(editedItem);

            // Return the object
            return Mapper.Map<ManufacturerBase>(updatedItem);
        }

    }

}
