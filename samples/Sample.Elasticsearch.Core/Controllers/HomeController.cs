using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Sample.Elasticsearch.Core.Models;
using StackExchange.Profiling;

namespace Sample.Elasticsearch.Core.Controllers {
    public class HomeController : Controller {

        private readonly IElasticClient _client;

        public HomeController(IElasticClient client) {
            _client = client;
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Index() {
            var person = new Person {
                Id = "1",
                Firstname = "Martijn",
                Lastname = "Laarman"
            };

            _client.IndexDocument(person);
            _client.IndexMany(new List<Person> { person, person, person });
            _client.Get<Person>("1");
            _client.DeleteIndex("not-existing-index");
            _client.ClusterHealth();

            using (MiniProfiler.Current.Step("Async")) {
                await _client.IndexDocumentAsync(person);
                using (MiniProfiler.Current.Step("Async inner 1")) {
                    await _client.IndexDocumentAsync(new List<Person> { person, person, person });
                }
                using (MiniProfiler.Current.Step("Async inner 2")) {
                    await _client.IndexManyAsync(new List<Person> { person, person, person });
                    await _client.GetAsync<Person>("1");
                }
            }

            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<JsonResult> Ajax() {
            var result = _client.Get<Person>("1").Source;
            // execute duplicate async
            var resultAsync = (await _client.GetAsync<Person>("1")).Source;
            return Json(new { result, resultAsync });
        }
    }
}
