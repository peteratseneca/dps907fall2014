using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// This source code file is used to define your model classes

namespace ColourList.Models
{
    /// <summary>
    /// A simple Colour class
    /// </summary>
    public class Colour
    {
        /// <summary>
        /// Identifier; by convention, if its name is "Id" or "classnameId", then its column in the database table will be managed by the database server, and will be auto-incrementing
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Colour name
        /// </summary>
        public string ColourName { get; set; }
    }
}
