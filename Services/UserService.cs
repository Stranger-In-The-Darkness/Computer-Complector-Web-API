using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Helpers;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ComputerComplectorWebAPI.Services
{
	/// <summary>
	/// Users authentication service
	/// </summary>
	public class UserService : IUserService
	{
		/// <summary>
		/// Users data context
		/// </summary>
		private UsersContext _dbContext;

		/// <summary>
		/// Application settings
		/// </summary>
		private readonly AppSettings _appSettings;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context">User-data context</param>
		/// <param name="appSettings">Application settings</param>
		public UserService(UsersContext context, IOptions<AppSettings> appSettings)
		{
			_dbContext = context;
			_appSettings = appSettings.Value;
		}

		/// <summary>
		/// Authenticate user with given username and password
		/// </summary>
		/// <param name="username">Username</param>
		/// <param name="password">Password</param>
		/// <returns>Authorized <see cref="User"/> or null</returns>
		public User Authenticate(string username, string password)
		{
			var users = _dbContext.Users.ToList();
			_dbContext.Roles.ToList();

			var user = users.FirstOrDefault(e => e.Name == username && e.Password == password);
			if (user == null)
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.ID.ToString()),
					new Claim(ClaimTypes.Role, user.ROLE.Name)
				}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			user.Token = tokenHandler.WriteToken(token);

			// remove password before returning
			user.Password = null;

			return user;
		}

		/// <summary>
		/// Get all user records
		/// </summary>
		/// <returns><see cref="IEnumerable{User}"/> of <see cref="User"/></returns>
		public IEnumerable<User> GetAll()
		{
			return _dbContext.Users.ToList().Select(e => { e.Password = null; return e; });
		}

		/// <summary>
		/// Get user record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="User"/> with given ID or null</returns>
		public User GetById(int id)
		{
			var user = _dbContext.Users.ToList().FirstOrDefault(e => e.ID == id);
			if (user != null)
			{
				user.Password = null;
			}
			return user;
		}
	}
}
