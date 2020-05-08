using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Analytics.Requests
{
	public class AddSelectionRequest
	{
		public string ComponentType { get; set; }
		public int ElementID { get; set; }
		public IDictionary<string, string> Properties { get; set; }
		public DateTime Date { get; set; }
	}
}
