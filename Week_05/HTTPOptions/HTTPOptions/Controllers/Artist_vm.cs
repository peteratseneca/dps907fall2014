using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace HTTPOptions.Controllers
{
    public class ArtistList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ArtistAdd
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        public string BirthName { get; set; }
        // For a person, the birth date
        // For a duo/band/group, the date the band started working together
        public DateTime BirthOrStartDate { get; set; }
        [Required, StringLength(50)]
        public string Genre { get; set; }
    }

    public class ArtistBase : ArtistAdd
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// Resource that includes the Link property
    /// </summary>
    public class ArtistWithLink : ArtistBase
    {
        public Link Link { get; set; }
    }

    /// <summary>
    /// A 'container' representation that includes a link to the resource, 
    /// and an 'Item' property for the enclosed data
    /// </summary>
    public class ArtistLinked : LinkedItem<ArtistWithLink> { }

    /// <summary>
    /// A 'container' representation that includes a link to the resource, 
    /// and a 'Collection' property for the enclosed data
    /// </summary>
    public class ArtistsLinked : LinkedCollection<ArtistWithLink> { }

    /// <summary>
    /// Another 'container' representation that includes a link to the resource,
    /// a 'Collection' property for the enclosed data, and
    /// a 'Template' property that tells the user about the 'add new' properties
    /// </summary>
    public class ArtistsLinkedWithTemplateInfo : LinkedCollection<ArtistWithLink> 
    {
        public ArtistsLinkedWithTemplateInfo()
        {
            this.Template = new ArtistAddTemplate();
        }

        /// <summary>
        /// 'Add new' template property
        /// </summary>
        public ArtistAddTemplate Template { get; set; }
    }

    /// <summary>
    /// 'Add new' item template; tells the users about the 'add new' properties
    /// </summary>
    public class ArtistAddTemplate
    {
        public string Name { get { return "Artist name, required, string, up to 50 characters"; } }
        public string BirthName { get { return "Birth name, string, up to 50 characters"; } }
        public string BirthOrStartDate { get { return "Birth date for individual or start date for duo/band/group, required, date in ISO 8601 format"; } }
        public string Genre { get { return "Genre, required, string, up to 50 characters"; } }
    }

}
