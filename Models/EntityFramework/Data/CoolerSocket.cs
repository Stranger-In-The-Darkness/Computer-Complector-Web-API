using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("COOLER_SOCKET")]
	public class CoolerSocket
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int		ID			{ get; set; }
		[Required]
		public int		CoolerID	{ get; set; }
		[ForeignKey("CoolerID")]
		public Cooler	Cooler		{ get; set; }
		[Required]
		public string	Socket		{ get; set; }
	}
}
