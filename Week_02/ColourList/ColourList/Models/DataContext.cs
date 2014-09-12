using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.Data.Entity;

namespace ColourList.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DefaultConnection") { }

        public DbSet<Colour> Colours { get; set; }
    }

    public class StoreInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            Colour red = new Colour() { ColourName = "Red" };
            context.Colours.Add(red);

            Colour green = new Colour() { ColourName = "Green" };
            context.Colours.Add(green);

            context.SaveChanges();

        }
    }
}




