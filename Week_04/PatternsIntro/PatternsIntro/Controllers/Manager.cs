using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using AssociationOneToMany.Models;
using AutoMapper;

// This source code file is used as the app's single central location 
// to hold code for data service operations

// Notice (above) that it has a reference to the 'design model' classes
// Notice (below) that its public methods accept and return instances of 'resource model' objects or collections

namespace AssociationOneToMany.Controllers
{
    public class Manager
    {
        // Reference to the facade services class
        private DataContext ds = new DataContext();

        // Add methods for the data service operations

        // All manufacturers with their vehicles
        public IEnumerable<ManufacturerWithVehicles> GetAllMfrWithVehicles()
        {
            // Note that we must .Include("Vehicles") to fetch the associated objects
            var fetchedObjects = ds.Manufacturers.Include("Vehicles").OrderBy(man => man.Name);

            return Mapper.Map<IEnumerable<ManufacturerWithVehicles>>(fetchedObjects);
        }

        // One manufacturer with its vehicles
        public ManufacturerWithVehicles GetOneMfrWithVehicles(int id)
        {
            // Note that we must .Include("Vehicles") to fetch the associated objects
            var fetchedObject = ds.Manufacturers.Include("Vehicles").SingleOrDefault(i => i.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<ManufacturerWithVehicles>(fetchedObject);
        }

        // All vehicles with manufacturer info
        public IEnumerable<VehicleWithManufacturer> GetAllVehWithManufacturer()
        {
            // Note that we must .Include("Manufacturer") to fetch the associated object
            var fetchedObjects = ds.Vehicles.Include("Manufacturer").OrderBy(veh => veh.Model);

            return Mapper.Map<IEnumerable<VehicleWithManufacturer>>(fetchedObjects);
        }

        // One vehicle with its manufacturer info
        public VehicleWithManufacturer GetOneVehWithManufacturer(int id)
        {
            // Note that we must .Include("Manufacturer") to fetch the associated object
            var fetchedObject = ds.Vehicles.Include("Manufacturer").SingleOrDefault(i => i.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<VehicleWithManufacturer>(fetchedObject);
        }

        // Add new vehicle
        public VehicleBase AddVehicle(VehicleAdd newItem)
        {
            // Ensure that we can continue
            if (newItem == null)
            {
                return null;
            }
            else
            {
                // Must validate the Manufacturer
                var associatedItem = ds.Manufacturers.Find(newItem.ManufacturerId);
                if (associatedItem == null)
                {
                    return null;
                }

                // Add the new object
                Vehicle addedItem = Mapper.Map<Vehicle>(newItem);
                addedItem.Manufacturer = associatedItem;

                ds.Vehicles.Add(addedItem);
                ds.SaveChanges();

                // Return the object
                return Mapper.Map<VehicleBase>(addedItem);
            }
        }
    }
}
