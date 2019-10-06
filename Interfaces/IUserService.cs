using System.Collections.Generic;
using ComputerComplectorWebAPI.Models.Users;

namespace ComputerComplectorWebAPI.Interfaces
{
	public interface IUserService
	{
		User Authenticate(string username, string password);
		IEnumerable<User> GetAll();
		User GetById(int id);
	}
}
