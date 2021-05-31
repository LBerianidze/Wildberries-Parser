namespace Wildberries_WScrapper.Forms
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.start = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.loadCategories = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.editCategories = new System.Windows.Forms.Button();
			this.wildberriesCategoriesTabControl = new System.Windows.Forms.TabControl();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.saveCategoriesInOneFile = new System.Windows.Forms.Button();
			this.saveCompaniesList = new System.Windows.Forms.Button();
			this.SaveProducts = new System.Windows.Forms.Button();
			this.saveCompanies = new System.Windows.Forms.Button();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.parseContactsFromWebsite = new System.Windows.Forms.Button();
			this.loadYandexWithSites = new System.Windows.Forms.Button();
			this.phonesTreeView = new System.Windows.Forms.TreeView();
			this.saveBrandsWebsites = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.acceptSettings = new System.Windows.Forms.Button();
			this.startYandexParsing = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.loadBrands = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.yandexMarketActionsTabPage = new System.Windows.Forms.TabPage();
			this.saveYandexMarketBrands = new System.Windows.Forms.Button();
			this.saveYandexMarketCompanies = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.editYandexMarketCategories = new System.Windows.Forms.Button();
			this.startYandexMarketParsing = new System.Windows.Forms.Button();
			this.tabPage7 = new System.Windows.Forms.TabPage();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.apiKeyTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.yandexMarketSyncDataLabel = new System.Windows.Forms.Label();
			this.yandexMarketWebsitesQueueLabel = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.wildberriesCategoriesTabControl.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.tabPage5.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.yandexMarketActionsTabPage.SuspendLayout();
			this.tabPage7.SuspendLayout();
			this.SuspendLayout();
			// 
			// start
			// 
			this.start.AutoSize = true;
			this.start.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.start.Location = new System.Drawing.Point(3, 41);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(211, 30);
			this.start.TabIndex = 2;
			this.start.Text = "Запустить";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new System.EventHandler(this.startWildberriesParsing_Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1050, 689);
			this.panel1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage7);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1064, 721);
			this.tabControl1.TabIndex = 4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1056, 695);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Browser";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.loadCategories);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.editCategories);
			this.tabPage2.Controls.Add(this.wildberriesCategoriesTabControl);
			this.tabPage2.Controls.Add(this.start);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1056, 695);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Wildberries";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// loadCategories
			// 
			this.loadCategories.AutoSize = true;
			this.loadCategories.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.loadCategories.Location = new System.Drawing.Point(214, 3);
			this.loadCategories.Name = "loadCategories";
			this.loadCategories.Size = new System.Drawing.Size(211, 36);
			this.loadCategories.TabIndex = 7;
			this.loadCategories.Text = "Загрузить файлы выгрузки";
			this.loadCategories.UseVisualStyleBackColor = true;
			this.loadCategories.Click += new System.EventHandler(this.loadCategories_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(217, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 21);
			this.label2.TabIndex = 6;
			// 
			// editCategories
			// 
			this.editCategories.AutoSize = true;
			this.editCategories.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.editCategories.Location = new System.Drawing.Point(3, 3);
			this.editCategories.Name = "editCategories";
			this.editCategories.Size = new System.Drawing.Size(211, 36);
			this.editCategories.TabIndex = 5;
			this.editCategories.Text = "Редактировать категории";
			this.editCategories.UseVisualStyleBackColor = true;
			this.editCategories.Click += new System.EventHandler(this.editCategories_Click);
			// 
			// wildberriesCategoriesTabControl
			// 
			this.wildberriesCategoriesTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wildberriesCategoriesTabControl.Controls.Add(this.tabPage3);
			this.wildberriesCategoriesTabControl.Location = new System.Drawing.Point(3, 77);
			this.wildberriesCategoriesTabControl.Name = "wildberriesCategoriesTabControl";
			this.wildberriesCategoriesTabControl.SelectedIndex = 0;
			this.wildberriesCategoriesTabControl.Size = new System.Drawing.Size(1050, 615);
			this.wildberriesCategoriesTabControl.TabIndex = 4;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.saveCategoriesInOneFile);
			this.tabPage3.Controls.Add(this.saveCompaniesList);
			this.tabPage3.Controls.Add(this.SaveProducts);
			this.tabPage3.Controls.Add(this.saveCompanies);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(1042, 589);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Действия";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// saveCategoriesInOneFile
			// 
			this.saveCategoriesInOneFile.AutoSize = true;
			this.saveCategoriesInOneFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveCategoriesInOneFile.Location = new System.Drawing.Point(4, 96);
			this.saveCategoriesInOneFile.Name = "saveCategoriesInOneFile";
			this.saveCategoriesInOneFile.Size = new System.Drawing.Size(353, 31);
			this.saveCategoriesInOneFile.TabIndex = 3;
			this.saveCategoriesInOneFile.Text = "Сохранить категории и товары одним файлом";
			this.saveCategoriesInOneFile.UseVisualStyleBackColor = true;
			this.saveCategoriesInOneFile.Click += new System.EventHandler(this.saveCategoriesInOneFile_Click);
			// 
			// saveCompaniesList
			// 
			this.saveCompaniesList.AutoSize = true;
			this.saveCompaniesList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveCompaniesList.Location = new System.Drawing.Point(4, 66);
			this.saveCompaniesList.Name = "saveCompaniesList";
			this.saveCompaniesList.Size = new System.Drawing.Size(353, 31);
			this.saveCompaniesList.TabIndex = 2;
			this.saveCompaniesList.Text = "Сохранить список брендов";
			this.saveCompaniesList.UseVisualStyleBackColor = true;
			this.saveCompaniesList.Click += new System.EventHandler(this.saveCompaniesList_Click);
			// 
			// SaveProducts
			// 
			this.SaveProducts.AutoSize = true;
			this.SaveProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SaveProducts.Location = new System.Drawing.Point(4, 6);
			this.SaveProducts.Name = "SaveProducts";
			this.SaveProducts.Size = new System.Drawing.Size(353, 31);
			this.SaveProducts.TabIndex = 1;
			this.SaveProducts.Text = "Сохранить категории и товары";
			this.SaveProducts.UseVisualStyleBackColor = true;
			this.SaveProducts.Click += new System.EventHandler(this.SaveProducts_Click);
			// 
			// saveCompanies
			// 
			this.saveCompanies.AutoSize = true;
			this.saveCompanies.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveCompanies.Location = new System.Drawing.Point(4, 36);
			this.saveCompanies.Name = "saveCompanies";
			this.saveCompanies.Size = new System.Drawing.Size(353, 31);
			this.saveCompanies.TabIndex = 0;
			this.saveCompanies.Text = "Сохранить список компаний и категории";
			this.saveCompanies.UseVisualStyleBackColor = true;
			this.saveCompanies.Click += new System.EventHandler(this.saveCompanies_Click);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.label6);
			this.tabPage4.Controls.Add(this.button2);
			this.tabPage4.Controls.Add(this.textBox1);
			this.tabPage4.Controls.Add(this.numericUpDown1);
			this.tabPage4.Controls.Add(this.parseContactsFromWebsite);
			this.tabPage4.Controls.Add(this.loadYandexWithSites);
			this.tabPage4.Controls.Add(this.phonesTreeView);
			this.tabPage4.Controls.Add(this.saveBrandsWebsites);
			this.tabPage4.Controls.Add(this.label3);
			this.tabPage4.Controls.Add(this.acceptSettings);
			this.tabPage4.Controls.Add(this.startYandexParsing);
			this.tabPage4.Controls.Add(this.label1);
			this.tabPage4.Controls.Add(this.loadBrands);
			this.tabPage4.Controls.Add(this.label4);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(1056, 695);
			this.tabPage4.TabIndex = 2;
			this.tabPage4.Text = "Yandex";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(306, 134);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(0, 21);
			this.label6.TabIndex = 25;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.AutoSize = true;
			this.button2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.button2.Location = new System.Drawing.Point(742, 71);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(306, 36);
			this.button2.TabIndex = 24;
			this.button2.Text = "Сохранить бренды и сайты";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
			this.textBox1.Location = new System.Drawing.Point(461, 81);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(275, 26);
			this.textBox1.TabIndex = 23;
			this.textBox1.Text = "официальный представитель {0}";
			this.toolTip1.SetToolTip(this.textBox1, "Введите {0} для подстановки имени бренда\r\nВведите {1} для подстановки имени катег" +
        "ории");
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown1.Location = new System.Drawing.Point(989, 130);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(64, 20);
			this.numericUpDown1.TabIndex = 21;
			this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// parseContactsFromWebsite
			// 
			this.parseContactsFromWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.parseContactsFromWebsite.AutoSize = true;
			this.parseContactsFromWebsite.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.parseContactsFromWebsite.Location = new System.Drawing.Point(742, 36);
			this.parseContactsFromWebsite.Name = "parseContactsFromWebsite";
			this.parseContactsFromWebsite.Size = new System.Drawing.Size(306, 36);
			this.parseContactsFromWebsite.TabIndex = 17;
			this.parseContactsFromWebsite.Text = "Запустить парсинг контактов с сайтов";
			this.parseContactsFromWebsite.UseVisualStyleBackColor = true;
			this.parseContactsFromWebsite.Click += new System.EventHandler(this.parseContactsFromWebsite_Click);
			// 
			// loadYandexWithSites
			// 
			this.loadYandexWithSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.loadYandexWithSites.AutoSize = true;
			this.loadYandexWithSites.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.loadYandexWithSites.Location = new System.Drawing.Point(742, 1);
			this.loadYandexWithSites.Name = "loadYandexWithSites";
			this.loadYandexWithSites.Size = new System.Drawing.Size(306, 36);
			this.loadYandexWithSites.TabIndex = 16;
			this.loadYandexWithSites.Text = "Загрузить файл с сайтами";
			this.loadYandexWithSites.UseVisualStyleBackColor = true;
			this.loadYandexWithSites.Click += new System.EventHandler(this.loadYandexWithSites_Click);
			// 
			// phonesTreeView
			// 
			this.phonesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.phonesTreeView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.phonesTreeView.Location = new System.Drawing.Point(2, 113);
			this.phonesTreeView.Name = "phonesTreeView";
			this.phonesTreeView.Size = new System.Drawing.Size(304, 579);
			this.phonesTreeView.TabIndex = 15;
			this.phonesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.phonesTreeView_AfterSelect);
			this.phonesTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.phonesTreeView_KeyDown);
			this.phonesTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.phonesTreeView_MouseDoubleClick);
			// 
			// saveBrandsWebsites
			// 
			this.saveBrandsWebsites.AutoSize = true;
			this.saveBrandsWebsites.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.saveBrandsWebsites.Location = new System.Drawing.Point(1, 72);
			this.saveBrandsWebsites.Name = "saveBrandsWebsites";
			this.saveBrandsWebsites.Size = new System.Drawing.Size(306, 36);
			this.saveBrandsWebsites.TabIndex = 13;
			this.saveBrandsWebsites.Text = "Сохранить сайты для парсинга номеров";
			this.saveBrandsWebsites.UseVisualStyleBackColor = true;
			this.saveBrandsWebsites.Click += new System.EventHandler(this.saveBrandsWebsites_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(306, 113);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 21);
			this.label3.TabIndex = 12;
			// 
			// acceptSettings
			// 
			this.acceptSettings.AutoSize = true;
			this.acceptSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.acceptSettings.Location = new System.Drawing.Point(308, 40);
			this.acceptSettings.Name = "acceptSettings";
			this.acceptSettings.Size = new System.Drawing.Size(329, 36);
			this.acceptSettings.TabIndex = 11;
			this.acceptSettings.Text = "Настройте браузер и нажмите на эту кнопку";
			this.acceptSettings.UseVisualStyleBackColor = true;
			this.acceptSettings.Visible = false;
			this.acceptSettings.Click += new System.EventHandler(this.acceptSettings_Click);
			// 
			// startYandexParsing
			// 
			this.startYandexParsing.AutoSize = true;
			this.startYandexParsing.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.startYandexParsing.Location = new System.Drawing.Point(1, 37);
			this.startYandexParsing.Name = "startYandexParsing";
			this.startYandexParsing.Size = new System.Drawing.Size(306, 36);
			this.startYandexParsing.TabIndex = 10;
			this.startYandexParsing.Text = "Запустить парсинг брендов";
			this.startYandexParsing.UseVisualStyleBackColor = true;
			this.startYandexParsing.Click += new System.EventHandler(this.startYandexParsing_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(313, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 21);
			this.label1.TabIndex = 9;
			// 
			// loadBrands
			// 
			this.loadBrands.AutoSize = true;
			this.loadBrands.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.loadBrands.Location = new System.Drawing.Point(1, 2);
			this.loadBrands.Name = "loadBrands";
			this.loadBrands.Size = new System.Drawing.Size(306, 36);
			this.loadBrands.TabIndex = 8;
			this.loadBrands.Text = "Загрузить файл категорий и брендов xlsx";
			this.loadBrands.UseVisualStyleBackColor = true;
			this.loadBrands.Click += new System.EventHandler(this.loadBrands_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(313, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(151, 20);
			this.label4.TabIndex = 22;
			this.label4.Text = "Поисковая строка:";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.yandexMarketWebsitesQueueLabel);
			this.tabPage5.Controls.Add(this.yandexMarketSyncDataLabel);
			this.tabPage5.Controls.Add(this.tabControl2);
			this.tabPage5.Controls.Add(this.button1);
			this.tabPage5.Controls.Add(this.editYandexMarketCategories);
			this.tabPage5.Controls.Add(this.startYandexMarketParsing);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(1056, 695);
			this.tabPage5.TabIndex = 3;
			this.tabPage5.Text = "Yandex Market";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// tabControl2
			// 
			this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl2.Controls.Add(this.yandexMarketActionsTabPage);
			this.tabControl2.Location = new System.Drawing.Point(3, 77);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(1050, 615);
			this.tabControl2.TabIndex = 11;
			// 
			// yandexMarketActionsTabPage
			// 
			this.yandexMarketActionsTabPage.Controls.Add(this.saveYandexMarketBrands);
			this.yandexMarketActionsTabPage.Controls.Add(this.saveYandexMarketCompanies);
			this.yandexMarketActionsTabPage.Location = new System.Drawing.Point(4, 22);
			this.yandexMarketActionsTabPage.Name = "yandexMarketActionsTabPage";
			this.yandexMarketActionsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.yandexMarketActionsTabPage.Size = new System.Drawing.Size(1042, 589);
			this.yandexMarketActionsTabPage.TabIndex = 0;
			this.yandexMarketActionsTabPage.Text = "Действия";
			this.yandexMarketActionsTabPage.UseVisualStyleBackColor = true;
			// 
			// saveYandexMarketBrands
			// 
			this.saveYandexMarketBrands.AutoSize = true;
			this.saveYandexMarketBrands.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveYandexMarketBrands.Location = new System.Drawing.Point(4, 6);
			this.saveYandexMarketBrands.Name = "saveYandexMarketBrands";
			this.saveYandexMarketBrands.Size = new System.Drawing.Size(353, 31);
			this.saveYandexMarketBrands.TabIndex = 1;
			this.saveYandexMarketBrands.Text = "Сохранить категории и бренды";
			this.saveYandexMarketBrands.UseVisualStyleBackColor = true;
			this.saveYandexMarketBrands.Click += new System.EventHandler(this.saveYandexMarketBrands_Click);
			// 
			// saveYandexMarketCompanies
			// 
			this.saveYandexMarketCompanies.AutoSize = true;
			this.saveYandexMarketCompanies.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveYandexMarketCompanies.Location = new System.Drawing.Point(4, 36);
			this.saveYandexMarketCompanies.Name = "saveYandexMarketCompanies";
			this.saveYandexMarketCompanies.Size = new System.Drawing.Size(353, 31);
			this.saveYandexMarketCompanies.TabIndex = 0;
			this.saveYandexMarketCompanies.Text = "Сохранить категории и компании";
			this.saveYandexMarketCompanies.UseVisualStyleBackColor = true;
			this.saveYandexMarketCompanies.Click += new System.EventHandler(this.saveYandexMarketCompanies_Click);
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.button1.Location = new System.Drawing.Point(214, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(240, 36);
			this.button1.TabIndex = 10;
			this.button1.Text = "Загрузить файл синхронизации";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// editYandexMarketCategories
			// 
			this.editYandexMarketCategories.AutoSize = true;
			this.editYandexMarketCategories.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.editYandexMarketCategories.Location = new System.Drawing.Point(3, 3);
			this.editYandexMarketCategories.Name = "editYandexMarketCategories";
			this.editYandexMarketCategories.Size = new System.Drawing.Size(211, 36);
			this.editYandexMarketCategories.TabIndex = 9;
			this.editYandexMarketCategories.Text = "Редактировать категории";
			this.editYandexMarketCategories.UseVisualStyleBackColor = true;
			this.editYandexMarketCategories.Click += new System.EventHandler(this.editYandexMarketCategories_Click);
			// 
			// startYandexMarketParsing
			// 
			this.startYandexMarketParsing.AutoSize = true;
			this.startYandexMarketParsing.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.startYandexMarketParsing.Location = new System.Drawing.Point(3, 41);
			this.startYandexMarketParsing.Name = "startYandexMarketParsing";
			this.startYandexMarketParsing.Size = new System.Drawing.Size(211, 30);
			this.startYandexMarketParsing.TabIndex = 8;
			this.startYandexMarketParsing.Text = "Запустить";
			this.startYandexMarketParsing.UseVisualStyleBackColor = true;
			this.startYandexMarketParsing.Click += new System.EventHandler(this.startYandexMarketParsing_Click);
			// 
			// tabPage7
			// 
			this.tabPage7.Controls.Add(this.linkLabel1);
			this.tabPage7.Controls.Add(this.apiKeyTextBox);
			this.tabPage7.Controls.Add(this.label5);
			this.tabPage7.Location = new System.Drawing.Point(4, 22);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage7.Size = new System.Drawing.Size(1056, 695);
			this.tabPage7.TabIndex = 4;
			this.tabPage7.Text = "Настройки";
			this.tabPage7.UseVisualStyleBackColor = true;
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
			this.linkLabel1.Location = new System.Drawing.Point(464, 6);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(144, 17);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "https://rucaptcha.com";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// apiKeyTextBox
			// 
			this.apiKeyTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F);
			this.apiKeyTextBox.Location = new System.Drawing.Point(72, 2);
			this.apiKeyTextBox.Name = "apiKeyTextBox";
			this.apiKeyTextBox.Size = new System.Drawing.Size(390, 26);
			this.apiKeyTextBox.TabIndex = 1;
			this.apiKeyTextBox.TextChanged += new System.EventHandler(this.apiKeyTextBox_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(3, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(71, 20);
			this.label5.TabIndex = 0;
			this.label5.Text = "Ключ API:";
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 100;
			this.toolTip1.AutoPopDelay = 10000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 20;
			this.toolTip1.ShowAlways = true;
			this.toolTip1.UseAnimation = false;
			this.toolTip1.UseFading = false;
			// 
			// yandexMarketSyncDataLabel
			// 
			this.yandexMarketSyncDataLabel.AutoSize = true;
			this.yandexMarketSyncDataLabel.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.yandexMarketSyncDataLabel.Location = new System.Drawing.Point(453, 11);
			this.yandexMarketSyncDataLabel.Name = "yandexMarketSyncDataLabel";
			this.yandexMarketSyncDataLabel.Size = new System.Drawing.Size(0, 20);
			this.yandexMarketSyncDataLabel.TabIndex = 23;
			// 
			// yandexMarketWebsitesQueueLabel
			// 
			this.yandexMarketWebsitesQueueLabel.AutoSize = true;
			this.yandexMarketWebsitesQueueLabel.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.yandexMarketWebsitesQueueLabel.Location = new System.Drawing.Point(220, 48);
			this.yandexMarketWebsitesQueueLabel.Name = "yandexMarketWebsitesQueueLabel";
			this.yandexMarketWebsitesQueueLabel.Size = new System.Drawing.Size(0, 20);
			this.yandexMarketWebsitesQueueLabel.TabIndex = 24;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
			this.ClientSize = new System.Drawing.Size(1064, 721);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(1080, 760);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Wildberries Scrapper";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.wildberriesCategoriesTabControl.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			this.tabControl2.ResumeLayout(false);
			this.yandexMarketActionsTabPage.ResumeLayout(false);
			this.yandexMarketActionsTabPage.PerformLayout();
			this.tabPage7.ResumeLayout(false);
			this.tabPage7.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button start;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabControl wildberriesCategoriesTabControl;
		private System.Windows.Forms.Button editCategories;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button saveCompanies;
		private System.Windows.Forms.Button SaveProducts;
		private System.Windows.Forms.Button saveCompaniesList;
		private System.Windows.Forms.Button loadCategories;
		private System.Windows.Forms.Button saveCategoriesInOneFile;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button loadBrands;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button startYandexParsing;
		private System.Windows.Forms.Button acceptSettings;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button saveBrandsWebsites;
		private System.Windows.Forms.TreeView phonesTreeView;
		private System.Windows.Forms.Button loadYandexWithSites;
		private System.Windows.Forms.Button parseContactsFromWebsite;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button editYandexMarketCategories;
		private System.Windows.Forms.Button startYandexMarketParsing;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage yandexMarketActionsTabPage;
		private System.Windows.Forms.Button saveYandexMarketBrands;
		private System.Windows.Forms.Button saveYandexMarketCompanies;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.TextBox apiKeyTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label yandexMarketSyncDataLabel;
		private System.Windows.Forms.Label yandexMarketWebsitesQueueLabel;
	}
}

