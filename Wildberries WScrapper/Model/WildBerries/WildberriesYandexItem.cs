using System.Collections.Generic;

namespace Wildberries_WScrapper.Model.WildBerries
{
	public class WildberriesYandexItem
	{
		public string Brand { get; set; }
		public List<Website> Websites { get; set; } = new List<Website>();
		public string Category { get; set; }
	}
}

