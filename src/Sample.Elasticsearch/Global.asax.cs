namespace Sample.Elasticsearch
{
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;
	using StackExchange.Profiling;
	using StackExchange.Profiling.Mvc;

	public class MvcApplication : HttpApplication
	{

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			// Setup profiler for Controllers via a Global ActionFilter
			GlobalFilters.Filters.Add(new ProfilingActionFilter());

			// initialize automatic view profiling
			var copy = ViewEngines.Engines.ToList();
			ViewEngines.Engines.Clear();
			foreach (var item in copy)
			{
				ViewEngines.Engines.Add(new ProfilingViewEngine(item));
			}
		}

		protected void Application_BeginRequest()
		{
			MiniProfiler.Start();
		}
		protected void Application_EndRequest()
		{
			MiniProfiler.Stop();
		}
	}
}
