using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using HTTPOptions.Models;
using AutoMapper;

namespace HTTPOptions.ServiceLayer
{
    public class Album_repo : Repository<Album>
    {
        // Constructor
        public Album_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers


    }

}
