using ComputerComplectorWebAPI.EntityFramework.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerComplectorWebAPI.DataContext
{
	public class ComponentsContext : DbContext
	{
		public DbSet<Body> Bodies { get; set; }
		public DbSet<Charger> Chargers { get; set; }
		public DbSet<Cooler> Coolers { get; set; }
		public DbSet<CoolerSocket> CoolerSockets { get; set; }
		public DbSet<CPU> CPUs { get; set; }
		public DbSet<HDD> HDDs { get; set; }
		public DbSet<HDDInterface> HDDInterfaces { get; set; }
		public DbSet<Motherboard> Motherboards { get; set; }
		public DbSet<MotherboardSlot> MotherboardSlots { get; set; }
		public DbSet<RAM> RAMs { get; set; }
		public DbSet<SSD> SSDs { get; set; }
		public DbSet<SSDInterface> SSDInterfaces { get; set; }
		public DbSet<Videocard> Videocards { get; set; }
		public DbSet<VideocardConnector> VideocardConnectors { get; set; }

		public ComponentsContext(DbContextOptions<ComponentsContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
