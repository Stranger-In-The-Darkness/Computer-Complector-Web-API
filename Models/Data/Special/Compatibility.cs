using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Data.Special
{
	public class Compatibility
	{
		public IEnumerable<string> Relations { get; set; }

		public Dictionary<string, Dictionary<string, List<string>>> Body { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> Charger { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> Cooler { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> CPU { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> HDD { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> Motherboard { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> RAM { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> SSD { get; set; }
		public Dictionary<string, Dictionary<string, List<string>>> Videocard { get; set; }
	}
}
