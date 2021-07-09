using DynamicCoffeeSellers.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace DynamicCoffeeSellers.Models
{
	public class EntityDataService
	{
		private readonly DynamicsConnector _dynamicsConnector;

		public EntityDataService(DynamicsConnector dynamicsConnector)
		{
			_dynamicsConnector = dynamicsConnector;
		}

		public ProductViewModel GetProducts()
		{
			JObject rawData = _dynamicsConnector.GetJObject("products");
			IEnumerable<JToken> data = from p in rawData["value"]
									   where p["_defaultuomscheduleid_value"].ToString() == "d58a4dcf-62df-eb11-bacb-000d3a1faed2"
									   select p;

			List<Product> productList = new List<Product>();
			foreach (var record in data)
			{
				var formattedRecord = new Product();
				formattedRecord.Name = record["name"].ToString();
				formattedRecord.ProductId = record["productid"].ToString();
				formattedRecord.Description = record["description"].ToString();
				formattedRecord.QuantityOnHand = record["quantityonhand"].ToString();
				formattedRecord.ImgUrl = record["jej_imageurl"].ToString();
				productList.Add(formattedRecord);
			}

			var productVM = new ProductViewModel();
			productVM.AllProducts = productList;

			return productVM;
		}

		public RequestFormViewModel GetRequestForm(string productName)
		{
			JObject rawData = _dynamicsConnector.GetJObject("jej_industries");
			IEnumerable<JToken> data = from i in rawData["value"]
									   select i;

			List<Industry> industryList = new List<Industry>();
			foreach (var record in data)
			{
				var formattedRecord = new Industry();
				formattedRecord.Name = record["jej_name"].ToString();
				formattedRecord.IndustryId = record["jej_industryid"].ToString();
				industryList.Add(formattedRecord);
			}
			industryList = industryList.OrderBy(industry => industry.Name).ToList();

			var requestFormVM = new RequestFormViewModel();
			requestFormVM.SelectedProduct = productName;
			requestFormVM.AllIndustries = industryList;

			return requestFormVM;
		}
	}
}
