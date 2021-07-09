using DynamicCoffeeSellers.Models;
using DynamicCoffeeSellers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DynamicCoffeeSellers.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DynamicsConnector _dynamicsConnector;
		private readonly EntityDataService _entityDataService;
		private readonly CacheService _cacheService;

		public HomeController(ILogger<HomeController> logger, DynamicsConnector dynamicsConnector, EntityDataService entityDataService, CacheService cacheService)
		{
			_logger = logger;
			_entityDataService = entityDataService;
			_cacheService = cacheService;
			_dynamicsConnector = dynamicsConnector;
		}

		public IActionResult Index()
        {
			return View("Index");
        }

		[Route("Home/Products")]
		public IActionResult Products()
		{
			ProductViewModel productVM = _cacheService.GetSetProductsCache();
			return View("ProductsPage", productVM);
		}

		[Route("Home/RequestForm/{productName?}")]
		public IActionResult RequestForm(string productName)
        {
			RequestFormViewModel requestFormVM = _cacheService.GetSetRequestFormCache(productName);
			return View("RequestForm", requestFormVM);
        }

		[HttpPost]
		public IActionResult FormSubmitted(RequestForm form)
		{

			string payload = JsonConvert.SerializeObject(form);

			_dynamicsConnector.Post("leads", payload);

			return View("FormSubmitted", form);
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
