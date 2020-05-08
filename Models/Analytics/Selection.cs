using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerComplectorWebAPI.Models.Analytics
{
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
