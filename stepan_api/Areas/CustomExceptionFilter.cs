using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace stepan_api.Areas
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {

        public override void OnException (HttpActionExecutedContext context)
        {
            //var exceptionType = context.Exception.GetType();
            //if (exceptionType == typeof(ArgumentNullException))
            //{
            //    context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            //    Exception exp = context.Exception;
            //    ExceptionLogging.SendExcepToDB(exp);
            //}
            ////HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            ////{
            ////    Content = new StringContent("An unhandled exception was thrown by Customer Web API controller."),
            ////    ReasonPhrase = "An unhandled exception was thrown by Customer Web API controller."
            ////};
            ////context.Response = msg;
           
            //////HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            //////{
            //////    Content = new StringContent("An unhandled exception was thrown by Customer Web API controller."),
            //////    ReasonPhrase = "An unhandled exception was thrown by Customer Web API controller."
            //////};
            ////// Exception exp = context.Exception;
            //////ExceptionLogging.SendExcepToDB(exp);
            //// //context.Response = msg;


            if (context.Exception is ArgumentException)
            {
                HttpResponseMessage msg =  new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "This method require atleast one parameter"
                };

            }
            
            Exception exp = context.Exception;
            ExceptionLogging.SendExcepToDB(exp);
            //Log Critical errors
            Debug.WriteLine(context.Exception);
           


            //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            //{
            //    Content = new StringContent("An error occurred, please try again or contact the administrator."),
            //    ReasonPhrase = "Critical Exception"
            //});
        }

        }
    
}