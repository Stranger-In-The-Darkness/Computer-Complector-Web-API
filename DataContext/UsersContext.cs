using Microsoft.EntityFrameworkCore;
using ComputerComplectorWebAPI.Models.Users;

namespace ComputerComplectorWebAPI.DataContext
{
	public class UsersContext : DbContext
	{
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }

		public UsersContext(DbContextOptions<UsersContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
