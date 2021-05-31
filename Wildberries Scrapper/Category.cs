using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wildberries_Scrapper
{
	public class Category
	{
		public string Name { get; set; }
		public string URL { get; set; }
		public List<WildBerriesItem> Items = new List<WildBerriesItem>();
		public Category(string url, string name)
		{
			URL = url;
			Name = name;
		}
		public Category()
		{

		}
	}
}
