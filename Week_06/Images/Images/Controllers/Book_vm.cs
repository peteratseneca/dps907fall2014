using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Images.Controllers
{
    public class BookList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string TitleAndAuthor 
        {
            get { return string.Format("{0}, by {1}", Title, Author); }
        }
    }

    public class BookAdd
    {
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required, StringLength(100)]
        public string Author { get; set; }
        [Required, StringLength(13)]
        public string ISBN13 { get; set; }
        [Range(10,2000)]
        public int Pages { get; set; }
        public DateTime Published { get; set; }
        [Required, StringLength(50)]
        public string Format { get; set; }
    }

    public class BookAddTemplate
    {
        public string Title { get { return "Book title, required, string, up to 100 characters"; } }
        public string Author { get { return "Author name (Lastname, Firstname), required, string, up to 100 characters"; } }
        public string ISBN13 { get { return "ISBN-13 number, required, up to 13 alpha-numeric characters"; } }
        public string Pages { get { return "Pages, required, number, ranges from 10 to 2000"; } }
        public string Published { get { return "Date published, required, date in ISO 8601 format (YYYY-MM-DDThh:mm:ss"; } }
        public string Format { get { return "Format (hardcover, paperback, etc.), required, string, up to 50 characters"; } }
    }

    /// <summary>
    /// Book edit, includes only the propeties that we allow to be edited
    /// </summary>
    public class BookEdit
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Author { get; set; }
        [Required, StringLength(13)]
        public string ISBN13 { get; set; }
        [Required, StringLength(50)]
        public string Format { get; set; }
    }

    public interface IObjectWithImage
    {
        byte[] Photo { get; set; }
        string ContentType { get; set; }
    }

    public class BookBase : BookAdd, IObjectWithImage
    {
        public int Id { get; set; }

        // The following are image-related properties
        // For this code example, we do NOT expose them during normal JSON or XML serialization
        [JsonIgnore, IgnoreDataMember]
        public byte[] Photo { get; set; }
        [JsonIgnore, IgnoreDataMember]
        public string ContentType { get; set; }
    }

    public class BookWithLink : BookBase
    {
        public Link Link { get; set; }
    }

    public class BookLinked : LinkedItem<BookWithLink> { }

    public class BooksLinked : LinkedCollection<BookWithLink>
    {
        public BooksLinked()
        {
            this.Template = new BookAddTemplate();
        }

        public BookAddTemplate Template { get; set; }
    }

}
