using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using ColourList.Models;
using AutoMapper;

// This source code file is used as the app's single central location 
// to hold code for data service operations

// Notice (above) that it has a reference to the 'design model' classes
// Notice (below) that its public methods accept and return instances of 'resource model' objects or collections

namespace ColourList.Controllers
{
    public class Manager
    {
        // Reference to the facade services class
        private DataContext ds = new DataContext();

        // All objects
        public IEnumerable<ColourBase> GetAllColours()
        {
            // Get all the objects from the data store
            var fetchedObjects = ds.Colours.OrderBy(clr => clr.ColourName);

            // Return them as a 'resource model' collection
            return Mapper.Map<IEnumerable<ColourBase>>(fetchedObjects);
        }

        // Single object, by identifier
        public ColourBase GetColourById(int id)
        {
            // Get the matching object
            var fetchedObject = ds.Colours.Find(id);

            // Return it (or null if not found)
            return (fetchedObject == null) ? null : Mapper.Map<ColourBase>(fetchedObject);
        }

        // Add object
        public ColourBase AddColour(ColourAdd newItem)
        {
            // Ensure that we can continue
            if (newItem == null)
            {
                return null;
            }
            else
            {
                // Add the new object, by mapping from the 'resource model' object
                Colour addedItem = Mapper.Map<Colour>(newItem);

                ds.Colours.Add(addedItem);
                ds.SaveChanges();

                // Return the now-fully-configured 'resource model' version of the object
                return Mapper.Map<ColourBase>(addedItem);
            }
        }

        // The following method will re-seed the data store
        // This method is only for our own private use (not intended for public users)

        public void ReSeedDataStore()
        {
            // Remove existing items
            foreach (var item in ds.Colours)
            {
                ds.Colours.Remove(item);
            }

            // Add new objects
            Colour red = new Colour() { ColourName = "Red" };
            ds.Colours.Add(red);

            Colour green = new Colour() { ColourName = "Green" };
            ds.Colours.Add(green);

            ds.SaveChanges();

        }

    }
}
