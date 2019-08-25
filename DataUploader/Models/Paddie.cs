using System.Collections.Generic;

namespace DataUploader.Models
{
	public class Paddie
	{
		public Paddie()
		{
			Tickets=new List<Ticket>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public Branch Branch { get; set; }
		public string BranchCode { get; set; }
		public  ICollection<Ticket> Tickets { get; set; }
	}
}