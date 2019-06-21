using System;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Tracing;
using System.Web.Http;
using FSE_ProjMgr.Log;

namespace FSE_ProjMgr.ActionFilters
{
    public class TransactionLogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)

        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new TransactionExceptionLogEntry());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(filterContext.Request, "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON", filterContext.ActionArguments);
        }
    }
}