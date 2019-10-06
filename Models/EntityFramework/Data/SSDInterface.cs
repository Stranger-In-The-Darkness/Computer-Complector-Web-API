using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("SSD_INTERFACE")]
	public class SSDInterface
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public int SSDID { get; set; }
		[ForeignKey("SSDID")]
		public SSD SSD { get; set; }
		[Required]
		public string Interface { get; set; }
	}
}
