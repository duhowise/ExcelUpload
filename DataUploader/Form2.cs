using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DataUploader.Models;
using ExcelDataReader;

namespace DataUploader
{
	public partial class Form2 : Form
	{
		private List<Attendant> _attendants;
		private DataSet _dataSet;

		public Form2()
		{
			InitializeComponent();
			_attendants = new List<Attendant>();
			_dataSet=new DataSet();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog() { Filter = @"Excel 1996-2007 Files |*.xls;*.xlsx;", ValidateNames = true })
			{
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{

					try
					{
						FileStream fs = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
						label1.Text = $@"File: {openFileDialog.SafeFileName}";
						var reader = ExcelReaderFactory.CreateReader(fs);
						_dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
						{
							ConfigureDataTable = data => new ExcelDataTableConfiguration
							{
								UseHeaderRow = true
							}
						});
						foreach (DataTable table in _dataSet.Tables)
						{
							AttendantGrid.DataSource = _dataSet.Tables[table.TableName].DefaultView;
						}
						//foreach (DataGridViewRow row in NamesGrid.Rows)
						//{
						//	var val = row.Cells[1].Value as string;
						//	if (string.IsNullOrWhiteSpace(val))
						//	{
						//		NamesGrid.Rows.Remove(row);
						//	}
						//}
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.Message, @"File Read Error");
					}
				}

			}
		}

		private void ExtractTickets()
		{
			var nameIndex = 0;
			var branchNameIndex = 0;
			var phoneIndex = 0;
			foreach (DataGridViewColumn column in AttendantGrid.Columns)
			{
				if (!column.HeaderText.Equals("Name", StringComparison.InvariantCultureIgnoreCase)) continue;
				nameIndex = column.DisplayIndex;
				break;
			}

			foreach (DataGridViewColumn column in AttendantGrid.Columns)
			{
				if (!column.HeaderText.Equals("BranchName", StringComparison.InvariantCultureIgnoreCase)) continue;
				branchNameIndex = column.DisplayIndex;
				break;
			}


			foreach (DataGridViewColumn column in AttendantGrid.Columns)
			{
				if (!column.HeaderText.Equals("Number", StringComparison.InvariantCultureIgnoreCase)) continue;
				phoneIndex = column.DisplayIndex;
				break;
			}

			foreach (DataGridViewRow row in AttendantGrid.Rows)
			{
				try
				{
					var value = row.Cells[nameIndex].Value.ToString();
					if (!string.IsNullOrWhiteSpace(value))
					{
						var ticket = new Attendant
						{
							Phone = row.Cells[phoneIndex]?.Value?.ToString(),
							Name = row.Cells[branchNameIndex]?.Value?.ToString(),
							BranchName = row.Cells[phoneIndex]?.Value?.ToString()
						};

						if (_attendants.All(o => o.Phone != ticket.Phone))
						{
							_attendants.Add(ticket);
						}
					}
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
				}


			}
			using (var context = new DalexDbContext())
			{
				context.Attendants.AddRange(_attendants);
				context.SaveChanges();
				//MessageBox.Show(@"Success");

			}
		}
	}
}
