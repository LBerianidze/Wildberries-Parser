namespace Wildberries_WScrapper.Forms
{
	partial class EditCategories
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
			this.categoriesListbox = new System.Windows.Forms.ListBox();
			this.addCategory = new System.Windows.Forms.Button();
			this.deleteCategory = new System.Windows.Forms.Button();
			this.nameTextbox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.urlTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// categoriesListbox
			// 
			this.categoriesListbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.categoriesListbox.FormattingEnabled = true;
			this.categoriesListbox.ItemHeight = 21;
			this.categoriesListbox.Location = new System.Drawing.Point(2, 2);
			this.categoriesListbox.Name = "categoriesListbox";
			this.categoriesListbox.Size = new System.Drawing.Size(254, 361);
			this.categoriesListbox.TabIndex = 0;
			this.categoriesListbox.SelectedIndexChanged += new System.EventHandler(this.categoriesListbox_SelectedIndexChanged);
			// 
			// addCategory
			// 
			this.addCategory.AutoSize = true;
			this.addCategory.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.addCategory.Location = new System.Drawing.Point(1, 363);
			this.addCategory.Name = "addCategory";
			this.addCategory.Size = new System.Drawing.Size(256, 30);
			this.addCategory.TabIndex = 3;
			this.addCategory.Text = "Добавить";
			this.addCategory.UseVisualStyleBackColor = true;
			this.addCategory.Click += new System.EventHandler(this.addCategory_Click);
			// 
			// deleteCategory
			// 
			this.deleteCategory.AutoSize = true;
			this.deleteCategory.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.deleteCategory.Location = new System.Drawing.Point(1, 393);
			this.deleteCategory.Name = "deleteCategory";
			this.deleteCategory.Size = new System.Drawing.Size(256, 30);
			this.deleteCategory.TabIndex = 4;
			this.deleteCategory.Text = "Удалить";
			this.deleteCategory.UseVisualStyleBackColor = true;
			this.deleteCategory.Click += new System.EventHandler(this.deleteCategory_Click);
			// 
			// nameTextbox
			// 
			this.nameTextbox.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.nameTextbox.Location = new System.Drawing.Point(335, 2);
			this.nameTextbox.Name = "nameTextbox";
			this.nameTextbox.Size = new System.Drawing.Size(555, 27);
			this.nameTextbox.TabIndex = 6;
			this.nameTextbox.TextChanged += new System.EventHandler(this.nameTextbox_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(258, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 20);
			this.label1.TabIndex = 5;
			this.label1.Text = "Название:";
			// 
			// urlTextBox
			// 
			this.urlTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.urlTextBox.Location = new System.Drawing.Point(335, 35);
			this.urlTextBox.Name = "urlTextBox";
			this.urlTextBox.Size = new System.Drawing.Size(555, 27);
			this.urlTextBox.TabIndex = 8;
			this.urlTextBox.TextChanged += new System.EventHandler(this.urlTextBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(258, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 20);
			this.label2.TabIndex = 7;
			this.label2.Text = "Ссылка:";
			// 
			// EditCategories
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(893, 426);
			this.Controls.Add(this.urlTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nameTextbox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.deleteCategory);
			this.Controls.Add(this.addCategory);
			this.Controls.Add(this.categoriesListbox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "EditCategories";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "EditCategories";
			this.Load += new System.EventHandler(this.EditCategories_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox categoriesListbox;
		private System.Windows.Forms.Button addCategory;
		private System.Windows.Forms.Button deleteCategory;
		private System.Windows.Forms.TextBox nameTextbox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.Label label2;
	}
}