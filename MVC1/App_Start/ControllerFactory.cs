using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Logging;
using MVC1.Controllers;

namespace MVC1.App_Start
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType.IsSubclassOf(typeof(BaseController)))
            {
                requestContext.HttpContext.Items.Add("app.context", Guid.NewGuid().ToString());
                
                return Activator.CreateInstance(controllerType, new object[] { new MongoDBLogger("mongodb://localhost:27017", "logs", "mvc1") }) as BaseController;
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}