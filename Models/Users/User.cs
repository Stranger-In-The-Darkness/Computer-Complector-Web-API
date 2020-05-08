using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComputerComplectorWebAPI.Models.Users
{
	/// <summary>
	/// User data model
	/// </summary>
	[Table("USERS")]
	public class User
	{
		/// <summary>
		/// Record ID
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		/// <summary>
		/// User name
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// User email
		/// </summary>
		[Required]
		public string Email { get; set; }
		/// <summary>
		/// User password
		/// </summary>
		[Required]
		public string Password { get; set; }
		/// <summary>
		/// User role
		/// </summary>
		[Required]
		public int Role { get; set; }
		/// <summary>
		/// User role as <see cref="Users.Role"/>
		/// </summary>
		[JsonIgnore]
		[ForeignKey("Role")]
		public Role ROLE { get; set; }

		/// <summary>
		/// JWT Token
		/// </summary>
		[NotMapped]
		public string Token { get; set; }
	}
}
