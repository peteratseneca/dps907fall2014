using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using Images.Models;
using Images.Controllers;
using AutoMapper;

namespace Images.ServiceLayer
{
    public class Book_repo : Repository<Book>
    {
        // Constructor
        public Book_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers

        // Get all
        public IEnumerable<BookBase> GetAll()
        {
            var fetchedObjects = RGetAll();
            return Mapper.Map<IEnumerable<BookBase>>(fetchedObjects.OrderBy(t => t.Title));
        }

        // Get all, filtered

        // Get one, by its identifier
        public BookBase GetById(int id)
        {
            var fetchedObject = RGetById(id);
            return (fetchedObject == null) ? null : Mapper.Map<BookBase>(fetchedObject);
        }

        // Add new
        public BookBase AddNew(BookAdd newItem)
        {
            // Add the new object
            var addedItem = RAdd(Mapper.Map<Book>(newItem));

            // Return the object
            return Mapper.Map<BookBase>(addedItem);
        }

        // Edit existing
        public BookBase EditExisting(BookEdit editedItem)
        {
            // Update the object
            var updatedItem = REdit(editedItem);

            // Return the object
            return Mapper.Map<BookBase>(updatedItem);
        }

        // Delete existing
        // (uses the base class implementation)

        // ############################################################

        // Upload / save / set the photo

        public bool SetPhoto(int id, string contentType, byte[] photo)
        {
            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | photo == null)
            {
                return false;
            }

            // Attempt to find the matching object
            var storedItem = _dbset.Find(id);

            // Ensure that we can continue
            if (storedItem == null)
            {
                return false;
            }

            // Save the photo
            storedItem.ContentType = contentType;
            storedItem.Photo = photo;

            // Attempt to save changes
            return (SaveChanges() > 0) ? true : false;
        }

    }

}
