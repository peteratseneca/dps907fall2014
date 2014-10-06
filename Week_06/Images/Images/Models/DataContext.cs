using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.Data.Entity;
using System.Web.Hosting;
using System.IO;

// This source code file is used to define the data context and optional store initializer

// The data context is directly used by manager (i.e. data service operations) classes,
// and other business and application classes

// The store initializer is used in simple projects, and enables you to create some initial data

namespace Images.Models
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

        // Add DbSet<TEntity> properties here

        public DbSet<Book> Books { get; set; }
    }

    // Disabled, because this project now uses Code First Migrations

    /// <summary>
    /// Store initializer, notice the inheritance
    /// </summary>
    public class StoreInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        // Enabled by a statement in the WebApiApplication class (in Global.asax.cs)
        // Disable that statement when you are using Entity Framework Code First Migrations

        /// <summary>
        /// Seed the data store with initial data
        /// </summary>
        /// <param name="context">The project's data context object</param>
        protected override void Seed(DataContext context)
        {
            // Create some new objects, and save them

            // You must move this code to the Configurations.cs source code file
            // when you enable Entity Framework Code First Migrations

            Book shady = new Book()
            {
                Title = "Shady Characters",
                Author = "Houston, Keith",
                ISBN13 = "9780393064421",
                Pages = 340,
                Published = new DateTime(2013, 9, 24),
                Format = "Hardcover",
                Photo = File.ReadAllBytes(HostingEnvironment.MapPath("~/App_Data/images/shady-characters.png")),
                ContentType = "image/png"
            };
            context.Books.Add(shady);

            Book jack = new Book()
            {
                Title = "Gone Tomorrow",
                Author = "Child, Lee",
                ISBN13 = "9780593057056",
                Pages = 440,
                Published = new DateTime(2009, 4, 1),
                Format = "Hardcover",
                Photo = File.ReadAllBytes(HostingEnvironment.MapPath("~/App_Data/images/gone-tomorrow.png")),
                ContentType = "image/png"
            };
            context.Books.Add(jack);

            Book english = new Book()
            {
                Title = "The Mother Tongue, English and How It Got That Way",
                Author = "Bryson, Bill",
                ISBN13 = "9780380715435",
                Pages = 272,
                Published = new DateTime(1991, 9, 28),
                Format = "Paperback",
                Photo = File.ReadAllBytes(HostingEnvironment.MapPath("~/App_Data/images/the-mother-tongue.png")),
                ContentType = "image/png"
            };
            context.Books.Add(english);

            context.SaveChanges();
        }

    }

}
