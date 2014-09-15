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

namespace AllHttpMethods.Models
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

        /// <summary>
        /// Collection of the Human objects
        /// </summary>
        public DbSet<Human> Humans { get; set; }
    }

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

            Human peter = new Human() { FamilyName = "McIntyre", GivenNames = "Peter" };
            context.Humans.Add(peter);

            Human ian = new Human() { FamilyName = "Tipson", GivenNames = "Ian", BirthDate = new DateTime(1985, 04, 23) };
            context.Humans.Add(ian);

            context.SaveChanges();

        }
    }
}
