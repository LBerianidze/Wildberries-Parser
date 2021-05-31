using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wildberries_WScrapper.Model;
using Wildberries_WScrapper.Model.Interfaces;
using Wildberries_WScrapper.Model.YandexMarket;

namespace Wildberries_WScrapper.Forms
{
	public partial class EditCategoriesYandex : Form
	{
		List<YandexMarketCategory> categories;
		int type;
		public EditCategoriesYandex(List<YandexMarketCategory> categories, int type = 0)
		{
			InitializeComponent();
			this.categories = categories;
			this.type = type;
		}

		private void EditCategories_Load(object sender, EventArgs e)
		{
			foreach (var item in categories)
			{
				categoriesListbox.Items.Add(item.Name);
			}
		}

		private void addCategory_Click(object sender, EventArgs e)
		{
			categories.Add(new YandexMarketCategory("", "new",1));
			categoriesListbox.Items.Add("new");
			categoriesListbox.SelectedIndex = categoriesListbox.Items.Count - 1;
		}

		private void categoriesListbox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (categoriesListbox.SelectedIndex == -1)
				return;
			int index = categoriesListbox.SelectedIndex;
			this.nameTextbox.Text = categories[index].Name;
			this.urlTextBox.Text = categories[index].URL;
			this.comboBox1.SelectedIndex = categories[index].Type;
			nameTextbox.Focus();
			nameTextbox.Select(nameTextbox.Text.Length, 0);
		}

		private void deleteCategory_Click(object sender, EventArgs e)
		{
			int index = categoriesListbox.SelectedIndex;
			if (index == -1)
				return;
			categories.RemoveAt(index);
			categoriesListbox.Items.RemoveAt(index);
			if (index == 0 && categoriesListbox.Items.Count != 0)
				categoriesListbox.SelectedIndex = 0;
			else if (categoriesListbox.Items.Count != 0)
				categoriesListbox.SelectedIndex = index - 1;
		}

		private void nameTextbox_TextChanged(object sender, EventArgs e)
		{
			int index = categoriesListbox.SelectedIndex;
			if (index == -1)
				return;

			categories[index].Name = nameTextbox.Text;
			categoriesListbox.Items[index] = (object)nameTextbox.Text;
		}

		private void urlTextBox_TextChanged(object sender, EventArgs e)
		{
			int index = categoriesListbox.SelectedIndex;
			if (index == -1)
				return;

			categories[index].URL = urlTextBox.Text;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = categoriesListbox.SelectedIndex;
			if (index == -1)
				return;
			categories[index].Type = comboBox1.SelectedIndex;
		}
	}
}
