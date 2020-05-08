using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("BODY_FORMFACTOR")]
	public class BodyFormfactor
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public int BodyID { get; set; }
		[ForeignKey("BodyID")]
		public Body Body { get; set; }
		[Required]
		public string Formfactor { get; set; }
	}
}
