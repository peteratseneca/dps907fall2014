namespace AssociationOneToMany.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    // more...
    using AssociationOneToMany.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AssociationOneToMany.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AssociationOneToMany.Models.DataContext";
        }

        protected override void Seed(AssociationOneToMany.Models.DataContext context)
        {
            // This code was moved here from the DataContext.cs source code file
            // after Code First Migrations was activated

            if (context.Manufacturers.Count() == 0)
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
}
