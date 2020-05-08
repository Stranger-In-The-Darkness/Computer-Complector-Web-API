using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("PROPERTY_VALUES")]
	public class PropertyValue
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public int PropertyID { get; set; }
		[ForeignKey("PropertyID")]
		public Property Property { get; set; }
		[Required]
		public string Value { get; set; }
	}
}
