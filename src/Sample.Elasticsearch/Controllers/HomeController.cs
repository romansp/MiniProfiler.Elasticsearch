namespace Sample.Elasticsearch.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Web.Mvc;
	using Models;
	using Nest;
	using StackExchange.Profiling;
	using StackExchange.Profiling.Elasticsearch;

	public class HomeController : Controller
	{
		private readonly ProfiledElasticClient _client;

		public HomeController()
		{
			var node = new Uri("http://localhost:9200");
			var connectionSettings = new ConnectionSettings(node, defaultIndex: "elasticsearch-sample");
			_client = new ProfiledElasticClient(connectionSettings);
		}
		
		public async Task<ActionResult> Index()
		{
			var person = new Person
			{
				Id = "1",
				Firstname = "Martijn",
				Lastname = "Laarman"
			};

			_client.Index(person);
			_client.IndexMany(new List<Person> { person, person, person });
			_client.Get<Person>("1");
			_client.DeleteIndex("not-existing-index");
			_client.ClusterHealth();

			using (MiniProfiler.StepStatic("Async"))
			{
				// timings not working 
				await _client.IndexAsync(person);
				await _client.IndexAsync(new List<Person> { person, person, person });
				await _client.IndexManyAsync(new List<Person> { person, person, person });
				await _client.GetAsync<Person>("1");
			}
			
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Ajax()
		{
			var result = _client.Get<Person>("1");
			return Json(result);
		}
	}
}