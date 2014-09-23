using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace AssociationOneToMany.Controllers
{
    public class VehicleAdd
    {
        [Required, StringLength(50)]
        public string Model { get; set; }
        [Required, StringLength(50)]
        public string Trim { get; set; }
        [Range(2010, 2050)]
        public int ModelYear { get; set; }
        [Range(5000, 500000)]
        public int MSRP { get; set; }

        public int ManufacturerId { get; set; }
    }

    public class VehicleBase : VehicleAdd
    {
        public int Id { get; set; }
    }

    public class VehicleWithManufacturer : VehicleBase
    {
        public ManufacturerBase Manufacturer { get; set; }
    }
}
