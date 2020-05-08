using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Data
{
	public class Property
	{
		public string Name { get; set; }
		public string Text { get; set; }
		public bool ShowDescription { get; set; }
		public string Description { get; set; }
		public IEnumerable<string> Values { get; set; }
		public string Component { get; set; }
	}
}