using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wildberries_WScrapper.Model.Interfaces;

namespace Wildberries_WScrapper.Model.YandexMarket
{
	public class ExcelAttribute : Attribute
	{
		public ExcelAttribute()
		{

		}

		private string name;

		public string Name => name;

		public ExcelAttribute(string name)
		{
			this.name = name;
		}
	}
	public class YandexMarketCompany : ICardItem
	{
		[JsonIgnore]
		public List<Website> Websites { get; set; } = new List<Website>(); // without list
		public string Category { get; set; }
		[Excel("Категория")]
		public string CategoryList { get; set; }
		[Excel("Рейтинг")]
		public string Rating { get; set; }
		[Excel("Оценка за весь период")]
		public string MarkForFullPeriod { get; set; }
		[Excel("Оценка за три месяца")]
		public string MarkForThreeMonth { get; set; }
		[Excel("Компания")]
		public string Company { get; set; }
		[Excel("ОГРН")]
		public string OGRN { get; set; }
		[Excel("Время на Я.Маркете")]
		public string AppearTime { get; set; }
		[Excel("Руководитель")]
		public string Boss { get; set; }
		//[System.Xml.Serialization.XmlElement(ElementName = "Персонал", IsNullable = true)]
		[Excel("Кол-во персонала")]
		public string PersonalCount { get; set; }
		[Excel("Адрес на list-org")]
		public string ListorgURL { get; set; }
		//[System.Xml.Serialization.XmlElement(ElementName = "Датарегистрации", IsNullable = true)]
		[Excel("Дата регистрации")]
		public string RegisterDate { get; set; }
		//[System.Xml.Serialization.XmlElement(ElementName = "Малоепредприятие")]
		[Excel("Является малым предпринимательством")]
		public bool IsSmall { get; set; }
		//[System.Xml.Serialization.XmlElement(ElementName = "Естьсудебныедела")]
		[Excel("Учавствует в судебных делах")]
		public bool HasJudgments { get; set; }
		//[System.Xml.Serialization.XmlElement(ElementName = "Ответчик")]
		[Excel("Выступал в роли ответчика")]
		public bool Response { get; set; }
		[JsonIgnore]
		public List<Capital> Capitals { get; set; } = new List<Capital>(); // without list
		[Excel("Ссылка на Я.Маркет")]
		public string URL { get; set; }
		[Excel("Количество просмотров за месяц")]
		public string MonthViews { get; set; }
		[Excel("Количество просмотров за неделю")]
		public string WeekViews { get; set; }
		[Excel("Количество просмотров за день")]
		public string DayViews { get; set; }

		[Excel("Юр.Адрес")]
		public string LegalAddress { get; set; }
		[Excel("Сайт компании")]
		public string Website
		{
			get
			{
				if (Websites.Count == 0)
					return "";
				return Websites[0].URL;
			}
		}
		[Excel("Капитал")]
		public Capital Capital
		{
			get
			{
				if (Capitals.Count == 0)
					return new Capital();
				return Capitals[0];
			}
		}
		[Excel("Телефон")]
		public List<string> WebsitePhones
		{
			get
			{
				if (Websites == null || Websites.Count == 0)
					return new List<string>();
				if (Websites[0].Phones == null)
					return new List<string>();
				return Websites[0].Phones;
			}
		}
		[System.Xml.Serialization.XmlIgnore]
		[JsonIgnore]
		public string Brand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[System.Xml.Serialization.XmlIgnore]
		[JsonIgnore]
		public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[System.Xml.Serialization.XmlIgnore]
		[JsonIgnore]
		public string Price { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	}
	public class Capital
	{
		[System.Xml.Serialization.XmlElement(ElementName = "Год")]
		[Excel("Год")]
		public string Year { get; set; }
		[System.Xml.Serialization.XmlElement(ElementName = "Доход")]
		[Excel("Доход")]
		public string In { get; set; }
		[System.Xml.Serialization.XmlElement(ElementName = "Расход")]
		[Excel("Расход")]
		public string Out { get; set; }
		[System.Xml.Serialization.XmlElement(ElementName = "ДоходРасход")]
		[Excel("Сумма")]
		public string InOut { get; set; }
	}
}
