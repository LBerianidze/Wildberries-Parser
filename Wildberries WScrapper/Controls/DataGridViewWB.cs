using System.Windows.Forms;

namespace Wildberries_WScrapper.Controls
{
	public class DataGridViewWB : DataGridView
	{
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
		public DataGridViewWB()
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
			this.Column5,
			this.Column6,
			this.Column7});
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
			this.Column2.HeaderText = "Name";
			this.Column2.Name = "Column2";
			this.Column2.Width = 60;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Price";
			this.Column3.Name = "Column3";
			this.Column3.Width = 56;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Brand";
			this.Column4.Name = "Column4";
			this.Column4.Width = 60;
			// 
			// Column5
			// 
			this.Column5.HeaderText = "Company";
			this.Column5.Name = "Column5";
			this.Column5.Width = 76;
			// 
			// Column6
			// 
			this.Column6.HeaderText = "OGRN";
			this.Column6.Name = "Column6";
			this.Column6.Width = 64;
			// 
			// Column7
			// 
			this.Column7.HeaderText = "URL";
			this.Column7.Name = "Column7";
			this.Column7.Width = 54;
		}
	}
}
