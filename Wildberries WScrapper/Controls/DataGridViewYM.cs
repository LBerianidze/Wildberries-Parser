using System.Windows.Forms;

namespace Wildberries_WScrapper.Controls
{
	public class DataGridViewYM : DataGridView
	{
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
		public DataGridViewYM()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AllowUserToAddRows = false;
			this.AllowUserToDeleteRows = false;
			this.AllowUserToResizeColumns = false;
			this.AllowUserToResizeRows = false;
			this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.Dock = DockStyle.Fill;
			this.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.Column1,
			this.Column2,
			this.Column3,
			this.Column4,
			this.Column12,
			this.Column13,
			this.Column14,
			this.Column5,
			this.Column6,
			this.Column7,
			this.Column8,
			this.Column9,
			this.Column10,
			this.Column11});
			this.Location = new System.Drawing.Point(2, 4);
			this.Name = "dataGridView1";
			this.RowHeadersVisible = false;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.Size = new System.Drawing.Size(1037, 582);
			this.TabIndex = 0;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "#";
			this.Column1.Name = "Column1";
			this.Column1.Width = 39;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Название";
			this.Column2.Name = "Column2";
			this.Column2.Width = 60;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "ОГРН";
			this.Column3.Name = "Column3";
			this.Column3.Width = 56;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "ЯСайт";
			this.Column4.Name = "Column4";
			this.Column4.Width = 60;
			// 
			// Column5
			// 
			this.Column5.HeaderText = "Рейтинг";
			this.Column5.Name = "Column5";
			this.Column5.Width = 76;
			// 
			// Column6
			// 
			this.Column6.HeaderText = "Оценка";
			this.Column6.Name = "Column6";
			this.Column6.Width = 64;
			// 
			// Column7
			// 
			this.Column7.HeaderText = "Оценка 3 месяца";
			this.Column7.Name = "Column7";
			this.Column7.Width = 54;
			// 
			// Column8
			// 
			this.Column8.HeaderText = "Дата появления";
			this.Column8.Name = "Column8";
			this.Column8.Width = 64;
			// 
			// Column9
			// 
			this.Column9.HeaderText = "Дата регистрации";
			this.Column9.Name = "Column9";
			this.Column9.Width = 64;
			// 
			// Column10
			// 
			this.Column10.HeaderText = "List-org Ссылка";
			this.Column10.Name = "Column10";
			this.Column10.Width = 64;
			// 
			// Column11
			// 
			this.Column11.HeaderText = "Ссылка";
			this.Column11.Name = "Column11";
			this.Column11.Width = 54;

			this.Column12.HeaderText = "День";
			this.Column12.Name = "Column12";
			this.Column12.Width = 64;
			// 
			// Column10
			// 
			this.Column13.HeaderText = "Неделя";
			this.Column13.Name = "Column13";
			this.Column13.Width = 64;
			// 
			// Column11
			// 
			this.Column14.HeaderText = "Месяц";
			this.Column14.Name = "Column14";
			this.Column14.Width = 54;
		}
	}
}
