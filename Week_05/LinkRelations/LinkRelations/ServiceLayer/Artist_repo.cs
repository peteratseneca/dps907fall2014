using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using LinkRelations.Models;
using LinkRelations.Controllers;
using AutoMapper;

namespace LinkRelations.ServiceLayer
{
    public class Artist_repo : Repository<Artist>
    {
        // Constructor
        public Artist_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers

        public IEnumerable<ArtistBase> GetAll()
        {
            var fetchedObjects = RGetAll();
            return Mapper.Map<IEnumerable<ArtistBase>>(fetchedObjects);
        }

        public ArtistBase GetById(int id)
        {
            var fetchedObject = RGetById(id);
            return (fetchedObject == null) ? null : Mapper.Map<ArtistBase>(fetchedObject);
        }

        public ArtistBase AddNew(ArtistAdd newItem)
        {
            // Ensure that we can continue
            if (newItem == null)
            {
                return null;
            }
            else
            {
                // Add the new object
                Artist addedItem = RAdd(Mapper.Map<Artist>(newItem));

                // Return the object
                return Mapper.Map<ArtistBase>(addedItem);
            }
        }

    }

}
