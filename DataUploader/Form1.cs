using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using DataUploader.Models;
using ExcelDataReader;

namespace DataUploader
{
	public partial class Form1 : Form
	{
		List<Paddie> paddies=new List<Paddie>();
		List<Branch> branches=new List<Branch>();
		List<Ticket> tickets=new List<Ticket>();
		private DataSet _dataSet;

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ImportMembers();
			ExtractBranches();
			ExtractPaddies();
			ExtractTickets();
			MessageBox.Show(@"Success");

		}

		private void ImportMembers()
		{
			using (var openFileDialog = new OpenFileDialog() { Filter = @"Excel 1996-2007 Files |*.xls;*.xlsx;", ValidateNames = true })
			{
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{

					try
					{
						FileStream fs = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
						ImportedFilename.Text = $@"File: {openFileDialog.SafeFileName}";
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
							NamesGrid.DataSource = _dataSet.Tables[table.TableName].DefaultView;
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

		private void Paddies_Click(object sender, EventArgs e)
		{
			ExtractPaddies();

		
		}

		private void ExtractPaddies()
		{
			var nameIndex = 0;
			var codeIndex = 0;
			foreach (DataGridViewColumn column in NamesGrid.Columns)
			{
				if (!column.HeaderText.Equals("DPName",StringComparison.InvariantCultureIgnoreCase) ) continue;
				nameIndex = column.DisplayIndex;
				break;
			}

			foreach (DataGridViewColumn column in NamesGrid.Columns)
			{
				if (!column.HeaderText.Equals("BranchCode", StringComparison.InvariantCultureIgnoreCase)) continue;
				codeIndex= column.DisplayIndex;
				break;
			}
			foreach (DataGridViewRow row in NamesGrid.Rows)
			{
				try
				{
					var value = row.Cells[nameIndex].Value;
					if (value != null)
					{
						var paddie=new Paddie
						{
							Name = value.ToString(),
							BranchCode = row.Cells[codeIndex]?.Value?.ToString()
						};

						if (paddies.All(o => o.Name != paddie.Name))
						{
							paddies.Add(paddie);
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
				context.Paddies.AddRange(paddies);
				context.SaveChanges();
				//MessageBox.Show(@"Success");

			}
		}

		private void Branches_Click(object sender, EventArgs e)
		{
			ExtractBranches();
		}

		private void ExtractBranches()
		{
			int branchIndex = 0;
			int codeIndex = 0;

			foreach (DataGridViewColumn column in NamesGrid.Columns)
			{
				if (!column.HeaderText.Equals("Branch", StringComparison.InvariantCultureIgnoreCase)) continue;
				branchIndex = column.DisplayIndex;
				break;
			}
			foreach (DataGridViewColumn column in NamesGrid.Columns)
			{
				if (!column.HeaderText.Equals("BranchCode", StringComparison.InvariantCultureIgnoreCase)) continue;
				codeIndex = column.DisplayIndex;
				break;
			}
			foreach (DataGridViewRow row in NamesGrid.Rows)
			{
				try
				{
					var value = row.Cells[branchIndex].Value;
					if (value != null)
					{
						var branch = new Branch
						{
							Name = value.ToString(),
							Code = row.Cells[codeIndex].Value?.ToString()
						};


						if (branches.All(o => o.Code != branch.Code))
						{
							branches.Add(branch);
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
				context.Branches.AddRange(branches);
				context.SaveChanges();
				//MessageBox.Show(@"Success");

			}

		}

		private void BtnTickets_Click(object sender, EventArgs e)
		{
			ExtractTickets();
		}

		private void ExtractTickets()
		{
			var nameIndex = 0;
			var codeIndex = 0;
			var ticketIndex = 0;
			foreach (DataGridViewColumn column in NamesGrid.Columns)
			{
				if (!column.HeaderText.Equals("DPName", StringComparison.InvariantCultureIgnoreCase)) continue;
				nameIndex = column.DisplayIndex;
				break;
			}

			foreach (DataGridViewColumn column in NamesGrid.Columns)
			{
				if (!column.HeaderText.Equals("BranchCode", StringComparison.InvariantCultureIgnoreCase)) continue;
				codeIndex = column.DisplayIndex;
				break;
			}


			foreach (DataGridViewColumn column in NamesGrid.Columns)
			{
				if (!column.HeaderText.Equals("TicketNo", StringComparison.InvariantCultureIgnoreCase)) continue;
				ticketIndex = column.DisplayIndex;
				break;
			}
			foreach (DataGridViewRow row in NamesGrid.Rows)
			{
				try
				{
					var value = row.Cells[nameIndex].Value.ToString();
					if (!string.IsNullOrWhiteSpace(value))
					{
						var ticket = new Ticket
						{
							PaddieId = GetPaddiesId(value),
							BranchCode = row.Cells[codeIndex]?.Value?.ToString(),
							Number = row.Cells[ticketIndex]?.Value?.ToString()
						};

						if (tickets.All(o => o.Number != ticket.Number))
						{
							tickets.Add(ticket);
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
				context.Tickets.AddRange(tickets);
				context.SaveChanges();
				//MessageBox.Show(@"Success");

			}
		}

		private static int GetPaddiesId(string value)
		{
			using (var context = new DalexDbContext())
			{
				var paddie = context.Paddies.FirstOrDefault(o =>o.Name==value);

				if (paddie?.Id != null) return (int) paddie?.Id;
			}

			return 0;
		}
	}
}
