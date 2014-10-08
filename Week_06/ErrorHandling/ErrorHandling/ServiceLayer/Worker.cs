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

        private Template_repo _tmp;
        private LoggedException_repo _log;

        // Custom getters for each repository

        public Template_repo Tmp
        {
            get
            {
                if (this._tmp == null)
                {
                    this._tmp = new Template_repo(_ds);
                }
                return this._tmp;
            }
        }

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
