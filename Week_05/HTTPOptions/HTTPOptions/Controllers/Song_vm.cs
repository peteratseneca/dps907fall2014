using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace HTTPOptions.Controllers
{
    public class SongList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SongAdd
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        public DateTime SingleReleaseDate { get; set; }
        [Required, StringLength(50)]
        public string Composer { get; set; }
        public int TrackNumber { get; set; }
        public int LengthInSeconds { get; set; }
        [Required, StringLength(50)]
        public string Genre { get; set; }

        public int AlbumId { get; set; }
    }

    public class SongBase : SongAdd
    {
        public int Id { get; set; }
    }

}
