using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.Common
{
    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);

            //write code for filtering exception !!
        }
        
    }
}