using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("HDD_INTERFACE")]
	public class HDDInterface
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public int HDDID { get; set; }
		[ForeignKey("HDDID")]
		public HDD HDD { get; set; }
		[Required]
		public string Interface { get; set; }
	}
}
