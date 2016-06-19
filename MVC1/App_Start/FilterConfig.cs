using System.Web;
using System.Web.Mvc;
using MVC1.Filters;

namespace MVC1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogFilter());

            filters.Add(new HandleErrorAttribute());
        }
    }
}
