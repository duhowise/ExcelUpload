namespace DataUploader.Models
{
	public class Ticket
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public  Paddie Paddie { get; set; }
		public int PaddieId { get; set; }
		public  Branch Branch { get; set; }
		public string BranchCode { get; set; }
	}
}