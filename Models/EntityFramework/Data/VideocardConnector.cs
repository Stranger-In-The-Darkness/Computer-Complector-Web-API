using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("VIDEOCARD_CONNECTOR")]
	public class VideocardConnector
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public int VideocardID { get; set; }
		[ForeignKey("VideocardID")]
		public Videocard Videocard { get; set; }
		[Required]
		public string Connector { get; set; }
	}
}
