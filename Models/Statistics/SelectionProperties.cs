using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.EntityFramework.Models.Data;

namespace ComputerComplectorWebAPI.Models.Statistics
{
	[Table("SELECTION_PROPERTIES")]
	public class SelectionProperties
	{
		[Required]
		public int ID { get; set; }
		[Required]
		public int SelectionID { get; set; }
		[ForeignKey("SelectionID")]
		public Selection Selection { get; set; }
		[Required]
		public int PropertyValueID { get; set; }
		[ForeignKey("PropertyValueID")]
		public PropertyValue PropertyValue { get; set; }

	}
}
