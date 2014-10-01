using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

// This source code file is used to define your model classes

namespace LinkRelations.Models
{
    // Music web service
    // Artist - Album - Song

    public class Artist
    {
        public Artist()
        {
            this.BirthOrStartDate = DateTime.Now.AddYears(-10);
            this.Albums = new List<Album>();
        }

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public string BirthName { get; set; }
        // For a person, the birth date
        // For a duo/band/group, the date the band started working together
        public DateTime BirthOrStartDate { get; set; }
        [Required, StringLength(50)]
        public string Genre { get; set; }

        public ICollection<Album> Albums { get; set; }

    }

    public class Album
    {
        public Album()
        {
            this.ReleaseDate = DateTime.Now.AddYears(-1);
            this.Songs = new List<Song>();
        }

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required, StringLength(50)]
        public string Genre { get; set; }

        public ICollection<Song> Songs { get; set; }
        public Artist Artist { get; set; }
    }

    public class Song
    {
        public Song()
        {
            this.SingleReleaseDate = DateTime.Now.AddYears(-1);
            this.TrackNumber = 1;
            this.LengthInSeconds = 240;
        }

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public DateTime SingleReleaseDate { get; set; }
        [Required, StringLength(50)]
        public string Composer { get; set; }
        public int TrackNumber { get; set; }
        public int LengthInSeconds { get; set; }
        [Required, StringLength(50)]
        public string Genre { get; set; }

        public Album Album { get; set; }
    }

}
