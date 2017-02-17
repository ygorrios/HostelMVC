using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Camada.Aplicacao.Controllers.ActionsFilters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
            foreach (var parameter in filterContext.ActionParameters)
            {
                //response.Write(string.Format("{0}: {1}", parameter.Key, parameter.Value));
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
            if (filterContext.Exception != null)
            {
                filterContext.ExceptionHandled = true;

                //geturlparameter
                string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string action = filterContext.ActionDescriptor.ActionName;
                if (filterContext.Exception.InnerException != null && !string.IsNullOrEmpty(filterContext.Exception.Message))
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = action, msg = filterContext.Exception.InnerException.Message }));
                else if (!string.IsNullOrEmpty(filterContext.Exception.Message))
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = action, msg = filterContext.Exception.Message }));
                else
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = action }));
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            //Debug.WriteLine(message, "Action Filter Log");
        }

    }
}