using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace PatternsIntro.Controllers
{
    public class ManufacturerAdd
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Country { get; set; }
        [Range(1850, 2200)]
        public int YearStarted { get; set; }
    }

    public class ManufacturerBase : ManufacturerAdd
    {
        public int Id { get; set; }
    }

    public class ManufacturerWithVehicles : ManufacturerBase
    {
        public ICollection<VehicleBase> Vehicles { get; set; }
    }
}
