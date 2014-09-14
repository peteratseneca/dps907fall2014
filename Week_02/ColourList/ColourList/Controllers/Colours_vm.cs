using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

// This source code file is used to define the 'resource model' classes

// Create a source code file for each entity class (or each logically-related group of entities)
// Notice the source code file name - it uses a "_vm" suffix for logical consistency and continuity
// with your past experience with 'view model' classes in ASP.NET MVC web apps

namespace ColourList.Controllers
{
    // For a user interface list that needs only identifiers and name-labels
    public class ColoursList
    {
        public int Id { get; set; }
        public string ColourName { get; set; }
    }

    // For the 'add object' task, includes properties needed when creating a new object
    public class ColourAdd
    {
        [Required]
        public string ColourName { get; set; }
    }

    // For the typical use of an object, includes the identifier property
    // Notice the use of inheritance
    public class ColourBase : ColourAdd
    {
        public int Id { get; set; }
    }

    // Others could be added later if we needed them...

}
