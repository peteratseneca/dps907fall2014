using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

// This source code file is used to define your model classes

namespace ErrorHandling.Models
{
    /// <summary>
    /// Exception data object, saved in the persistent store
    /// </summary>
    public class LoggedException
    {
        public LoggedException()
        {
            this.DateAndTime = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        [StringLength(1000)]
        public string UserName { get; set; }
        [StringLength(5000)]
        public string Message { get; set; }
        [StringLength(1000)]
        public string Source { get; set; }
        [StringLength(1000)]
        public string Method { get; set; }
        public string StackTrace { get; set; }
    }

    // Add your design model classes here
}
