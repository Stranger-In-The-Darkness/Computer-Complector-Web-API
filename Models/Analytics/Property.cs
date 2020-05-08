using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerComplectorWebAPI.Models.Analytics
{
	public class Property
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string ComponentType { get; set; }
		public ICollection<PropertyValue> Values { get; set; }
	}
}
