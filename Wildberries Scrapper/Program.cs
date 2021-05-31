using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using HtmlAgilityPack;

namespace Wildberries_Scrapper
{
	public static class Extenstions
	{
		public static List<HtmlAgilityPack.HtmlNode> GetElementsByTagAndAttributeContain(this List<HtmlAgilityPack.HtmlNode> nodes, string tag, string attribute_name, params string[] attribute_possible_value)
		{
			List<HtmlAgilityPack.HtmlNode> result = new List<HtmlAgilityPack.HtmlNode>();
			foreach (var item in nodes)
			{
				if (item.Name == tag)
				{
					if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name)?.Value != null)
					{
						foreach (var it in attribute_possible_value)
						{
							if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name).Value.ToLower().Contains(it.ToLower()))
							{
								result.Add(item);
							}
						}

					}



				}
			}
			return result;
		}
		public static HtmlAgilityPack.HtmlDocument GetHtmlDocument(this OpenQA.Selenium.Chrome.ChromeDriver driver)
		{
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(driver.PageSource);
			return doc;
		}
		public static List<HtmlAgilityPack.HtmlNode> GetAllNodes(this HtmlAgilityPack.HtmlNode parentNode)
		{
			List<HtmlAgilityPack.HtmlNode> allNodes = new List<HtmlAgilityPack.HtmlNode>();
			if (parentNode == null)
			{
				return allNodes;
			}

			allNodes.Add(parentNode);
			Stack<HtmlAgilityPack.HtmlNode> currentNodes = new Stack<HtmlAgilityPack.HtmlNode>();
			currentNodes.Push(parentNode);
			while (currentNodes.Count != 0)
			{
				HtmlAgilityPack.HtmlNode currentNode = currentNodes.Pop();
				foreach (HtmlAgilityPack.HtmlNode item in currentNode.ChildNodes)
				{
					currentNodes.Push(item);
					allNodes.Add(item);
				}
			}
			return allNodes;

		}
		/// <summary>
		/// Looks for tag in elements list and return n`s matching element
		/// </summary>
		/// <param name="nodes">List of Html Nodes</param>
		/// <param name="tag">Tag name</param>
		/// <param name="skip_count">Defines the number of elements to skip before returning</param>
		/// <returns></returns>
		public static HtmlAgilityPack.HtmlNode GetElementByTag(this List<HtmlAgilityPack.HtmlNode> nodes, string tag, int skip_count = 0)
		{
			int currentIndex = 0;
			foreach (HtmlAgilityPack.HtmlNode item in nodes)
			{
				if (item.Name == tag)
				{
					if (currentIndex == skip_count)
					{
						return item;
					}

					currentIndex++;
				}
			}
			return null;
		}
		/// <summary>
		/// Looks for tag in elements list and return n`s matching element
		/// </summary>
		/// <param name="nodes">List of Html Nodes</param>
		/// <param name="tag">Tag name</param>
		/// <param name="skip_count">Defines the number of elements to skip before returning</param>
		/// <returns></returns>
		public static List<HtmlAgilityPack.HtmlNode> GetElementsByTag(this List<HtmlAgilityPack.HtmlNode> nodes, string tag)
		{
			List<HtmlAgilityPack.HtmlNode> result_nodes = new List<HtmlAgilityPack.HtmlNode>();
			foreach (HtmlAgilityPack.HtmlNode item in nodes)
			{
				if (item.Name == tag)
				{
					result_nodes.Add(item);
				}
			}
			return result_nodes;
		}
		/// <summary>
		/// Looks for tag in elements list and return n`s matching element
		/// </summary>
		/// <param name="nodes">List of Html Nodes</param>
		/// <param name="tag">Tag name</param>
		/// <param name="skip_count">Defines the number of elements to skip before returning</param>
		/// <returns></returns>
		public static HtmlAgilityPack.HtmlNode GetElementByTagAndAttributeEquality(this List<HtmlAgilityPack.HtmlNode> nodes, string tag, string attribute_name, string attribute_value, int skip_count = 0)
		{
			int currentIndex = 0;
			foreach (HtmlAgilityPack.HtmlNode item in nodes)
			{
				if (item.Name == tag)
				{
					if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name)?.Value.ToLower().Contains(attribute_value) == true)
					{
						if (currentIndex == skip_count)
						{
							return item;
						}

						currentIndex++;
					}



				}
			}
			return null;
		}
		/// <summary>
		/// Looks for tag in elements list and return n`s element which containt specified attribute and it's value is also equal to specified
		/// </summary>
		/// <param name="nodes">List of Html Nodes</param>
		/// <param name="tag">Tag name</param>
		/// <param name="skip_count">Defines the number of elements to skip before returning</param>
		/// <returns></returns>
		public static HtmlAgilityPack.HtmlNode GetElementByTagAndAttributeEquality(this List<HtmlAgilityPack.HtmlNode> nodes, string tag, string attribute_name, params string[] attribute_possible_value)
		{
			foreach (HtmlAgilityPack.HtmlNode item in nodes)
			{
				if (item.Name == tag)
				{
					if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name)?.Value != null)
					{
						foreach (string it in attribute_possible_value)
						{
							if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name).Value.ToLower() == it.ToLower())
							{
								return item;
							}
						}

					}



				}
			}
			return null;
		}
		/// <summary>
		/// Looks for tag in elements list and return n`s element which containt specified attribute and it's value contains specified
		/// </summary>
		/// <param name="nodes">List of Html Nodes</param>
		/// <param name="tag">Tag name</param>
		/// <param name="skip_count">Defines the number of elements to skip before returning</param>
		/// <returns></returns>
		public static HtmlAgilityPack.HtmlNode GetElementByTagAndAttributeContain(this List<HtmlAgilityPack.HtmlNode> nodes, string tag, string attribute_name, params string[] attribute_possible_value)
		{
			foreach (HtmlAgilityPack.HtmlNode item in nodes)
			{
				if (item.Name == tag)
				{
					if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name)?.Value != null)
					{
						foreach (string it in attribute_possible_value)
						{
							if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name).Value.ToLower().Contains(it.ToLower()))
							{
								return item;
							}
						}

					}



				}
			}
			return null;
		}
	}

	public class Program
	{
		private static readonly string[] ExcludedWebsites = new string[]
	{
	"facebook.com",
	"vk.com",
	"instagram.com",
	"youtube.com",
	"aliexpress.ru"
	};
		private const string spanPhonePattern = @"<.+>((\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2})<\/.+>";
		private const string russianPhonePattern = @"((8|\+7)-?)?\(?\d{3}\)?-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}";

		private static void FindPhoneOnWebpage(string url, IGrouping<string, Website> item, bool visitContactsPage = true)
		{

			driver.Url = url;
			HtmlAgilityPack.HtmlDocument document = driver.GetHtmlDocument();
			List<HtmlAgilityPack.HtmlNode> nodes = document.DocumentNode.GetAllNodes().GetElementsByTag("a");
			foreach (HtmlAgilityPack.HtmlNode a in nodes)
			{
				if (a.Attributes.FirstOrDefault(t => t.Name == "href") != null)
				{
					string href = a.Attributes["href"].Value;
					if (href.StartsWith("tel:"))
					{
						string phone = href.Substring(4);
						foreach (Website sub in item)
						{
							sub.Phone = phone;
						}
						return;
					}

				}
			}
			if (item.ElementAt(0).Phone == null)
			{
				string source = driver.PageSource.Replace("\n", "");
				foreach (Match m in Regex.Matches(source, spanPhonePattern, RegexOptions.Multiline))
				{
					string phone = m.Groups[1].Value;
					foreach (Website sub in item)
					{
						sub.Phone = phone;
					}
					return;
				}
			}
			if (item.ElementAt(0).Phone == null)
			{
				string source = driver.PageSource;
				Match k = Regex.Match(source, russianPhonePattern, RegexOptions.Multiline);
				foreach (Match m in Regex.Matches(source, russianPhonePattern, RegexOptions.Multiline))
				{
					if (m.Groups.Count != 3 || m.Groups[2].Value != "8")
					{
						continue;
					}

					string phone = m.Groups[0].Value;
					foreach (Website sub in item)
					{
						sub.Phone = phone;
					}
					return;
				}
			}
			if (visitContactsPage)
			{
				foreach (HtmlAgilityPack.HtmlNode suburl in nodes)
				{
					if (suburl.Attributes.Contains("href"))
					{
						string href = suburl.Attributes["href"].Value;
						if (href.Contains("contact"))
						{
							if (href.StartsWith("/"))
							{
								href = url + href;
							}

							FindPhoneOnWebpage(href, item, false);
							if (item.ElementAt(0).Phone != null)
							{
								return;
							}
						}
					}
				}
			}
		}

		private static OpenQA.Selenium.Chrome.ChromeDriver driver;
		public class YandexCategory
		{
			public string Name { get; set; }
			public List<WildberriesYandexItem> Items = new List<WildberriesYandexItem>();
			public YandexCategory(string name)
			{
				Name = name;
			}
			public YandexCategory()
			{

			}
		}
		public class WildberriesYandexItem
		{
			public string Brand { get; set; }
			public List<Website> Websites { get; set; } = new List<Website>();
			public string Category { get; set; }
		}
		private static readonly Dictionary<string, string> Categories = new Dictionary<string, string>()
		{
		  { "Пылесосы", "https://www.wildberries.ru/catalog/elektronika/tehnika-dlya-doma/pylesosy-i-parootchistiteli?xsubject=2791" },
		  { "Вытяжки","https://www.wildberries.ru/catalog/elektronika/tehnika-dlya-kuhni/vytyazhki-kuhonnye"},
		  {"Ноутбуки", "https://www.wildberries.ru/catalog/elektronika/noutbuki-pereferiya/noutbuki-ultrabuki" },
		  {"Водонагреватели","https://www.wildberries.ru/catalog/elektronika/tehnika-dlya-doma/vodonagrevateli?xsubject=1414" }
		};

		private static void ParseWebSites()
		{
			Console.OutputEncoding = Encoding.UTF8;

			ChromeDriverService service = ChromeDriverService.CreateDefaultService();
			service.EnableVerboseLogging = false;
			service.SuppressInitialDiagnosticInformation = true;
			ChromeOptions chromeOptions = new ChromeOptions();
			chromeOptions.AddArgument("--log-level=3");
			driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, chromeOptions);


			XmlSerializer serializer = new XmlSerializer(typeof(List<YandexCategory>));
			var yandexParsingCategories = (List<YandexCategory>)serializer.Deserialize(new FileStream(@"C:\Users\User\Desktop\file_last.xml", FileMode.Open));
			List<IGrouping<string, Website>> webSites = yandexParsingCategories.SelectMany(t => t.Items).SelectMany(z => z.Websites).Where(t => t.Phones.Count == 0).GroupBy(t => t.URL).ToList();


			Console.WriteLine("Websites count is " + webSites.Count);
			int success_count = 0;
			int checked_count = 0;
			foreach (IGrouping<string, Website> item in webSites)
			{
				try
				{
					FindPhoneOnWebpage("http://" + item.Key, item);

					if (item.ElementAt(0).Phone != null)
					{
						success_count++;
						Console.WriteLine("Found phone for website " + item.Key);
					}
					if (item.ElementAt(0).Phone == null)
					{
					}

				}
				catch (Exception)
				{
					Console.WriteLine("Error on website " + item.ElementAt(0).URL);
				}
				checked_count++;
				Console.WriteLine($"Checked {checked_count}/{webSites.Count}");
			}
			Console.WriteLine($"Success {success_count}/{webSites.Count}");
			Console.ReadLine();
		}
		private static void ParseBrands()
		{
			Console.OutputEncoding = Encoding.UTF8;
			XmlSerializer serializer = new XmlSerializer(typeof(List<WildBerriesItem>));
			List<WildBerriesItem> wildBerriesItems = (List<WildBerriesItem>)serializer.Deserialize(new FileStream("wildberries.xml", FileMode.Open));
			var results = from p in wildBerriesItems
						  group p by p.Brand into g
						  select new { Brand = g.Key, Items = g.ToList() };
			ChromeDriverService service = ChromeDriverService.CreateDefaultService();
			service.EnableVerboseLogging = false;
			service.SuppressInitialDiagnosticInformation = true;
			ChromeOptions chromeOptions = new ChromeOptions();
			chromeOptions.AddArgument("--log-level=3");
			OpenQA.Selenium.Chrome.ChromeDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, chromeOptions)
			{
				Url = "https://yandex.ru/search"
			};
			Thread.Sleep(1500);
			Console.Clear();
			Console.WriteLine($"Всего загружено {results.Count()} брендов");
			Console.WriteLine("Настройки поисковую систему и нажмите Enter");
			Console.ReadKey();
			int count = 0;
			string pattern = @":""([+\d \(\)-]*)""";
			RegexOptions options = RegexOptions.Multiline;
			foreach (var group in results)
			{
				try
				{
					string pagesource = driver.PageSource;
					while ((pagesource = driver.PageSource).Contains("Подтвердите, что запросы отправляли вы, а не робот"))
					{
						;
					}

					string vvv = (string)driver.ExecuteScript("return document.readyState");
					while ((vvv = (string)driver.ExecuteScript("return document.readyState")) != "complete")
					{
						;
					}

					Thread.Sleep(150);
					driver.FindElementByXPath("/html/body/header/div/div/div[3]/form/div[1]/span/span/input").Clear();
					driver.FindElementByXPath("/html/body/header/div/div/div[3]/form/div[1]/span/span/input").SendKeys("официальный представитель " + group.Brand);
					driver.FindElementByXPath("/html/body/header/div/div/div[3]/form/div[2]/button").Click();
					Thread.Sleep(500);
					HtmlAgilityPack.HtmlDocument doc = driver.GetHtmlDocument();
					List<HtmlAgilityPack.HtmlNode> nodes = doc.DocumentNode.GetAllNodes();
					nodes = nodes.GetElementByTagAndAttributeEquality("ul", "id", "search-result").GetAllNodes();
					int i = 0;
					foreach (HtmlAgilityPack.HtmlNode node in nodes)
					{
						if (node.Name == "li" && node.Attributes.FirstOrDefault(t => t.Name == "data-fast-name")?.Value != "videowiz" && node.Attributes.FirstOrDefault(t => t.Name == "data-fast-name")?.Value != "companies")
						{
							List<HtmlAgilityPack.HtmlNode> linodes = node.GetAllNodes();
							string url = linodes.GetElementByTagAndAttributeContain("div", "class", "path organic__path", "Path OrganicSubtitle-Path").GetAllNodes().GetElementByTag("b").InnerText;
							HtmlAgilityPack.HtmlNode phones = linodes.GetElementByTagAndAttributeContain("span", "class", "CoveredPhone", "covered-phone");
							Website wb = new Website();
							if (phones != null)
							{
								string phone = "";
								if (phones.Attributes.Contains("data-vnl"))
								{
									phone = HttpUtility.HtmlDecode(phones.Attributes["data-vnl"].Value);
									phone = Regex.Match(phone, pattern, options).Groups[1].Value;

								}
								else
								{
									phone = phones.Attributes["aria-label"].Value;
								}
								wb.Phone = phone;
							}
							try
							{
								UriBuilder b = new UriBuilder(url);
								bool skip = false;
								foreach (string exclucedLink in ExcludedWebsites)
								{
									if (b.Host == exclucedLink)
									{
										skip = true; break;
									}
								}
								if (skip)
								{
									continue;
								}

								wb.URL = b.Host;
							}
							catch
							{
								wb.URL = url;
							}
							group.Items[0].Websites.Add(wb);
							if (++i == 10)
							{
								break;
							}
						}
					}
					group.Items[0].Websites = group.Items[0].Websites.GroupBy(t => t.URL).Select(e => e.First()).ToList();
					foreach (WildBerriesItem ppp in group.Items)
					{
						ppp.Websites = group.Items[0].Websites;
					}
					Console.WriteLine($"Обработано {++count} брендов из {results.Count()}");
				}
				catch (Exception)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Ошибка" + group.Brand);
					Console.ForegroundColor = ConsoleColor.Gray;
					foreach (WildBerriesItem ppp in group.Items)
					{
						ppp.Websites = group.Items[0].Websites;
					}
					Console.WriteLine($"Обработано {++count} брендов из {results.Count()}");
				}

				Thread.Sleep(1500);
			}

			wildBerriesItems = wildBerriesItems.GroupBy(t => t.Brand).Select(e => e.FirstOrDefault()).ToList();
			serializer = new XmlSerializer(typeof(List<WildBerriesItem>));
			serializer.Serialize(new FileStream("wildberries+yandex.xml", FileMode.Create), wildBerriesItems);
			driver.Close();
			driver.Quit();

		}
		private static void ParseWildBerriesManyCategory()
		{
			Console.OutputEncoding = Encoding.UTF8;
			string pattern = @"""ogrn"":""(.+?)""";
			RegexOptions options = RegexOptions.Multiline;
			ChromeDriver driver = new ChromeDriver();
			foreach (KeyValuePair<string, string> startUrl in Categories)
			{
				List<WildBerriesItem> wildBerriesItems = new List<WildBerriesItem>();

				int pageIndex = 1;
				List<string> found = new List<string>();
				while (true)
				{
					if (startUrl.Value.Contains("?"))
					{
						driver.Url = startUrl.Value + "&page=" + pageIndex++;
					}
					else
					{
						driver.Url = startUrl.Value + "?page=" + pageIndex++;
					}
					if (driver.PageSource.Contains("К сожалению, именно такого товара сейчас нет. "))
					{
						break;
					}
					HtmlAgilityPack.HtmlDocument doc = driver.GetHtmlDocument();
					HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode(@"//*[@id=""catalog-content""]").ChildNodes[11];
					foreach (HtmlAgilityPack.HtmlNode item in node.ChildNodes)
					{

						if (item.Name == "div")
						{
							List<HtmlAgilityPack.HtmlNode> nodes = item.GetAllNodes();
							HtmlAgilityPack.HtmlNode a = nodes.GetElementByTag("a");
							found.Add("https://www.wildberries.ru" + a.Attributes["href"].Value);
						}
					}
				}
				foreach (string item in found)
				{
					driver.Url = item;
					if (driver.PageSource.Contains("По Вашему запросу ничего не найдено"))
					{
						continue;
					}

					Thread.Sleep(500);
					HtmlAgilityPack.HtmlDocument doc = driver.GetHtmlDocument();
					HtmlAgilityPack.HtmlNode title = doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeEquality("div", "class", "brand-and-name j-product-title");
					string brand = title.ChildNodes[1].InnerText.Trim();
					string name = title.ChildNodes[3].InnerText.Trim();
					string price = HttpUtility.HtmlDecode(doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeEquality("span", "class", "final-cost").InnerText.Trim());
					string seller = HttpUtility.HtmlDecode(doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeEquality("span", "class", "seller__text", 1).InnerText.Trim());
					string ogrn = Regex.Match(driver.PageSource, pattern, options).Groups[1].Value;
					wildBerriesItems.Add(new WildBerriesItem()
					{
						Brand = brand,
						Name = name,
						Price = price,
						Company = seller,
						OGRN = ogrn,
						URL = item
					});
					Console.WriteLine(title.InnerText.Trim() + "   " + ogrn);
					Console.WriteLine(item);
				}
				XmlSerializer serializer = new XmlSerializer(typeof(List<WildBerriesItem>));
				serializer.Serialize(new FileStream($"wildberries_{startUrl.Key}.xml", FileMode.Create), wildBerriesItems);

			}
			driver.Close();
			driver.Quit();
		}
		private static void ParseMatchedBrands()
		{
			Console.OutputEncoding = Encoding.UTF8;
			XmlSerializer serializer = new XmlSerializer(typeof(Category));
			Category categorieswildBerriesItems1 = (Category)serializer.Deserialize(new FileStream(@"C:\Users\User\source\repos\Wildberries Scrapper\Wildberries WScrapper\bin\Debug\Видеонаблюдение.xml", FileMode.Open));
			Category categorieswildBerriesItems2 = (Category)serializer.Deserialize(new FileStream(@"C:\Users\User\source\repos\Wildberries Scrapper\Wildberries WScrapper\bin\Debug\Обогреватели.xml", FileMode.Open));
			Category categorieswildBerriesItems3 = (Category)serializer.Deserialize(new FileStream(@"C:\Users\User\source\repos\Wildberries Scrapper\Wildberries WScrapper\bin\Debug\Парогенераторы.xml", FileMode.Open));
			Category categorieswildBerriesItems4 = (Category)serializer.Deserialize(new FileStream(@"C:\Users\User\source\repos\Wildberries Scrapper\Wildberries WScrapper\bin\Debug\Пароочистители.xml", FileMode.Open));
			Category categorieswildBerriesItems5 = (Category)serializer.Deserialize(new FileStream(@"C:\Users\User\source\repos\Wildberries Scrapper\Wildberries WScrapper\bin\Debug\Электротранспорт.xml", FileMode.Open));

			categorieswildBerriesItems1.Items.RemoveAll(t => string.IsNullOrEmpty(t.OGRN));
			categorieswildBerriesItems2.Items.RemoveAll(t => string.IsNullOrEmpty(t.OGRN));
			categorieswildBerriesItems3.Items.RemoveAll(t => string.IsNullOrEmpty(t.OGRN));
			categorieswildBerriesItems4.Items.RemoveAll(t => string.IsNullOrEmpty(t.OGRN));
			categorieswildBerriesItems5.Items.RemoveAll(t => string.IsNullOrEmpty(t.OGRN));


			categorieswildBerriesItems1.Items.ForEach((f) => { f.Category = categorieswildBerriesItems1.Name; });
			categorieswildBerriesItems2.Items.ForEach((f) => { f.Category = categorieswildBerriesItems2.Name; });
			categorieswildBerriesItems3.Items.ForEach((f) => { f.Category = categorieswildBerriesItems3.Name; });
			categorieswildBerriesItems4.Items.ForEach((f) => { f.Category = categorieswildBerriesItems4.Name; });
			categorieswildBerriesItems5.Items.ForEach((f) => { f.Category = categorieswildBerriesItems5.Name; });


			var wildBerriesItems1 = categorieswildBerriesItems1.Items.GroupBy(t => t.OGRN).Select(e => e.First()).ToList();
			var wildBerriesItems2 = categorieswildBerriesItems2.Items.GroupBy(t => t.OGRN).Select(e => e.First()).ToList();
			var wildBerriesItems3 = categorieswildBerriesItems3.Items.GroupBy(t => t.OGRN).Select(e => e.First()).ToList();
			var wildBerriesItems4 = categorieswildBerriesItems4.Items.GroupBy(t => t.OGRN).Select(e => e.First()).ToList();
			var wildBerriesItems5 = categorieswildBerriesItems5.Items.GroupBy(t => t.OGRN).Select(e => e.First()).ToList();

			var joined = categorieswildBerriesItems1.Items.Union(categorieswildBerriesItems2.Items).ToList();
			joined = joined.Union(categorieswildBerriesItems3.Items).ToList();
			joined = joined.Union(categorieswildBerriesItems4.Items).ToList();
			joined = joined.Union(categorieswildBerriesItems5.Items).ToList();
			var ogrnSort = joined.GroupBy(t => t.OGRN).ToList();

			//List<string> inseresect = wildBerriesItems1.Select(e => e.OGRN).Intersect(wildBerriesItems2.Select(f => f.OGRN)).ToList();
			//inseresect = inseresect.Intersect(wildBerriesItems3.Select(f => f.OGRN)).ToList();
			//inseresect = inseresect.Intersect(wildBerriesItems4.Select(f => f.OGRN)).ToList();
			//inseresect = inseresect.Intersect(wildBerriesItems5.Select(f => f.OGRN)).ToList();

			List<WildBerriesItem> union = wildBerriesItems1.Union(wildBerriesItems2).Union(wildBerriesItems3).Union(wildBerriesItems4).Union(wildBerriesItems5).ToList();
			//
			List<IGrouping<string, WildBerriesItem>> brand = union.GroupBy(t => t.OGRN).ToList();


			List<WildBerriesTotalItem> result = new List<WildBerriesTotalItem>();
			foreach (IGrouping<string, WildBerriesItem> item in ogrnSort)
			{
				Console.Write($"Компания с ОГРН {item.ElementAt(0).OGRN} и названием {item.ElementAt(0).Company} продает товары следующих категорий: ");
				Console.SetCursorPosition(150, Console.CursorTop);
				Console.WriteLine(string.Join(", ", item.GroupBy(t => t.Category).Select(e => e.Key)));

				WildBerriesTotalItem wb = new WildBerriesTotalItem();
				wb.OGRN = item.ElementAt(0).OGRN;
				wb.Company = item.ElementAt(0).Company;
				wb.CategoryList = string.Join(", ", item.GroupBy(t => t.Category).Select(e => e.Key));
				wb.BrandList = string.Join(", ", item.GroupBy(t => t.Brand).Select(e => e.Key));
				wb.YandexUrl1 = $"https://yandex.ru/search/?lr=10277&text={wb.Company} {item.ElementAt(0).Brand}";
				wb.YandexUrl2 = $"https://yandex.ru/search/?lr=10277&text={wb.Company} {item.ElementAt(0).Brand} {wb.OGRN}";
				result.Add(wb);
			}
			XmlSerializer serializer1 = new XmlSerializer(typeof(List<WildBerriesTotalItem>));

			serializer1.Serialize(new FileStream(@"C:\Users\User\source\repos\Wildberries Scrapper\Wildberries WScrapper\bin\Debug\joined.xml", FileMode.Create), result);
			Console.Read();
		}

		private static void ParseYandexMarketCatalog(string url)
		{
			Console.OutputEncoding = Encoding.UTF8;
			RegexOptions options = RegexOptions.Multiline;
			driver = new ChromeDriver();
			driver.Url = url;

			var pagesource = driver.PageSource;
			while ((pagesource = driver.PageSource).Contains(
					   "Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" ||
				   pagesource.Contains("captchaKey"))
			{
				Thread.Sleep(500);
			}

			var document = driver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("button", "class", "_1KpjX8xME8");
			driver.FindElementByXPath(document.XPath).Click();
			while (driver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5ropeY89h")==null)
			{
				
			}
			Thread.Sleep(1000);
			List<string> companies = new List<string>();
			int scrollOffset = 1;
			int maxHeight =Convert.ToInt32( driver.ExecuteScript($"return document.getElementsByClassName('_5ropeY89h')[0].scrollHeight;"));
			while (true)
			{
				var ul = driver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5ropeY89h");
				var all = ul.GetAllNodes();
				List<HtmlNode> inputs = new List<HtmlNode>();
				foreach (var item in all)
				{
					if (item.Name == "input")
					{
						inputs.Add(item);
						companies.Add(item.Attributes["id"].Value.Substring(5));
					}
				}

				if ((scrollOffset -1) * 500 > maxHeight)
					break;
				driver.ExecuteScript($"document.getElementsByClassName('_5ropeY89h')[0].scrollTop = {scrollOffset++ * 500};");
				Thread.Sleep(500);

			}

			companies = companies.Distinct().ToList();
			foreach (var company in companies)
			{
				driver.Url = $"https://market.yandex.ru/shop--and-systems-ru/{company}/reviews";
				pagesource = driver.PageSource;
				while ((pagesource = driver.PageSource).Contains(
					       "Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" ||
				       pagesource.Contains("captchaKey"))
				{
					Thread.Sleep(500);
					Console.Beep();
				}

				var document1 = driver.GetHtmlDocument();
				Console.WriteLine(companies.IndexOf(company));
				var nodeList = document1.DocumentNode.GetAllNodes();
				var appearNode = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2lRN4IzLYH")
					.ChildNodes[0].InnerText;
				var rating = nodeList.GetElementByTagAndAttributeContain("div", "class", "yzkuM642Et").InnerText;
				var marks = nodeList.GetElementsByTagAndAttributeContain("div", "class", "_2v0orscee-")
					.Select(t => t.InnerText).ToList();
				var ogrn = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2F_FVZVxQo").ChildNodes
					.Last().InnerText;
				var seller = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2186g3xAqi")?.InnerText;
				var website = nodeList.GetElementByTagAndAttributeContain("a", "class", "_21WngnAbDC")?.InnerText;
			}
		}
		private static void Main()
		{
			AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
			ParseYandexMarketCatalog("https://market.yandex.ru/catalog--noutbuki/54544/list?cpa=0&hid=91013&how=aprice&grhow=shop&suggest_text=%D0%9D%D0%BE%D1%83%D1%82%D0%B1%D1%83%D0%BA%D0%B8&onstock=1&local-offers-first=0");
			Console.ReadKey();
		}

		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			driver.Close();
		}
	}
}
