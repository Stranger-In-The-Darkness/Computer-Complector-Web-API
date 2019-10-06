using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComputerComplectorWebAPI.Models.Users
{
	[Table("USERS")]
	public class User
	{
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public int Role { get; set; }
		[JsonIgnore]
		[ForeignKey("Role")]
		public Role ROLE { get; set; }

		[NotMapped]
		public string Token { get; set; }
	}
}
