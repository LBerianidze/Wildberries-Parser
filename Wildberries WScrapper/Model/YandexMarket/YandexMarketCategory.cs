using System.Collections.Generic;
using System.Xml.Serialization;
using Wildberries_WScrapper.Model.Interfaces;

namespace Wildberries_WScrapper.Model.YandexMarket
{
	public class YandexMarketCategory : ICategory
	{
		public string Name { get; set; }
		public string URL { get; set; }
		public List<YandexMarketCompany> Items { get; } = new List<YandexMarketCompany>();
		[XmlIgnore]
		public List<string> Brands { get; } = new List<string>();

		public int Type { get; set; }

		public YandexMarketCategory(string url, string name)
		{
			URL = url;
			Name = name;
		}
		public YandexMarketCategory(string url, string name,int type):this(url,name)
		{
			this.Type = type;
		}
		public YandexMarketCategory()
		{
		}
	}
}
