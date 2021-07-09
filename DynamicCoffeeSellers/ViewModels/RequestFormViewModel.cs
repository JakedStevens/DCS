using DynamicCoffeeSellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicCoffeeSellers.ViewModels
{
	public class RequestFormViewModel
	{
		public string SelectedProduct { get; set; }
		public List<Industry> AllIndustries { get; set; }
	}
}
