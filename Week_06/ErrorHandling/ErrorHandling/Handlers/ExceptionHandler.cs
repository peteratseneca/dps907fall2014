﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.Web.Http.Filters;
using ErrorHandling.Controllers;
using ErrorHandling.ServiceLayer;
using System.Net;
using System.Net.Http;

namespace ErrorHandling.Handlers
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Worker m = new Worker();

            // Create and configure a new logged exception object
            LoggedExceptionAdd ex = new LoggedExceptionAdd();

            ex.Message = actionExecutedContext.Exception.Message;
            if (actionExecutedContext.Exception.InnerException != null)
            {
                ex.Message = string.Format("{0} {1}", ex.Message,
                    actionExecutedContext.Exception.InnerException.Message);
            }

            ex.Source = actionExecutedContext.Exception.Source;
            ex.Method = actionExecutedContext.Exception.TargetSite.Name;
            ex.StackTrace = actionExecutedContext.Exception.StackTrace;

            ex.UserName = (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) ?
                "anonymous" :
                HttpContext.Current.User.Identity.Name;

            // Add it to the persistent store
            m.Exceptions.AddNew(ex);

            // Create a new response
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            // This will appear in the first line of the response
            response.ReasonPhrase = "Application Execution Error";

            // This will appear in the response message body
            response.Content = new StringContent("Sorry - an application execution error has happened. Please send another request.");

            // Configure the response property
            actionExecutedContext.Response = response;
        }
    }

}
