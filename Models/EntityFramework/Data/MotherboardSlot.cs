using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("MOTHERBOARD_SLOTS")]
	public class MotherboardSlot
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public int MotherboardID { get; set; }
		[ForeignKey("MotherboardID")]
		public Motherboard Motherboard { get; set; }
		[Required]
		public string Slot { get; set; }
	}
}
