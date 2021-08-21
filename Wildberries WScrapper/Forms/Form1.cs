using _2CaptchaSolver;
using _2CaptchaSolver.Captcha;
using FluentFTP;
using Gecko;
using Gecko.DOM;
using HtmlAgilityPack;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Serialization;
using Wildberries_WScrapper.Controls;
using Wildberries_WScrapper.Helper;
using Wildberries_WScrapper.Model;
using Wildberries_WScrapper.Model.WildBerries;
using Wildberries_WScrapper.Model.YandexMarket;
using Wildberries_WScrapper.Properties;
using xNet;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using HttpRequest = xNet.HttpRequest;

namespace Wildberries_WScrapper.Forms
{
    public partial class Form1 : Form
    {
        private const string SpanPhonePattern = @"<[^>]*>((\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2})<\/[^>]*>";

        private const string RussianPhonePattern = @"(?<!\d)((8|\+7)-?)?\(?\d{3}\)?-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}(?!\d)";

        private readonly List<WildberriesCategory> _categories = new List<WildberriesCategory>();
        private readonly string _pattern = @"""ogrn"":""(\d+?)""";
        private readonly List<YandexMarketCategory> _yandexMarketCategories = new List<YandexMarketCategory>();

        private readonly string[] ExcludedWebsites = { "facebook.com", "vk.com", "instagram.com", "youtube.com", "aliexpress.ru", "shop-lot.ru", "qiosk.ru", "shopliga.ru", "ozon.ru", "Яндекс.Картинки", "merlion.com", "fbq.ru", "ilight.aliexpress.ru", "wildberries.ru", "b2b-postavki.ru", "eggheads.solutions", "adata.kz", "genshin.mihoyo.com", "zen.yandex.ru", "totu.aliexpress.ru", "luxcase.ru", "myluxcase.ru", "pokupki.market.yandex.ru", "onlinetrade.ru", "market.yandex.ru", "rusactions.ru", "rusprofile.ru", "mtplatform.com", "spark-interfax.ru", "financeotzyvy.com", "zachestnyibiznes.ru", "moskva.regtorg.ru", "2gis.ru", "otzyvdengi.com", "breaking-group.ru", "bearking.aliexpress.ru", "breakingclub.ru", "statpad.ru", "yandex.ru", "moskva.spravmer.ru", "moskva.yacatalog.com", "goodchoice.ltd", "spravkainform.ru", "zoon.ru" };

        private int _count;
        private GeckoBrowserEx _geckoWebBrowser;
        private bool _loadedLocal;
        private int loadedCounter;

        private bool Loaded
        {
            get
            {
                this.loadedCounter++;
                if (this.loadedCounter > 50)
                {
                    var lastUrl = this._geckoWebBrowser.LastUrl;
                    this.InitBrowser();
                    this._geckoWebBrowser.Navigate(lastUrl);
                    this.loadedCounter = 0;
                }
                return this._loadedLocal;
            }
            set
            {
                this.loadedCounter = 0;
                this._loadedLocal = value;
            }

        }
        private bool _settingsAccepted;
        private TwoCaptcha _solver;
        private int _successCount;
        private int _totalCount;
        private List<YandexCategory> _yandexParsingCategories = new List<YandexCategory>();

        private readonly object ob = new object();

        private ChromeDriver yandexMarketChromeDriver;
        /// <summary>
        /// Queue for web site contacts parallel parsing while working with Yandex.Market
        /// </summary>
        private ConcurrentQueue<YandexMarketQueueItem> yandexMarketWebsitesQueue = new ConcurrentQueue<YandexMarketQueueItem>();

        private bool YandexMarketParsingIsRunning { get; set; } = false;

        public Form1()
        {
            this.InitializeComponent();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Xpcom.Initialize("Firefox");
            this.InitBrowser();
            this.apiKeyTextBox.Text = Settings.Default.ApiKey;
            this._solver = new TwoCaptcha(this.apiKeyTextBox.Text);
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Excluded.txt"))
            {
                this.ExcludedWebsites = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Excluded.txt").ToArray();
            }

#if DEBUG
            _yandexMarketCategories.Add(new YandexMarketCategory("https://market.yandex.ru/catalog--molochnye-smesi/65502/list?cpa=1&onstock=1&hid=13360751&local-offers-first=0&viewtype=list", "Смеси", 1));
#endif
        }

        private async void startWildberriesParsing_Click(object sender, EventArgs e)
        {
            for (var i = this.wildberriesCategoriesTabControl.TabPages.Count - 1; i > 0; i--)
            {
                if (this.wildberriesCategoriesTabControl.TabPages[i].Text != "Действия")
                {
                    this.wildberriesCategoriesTabControl.TabPages.RemoveAt(i);
                }
            }

            this._categories.ForEach(t => t.Items.Clear());
            foreach (var item in this._categories)
            {
                await this.ParseWildBerriesSingleCategory(item);
            }
        }

        private async Task ParseWildBerriesSingleCategory(WildberriesCategory category)
        {
            var pageIndex = 1;
            var goodCount = 0;
            var parsed = 0;
            var first = true;
            var found = new List<string>();
            var startUrl = category.URL;
            var datagridview = new DataGridViewWB();
            this.wildberriesCategoriesTabControl.TabPages.Insert(this.wildberriesCategoriesTabControl.TabCount - 1, category.Name);
            var page = this.wildberriesCategoriesTabControl.TabPages[this.wildberriesCategoriesTabControl.TabPages.Count - 2];
            page.Controls.Add(datagridview);
            while (true)
            {
                if (startUrl.Contains("?"))
                {
                    this._geckoWebBrowser.Navigate(startUrl + "&page=" + pageIndex++);
                }
                else
                {
                    this._geckoWebBrowser.Navigate(startUrl + "?page=" + pageIndex++);
                }

                this.Loaded = false;
                while (!this.Loaded)
                {
                    await Task.Delay(500);
                }

                var outerHtml = (this._geckoWebBrowser.DomDocument.DocumentElement as GeckoHtmlElement)?.OuterHtml;
                if (outerHtml != null && (outerHtml.Contains("К сожалению, именно такого товара сейчас нет.") || outerHtml.Contains("По Вашему запросу ничего не найдено.")))
                {
                    break;
                }

                var doc = this._geckoWebBrowser.GetHtmlDocument();
                if (first)
                {
                    var goodsRegex = @"(\d*)";
                    var goodCountNode = doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("span", "class", "goods-count j-goods-count").InnerText.Trim();
                    var goodCountString = "";
                    foreach (Match m in Regex.Matches(goodCountNode, goodsRegex, RegexOptions.Multiline))
                    {
                        goodCountString += m.Value;
                    }

                    goodCount = Int32.Parse(goodCountString);
                    first = false;
                }

                var node = doc.DocumentNode.SelectSingleNode(@"//*[@id=""catalog-content""]").GetAllNodes().GetElementByTagAndAttributeContain("div", "class", "catalog_main_table j-products-container");
                foreach (var item in node.ChildNodes)
                {
                    if (item.Name == "div")
                    {
                        var nodes = item.GetAllNodes();
                        var a = nodes.GetElementByTag("a");
                        found.Add("https://www.wildberries.ru" + a.Attributes["href"].Value);
                    }
                }

                if (found.Count >= goodCount)
                {
                    break;
                }
            }

            foreach (var item in found)
            {
                var parseError = false;
                var wild = new WildBerriesItem
                {
                    URL = item,
                    Category = category.Name
                };
                try
                {
                    this.Loaded = false;
                    this._geckoWebBrowser.Navigate(item);
                    while (!this.Loaded)
                    {
                        await Task.Delay(500);
                    }

                    await Task.Delay(500);
                    var doc = this._geckoWebBrowser.GetHtmlDocument();
                    var title = doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeEquality("div", "class", "brand-and-name j-product-title");
                    wild.Brand = title.ChildNodes[1].InnerText.Trim();
                    wild.Name = title.ChildNodes[3].InnerText.Trim();
                    wild.Price = HttpUtility.HtmlDecode(doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeEquality("span", "class", "final-cost").InnerText.Trim());
                    wild.Company = HttpUtility.HtmlDecode(doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeEquality("span", "class", "seller__text", 1).InnerText.Trim());
                    wild.OGRN = Regex.Match(this._geckoWebBrowser.GetPageSource(), this._pattern, RegexOptions.Multiline).Groups[1].Value;
                }
                catch
                {
                    parseError = true;
                }

                category.Items.Add(wild);
                var it = category.Items.Last();
                datagridview.Rows.Add(++parsed, it.Name, it.Price, it.Brand, it.Company, it.OGRN, item);
                if (parseError)
                {
                    var row = datagridview.Rows[datagridview.Rows.GetLastRow(DataGridViewElementStates.None)];
                    row.DefaultCellStyle.BackColor = Color.Pink;
                }

                this.wildberriesCategoriesTabControl.TabPages[this.wildberriesCategoriesTabControl.TabPages.Count - 2].Text = $"{category.Name} {parsed}/{goodCount}";
            }

            var xmlSerializer = new XmlSerializer(typeof(WildberriesCategory));
            xmlSerializer.Serialize(new FileStream($"{category.Name}.xml", FileMode.Create), category);
        }

        private void editCategories_Click(object sender, EventArgs e)
        {
            new EditCategories(this._categories).ShowDialog(this);
            this.label2.Text = $"Количество категорий: {this._categories.Count}";
        }

        private void saveCompanies_Click(object sender, EventArgs tz)
        {
            var ofd = new SaveFileDialog { FileName = "file", Filter = "XML Files|*.xml" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var totalList = new List<WildBerriesItem>();
            foreach (var item in this._categories)
            {
                totalList = totalList.Union(item.Items).ToList();
            }

            var ogrnSort = totalList.GroupBy(t => t.OGRN).ToList();

            var result = new List<WildBerriesTotalItem>();
            foreach (var item in ogrnSort)
            {
                var wb = new WildBerriesTotalItem
                {
                    OGRN = item.ElementAt(0).OGRN,
                    Company = item.ElementAt(0).Company,
                    CategoryList = String.Join(", ", item.GroupBy(t => t.Category).Select(e => e.Key)),
                    BrandList = String.Join(", ", item.GroupBy(t => t.Brand).Select(e => e.Key))
                };
                wb.YandexUrl1 = $"https://yandex.ru/search/?lr=10277&text={wb.Company} {item.ElementAt(0).Brand}";
                wb.YandexUrl2 = $"https://yandex.ru/search/?lr=10277&text={wb.Company} {item.ElementAt(0).Brand} {wb.OGRN}";
                result.Add(wb);
            }

            var serializer = new XmlSerializer(typeof(List<WildBerriesTotalItem>));
            var fs = new FileStream(ofd.FileName, FileMode.Create);
            serializer.Serialize(fs, result);
            fs.Close();
            MessageBox.Show("Сохранено");
        }

        private void SaveProducts_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(WildberriesCategory));

            foreach (var item in this._categories)
            {
                var fs = new FileStream($"{dialog.SelectedPath}\\{item.Name}.xml", FileMode.Create);
                xmlSerializer.Serialize(fs, item);
                fs.Dispose();
            }

            MessageBox.Show("Сохранено");
        }

        //Проверить count () в цикле
        private async void saveCompaniesList_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog { FileName = "file", Filter = "Excel Files|*.xlsx" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            List<string> parsedBrands = null;
            try
            {
                using (var fs1 = File.Open("AllParsedBrands.xml", FileMode.OpenOrCreate))
                {
                    parsedBrands = (List<string>)new XmlSerializer(typeof(List<string>)).Deserialize(fs1);
                }
            }
            catch
            {
                parsedBrands = new List<string>();
            }

            var book = new XSSFWorkbook();
            var sheet = book.CreateSheet();
            var headerRow = sheet.CreateRow(0);
            for (var i = 0; i < this._categories.Count; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(this._categories[i].Name);
                var grouped = this._categories[i].Items.GroupBy(t => t.Brand).Select(f => f.First()).ToList();
                for (var z = 0; z < grouped.Count; z++)
                {
                    if (parsedBrands.Contains(grouped[z].Brand))
                    {
                        continue;
                    }

                    if (z + 1 >= sheet.PhysicalNumberOfRows)
                    {
                        sheet.CreateRow(z + 1);
                    }

                    var row = sheet.GetRow(z + 1);
                    cell = row.CreateCell(i);
                    cell.SetCellValue(grouped[z].Brand);
                }
            }

            using (var fs = new FileStream(ofd.FileName, FileMode.Create))
            {
                book.Write(fs);
            }

            parsedBrands = parsedBrands.Union(this._categories.SelectMany(t => t.Items.Select(a => a.Brand)).GroupBy(t => t).Select(t => t.First())).ToList();
            using (var fs1 = File.Open("AllParsedBrands.xml", FileMode.Create))
            {
                new XmlSerializer(typeof(List<string>)).Serialize(fs1, parsedBrands);
            }

            try
            {
                using (var client = new FtpClient("vh415782.eurodir.ru", "vh415782_untouchable", "7T6z9B4l"))
                {
                    await client.ConnectAsync();
                    await client.SetWorkingDirectoryAsync("/www/vh415782.eurodir.ru/wildberries/brands");
                    await client.UploadAsync(File.ReadAllBytes(Application.StartupPath + "\\" + "AllParsedBrands.xml"), "/www/vh415782.eurodir.ru/wildberries/brands/" + DateTime.Now.Ticks + ".xml");
                }
            }
            catch
            {
                MessageBox.Show("Бренды не удалось загрузить на FTP", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCategories_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { FileName = "file", Filter = "XML Files|*.xml", Multiselect = true };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(WildberriesCategory));
            foreach (var item in ofd.FileNames)
            {
                var ct = new WildberriesCategory();
                var fs = new FileStream(item, FileMode.Open);
                ct = (WildberriesCategory)xmlSerializer.Deserialize(fs);
                fs.Dispose();

                this._categories.Add(ct);
            }
        }

        private void saveCategoriesInOneFile_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog { FileName = "file", Filter = "XML Files|*.xml" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(List<WildberriesCategory>));
            var fs = new FileStream(ofd.FileName, FileMode.Create);
            xmlSerializer.Serialize(fs, this._categories);
            fs.Close();
        }

        private void loadBrands_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { FileName = "Excel", Filter = "xlsx File|*.xlsx" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var book = new XSSFWorkbook(new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var first = true;
            foreach (IRow item in book.GetSheetAt(0))
            {
                if (first)
                {
                    foreach (var cell in item.Cells)
                    {
                        this._yandexParsingCategories.Add(new YandexCategory(cell.StringCellValue));
                    }

                    first = false;
                }
                else
                {
                    foreach (var cell in item)
                    {
                        this._yandexParsingCategories[cell.ColumnIndex].Items.Add(new WildberriesYandexItem { Brand = cell.StringCellValue, Category = this._yandexParsingCategories[cell.ColumnIndex].Name });
                    }
                }
            }

            this.label1.Text = $"Загружено {this._yandexParsingCategories.Count} категорий";
        }

        private void InitBrowser()
        {
            if (this._geckoWebBrowser != null)
            {
                this._geckoWebBrowser.Stop();

                this.panel1.Controls.Remove(this._geckoWebBrowser);
                this._geckoWebBrowser.Dispose();
            }

            this._geckoWebBrowser = new GeckoBrowserEx();
            this._geckoWebBrowser.PageLoaded += this.PageLoaded;
            this._geckoWebBrowser.RequestLimitReached += this._geckoWebBrowser_RequestLimitReached;
            this.panel1.Controls.Add(this._geckoWebBrowser);
        }

        private async void _geckoWebBrowser_RequestLimitReached(string obj)
        {
            this.InitBrowser();
            this.Loaded = false;
            this._geckoWebBrowser.Navigate(obj);
            while (!this.Loaded)
            {
                await Task.Delay(500);
            }
        }

        private void PageLoaded()
        {
            this.Loaded = true;
        }

        private async void startYandexParsing_Click(object sender, EventArgs eee)
        {
            var results = this._yandexParsingCategories.SelectMany(t => t.Items).GroupBy(e => e.Brand).Select((t, y) => new { Brand = t.Key, Items = t.ToList() });
            var searchPattern = this.textBox1.Text;
            this._geckoWebBrowser.Navigate("https://yandex.ru/search");
            while (!this.Loaded)
            {
                await Task.Delay(500);
            }

            this.acceptSettings.Visible = true;
            while (!this._settingsAccepted)
            {
                await Task.Delay(500);
            }

            this.acceptSettings.Visible = false;
            var count = 0;
            var pattern = @":""([+\d \(\)-]*)""";
            var options = RegexOptions.Multiline;
            var totalCount = results.Count();
            foreach (var group in results)
            {
                try
                {
                    var pagesource = this._geckoWebBrowser.GetPageSource();
                    while ((pagesource = this._geckoWebBrowser.GetPageSource()).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
                    {
                        await Task.Delay(500);
                    }

                    while (!this.Loaded)
                    {
                        await Task.Delay(500);
                    }

                    var eval = this._geckoWebBrowser.Document.EvaluateXPath("/html/body/header/div/div/div[3]/form/div[1]/span/span/input").GetNodes().ToList();
                    (eval[0] as GeckoInputElement).Value = searchPattern.Replace("{0}", group.Brand).Replace("{1}", group.Items.FirstOrDefault().Category);

                    eval = this._geckoWebBrowser.Document.EvaluateXPath("/html/body/header/div/div/div[3]/form/div[2]/button").GetNodes().ToList();
                    this.Loaded = false;
                    (eval[0] as GeckoHtmlElement).Click();
                    var tcount = 0;
                    while (!this.Loaded)
                    {
                        await Task.Delay(500);
                        if (tcount++ == 10)
                        {
                            this.Loaded = true;
                            break;
                        }
                    }

                    var hascaptcha = false;
                    while ((pagesource = this._geckoWebBrowser.GetPageSource()).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
                    {
                        await Task.Delay(500);
                        hascaptcha = true;
                        this.Loaded = false;
                    }

                    if (hascaptcha)
                    {
                        while (!this.Loaded)
                        {
                            await Task.Delay(500);
                        }
                    }

                    var doc = this._geckoWebBrowser.GetHtmlDocument();
                    var nodes = doc.DocumentNode.GetAllNodes();
                    nodes = nodes.GetElementByTagAndAttributeEquality("ul", "id", "search-result").GetAllNodes();
                    var i = 0;
                    var memoryService = Xpcom.GetService<nsIMemory>("@mozilla.org/xpcom/memory-service;1");
                    memoryService.HeapMinimize(false);
                    foreach (var node in nodes)
                    {
                        try
                        {
                            if (node.Name == "li" && node.Attributes.FirstOrDefault(t => t.Name == "data-fast-name")?.Value != "videowiz" && node.Attributes.FirstOrDefault(t => t.Name == "data-fast-name")?.Value != "companies")
                            {
                                var linodes = node.GetAllNodes();
                                var url = linodes.GetElementByTagAndAttributeContain("div", "class", "path organic__path", "Path OrganicSubtitle-Path").GetAllNodes().GetElementByTag("b").InnerText;
                                var phones = linodes.GetElementByTagAndAttributeContain("span", "class", "CoveredPhone", "covered-phone");
                                var wb = new Website();
                                if (phones != null)
                                {
                                    var phone = "";
                                    if (phones.Attributes.Contains("data-vnl"))
                                    {
                                        phone = HttpUtility.HtmlDecode(phones.Attributes["data-vnl"].Value);
                                        phone = Regex.Match(phone, pattern, options).Groups[1].Value;
                                    }
                                    else
                                    {
                                        phone = phones.Attributes["aria-label"].Value;
                                    }

                                    wb.Phones.Add(phone);
                                }

                                try
                                {
                                    var b = new UriBuilder(url);
                                    var skip = false;
                                    foreach (var exclucedLink in this.ExcludedWebsites)
                                    {
                                        if (b.Host == exclucedLink)
                                        {
                                            skip = true;
                                            break;
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
                        catch
                        {
                        }
                    }

                    group.Items[0].Websites = group.Items[0].Websites.GroupBy(t => t.URL).Select(e => e.First()).ToList();
                    foreach (var ppp in group.Items)
                    {
                        ppp.Websites = group.Items[0].Websites;
                    }
                }
                catch (Exception)
                {
                    foreach (var ppp in group.Items)
                    {
                        ppp.Websites = group.Items[0].Websites;
                    }
                }

                var branditem = this.phonesTreeView.Nodes.Add(group.Brand);
                foreach (var st in group.Items[0].Websites)
                {
                    var sub = branditem.Nodes.Add(st.URL);
                    foreach (var pho in st.Phones)
                    {
                        sub.Nodes.Add(pho);
                    }
                }

                this.label3.Text = $"Спарсено {++count}/{totalCount} брендов";
                await Task.Delay(1000);
                if (count % 50 == 0)
                {
                    this.InitBrowser();
                    this.Loaded = false;
                    this._geckoWebBrowser.Navigate("https://yandex.ru/search");

                    while (!this.Loaded)
                    {
                        await Task.Delay(500);
                    }
                }
            }
        }

        private void acceptSettings_Click(object sender, EventArgs e)
        {
            this._settingsAccepted = true;
        }

        private async Task FindPhoneOnWebpage(string url, Website item, bool visitContactsPage = true)
        {
            var sw1 = new Stopwatch();
            sw1.Start();
            var source = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36");
                client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7,ka;q=0.6");
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                source = await client.GetStringAsync(url);
            }

            if (visitContactsPage)
            {
                //item.MainSource = source;
            }
            else
            {
                //item.ContactSource = source;
            }

            var document = new HtmlDocument();
            document.LoadHtml(source);
            var nodes = document.DocumentNode.GetAllNodes().GetElementsByTag("a");
            foreach (var a in nodes)
            {
                if (a.Attributes.FirstOrDefault(t => t.Name == "href") != null)
                {
                    var href = a.Attributes["href"].Value;
                    if (href.StartsWith("tel:"))
                    {
                        var phone = href.Substring(4);
                        item.Phones.Add(phone);
                    }
                }
            }


            var replaced = source.Replace("\n", "");
            foreach (Match m in Regex.Matches(replaced, SpanPhonePattern, RegexOptions.Multiline))
            {
                var phone = m.Groups[1].Value;
                item.Phones.Add(phone);
            }

            foreach (Match m in Regex.Matches(source, RussianPhonePattern, RegexOptions.Multiline))
            {
                if (m.Groups.Count != 3 || m.Groups[2].Value != "8")
                {
                    continue;
                }

                var phone = m.Groups[0].Value;
                item.Phones.Add(phone);
            }

            if (visitContactsPage)
            {
                var visited = new List<string>();
                foreach (var suburl in nodes)
                {
                    if (suburl.Attributes.Contains("href"))
                    {
                        var href = suburl.Attributes["href"].Value;
                        if (href.Contains("contact"))
                        {
                            if (href.StartsWith("/"))
                            {
                                href = url + href;
                            }
                            else if (!href.Contains("/"))
                            {
                                href = url + "/" + href;
                            }

                            if (visited.Contains(href))
                            {
                                continue;
                            }

                            visited.Add(href);
                            await this.FindPhoneOnWebpage(href, item, false);
                        }
                    }
                }
            }

            sw1.Stop();
            this.WriteLog($"GetSource: {url} took {sw1.Elapsed.TotalMilliseconds} milliseconds");
        }

        private void saveBrandsWebsites_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog { FileName = "file", Filter = "XML Files|*.xml" };
            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }


            var xmlSerializer = new XmlSerializer(typeof(List<YandexCategory>));
            var fs = new FileStream(ofd.FileName, FileMode.Create);
            xmlSerializer.Serialize(fs, this._yandexParsingCategories);
            fs.Close();
            this._yandexParsingCategories.Clear();
        }

        private void loadYandexWithSites_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { FileName = "XML", Filter = "xml File|*.xml" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(List<YandexCategory>));
            var fs = new FileStream(ofd.FileName, FileMode.Open);
            this._yandexParsingCategories = (List<YandexCategory>)xmlSerializer.Deserialize(fs);
            fs.Close();
        }

        private HttpRequest GenerateHttpRequest()
        {
            var request = new HttpRequest
            {
                Cookies = new CookieDictionary(),
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36"
            };
            request.AddHeader("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7,ka;q=0.6");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"89\", \"Chromium\";v=\"89\", \";Not A Brand\";v=\"99\"");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("upgrade-insecure-requests", "1");
            request.AddHeader("sec-fetch-dest", "document");
            request.AddHeader("sec-fetch-user", "?1");
            request.AddHeader("sec-fetch-site", "none");
            request.AddHeader("sec-fetch-mode", "navigate");
            request.AllowAutoRedirect = true;
            request.IgnoreProtocolErrors = true;
            request.KeepAlive = true;
            request.EnableEncodingContent = true;
            request.CharacterSet = Encoding.UTF8;
            return request;
        }

        private async Task ParsePartOfWebsites(List<IGrouping<string, Website>> webSites, bool addToListView = true)
        {
            foreach (var item in webSites)
            {
                var hasError = false;
                var error = "";
                try
                {
                    var was = item.ElementAt(0).Phones.Count;
                    await this.FindPhoneOnWebpage("http://" + item.Key, item.ElementAt(0));

                    item.ElementAt(0).Phones = item.ElementAt(0).Phones.Distinct().ToList();
                    for (var q = 1; q < item.Count(); q++)
                    {
                        item.ElementAt(q).Phones = item.ElementAt(0).Phones;
                    }

                    if (item.ElementAt(0).Phones.Count > was)
                    {
                        Interlocked.Increment(ref this._successCount);
                    }
                }
                catch (Exception ex)
                {
                    hasError = true;
                    error = ex.Message;
                }

                string cmpDayViews = "", cmpWeekViews = "", cmpMonthViews = "";
                if (addToListView)
                {
                    if (this.phonesTreeView.InvokeRequired)
                    {
                        this.phonesTreeView.BeginInvoke(new ThreadStart(() => { this.AddWebsiteToListView(item, hasError, cmpDayViews, cmpWeekViews, cmpMonthViews, error); }));
                    }
                    else
                    {
                        this.AddWebsiteToListView(item, hasError, cmpDayViews, cmpWeekViews, cmpMonthViews, error);
                    }
                }
            }
        }

        private void AddWebsiteToListView(IGrouping<string, Website> item, bool hasError, string cmpDayViews, string cmpWeekViews, string cmpMonthViews, string error)
        {
            var branditem = this.phonesTreeView.Nodes.Add(item.Key);
            var stview = branditem.Nodes.Add("Статистика");
            if (cmpDayViews != "" || cmpWeekViews != "" || cmpMonthViews != "")
            {
                stview.Nodes.Add(cmpDayViews);
                stview.Nodes.Add(cmpWeekViews);
                stview.Nodes.Add(cmpMonthViews);
            }

            foreach (var st in item.ElementAt(0).Phones)
            {
                var sub = branditem.Nodes.Add(st);
            }

            if (hasError)
            {
                branditem.BackColor = Color.Pink;
                branditem.Nodes.Add(error);
            }

            branditem.Tag = item.ElementAt(0);
            this.label3.Text = $"Спарсено {Interlocked.Increment(ref this._count)}/{this._totalCount} сайтов";
        }

        private void ParseCyfrovaryForBrands(Website website)
        {
            try
            {
                var uri = new Uri("https://" + website.URL);
                if (!uri.Host.EndsWith("ru") && !uri.Host.EndsWith("рф"))
                {
                    return;
                }

                this.yandexMarketChromeDriver.Url = $"https://a.pr-cy.ru/{website.URL}/";
                this.WriteLog($"Moving to yandex market  {this.yandexMarketChromeDriver.Url}");
                var pagesource = this.yandexMarketChromeDriver.PageSource;
                var document = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes();
                var statistic = document.GetElementByTagAndAInnetTextAndAttribute("div", "class", "analysis-test__name", "Открытая статистика".ToLower());
                var day = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[3].InnerText);
                var week = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[5].InnerText);
                var month = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[7].InnerText);
                website.DayViews = day;
                website.WeekViews = week;
                website.MonthViews = month;
            }
            catch (Exception)
            {
                this.WriteLog("Exception in Cifrovoyray");
            }
        }

        private async void parseContactsFromWebsite_Click(object sender, EventArgs e)
        {
            try
            {
                this.parseContactsFromWebsite.Enabled = false;
                this.phonesTreeView.Nodes.Clear();
                var webSites = this._yandexParsingCategories.SelectMany(t => t.Items).SelectMany(z => z.Websites).Where(t => t.Phones.Count == 0).GroupBy(t => t.URL).ToList();
                this._totalCount = webSites.Count;
                this._count = this._successCount = 0;
                var threadsCount = (int)this.numericUpDown1.Value;
                var ts = new Task[threadsCount];
                var spl = webSites.SplitOnParts(threadsCount);
                for (var i = 0; i < spl.Count; i++)
                {
                    var j = i;
                    ts[j] = Task.Run(async () => await this.ParsePartOfWebsites(spl[j]));
                }

                await Task.WhenAll(ts);

                await Task.Run(() =>
                {
                    webSites = this._yandexParsingCategories.SelectMany(t => t.Items).SelectMany(z => z.Websites).GroupBy(t => t.URL).ToList();
                    var statisticCounter = 0;
                    foreach (var webSite in webSites)
                    {
                        try
                        {
                            var uri = new Uri("https://" + webSite.Key);
                            if (uri.Host.EndsWith("ru") || uri.Host.EndsWith("рф") || webSite.ElementAt(0).Phones.Count > 0)
                            {
                                string source;
                                using (var client = new WebClient())
                                {
                                    source = Encoding.UTF8.GetString(client.DownloadData($"https://a.pr-cy.ru/{webSite.Key}/"));
                                }

                                var document = new HtmlDocument();
                                document.LoadHtml(source);
                                if (source.Contains("перестаньте жать F5 и немного отдохните"))
                                {
                                }

                                var statistic = document.GetAllNodes().GetElementByTagAndAInnetTextAndAttribute("div", "class", "analysis-test__name", "Открытая статистика".ToLower());
                                var day = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[3].InnerText);
                                var week = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[5].InnerText);
                                var month = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[7].InnerText);
                                webSite.ElementAt(0).DayViews = day;
                                webSite.ElementAt(0).WeekViews = week;
                                webSite.ElementAt(0).MonthViews = month;
                            }
                        }
                        catch
                        {
                        }

                        statisticCounter++;
                        ;
                        this.label6.BeginInvoke(new ThreadStart(() => { this.label6.Text = $"Статистика {statisticCounter}/{webSites.Count}"; }));

                    }
                });

                MessageBox.Show("Successfully parsed websites: " + this._successCount);
            }
            finally
            {
                this.parseContactsFromWebsite.Enabled = true;
            }
        }

        private void phonesTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(this.phonesTreeView.SelectedNode.Text);
            }
        }

        private void phonesTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("chrome.exe", "http:\\" + this.phonesTreeView.SelectedNode.Text);
            }
            catch
            {
            }
        }

        private void phonesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private async Task<List<string>> ParseYandexMarketProductList(YandexMarketCategory category)
        {
            var page = 1;
            var allCmps = new List<string>();
            while (true)
            {
                HtmlDocument doc;
                string html;

                this.Loaded = false;
                this._geckoWebBrowser.Navigate(category.URL + $"&page={page++}");
                while (!this.Loaded)
                {
                    await Task.Delay(500);
                }

                html = (this._geckoWebBrowser.DomDocument.DocumentElement as GeckoHtmlElement)?.OuterHtml;
                doc = new HtmlDocument();
                doc.LoadHtml(html);
                var pagesource = "";
                while ((pagesource = this._geckoWebBrowser.GetPageSource()).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
                {
                    await Task.Delay(500);
                }

                while (!this.Loaded)
                {
                    await Task.Delay(500);
                }

                await Task.Delay(250);
                html = (this._geckoWebBrowser.DomDocument.DocumentElement as GeckoHtmlElement)?.OuterHtml;
                doc = new HtmlDocument();
                doc.LoadHtml(html);

                while (!this.Loaded)
                {
                    await Task.Delay(50);
                }

                await Task.Delay(150);
                html = (this._geckoWebBrowser.DomDocument.DocumentElement as GeckoHtmlElement)?.OuterHtml;
                if (html.Contains("Тут ничего нет"))
                {
                    break;
                }

                doc = new HtmlDocument();
                doc.LoadHtml(html);
                var items = doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("div", "class", "_2Qo3ODl0by")?.ChildNodes;
                foreach (var product in items)
                {
                    var desc = product.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_1leWQd9vBF");
                    if (desc != null)
                    {
                        //continue;
                        foreach (var descItem in desc.ChildNodes)
                        {
                            if (descItem.InnerText.StartsWith("Производитель"))
                            {
                                category.Brands.Add(descItem.InnerText.Substring(15));
                            }
                        }
                    }


                    var linkNode = product.GetAllNodes().GetElementByTagAndAttributeContain("a", "class", "_2qvOOvezty");
                    if (linkNode == null)
                    {
                        continue;
                    }

                    var link = "https://market.yandex.ru" + linkNode.Attributes["href"].Value;
                    allCmps.Add(link);
                }

                var nds = doc.DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("a", "class", "_3OFYTyXi90");
                if (nds == null)
                {
                    break;
                }
            }

            return allCmps;
        }

        private async Task ParseYandexMarketBrands(YandexMarketCategory category)
        {
            this.yandexMarketChromeDriver.Url = category.URL;

            string pagesource;
            var hadCaptcha = false;
            while ((pagesource = this.yandexMarketChromeDriver.PageSource).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
            {
                await Task.Delay(500);
                hadCaptcha = true;
            }

            if (hadCaptcha)
            {
                await Task.Delay(1500);
            }

            //var parent = yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAInnetText("legend", "Магазины").ParentNode;
            //var document = parent.GetAllNodes().GetElementByTagAndAttributeContain("button", "class", "_1KpjX _2Wg9r");
            //
            //var el = yandexMarketChromeDriver.FindElementByXPath(document.XPath);
            //yandexMarketChromeDriver.ExecuteScript($"document.getElementsByTagName('html')[0].scrollTop  = {el.Location.Y+100};");
            //await Task.Delay(500);
            //yandexMarketChromeDriver.FindElementByXPath(document.XPath).Click();
            //while (yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop") == null)
            //{
            //	await Task.Delay(500);
            //}
            //
            //await Task.Delay(1000);
            //
            var scrollOffset = 1;
            var maxHeight = 0;
            // Convert.ToInt32(yandexMarketChromeDriver.ExecuteScript("return document.getElementsByClassName('_5rop')[0].scrollHeight;"));
            //List<string> usedNames = new List<string>();
            //while (true)
            //{
            //	var ul = yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop");
            //	var all = ul.GetAllNodes();
            //	foreach (var item in all)
            //	{
            //		if (item.Name == "input")
            //		{
            //			var name = item.Attributes["Name"].Value.Substring(9);
            //			if (usedNames.Contains(name))
            //				continue;
            //			usedNames.Add(name);
            //			companies.Add(item.Attributes["id"].Value.Substring(5));
            //		}
            //	}
            //
            //	if ((scrollOffset - 1) * 500 > maxHeight)
            //	{
            //		break;
            //	}
            //
            //	yandexMarketChromeDriver.ExecuteScript($"document.getElementsByClassName('_5rop')[0].scrollTop = {scrollOffset++ * 500};");
            //	await Task.Delay(500);
            //}

            //yandexMarketChromeDriver.Url = yandexMarketChromeDriver.Url;
            //while ((pagesource = yandexMarketChromeDriver.PageSource).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
            //{
            //	await Task.Delay(500);
            //}

            var dev = this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAInnetTextAndAttribute("legend", "class", "ShXb4", "производитель");
            if (dev == null)
            {
                return;
            }

            try
            {
                var xp = dev.ParentNode.ChildNodes.Last().ChildNodes.First().XPath;
                this.yandexMarketChromeDriver.FindElementByXPath(xp).Click();
                var counter = 0;
                while (this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop") == null)
                {
                    await Task.Delay(500);
                    if (counter++ > 10)
                    {
                        throw new Exception();
                    }
                }

                await Task.Delay(1000);

                scrollOffset = 1;
                maxHeight = Convert.ToInt32(this.yandexMarketChromeDriver.ExecuteScript("return document.getElementsByClassName('_5rop')[0].scrollHeight;"));

                while (true)
                {
                    var ul = this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop");
                    var all = ul.GetAllNodes();
                    foreach (var item in all)
                    {
                        if (item.Name == "span")
                        {
                            if (!category.Brands.Contains(item.InnerText))
                            {
                                category.Brands.Add(item.InnerText);
                            }
                        }
                    }

                    if ((scrollOffset - 1) * 500 > maxHeight)
                    {
                        break;
                    }

                    this.yandexMarketChromeDriver.ExecuteScript($"document.getElementsByClassName('_5rop')[0].scrollTop = {scrollOffset++ * 500};");
                    await Task.Delay(750);
                }
            }
            catch (Exception)
            {

            }

        }

        public static string ImageFileToBase64String(byte[] image)
        {
            try
            {
                var base64String = Convert.ToBase64String(image);

                return base64String;
            }
            catch
            {
                return null;
            }
        }

        private void PlaySound()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Wildberries_WScrapper.mp.wav";

                var stream = assembly.GetManifestResourceStream(resourceName);

                var player = new System.Media.SoundPlayer(stream);
                player.Play();
            }
            catch { }
        }
        private async Task ParseListorg(YandexMarketCompany cmp)
        {
            this.Loaded = false;
            if (cmp.OGRN.Length == 13)
            {
                this._geckoWebBrowser.Navigate($"https://list-org.com/search?type=ogrn&val={cmp.OGRN}");
            }
            else
            {
                this._geckoWebBrowser.Navigate($"https://list-org.com/search?type=fio&val={cmp.OGRN}");
            }

            while (!this.Loaded)
            {
                await Task.Delay(500);
            }

            var notified = false;
            var pagesource = "";
            while ((pagesource = this._geckoWebBrowser.GetPageSource()).Contains("Проверка, что Вы не робот") || pagesource == "")
            {
                var imageNode = this._geckoWebBrowser.Document.GetElementsByTagName("img")[0];

                var img = new ImageCreator(this._geckoWebBrowser);
                var image = img.CanvasGetPngImage((uint)imageNode.OffsetLeft, (uint)imageNode.OffsetTop, (uint)imageNode.OffsetWidth, (uint)imageNode.OffsetHeight);

                var nr = new Normal();
                nr.SetBase64(ImageFileToBase64String(image));
                nr.SetLang("ru");
                try
                {
                    await this._solver.Solve(nr);
                    ((GeckoInputElement)this._geckoWebBrowser.Document.GetElementsByTagName("input")[0]).Value = nr.Code.ToLower();
                    this._geckoWebBrowser.Document.GetElementsByTagName("input")[1].Click();
                }
                catch (Exception)
                {
                }
                finally
                {
                    this.Loaded = false;
                }

                if (!notified && !this.anticaptcha_isactive)
                {
                    this.PlaySound();
                    notified = true;
                }

                await Task.Delay(500);
            }

            while (!this.Loaded)
            {
                await Task.Delay(500);
            }

            var doc1 = this._geckoWebBrowser.GetHtmlDocument().GetAllNodes();
            var orgList = doc1.GetElementByTagAndAttributeEquality("div", "class", "org_list");
            if (orgList != null && orgList.ChildNodes.Count != 0)
            {
                this.Loaded = false;
                if (cmp.OGRN.Length == 13)
                {
                    cmp.ListorgURL = $"https://list-org.com{orgList.ChildNodes[0].ChildNodes[0].ChildNodes[1].Attributes[0].Value}";
                }
                else
                {
                    cmp.ListorgURL = $"https://list-org.com{orgList.ChildNodes[0].ChildNodes[0].Attributes[0].Value}";
                }

                this._geckoWebBrowser.Navigate(cmp.ListorgURL);
                while (!this.Loaded)
                {
                    await Task.Delay(500);
                }

                notified = false;
                while ((pagesource = this._geckoWebBrowser.GetPageSource()).Contains("Проверка, что Вы не робот") || pagesource == "")
                {
                    var imageNode = this._geckoWebBrowser.Document.GetElementsByTagName("img")[0];

                    var img = new ImageCreator(this._geckoWebBrowser);
                    var image = img.CanvasGetPngImage((uint)imageNode.OffsetLeft, (uint)imageNode.OffsetTop, (uint)imageNode.OffsetWidth, (uint)imageNode.OffsetHeight);

                    var nr = new Normal();
                    nr.SetBase64(ImageFileToBase64String(image));
                    nr.SetLang("ru");
                    try
                    {
                        await this._solver.Solve(nr);
                        (this._geckoWebBrowser.Document.GetElementsByTagName("input")[0] as GeckoInputElement).Value = nr.Code.ToLower();
                        this._geckoWebBrowser.Document.GetElementsByTagName("input")[1].Click();
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        this.Loaded = false;
                    }
                    if (!notified && !this.anticaptcha_isactive)
                    {
                        this.PlaySound();
                        notified = true;
                    }
                    await Task.Delay(500);
                }

                while (!this.Loaded)
                {
                    await Task.Delay(500);
                }

                using (var context = new AutoJSContext(this._geckoWebBrowser.Window))
                {
                    context.EvaluateScript("window.scrollTo(0, 99999);");
                }

                await Task.Delay(300);
                doc1 = this._geckoWebBrowser.GetHtmlDocument().GetAllNodes();
                cmp.PersonalCount = doc1.GetElementByTagAndAInnetText("i", "численность персонала:")?.ParentNode?.ParentNode?.ChildNodes.ElementAtOrDefault(1)?.InnerText;
                if (cmp.OGRN.Length == 13)
                {
                    cmp.Boss = doc1.GetElementByTagAndAInnetText("i", "руководитель:")?.ParentNode?.ParentNode?.ChildNodes.ElementAtOrDefault(1)?.InnerText;
                }
                else
                {
                    var pp = doc1.GetElementByTagAndAInnetText("i", "фио:");
                    cmp.Boss = pp?.ParentNode?.ChildNodes.ElementAtOrDefault((int)pp?.ParentNode?.ChildNodes.IndexOf(pp) + 1)?.InnerText.Trim();
                }

                if (cmp.OGRN.Length == 13)
                {
                    cmp.RegisterDate = doc1.GetElementByTagAndAInnetText("i", "дата регистрации:")?.ParentNode?.ChildNodes.ElementAtOrDefault(1)?.InnerText.Trim();
                    cmp.IsSmall = doc1.First(t => t.Name == "html").OuterHtml.Contains("как малое предприятие");
                }
                else
                {
                    var pp = doc1.GetElementByTagAndAInnetText("i", "дата регистрации:");
                    cmp.RegisterDate = pp?.ParentNode?.ChildNodes.ElementAtOrDefault((int)pp?.ParentNode?.ChildNodes.IndexOf(pp) + 1)?.InnerText.Trim();
                    cmp.IsSmall = true;
                }
                var legalAddressContainer = doc1.GetElementByTagAndAInnetText("i", "юридический адрес:");
                cmp.LegalAddress = legalAddressContainer?.ParentNode?.ChildNodes?.ElementAtOrDefault(2)?.InnerText;
                cmp.Response = doc1.GetElementByTagAndAInnetText("td", "ответчик") != null;
                var capitalNode = doc1.GetElementByTagAndAInnetText("td", "доходы - расходы");
                if (capitalNode != null)
                {
                    var capitalNodes = capitalNode.ParentNode.ParentNode.ChildNodes.Skip(1).Where(f => f.Name == "tr").ToList();
                    foreach (var item in capitalNodes)
                    {
                        var cp = new Capital { Year = item.ChildNodes[0].InnerText, In = item.ChildNodes[1].InnerText, Out = item.ChildNodes[2].InnerText, InOut = item.ChildNodes[3].InnerText };
                        cmp.Capitals.Add(cp);
                    }
                }

                capitalNode = doc1.GetElementByTagAndAInnetText("td", "сторона");
                if (capitalNode != null)
                {
                    cmp.HasJudgments = true;
                }
            }
        }


        private async Task ParseCifrovoyray(YandexMarketCompany cmp)
        {
            try
            {
                if (cmp.Websites.Count == 0)
                {
                    return;
                }

                this.Loaded = false;
                if (false)
                {
                    this._geckoWebBrowser.Navigate($"https://a.pr-cy.ru/{cmp.Websites[0].URL}/");
                    var i = 0;
                    while (!this.Loaded)
                    {
                        await Task.Delay(500).ConfigureAwait(false);
                        if (i++ > 3)
                        {
                            break;
                        }
                    }


                    //var document = _geckoWebBrowser.GetHtmlDocument().GetAllNodes();
                }

                this.yandexMarketChromeDriver.Url = $"https://a.pr-cy.ru/{cmp.Websites[0].URL}/";
                this.WriteLog($"Moving to yandex market  {this.yandexMarketChromeDriver.Url}");
                var pagesource = this.yandexMarketChromeDriver.PageSource;
                var document = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes();
                var statistic = document.GetElementByTagAndAInnetTextAndAttribute("div", "class", "analysis-test__name", "Открытая статистика".ToLower());
                var day = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[3].InnerText);
                var week = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[5].InnerText);
                var month = HtmlEntity.DeEntitize(statistic.ParentNode.ParentNode.ChildNodes[3].ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[7].InnerText);
                cmp.DayViews = day;
                cmp.WeekViews = week;
                cmp.MonthViews = month;
            }
            catch (Exception)
            {
                this.WriteLog("Exception in Cifrovoyray");
            }
        }

        private bool anticaptcha_isactive;
        private async void startYandexMarketParsing_Click(object sender, EventArgs e)
        {

            try
            {
                this.anticaptcha_isactive = false;
                if (await this._solver.Balance() == 0)
                {
                    if (MessageBox.Show("Баланс антикапчи равен нулю. Капчу придется решать вручную. Продолжить?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        ==
                        DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    this.anticaptcha_isactive = true;
                }
            }
            catch (Exception)
            {
                if (MessageBox.Show("Введен неверный ключ антикапчи. Капчу придется решать вручную. Продолжить?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            for (var i = this.yandexMarketTabControl.TabPages.Count - 2; i >= 0; i--)
            {
                this.yandexMarketTabControl.TabPages.RemoveAt(i);
            }
            this.yandexMarketActionsTabPage.Text = $"Действия 0/{this._yandexMarketCategories.Count}";
            this.YandexMarketParsingIsRunning = true;
            var task = Task.Run(async () =>
            {
                while (this.YandexMarketParsingIsRunning || this.yandexMarketWebsitesQueue.Count != 0)
                {
                    this.yandexMarketWebsitesQueueLabel.BeginInvoke(new Action(() => { this.yandexMarketWebsitesQueueLabel.Text = "Количество сайтов в очереди: " + this.yandexMarketWebsitesQueue.Count; }));

                    var peek = this.yandexMarketWebsitesQueue.TryDequeue(out var queueItem);
                    if (!peek)
                    {

                        await Task.Delay(150);
                        continue;
                    }

                    this.WriteLog($"Starting Parse website {queueItem.Website.URL} async");
                    await this.ParsePartOfWebsites((new List<Website>() { queueItem.Website }).GroupBy(t => t.URL).ToList(), false);
                    this.WriteLog("End parse website async");
                }
                this.yandexMarketWebsitesQueueLabel.BeginInvoke(new Action(() => { this.yandexMarketWebsitesQueueLabel.Text = "Количество сайтов в очереди: 0"; }));

            });
            try
            {
                this.yandexMarketChromeDriver = new ChromeDriver();
                this._yandexMarketCategories.ForEach(t => t.Brands.Clear());
                this._yandexMarketCategories.ForEach(t => t.Items.Clear());
                var counter = 0;
                foreach (var t in this._yandexMarketCategories)
                {
                    this.WriteLog($"YMarket parsing {t.URL} category started");
                    await this.ParseYandexMarketSingleUrlNew(t);
                    this.yandexMarketActionsTabPage.Text = $"Действия {++counter}/{this._yandexMarketCategories.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.YandexMarketParsingIsRunning = false;
                this.yandexMarketChromeDriver.Close();
                this.yandexMarketChromeDriver.Quit();
            }

            await task;
            MessageBox.Show("Парсинг завершен", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WriteLog(string info)
        {
            lock (this.ob)
            {
                using (var sw = new StreamWriter(Application.StartupPath + "\\logs.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":: " + info);
                }
            }
        }

        private async Task ParseYandexMarketSingleUrlNew(YandexMarketCategory category)
        {
            var datagridview = new DataGridViewYM();
            this.yandexMarketTabControl.TabPages.Insert(this.yandexMarketTabControl.TabCount - 1, category.Name);
            var tb = this.yandexMarketTabControl.TabPages[this.yandexMarketTabControl.TabPages.Count - 2];
            tb.Controls.Add(datagridview); //Add dg to created tab page

            this.WriteLog($"YMarket Companies Start Parsing from {category.URL}");
            await ParseYandexMarketBrands(category);
            var allCmps = await this.ParseYandexMarketCompanies(category);
            this.WriteLog($"YMarket Companies End Parsing from {category.URL}");
            var i = 0;
            var z = 0;
            foreach (var company in allCmps)
            {
                try
                {
                    this.WriteLog($"Start Parsing Company {company}");// https://market.yandex.ru/shop--and-systems-ru/{company}/reviews");
                    this.yandexMarketChromeDriver.Url = company;// $"https://market.yandex.ru/shop--and-systems-ru/{company}/reviews";
                    var pagesource = this.yandexMarketChromeDriver.PageSource;
                    var logWritten = false;
                    while ((pagesource = this.yandexMarketChromeDriver.PageSource).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
                    {
                        await Task.Delay(500);
                        if (!logWritten)
                        {
                            logWritten = true;
                            this.WriteLog("Captcha on Yandex Market");
                            this.PlaySound();
                        }
                    }

                    var document1 = this.yandexMarketChromeDriver.GetHtmlDocument();
                    var nodeList = document1.DocumentNode.GetAllNodes();
                    var appearNode = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2lRN4").ChildNodes[0].InnerText;
                    var rating = nodeList.GetElementByTagAndAttributeContain("div", "class", "yzkuM").InnerText;
                    var marks = nodeList.GetElementsByTagAndAttributeContain("div", "class", "_2v0or").Select(t => t.InnerText).ToList();
                    var ogrn = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2F_FV").ChildNodes.Last().InnerText;
                    var seller = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2186g")?.InnerText;
                    var website = nodeList.GetElementByTagAndAttributeContain("a", "class", "_2qvOO _21Wng")?.InnerText;
                    if (this.yandexMarketOgrnList.Contains(ogrn))
                    {
                        datagridview.Rows.Add(++i, seller, ogrn, website, "Skipped", "Skipped", "Skipped", rating, marks.ElementAtOrDefault(0), marks.ElementAtOrDefault(1), appearNode, "Skipped", "Skipped", "Skipped");
                        continue; ;

                    }
                    this.yandexMarketOgrnList.Add(ogrn);

                    var cmp = new YandexMarketCompany
                    {
                        URL = $"https://market.yandex.ru/shop--and-systems-ru/{company}/reviews",
                        Category = category.Name,
                        Rating = rating,
                        MarkForFullPeriod = marks.ElementAtOrDefault(0),
                        MarkForThreeMonth = marks.ElementAtOrDefault(1),
                        OGRN = ogrn,
                        Company = seller,
                        AppearTime = appearNode,
                        Websites = new List<Website> { new Website { URL = website } }
                    };
                    this.WriteLog("Company Added To List");
                    category.Items.Add(cmp);
                    try
                    {
                        this.WriteLog("Started List.org Parsing");
                        //#if !DEBUG
                        await this.ParseListorg(cmp);
                        //#endif
                        this.WriteLog("List.org Parsing End");
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        this.Loaded = false;
                    }

                    try
                    {
                        this.WriteLog("Started Cifrovoyray Parsing");
                        await this.ParseCifrovoyray(cmp);
                        this.WriteLog("Parsing Cifrovoyray end");
                    }
                    catch (Exception)
                    {
                    }

                    if (!String.IsNullOrWhiteSpace(cmp.Websites.ElementAt(0).URL))
                    {

                        try
                        {
                            this.WriteLog("Starting Parse website");
                            await this.ParsePartOfWebsites(cmp.Websites.GroupBy(t => t.URL).ToList(), false);
                            this.WriteLog("End parse website");
                        }
                        catch (Exception)
                        {
                        }
                    }

                    this.WriteLog("Datagridview Add items");
                    var rowIndex = datagridview.Rows.Add(++i, seller, ogrn, website, cmp.DayViews, cmp.WeekViews, cmp.MonthViews, rating, marks.ElementAtOrDefault(0), marks.ElementAtOrDefault(1), appearNode, cmp.RegisterDate, cmp.ListorgURL, cmp.URL);
                    this.yandexMarketWebsitesQueue.Enqueue(new YandexMarketQueueItem() { Website = cmp.Websites.FirstOrDefault(), Grid = datagridview, Index = rowIndex });
                }
                catch (Exception)
                {
                    this.WriteLog("Exception in main");
                }

                tb.Text = $"{category.Name} {++z}/{allCmps.Count}";
            }
        }

        private async Task<List<string>> ParseYandexMarketCompanies(YandexMarketCategory category)
        {
            this.yandexMarketChromeDriver.Url = category.URL;

            string pagesource;
            var hadCaptcha = false;

            while ((pagesource = this.yandexMarketChromeDriver.PageSource).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
            {
                await Task.Delay(500);
                hadCaptcha = true;
            }

            if (hadCaptcha)
            {
                await Task.Delay(1500);
            }

            var parent = this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAInnetText("legend", "Магазины").ParentNode;
            var document = parent.GetAllNodes().GetElementByTag("button");

            var el = this.yandexMarketChromeDriver.FindElementByXPath(document.XPath);
            this.yandexMarketChromeDriver.ExecuteScript($"document.getElementsByTagName('html')[0].scrollTop  = {el.Location.Y + 100};");
            await Task.Delay(500);
            this.yandexMarketChromeDriver.FindElementByXPath(document.XPath).Click();
            while (this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop") == null)
            {
                await Task.Delay(500);
            }
            var scrollOffset = 1;
            var companies = new List<string>();
            var ns = new List<HtmlNode>();

            var maxHeight = 0;
            maxHeight = Convert.ToInt32(this.yandexMarketChromeDriver.ExecuteScript("return document.getElementsByClassName('_5rop')[0].scrollHeight;"));
            var usedNames = new List<string>();
            string lastName = null;
            while (true)
            {
                try
                {
                    {
                        var ul = this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop");
                        var all = ul.ChildNodes.Cast<HtmlNode>().ToList();
                        var index = 0;
                        if (lastName != null)
                        {
                            index = all.FindIndex(t => t.ChildNodes.ElementAtOrDefault(0)?.ChildNodes?.ElementAtOrDefault(0)?.Attributes["for"]?.Value?.Trim() == lastName) + 1;
                        }
                        if (index >= all.Count - 1)
                        {
                            break;
                        }
                        if (all[index].ChildNodes.Count == 0 || all[index].ChildNodes[0].ChildNodes.Count == 0)
                        {
                            index++;
                        }
                        if (all[index].ChildNodes.Count == 0 || all[index].ChildNodes[0].ChildNodes.ElementAtOrDefault(0)?.Name != "label")
                        {
                            index++;
                        }
                        lastName = all[index].ChildNodes[0].ChildNodes[0].Attributes["for"].Value.Trim();
                        var currentNode = all[index];
                        this.yandexMarketChromeDriver.FindElementByXPath(currentNode.ChildNodes[0].ChildNodes[0].XPath).Click();
                        while (this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Count(t => t.GetAttributeValue<string>("class", "").Contains("_2Lvbi")) != 0)
                        {

                        }
                        await Task.Delay(500);
                        var cmpp = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_1CD2d Du2-z _1bE_e _3i4NH").ToList();
                        if (cmpp.Count != 0)
                        {
                            companies.Add($"https://market.yandex.ru{cmpp[0].Attributes["href"].Value.Split('?').First()}");
                        }
                        else
                        {
                            var nodes = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_3Fff3").ToList();
                            ns.Add(nodes.First());
                        }
                        WriteLog("Parsed company " + lastName);
                    }
                    {
                        var ul = this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop");
                        var all = ul.ChildNodes.Cast<HtmlNode>().ToList();
                        var index = all.FindIndex(t => t.ChildNodes.ElementAtOrDefault(0)?.ChildNodes?.ElementAtOrDefault(0)?.Attributes["for"]?.Value?.Trim() == lastName) + 1;
                        var currentNode = all[index - 1];
                        this.yandexMarketChromeDriver.FindElementByXPath(currentNode.ChildNodes[0].ChildNodes[0].XPath).Click();
                    }
                    this.yandexMarketChromeDriver.ExecuteScript($"document.getElementsByClassName('_5rop')[0].scrollTop += 25;");
                    await Task.Delay(500);
                }
                catch (Exception ex)
                {
                    WriteLog("Exception in select parsing" + ex.Message);
                }
            }
            WriteLog("Finished parsing select");
            while (false)
            {
                var ul = this.yandexMarketChromeDriver.GetHtmlDocument().DocumentNode.GetAllNodes().GetElementByTagAndAttributeContain("ul", "class", "_5rop");
                var all = ul.ChildNodes.Cast<HtmlNode>().ToList();
                foreach (var item in all)
                {
                    try
                    {
                        var ele = item.GetAllNodes().GetElementByTag("input");
                        if (ele != null)
                        {
                            if (usedNames.Contains(ele.ParentNode.InnerText))
                            {
                                continue;
                            }

                            usedNames.Add(ele.ParentNode.InnerText);
                            this.yandexMarketChromeDriver.FindElementByXPath(ele.ParentNode.XPath).Click();
                            while (this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Count(t => t.GetAttributeValue<string>("class", "").Contains("_2Lvbi")) != 0)
                            {

                            }

                            var cmpp = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_1CD2d Du2-z _1bE_e _3i4NH").ToList();
                            if (cmpp.Count != 0)
                            {
                                companies.Add($"https://market.yandex.ru{cmpp[0].Attributes["href"].Value.Split('?').First()}");

                            }
                            else
                            {
                                var nodes = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_3Fff3").ToList();
                                ns.Add(nodes.First());
                            }

                            this.yandexMarketChromeDriver.FindElementByXPath(ele.ParentNode.XPath).Click();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                if ((scrollOffset - 1) * 500 > maxHeight)
                {
                    break;
                }

                this.yandexMarketChromeDriver.ExecuteScript($"document.getElementsByClassName('_5rop')[0].scrollTop = {scrollOffset++ * 500};");
                await Task.Delay(500);
            }
            var page = 1;
            while (false)
            {
                this.yandexMarketChromeDriver.Url = $"{category.URL}&page={page++}";
                while ((pagesource = this.yandexMarketChromeDriver.PageSource).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
                {
                    Thread.Sleep(500);// await Task.Delay(500);
                    if (!hadCaptcha)
                    {
                        this.PlaySound();
                    }

                    hadCaptcha = true;
                }

                if (hadCaptcha)
                {
                    await Task.Delay(1500);
                }
                hadCaptcha = false;
                var nodes = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_3Fff3").ToList();
                ns.AddRange(nodes);
                var current = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_2prNU _3OFYT").ToList();
                if (current.Count == 0)
                {
                    break;
                }
            }

            foreach (var currentNode in ns)
            {
                try
                {
                    var link = currentNode.ChildNodes[0].ChildNodes[0].Attributes["href"].Value;
                    this.yandexMarketChromeDriver.Url = $"https://market.yandex.ru{link.Replace("&amp;", "&")}";
                    while ((pagesource = this.yandexMarketChromeDriver.PageSource).Contains("Подтвердите, что запросы отправляли вы, а не робот") || pagesource == "" || pagesource.Contains("captchaKey"))
                    {
                        await Task.Delay(1000);
                        if (!hadCaptcha)
                        {
                            this.PlaySound();
                        }

                        hadCaptcha = true;
                    }

                    if (hadCaptcha)
                    {
                        await Task.Delay(750);
                    }
                    hadCaptcha = false;
                    var current = this.yandexMarketChromeDriver.GetHtmlDocument().GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_2iTir gbGjL _2CEl7").FirstOrDefault();
                    if (current != null)
                    {
                        current = current.GetAllNodes().Cast<HtmlNode>().Where(t => t.GetAttributeValue<string>("class", "") == "_1CD2d Du2-z _1bE_e").FirstOrDefault();
                        if (current != null)
                        {
                            companies.Add($"https://market.yandex.ru{current.Attributes["href"].Value.Split('?').First()}");
                        }
                    }
                }
                catch
                {

                }
                Application.DoEvents();
            }
            return companies.GroupBy(t => t).Select(f => f.First()).ToList();

        }

        private void editYandexMarketCategories_Click(object sender, EventArgs e)
        {
            new EditCategoriesYandex(this._yandexMarketCategories).ShowDialog(this);
        }


        private void saveYandexMarketBrands_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog { FileName = "file", Filter = "Excel Files|*.xlsx" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var book = new XSSFWorkbook();
            var sheet = book.CreateSheet();
            var headerRow = sheet.CreateRow(0);
            for (var i = 0; i < this._yandexMarketCategories.Count; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(this._yandexMarketCategories[i].Name);
                var grouped = this._yandexMarketCategories[i].Brands.Distinct().ToList();
                grouped.Remove("Не определен");
                for (var z = 0; z < grouped.Count; z++)
                {
                    if (z + 1 >= sheet.PhysicalNumberOfRows)
                    {
                        sheet.CreateRow(z + 1);
                    }

                    var row = sheet.GetRow(z + 1);
                    cell = row.CreateCell(i);
                    cell.SetCellValue(grouped[z]);
                }
            }

            using (var fs = new FileStream(ofd.FileName, FileMode.Create))
            {
                book.Write(fs);
            }
        }

        private async void saveYandexMarketCompanies_Click(object sender, EventArgs e)
        {
            if (false)
            {
                this._yandexMarketCategories.Add(new YandexMarketCategory
                {
                    Name = "dasdas",
                    Items = { new YandexMarketCompany { Company = "TST1", OGRN = "3123123", Websites = new List<Website> { new Website { URL = "yandex.ru", Phones = new List<string> { "phone1", "phone2", "phone3" } } } },
                              new YandexMarketCompany { Company = "TST2", OGRN = "31231231", Websites = new List<Website> { new Website { URL = "yandex.ru", Phones = new List<string> { "phone1", "phone2", "phone3", "phone4" } } } } }
                });
            }

            var book = new XSSFWorkbook();
            var sheet = book.CreateSheet();
            var startRow = 0;
            if (MessageBox.Show("Желаете добавить данные в имеющийся файл?", "Объединение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var ofd = new OpenFileDialog { FileName = "file", Filter = "XLSX Files|*.xlsx" };

                if (ofd.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                var sr = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                book = new XSSFWorkbook(sr);
                startRow = book.GetSheetAt(0).PhysicalNumberOfRows;
                sheet = book.GetSheetAt(0);
                sr.Close();
            }


            var sfd = new SaveFileDialog { FileName = "file", Filter = "XLSX Files|*.xlsx" };

            if (sfd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var tp = typeof(YandexMarketCompany).GetTypeInfo();
            if (startRow == 0)
            {
                var headerRow = sheet.CreateRow(startRow);
                foreach (var VARIABLE in tp.GetProperties().Where(t => Attribute.IsDefined(t, typeof(ExcelAttribute))))
                {
                    if (VARIABLE.PropertyType.Assembly != Assembly.GetExecutingAssembly())
                    {
                        var cell = headerRow.CreateCell(headerRow.Cells.Count);
                        var attr = VARIABLE.GetCustomAttributes(false).FirstOrDefault(t => t is ExcelAttribute);
                        var name = attr == null ? VARIABLE.Name : (attr as ExcelAttribute).Name;
                        cell.SetCellValue(name);
                    }
                    else
                    {
                        var tp1 = VARIABLE.PropertyType.GetTypeInfo();
                        foreach (var pro in tp1.GetProperties())
                        {
                            var cell = headerRow.CreateCell(headerRow.Cells.Count);

                            var attr = pro.GetCustomAttributes(false).FirstOrDefault(t => t is ExcelAttribute);
                            var name = attr == null ? VARIABLE.Name + "_" + pro.Name : (attr as ExcelAttribute).Name;
                            cell.SetCellValue(name);
                        }
                    }
                }
            }

            var companies = this._yandexMarketCategories.SelectMany(t => t.Items).ToList();
            foreach (var VARIABLE in companies)
            {
                foreach (var wb in VARIABLE.Websites)
                {
                    wb.Phones = wb.Phones.Select(t => t.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("(", "").Replace(")", "")).GroupBy(t => t).Select(f => f.First()).ToList();
                    wb.Phones.RemoveAll(String.IsNullOrWhiteSpace);
                }
            }

            var groupedCompanies = new List<YandexMarketCompany>();
            var grouped = companies.GroupBy(t => t.OGRN).ToList();
            foreach (var item in grouped)
            {
                var categoryList = String.Join(", ", item.GroupBy(t => t.Category).Select(z => z.Key));
                var category = item.ElementAt(0);
                category.CategoryList = categoryList;
                groupedCompanies.Add(category);
            }

            var newCompanies = new List<YandexMarketCompany>();
            foreach (var item in groupedCompanies)
            {
                newCompanies.Add(item);
                var row = sheet.CreateRow(sheet.PhysicalNumberOfRows);
                foreach (var VARIABLE in tp.GetProperties().Where(t => Attribute.IsDefined(t, typeof(ExcelAttribute))))
                {
                    if (VARIABLE.PropertyType.Assembly != Assembly.GetExecutingAssembly())
                    {
                        if (VARIABLE.PropertyType == typeof(List<string>))
                        {
                            var list = (List<string>)VARIABLE.GetValue(item);
                            foreach (var l_item in list)
                            {
                                var cell = row.CreateCell(row.Cells.Count);
                                cell.SetCellValue(l_item);
                            }
                        }
                        else
                        {
                            var cell = row.CreateCell(row.Cells.Count);
                            cell.SetCellValue(VARIABLE.GetValue(item)?.ToString());
                        }
                    }
                    else
                    {
                        var tp1 = VARIABLE.PropertyType.GetTypeInfo();
                        var itm = VARIABLE.GetValue(item);
                        foreach (var pro in tp1.GetProperties())
                        {
                            var cell = row.CreateCell(row.Cells.Count);
                            cell.SetCellValue(pro.GetValue(itm)?.ToString());
                        }
                    }
                }
            }

            using (var fs1 = new FileStream(sfd.FileName, FileMode.Create))
            {
                book.Write(fs1);
            }

            if (MessageBox.Show("Отправить новые данные в AmoCRM?", "AmoCRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(newCompanies);
                using (var wb = new WebClient() { Encoding = Encoding.UTF8 })
                {
                    var result = await wb.UploadStringTaskAsync(new Uri("https://vh415782.eurodir.ru/amo/upload_companies.php"), "POST", json);
                    if (result != "")
                    {
                        this.WriteLog(result);
                    }
                }
            }
            MessageBox.Show("Сохранено");
        }

        private void apiKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ApiKey = this.apiKeyTextBox.Text;
            Settings.Default.Save();
            this._solver = new TwoCaptcha(this.apiKeyTextBox.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(this.linkLabel1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ogrs = new List<string>();
            var book = new XSSFWorkbook();
            var sheet = book.CreateSheet();
            var startRow = 0;
            if (MessageBox.Show("Желаете синхронизровать со старым файлом?", "Обновление файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var ofd = new OpenFileDialog { FileName = "file", Filter = "XLSX Files|*.xlsx" };

                if (ofd.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                var sr = new FileStream(ofd.FileName, FileMode.Open);
                book = new XSSFWorkbook(sr);
                for (var i = 1; i < book.GetSheetAt(0).PhysicalNumberOfRows; i++)
                {
                    ogrs.Add(book.GetSheetAt(0).GetRow(i).GetCell(0).StringCellValue);
                }

                startRow = book.GetSheetAt(0).PhysicalNumberOfRows;
                sheet = book.GetSheetAt(0);
                sr.Close();
            }


            var sfd = new SaveFileDialog { FileName = "file", Filter = "XLSX Files|*.xlsx" };

            if (sfd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var items = this._yandexParsingCategories.SelectMany(t => t.Items).ToList();
            var websites = this._yandexParsingCategories.SelectMany(t => t.Items.SelectMany(f => f.Websites)).ToList();
            websites = websites.GroupBy(t => t.URL).Select(f => f.First()).ToList();
            if (startRow == 0)
            {
                var maxNumbers = websites.Max(t => t.Phones.Count);
                var headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("Сайт");
                headerRow.CreateCell(1).SetCellValue("Бренд");
                headerRow.CreateCell(2).SetCellValue("Категория");
                headerRow.CreateCell(3).SetCellValue("Просмотры за день");
                headerRow.CreateCell(4).SetCellValue("Просмотры за неделю");
                headerRow.CreateCell(5).SetCellValue("Просмотры за месяц");
                headerRow.CreateCell(6).SetCellValue("Номера телефонов...");
            }

            foreach (var t1 in websites)
            {
                if (ogrs.Contains(t1.URL))
                {
                    continue;
                }

                var row = sheet.CreateRow(sheet.PhysicalNumberOfRows);
                row.CreateCell(0).SetCellValue(t1.URL);
                var brands = items.Where(t => t.Websites.Any(f => f.URL == t1.URL)).Distinct().ToList();
                row.CreateCell(1).SetCellValue(String.Join(";", brands.Select(f => f.Brand)));

                row.CreateCell(2).SetCellValue(String.Join(";", brands.Select(f => f.Category).Distinct()));
                row.CreateCell(3).SetCellValue(t1.DayViews);
                row.CreateCell(4).SetCellValue(t1.WeekViews);
                row.CreateCell(5).SetCellValue(t1.MonthViews);

                var phones = t1.Phones.Select(t => t.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("(", "").Replace(")", "")).ToList();
                for (var j = 0; j < phones.Count; j++)
                {
                    row.CreateCell(6 + j).SetCellValue(phones[j]);
                }
            }


            using (var fs = new FileStream(sfd.FileName, FileMode.Create))
            {
                book.Write(fs);
            }

            MessageBox.Show("Сохранено");
        }


        private async Task ParseYandexMarketSingleUrl(YandexMarketCategory category)
        {
            var datagridview = new DataGridViewYM();
            this.yandexMarketTabControl.TabPages.Insert(this.yandexMarketTabControl.TabCount - 1, category.Name);
            var pg = this.yandexMarketTabControl.TabPages[this.yandexMarketTabControl.TabPages.Count - 2];
            pg.Controls.Add(datagridview);


            var allCmps = await this.ParseYandexMarketProductList(category);
            allCmps = allCmps.Select(t => "https://market.yandex.ru" + new Uri(t).AbsolutePath).ToList();
            allCmps = allCmps.Distinct().ToList();
            var i = 0;
            foreach (var link in allCmps)
            {
                try
                {
                    this.Loaded = false;
                    this._geckoWebBrowser.Navigate(link);
                    while (!this.Loaded)
                    {
                        await Task.Delay(500);
                    }

                    var doc1 = this._geckoWebBrowser.GetHtmlDocument();
                    var nodeList = doc1.DocumentNode.GetAllNodes();
                    var appearNode = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2lRN4IzLYH").ChildNodes[0].InnerText;
                    var rating = nodeList.GetElementByTagAndAttributeContain("div", "class", "yzkuM642Et").InnerText;
                    var marks = nodeList.GetElementsByTagAndAttributeContain("div", "class", "_2v0orscee-").Select(t => t.InnerText).ToList();
                    var ogrn = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2F_FVZVxQo").ChildNodes.Last().InnerText;
                    var seller = nodeList.GetElementByTagAndAttributeContain("div", "class", "_2186g3xAqi")?.InnerText;
                    var website = nodeList.GetElementByTagAndAttributeContain("a", "class", "_21WngnAbDC")?.InnerText;
                    var cmp = new YandexMarketCompany
                    {
                        URL = link,
                        Category = category.Name,
                        Rating = rating,
                        MarkForFullPeriod = marks.ElementAtOrDefault(0),
                        MarkForThreeMonth = marks.ElementAtOrDefault(1),
                        OGRN = ogrn,
                        Company = seller,
                        AppearTime = appearNode,
                        Websites = new List<Website> { new Website { URL = website } }
                    };
                    category.Items.Add(cmp);
                    try
                    {
                        await this.ParseListorg(cmp);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        this.Loaded = false;
                    }

                    try
                    {
                        await this.ParseCifrovoyray(cmp);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        this.Loaded = false;
                    }

                    datagridview.Rows.Add(++i, seller, ogrn, website, cmp.DayViews, cmp.WeekViews, cmp.MonthViews, rating, marks.ElementAtOrDefault(0), marks.ElementAtOrDefault(1), appearNode, cmp.RegisterDate, cmp.ListorgURL, link);
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    this.Loaded = false;
                }
            }
        }

        private List<string> yandexMarketOgrnList = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { FileName = "file", Filter = "XLSX Files|*.xlsx" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var sr = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var book = new XSSFWorkbook(sr);
            for (var i = 1; i < book.GetSheetAt(0).PhysicalNumberOfRows; i++)
            {
                this.yandexMarketOgrnList.Add(book.GetSheetAt(0).GetRow(i).GetCell(5).StringCellValue);
            }

            this.yandexMarketSyncDataLabel.Text = $"Загружено ОГРН: {this.yandexMarketOgrnList.Count}";
            sr.Close();
        }
    }
}