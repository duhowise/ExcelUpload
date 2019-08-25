using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataUploader.Models
{
    public class Branch
    {
	    [Key]public string Code { get; set; }
		public string Name { get; set; }
	    public  ICollection<Ticket> Tickets { get; set; }

	}
}
