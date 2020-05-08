using System.Collections.Generic;
using ComputerComplectorWebAPI.Models.Users;

namespace ComputerComplectorWebAPI.Interfaces
{
	/// <summary>
	/// Interface of user authentication provider service
	/// </summary>
	public interface IUserService
	{
		User Authenticate(string username, string password);
		IEnumerable<User> GetAll();
		User GetById(int id);
	}
}
