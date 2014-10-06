using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using Images.Models;

namespace Images.ServiceLayer
{
    // Unit of Work

    public class Worker : IDisposable
    {
        private DataContext _ds = new DataContext();
        private bool disposed = false;

        // Properties for each repository

        private Book_repo _books;

        // Custom getters for each repository

        public Book_repo Books
        {
            get
            {
                if (this._books == null)
                {
                    this._books = new Book_repo(_ds);
                }
                return this._books;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ds.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
