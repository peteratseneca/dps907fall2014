using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using SecurityTests.Models;
using SecurityTests.Controllers;
using AutoMapper;

namespace SecurityTests.ServiceLayer
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
