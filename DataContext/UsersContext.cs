using Microsoft.EntityFrameworkCore;
using ComputerComplectorWebAPI.Models.Users;

namespace ComputerComplectorWebAPI.DataContext
{
	/// <summary>
	/// DB Context for users data
	/// </summary>
	public class UsersContext : DbContext
	{
		/// <summary>
		/// User roles table
		/// </summary>
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Users data table
		/// </summary>
		public DbSet<User> Users { get; set; }

		public UsersContext(DbContextOptions<UsersContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
