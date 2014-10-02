using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace HTTPOptions.Controllers
{
    public class AlbumList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AlbumAdd
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required, StringLength(50)]
        public string Genre { get; set; }
        public int ArtistId { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        public int Id { get; set; }
    }

}
