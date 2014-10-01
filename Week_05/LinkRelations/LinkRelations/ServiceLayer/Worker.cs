using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using LinkRelations.Models;

namespace LinkRelations.ServiceLayer
{
    // Unit of Work

    public class Worker : IDisposable
    {
        private DataContext _ds = new DataContext();
        private bool disposed = false;

        // Properties for each repository

        private Artist_repo _artist;
        private Album_repo _album;
        private Song_repo _song;

        // Custom getters for each repository

        public Artist_repo Artists
        {
            get
            {
                if (this._artist == null)
                {
                    this._artist = new Artist_repo(_ds);
                }
                return this._artist;
            }
        }

        public Album_repo Albums
        {
            get
            {
                if (this._album == null)
                {
                    this._album = new Album_repo(_ds);
                }
                return this._album;
            }
        }

        public Song_repo Songs
        {
            get
            {
                if (this._song == null)
                {
                    this._song = new Song_repo(_ds);
                }
                return this._song;
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
