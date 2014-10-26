using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace Associations.Controllers
{
    // Edit these definitions
    public class ClassList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ClassAdd
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        // Add more here
    }

    public class ClassAddTemplate
    {
        public string Name { get { return "Name, required, string, up to 50 characters"; } }
        // Add more here
    }

    public class ClassBase : ClassAdd
    {
        public int Id { get; set; }
    }

    public class ClassWithLink : ClassBase
    {
        public Link Link { get; set; }
    }

    public class ClassLinked : LinkedItem<ClassWithLink> { }

    public class ClassesLinked : LinkedCollection<ClassWithLink>
    {
        public ClassesLinked()
        {
            this.Template = new ClassAddTemplate();
        }

        public ClassAddTemplate Template { get; set; }
    }


}
