﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

// This source code file is used to define your model classes

namespace PatternsIntro.Models
{
    // Vehicles and their manufacturers

    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Vehicles = new List<Vehicle>();
            // There will be logic elsewhere to set the allowable range for the following
            this.YearStarted = DateTime.Now.Year;
        }

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Country { get; set; }
        public int YearStarted { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }

    public class Vehicle
    {
        public Vehicle()
        {
            // There will be logic elsewhere to set the allowable range for the following
            this.ModelYear = DateTime.Now.Year;
            this.MSRP = 20000;
        }
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Model { get; set; }
        [Required, StringLength(50)]
        public string Trim { get; set; }
        public int ModelYear { get; set; }
        public int MSRP { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }

}
