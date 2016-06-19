using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC1.Controllers;

namespace MVC1.Filters
{
    public class LogFilter : ActionFilterAttribute, IActionFilter
    {
        private void LogAction(string scope, IController controller, string context)
        {
            if (controller == null)
                return;

            if (!controller.GetType().IsSubclassOf(typeof(BaseController)))
                return;

            var baseController = controller as BaseController;

            baseController.Logger.LogInfo(context, scope, "message");//baseController.Request.RequestContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogAction("Request", filterContext.Controller, filterContext.HttpContext.Items["app.context"].ToString());

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LogAction("Request", filterContext.Controller, filterContext.HttpContext.Items["app.context"].ToString());

            base.OnActionExecuted(filterContext);
        }
    }
}