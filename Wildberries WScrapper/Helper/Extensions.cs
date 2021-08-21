using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Gecko;
using HtmlAgilityPack;

namespace Wildberries_WScrapper.Helper
{
	public static class Extenstions
	{


		public static HtmlAgilityPack.HtmlDocument GetHtmlDocument(this Gecko.GeckoWebBrowser geckoWebBrowser)
		{

			var html = (geckoWebBrowser.DomDocument.DocumentElement as GeckoHtmlElement).OuterHtml;
			var doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(html);
			return doc;
		}
		public static List<List<T>> SplitOnParts<T>(this List<T> list, int partsCount)
		{
			if (list.Count < partsCount)
				return new List<List<T>>() { list };
			int countper = list.Count / partsCount;
			List<List<T>> parts = new List<List<T>>();
			for (int i = 0; i < partsCount; i++)
			{
				List<T> part = list.Skip(i * countper).Take(countper).ToList();
				parts.Add(part);
			}
			if (countper * partsCount < list.Count)
			{
				parts.Last().AddRange(list.Skip(countper * partsCount));
			}
			return parts;
		}
		public static string GetPageSource(this Gecko.GeckoWebBrowser geckoWebBrowser)
		{

			try
			{
				var html = (geckoWebBrowser.DomDocument.DocumentElement as GeckoHtmlElement).OuterHtml;
				return html;
			}
			catch (Exception e)
			{
				return "";
			}

		}
		public static List<HtmlAgilityPack.HtmlNode> GetAllNodes(this HtmlAgilityPack.HtmlDocument document)
		{
			return GetAllNodes(document.DocumentNode);

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
				var currentNode = currentNodes.Pop();
				foreach (var item in currentNode.ChildNodes)
				{
					currentNodes.Push(item);
					allNodes.Add(item);
				}
			}
			return allNodes;

		}
		public static HtmlAgilityPack.HtmlDocument GetHtmlDocument(this OpenQA.Selenium.Chrome.ChromeDriver driver)
		{
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(driver.PageSource);
			return doc;
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
			foreach (var item in nodes)
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
			foreach (var item in nodes)
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
			foreach (var item in nodes)
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
			foreach (var item in nodes)
			{
				if (item.Name == tag)
				{
					if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name)?.Value != null)
					{
						foreach (var it in attribute_possible_value)
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
								return item;
							}
						}

					}



				}
			}
			return null;
		}
		/// <summary>
		/// Looks for tag in elements list and return first element which inner text contains specified value
		/// </summary>
		/// <param name="nodes">List of Html Nodes</param>
		/// <param name="tag">Tag name</param>
		/// <returns></returns>
		public static HtmlAgilityPack.HtmlNode GetElementByTagAndAInnetText(this List<HtmlAgilityPack.HtmlNode> nodes, string tag, string attribute_possible_value)
		{
			foreach (var item in nodes)
			{
				if (item.Name == tag)
				{
					if (item.InnerText.ToLower().Contains(attribute_possible_value.ToLower()))
					{
						return item;
					}
				}
			}
			return null;
		}
		/// <summary>
		/// Looks for tag in elements list and return first element which inner text contains specified value
		/// </summary>
		/// <param name="nodes">List of Html Nodes</param>
		/// <param name="tag">Tag name</param>
		/// <returns></returns>
		public static HtmlAgilityPack.HtmlNode GetElementByTagAndAInnetTextAndAttribute(this List<HtmlAgilityPack.HtmlNode> nodes, string tag, string attribute_name,string attribute_value, string attribute_possible_value)
		{
			foreach (var item in nodes)
			{
				if (item.Name == tag)
				{
					if (item.Attributes.FirstOrDefault(t => t.Name == attribute_name)?.Value != null)
					{
						if (item.Attributes[attribute_name].Value == attribute_value)
						{
							if (item.InnerText.ToLower().Contains(attribute_possible_value))
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
	}

}
