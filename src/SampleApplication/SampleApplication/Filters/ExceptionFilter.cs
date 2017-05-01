using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleApplication.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("An Exception has occured! {0}", context.Exception);
            context.ExceptionHandled = true;
            context.Result = new ContentResult()
            {
                StatusCode = 500,
                Content = "This should not happen. Contact administrator."
            };
        }
    }
}