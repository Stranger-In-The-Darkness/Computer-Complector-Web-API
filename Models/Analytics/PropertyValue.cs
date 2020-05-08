using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Analytics
{
	public class PropertyValue
	{
		[Key]
		public int ID { get; set; }
		public string Value { get; set; }
		[Required]
		public int PropertyID { get; set; }
		[ForeignKey("PropertyID")]
		public Property Property { get; set; }
	}
}
