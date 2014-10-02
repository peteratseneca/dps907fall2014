using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.Data.Entity;

// This source code file is used to define the data context and optional store initializer

// The data context is directly used by manager (i.e. data service operations) classes,
// and other business and application classes

// The store initializer is used in simple projects, and enables you to create some initial data

namespace HTTPOptions.Models
{
    /// <summary>
    /// Data context, the gateway to the project's persistent store
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Default constructor, notice the base name matches the name used in the Web.config connection string
        /// </summary>
        public DataContext() : base("name=DefaultConnection") { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
    }

    // Disabled, because this project now uses Code First Migrations

    /// <summary>
    /// Store initializer, notice the inheritance
    /// </summary>
    public class StoreInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        /// <summary>
        /// Seed the data store with initial data
        /// </summary>
        /// <param name="context">The project's data context object</param>
        protected override void Seed(DataContext context)
        {
            // Create some new objects, and save them

            Artist brad = new Artist() { Name = "Brad Paisley", BirthName = "Brad Paisley", BirthOrStartDate = new DateTime(1972, 10, 28), Genre = "Country" };
            context.Artists.Add(brad);

            Artist pink = new Artist() { Name = "P!nk", BirthName = "Alecia Beth Moore", BirthOrStartDate = new DateTime(1979, 9, 8), Genre = "Pop" };
            context.Artists.Add(pink);

            context.SaveChanges();
        }
    }
}
