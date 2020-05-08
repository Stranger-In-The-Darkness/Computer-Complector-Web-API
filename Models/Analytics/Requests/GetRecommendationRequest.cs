using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Analytics.Requests
{
	public class GetRecommendationRequest
	{
		public string ComponentType { get; set; }
		public IDictionary<string, string> Properties { get; set; }
		public SelectionTimeSpan SelectionTimeSpan { get; set; }
	}
}
