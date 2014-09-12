using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColourList.Controllers
{
    public class ColoursList
    {
        public int Id { get; set; }
        public string ColourName { get; set; }
    }

    public class ColourAdd
    {
        public string ColourName { get; set; }
    }

    public class ColourBase : ColourAdd
    {
        public int Id { get; set; }
    }

    // Others could be added later...



}
