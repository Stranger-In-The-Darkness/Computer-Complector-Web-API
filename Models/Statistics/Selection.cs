using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerComplectorWebAPI.Models.Statistics
{
	[Table("SELECTIONS")]
	public class Selection
	{
		[Key]
		public int ID { get; set; }
		public ICollection<SelectionProperties> Properties { get; set; }
		[Required]
		public string ComponentType { get; set; }
		[Required]
		public int ElementID { get; set; }
		[Required]
		public DateTime Date { get; set; }
	}
}
