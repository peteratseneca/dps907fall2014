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

namespace PatternsIntro.Models
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

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
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

            // Ford vehicles

            var ford = new Manufacturer() { Name = "Ford Motor Company", Country = "USA", YearStarted = 1903 };
            context.Manufacturers.Add(ford);

            var fiesta = new Vehicle() { ModelYear = 2014, Model = "Fiesta", Trim = "S Sedan", MSRP = 14499, Manufacturer = ford };
            context.Vehicles.Add(fiesta);

            var focus = new Vehicle() { ModelYear = 2014, Model = "Focus", Trim = "S Sedan", MSRP = 15999, Manufacturer = ford };
            context.Vehicles.Add(fiesta);

            var fusion = new Vehicle() { ModelYear = 2014, Model = "Fusion", Trim = "S", MSRP = 22499, Manufacturer = ford };
            context.Vehicles.Add(focus);

            context.SaveChanges();

            // Toyota vehicles

            var toyota = new Manufacturer() { Name = "Toyota Motor Corporation", Country = "Japan", YearStarted = 1937 };
            context.Manufacturers.Add(toyota);

            var corolla = new Vehicle() { ModelYear = 2014, Model = "Corolla", Trim = "CE", MSRP = 15995, Manufacturer = toyota };
            context.Vehicles.Add(corolla);

            var rav4 = new Vehicle() { ModelYear = 2014, Model = "RAV4", Trim = "FWD LE", MSRP = 23870, Manufacturer = toyota };
            context.Vehicles.Add(rav4);

            var camry = new Vehicle() { ModelYear = 2014, Model = "Camry", Trim = "LE", MSRP = 23750, Manufacturer = toyota };
            context.Vehicles.Add(camry);

            context.SaveChanges();

        }
    }
}
