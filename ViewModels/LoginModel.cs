using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage = "No Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "No password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
