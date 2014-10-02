using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using HTTPOptions.Models;
using AutoMapper;

namespace HTTPOptions.ServiceLayer
{
    public class Song_repo : Repository<Song>
    {
        // Constructor
        public Song_repo(DataContext ds) : base(ds) { }

        // Methods called by controllers


    }

}
