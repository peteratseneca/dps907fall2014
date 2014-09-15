using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace AllHttpMethods.Models
{
    public class Human
    {
        public Human()
        {
            // Default initial value, which can be overwritten
            this.BirthDate = DateTime.Now.AddYears(-20);
        }

        public int Id { get; set; }
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

}
