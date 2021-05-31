using System.Collections.Generic;

namespace Wildberries_WScrapper.Model.WildBerries
{
	public class WildBerriesItem
	{
		public string Brand { get; set; }
		public string Name { get; set; }
		public string Price { get; set; }
		public string OGRN { get; set; }
		public string Company { get; set; }
		public string URL { get; set; }
		public List<Website> Websites { get; set; } = new List<Website>();
		public string Category { get; set; }
	}
}
