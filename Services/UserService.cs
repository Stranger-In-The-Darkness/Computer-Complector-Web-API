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
	public class UserService : IUserService
	{
		private UsersContext _dbContext;

		private readonly AppSettings _appSettings;

		public UserService(UsersContext context, IOptions<AppSettings> appSettings)
		{
			_dbContext = context;
			_appSettings = appSettings.Value;
		}

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

		public IEnumerable<User> GetAll()
		{
			return _dbContext.Users.ToList().Select(e => { e.Password = null; return e; });
		}

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
