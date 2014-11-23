using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using Associations.Models;

namespace Associations.ServiceLayer
{
    // Unit of Work

    public class Worker : IDisposable
    {
        private DataContext _ds = new DataContext();
        private bool disposed = false;

        // Properties for each repository

        // Custom getters for each repository

        private Employee_repo _emp;

        public Employee_repo Employees
        {
            get
            {
                if (this._emp == null)
                {
                    this._emp = new Employee_repo(_ds);
                }
                return _emp;
            }
        }

        private Address_repo _add;

        public Address_repo Addresses
        {
            get
            {
                if (this._add == null)
                {
                    this._add = new Address_repo(_ds);
                }
                return _add;
            }
        }

        private JobDuty_repo _job;

        public JobDuty_repo JobDuties
        {
            get
            {
                if (this._job == null)
                {
                    this._job = new JobDuty_repo(_ds);
                }
                return _job;
            }
        }



        // ############################################################

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
