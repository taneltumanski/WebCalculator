using System.Web;
using System.Web.Mvc;
using WebCalculator.Filters;

namespace WebCalculator
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new LogErrorFilterAttribute());
		}
	}
}
