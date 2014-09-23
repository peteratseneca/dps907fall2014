using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using PatternsIntro.Models;

namespace PatternsIntro.Controllers
{
    // Unit of Work

    public class Worker : IDisposable
    {
        private DataContext _ds = new DataContext();
        private bool disposed = false;

        // Properties for each repository

        private Manufacturer_repo _man;

        // Other repos go here...

        // Custom getters for each repository

        public Manufacturer_repo Manufacturers
        {
            get
            {
                if (this._man == null)
                {
                    this._man = new Manufacturer_repo(_ds);
                }
                return this._man;
            }
        }

        // Other custom getters go here...

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
