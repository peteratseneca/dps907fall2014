using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using ErrorHandling.Models;

namespace ErrorHandling.ServiceLayer
{
    // Unit of Work

    public class Worker : IDisposable
    {
        private DataContext _ds = new DataContext();
        private bool disposed = false;

        // Properties for each repository

        private LoggedException_repo _log;

        // Custom getters for each repository

        public LoggedException_repo Exceptions
        {
            get
            {
                if (_log == null)
                {
                    _log = new LoggedException_repo(_ds);
                }
                return _log;
            }
        }

        // ############################################################
        // Memory usage cleanup code

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
