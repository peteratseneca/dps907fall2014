using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using ErrorHandling.Models;
using ErrorHandling.Controllers;
using AutoMapper;

namespace ErrorHandling.ServiceLayer
{
    public class LoggedException_repo : Repository<LoggedException>
    {
        // Constructor
        public LoggedException_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers

        // Get all
        public IEnumerable<LoggedExceptionBase> GetAll()
        {
            var fetchedObjects = RGetAll();
            return Mapper.Map<IEnumerable<LoggedExceptionBase>>(fetchedObjects.OrderByDescending(dt => dt.DateAndTime));
        }

        // Get most recent ten exceptions
        public IEnumerable<LoggedExceptionBase> GetRecent()
        {
            var fetchedObjects = _dbset.OrderByDescending(dt => dt.DateAndTime).Take(10);
            return Mapper.Map<IEnumerable<LoggedExceptionBase>>(fetchedObjects);
        }

        // Get all, filtered

        // Get one, by its identifier
        public LoggedExceptionBase GetById(int id)
        {
            var fetchedObject = RGetById(id);
            return (fetchedObject == null) ? null : Mapper.Map<LoggedExceptionBase>(fetchedObject);
        }

        // Add new
        public LoggedExceptionBase AddNew(LoggedExceptionAdd newItem)
        {
            var addedItem = RAdd(Mapper.Map<LoggedException>(newItem));
            return Mapper.Map<LoggedExceptionBase>(addedItem);
        }

        // Edit existing

        // Delete existing
    }

}
