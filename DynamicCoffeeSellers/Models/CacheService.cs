using DynamicCoffeeSellers.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DynamicCoffeeSellers.Models
{
	public class CacheService
	{
		private readonly IMemoryCache _cache;
		private readonly EntityDataService _entityDataService;

		public CacheService(IMemoryCache memoryCache, EntityDataService entityDataService)
		{
			_cache = memoryCache;
			_entityDataService = entityDataService;
		}

		public ProductViewModel GetSetProductsCache()
		{
			if (!_cache.TryGetValue("productsList", out ProductViewModel productVM))
			{
				productVM = _entityDataService.GetProducts();

				var cacheEntryOptions = new MemoryCacheEntryOptions()
				{
					AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
				};

				_cache.Set("productsList", productVM, cacheEntryOptions);
			}
			return productVM;
		}

		public RequestFormViewModel GetSetRequestFormCache(string productName)
		{
			if (!_cache.TryGetValue("requestForm", out RequestFormViewModel requestFormVM))
			{
				requestFormVM = _entityDataService.GetRequestForm(productName);

				var cacheEntryOptions = new MemoryCacheEntryOptions()
				{
					AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
				};

				_cache.Set("productsList", requestFormVM, cacheEntryOptions);
			}
			return requestFormVM;
		}
	}
}
