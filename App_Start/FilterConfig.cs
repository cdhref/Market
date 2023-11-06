using Market.Common.Filter;
using System.Web.Mvc;

namespace Market
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalFilterAttribute());
        }
    }
}
