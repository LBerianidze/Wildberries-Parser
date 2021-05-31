using System.Collections.Generic;

namespace Wildberries_WScrapper.Model.WildBerries
{
	public class YandexCategory
	{
		public string Name { get; set; }
		public List<WildberriesYandexItem> Items = new List<WildberriesYandexItem>();
		public YandexCategory( string name)
		{
			Name = name;
		}
		public YandexCategory()
		{

		}
	}
}
