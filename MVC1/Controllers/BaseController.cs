using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logging;

namespace MVC1.Controllers
{
    public abstract class BaseController : Controller
    {
        public ILogger Logger { get; private set; }

        protected BaseController(ILogger logger)
        {
            Logger = logger;
        }
    }
}