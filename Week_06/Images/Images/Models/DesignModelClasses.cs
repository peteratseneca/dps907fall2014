using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

// This source code file is used to define your model classes

namespace Images.Models
{
    // Template class, please delete it
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Add your design model classes here

    public class Book
    {
        public Book()
        {
            // Set a default non-null value
            this.Published = DateTime.Now.AddYears(-1);
        }

        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required, StringLength(100)]
        public string Author { get; set; }
        [Required, StringLength(13)]
        public string ISBN13 { get; set; }
        public int Pages { get; set; }
        public DateTime Published { get; set; }
        [Required, StringLength(50)]
        public string Format { get; set; }

        public byte[] Photo { get; set; }
        public string ContentType { get; set; }
    }
}
