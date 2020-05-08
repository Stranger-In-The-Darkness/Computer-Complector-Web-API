using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerComplectorWebAPI.Models.Users
{
	/// <summary>
	/// User role data model
	/// </summary>
	[Table("ROLES")]
	public class Role
	{
		/// <summary>
		/// Record ID
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		
		/// <summary>
		/// Role name
		/// </summary>
		[Required]
		public string Name { get; set; }
	}
}
