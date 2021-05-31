using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wildberries_WScrapper.Model.Interfaces
{
	public interface ICardItem
	{
		 string Brand { get; set; }
		 string Name { get; set; }
		 string Price { get; set; }
		 string OGRN { get; set; }
		 string Company { get; set; }
		 string URL { get; set; }
		 List<Website> Websites { get; set; }
		 string Category { get; set; }
	}
}
