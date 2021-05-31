using System.Collections.Generic;
using System.Xml.Serialization;
using Wildberries_WScrapper.Model.YandexMarket;

namespace Wildberries_WScrapper.Model
{
	public class Website
	{
		public string URL { get; set; }
		public List<string> Phones { get; set; } = new List<string>();
		[XmlIgnore]
		public bool StatisticParsed { get; set; }
		
		[XmlIgnore]
		[Excel("Количество просмотров за месяц")]
		public string MonthViews { get; set; }

		[Excel("Количество просмотров за неделю")]
		[XmlIgnore]
		public string WeekViews { get; set; }

		[Excel("Количество просмотров за день")]
		[XmlIgnore]
		public string DayViews { get; set; }

		[XmlIgnore]
		public string MainSource { get;  set; }
		[XmlIgnore]
		public string ContactSource { get;  set; }
	}
}
