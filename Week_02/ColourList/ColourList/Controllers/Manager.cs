using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using ColourList.Models;
using AutoMapper;

namespace ColourList.Controllers
{
    public class Manager
    {
        // Reference to the facade services class
        private DataContext ds = new DataContext();

        // All objects
        public IEnumerable<ColourBase> GetAllColours()
        {
            var fetchedObjects = ds.Colours.OrderBy(clr => clr.ColourName);

            return Mapper.Map<IEnumerable<ColourBase>>(fetchedObjects);
        }

        // Single object, by identifier
        public ColourBase GetColourById(int id)
        {
            var fetchedObject = ds.Colours.Find(id);

            return (fetchedObject == null) ? null : Mapper.Map<ColourBase>(fetchedObject);
        }

        // Add object
        public ColourBase AddColour(ColourAdd newItem)
        {
            Colour addedItem = Mapper.Map<Colour>(newItem);

            ds.Colours.Add(addedItem);
            ds.SaveChanges();

            return Mapper.Map<ColourBase>(addedItem);
        }

    }
}
