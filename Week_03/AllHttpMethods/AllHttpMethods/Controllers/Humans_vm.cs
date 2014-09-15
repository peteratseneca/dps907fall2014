using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace AllHttpMethods.Controllers
{
    public class HumanList
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public string FullName { get { return string.Format("{0}, {1}", this.FamilyName, this.GivenNames); } }
    }

    public class HumanAdd
    {
        [Required]
        [StringLength(50)]
        public string FamilyName { get; set; }
        [Required]
        [StringLength(50)]
        public string GivenNames { get; set; }
        public DateTime BirthDate { get; set; }
        [StringLength(50)]
        public string FavouriteColour { get; set; }
    }

    public class HumanBase : HumanAdd
    {
        public int Id { get; set; }
    }

    public class HumanEdit
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string FavouriteColour { get; set; }
    }

}
