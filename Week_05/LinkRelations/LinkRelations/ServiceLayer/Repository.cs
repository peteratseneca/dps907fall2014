using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using LinkRelations.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace LinkRelations.ServiceLayer
{
    public abstract class Repository<T> where T : class
    {
        // Fields that are the backing store for properties
        protected DataContext _ds;
        protected readonly IDbSet<T> _dbset;

        // Default constructor
        public Repository(DataContext ds)
        {
            _ds = ds;
            _dbset = ds.Set<T>();
        }

        // Save changes
        public int SaveChanges()
        {
            return _ds.SaveChanges();
        }

        // Standard data handling methods
        // Notice the "protected" visibility

        protected IEnumerable<T> RGetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        protected T RGetById(int id)
        {
            return _dbset.Find(id);
        }

        protected IEnumerable<T> RGetAllFiltered(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        protected T RAdd(T item)
        {
            _dbset.Add(item);
            SaveChanges();
            return item;
        }

        protected T REdit(T item)
        {
            throw new NotImplementedException();
        }

        protected T RDelete(T item)
        {
            throw new NotImplementedException();
        }

    }

}
