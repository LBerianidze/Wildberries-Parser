using System.Collections.Generic;
using Wildberries_WScrapper.Model.Interfaces;

namespace Wildberries_WScrapper.Model.WildBerries
{
	public class WildberriesCategory : ICategory
	{
		public string Name { get; set; }
		public string URL { get; set; }
		public List<WildBerriesItem> Items { get; } = new List<WildBerriesItem>();
		public WildberriesCategory(string url, string name)
		{
			URL = url;
			Name = name;
		}
		public WildberriesCategory()
		{

		}
	}
}
