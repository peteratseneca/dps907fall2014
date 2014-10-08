using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErrorHandling.Controllers
{
    public class LoggedExceptionAdd
    {
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

    public class LoggedExceptionBase : LoggedExceptionAdd
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
    }

}
