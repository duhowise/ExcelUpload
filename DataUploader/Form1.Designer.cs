﻿namespace DataUploader
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.ImportedFilename = new System.Windows.Forms.Label();
			this.NamesGrid = new System.Windows.Forms.DataGridView();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NamesGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.NamesGrid);
			this.panel1.Location = new System.Drawing.Point(-1, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(801, 407);
			this.panel1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(706, 415);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Upload";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ImportedFilename
			// 
			this.ImportedFilename.AutoSize = true;
			this.ImportedFilename.Location = new System.Drawing.Point(13, 424);
			this.ImportedFilename.Name = "ImportedFilename";
			this.ImportedFilename.Size = new System.Drawing.Size(35, 13);
			this.ImportedFilename.TabIndex = 2;
			this.ImportedFilename.Text = "label1";
			// 
			// NamesGrid
			// 
			this.NamesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.NamesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.NamesGrid.Location = new System.Drawing.Point(0, 0);
			this.NamesGrid.Name = "NamesGrid";
			this.NamesGrid.Size = new System.Drawing.Size(801, 407);
			this.NamesGrid.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ImportedFilename);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NamesGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label ImportedFilename;
		private System.Windows.Forms.DataGridView NamesGrid;
	}
}

