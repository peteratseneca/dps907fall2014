using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using Images.Models;
using Images.Controllers;
using AutoMapper;

namespace Images.ServiceLayer
{
    public class Template_repo : Repository<Template>
    {
        // Constructor
        public Template_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers

        // Get all

        // Get all, filtered

        // Get one, by its identifier

        // Add new

        // Edit existing

        // Delete existing
    }

}
