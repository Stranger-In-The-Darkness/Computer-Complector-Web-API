using ComputerComplectorWebAPI.EntityFramework.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerComplectorWebAPI.DataContext
{
	/// <summary>
	/// DB Context for components data
	/// </summary>
	public class ComponentsContext : DbContext
	{
		/// <summary>
		/// Bodies table
		/// </summary>
		public DbSet<Body> Bodies { get; set; }

		/// <summary>
		/// Bodies formfactors table
		/// </summary>
		public DbSet<BodyFormfactor> BodyFormfactor { get; set; }

		/// <summary>
		/// Chargers table
		/// </summary>
		public DbSet<Charger> Chargers { get; set; }

		/// <summary>
		/// Coolers table
		/// </summary>
		public DbSet<Cooler> Coolers { get; set; }

		/// <summary>
		/// Coolers sockets table
		/// </summary>
		public DbSet<CoolerSocket> CoolerSockets { get; set; }

		/// <summary>
		/// CPUs table
		/// </summary>
		public DbSet<CPU> CPUs { get; set; }

		/// <summary>
		/// HDDs table
		/// </summary>
		public DbSet<HDD> HDDs { get; set; }

		/// <summary>
		/// HDDs interfaces table
		/// </summary>
		public DbSet<HDDInterface> HDDInterfaces { get; set; }

		/// <summary>
		/// Motherboards table
		/// </summary>
		public DbSet<Motherboard> Motherboards { get; set; }

		/// <summary>
		/// Motherboards slots table
		/// </summary>
		public DbSet<MotherboardSlot> MotherboardSlots { get; set; }

		/// <summary>
		/// RAMs table
		/// </summary>
		public DbSet<RAM> RAMs { get; set; }

		/// <summary>
		/// SSDs table
		/// </summary>
		public DbSet<SSD> SSDs { get; set; }

		/// <summary>
		/// SSDs interfaces table
		/// </summary>
		public DbSet<SSDInterface> SSDInterfaces { get; set; }

		/// <summary>
		/// Videocards table
		/// </summary>
		public DbSet<Videocard> Videocards { get; set; }

		/// <summary>
		/// Videocards connectors table
		/// </summary>
		public DbSet<VideocardConnector> VideocardConnectors { get; set; }

		/// <summary>
		/// Properties table
		/// </summary>
		public DbSet<Property> Properties { get; set; }

		/// <summary>
		/// Propety values table
		/// </summary>
		public DbSet<PropertyValue> PropertyValues { get; set; }

		public ComponentsContext(DbContextOptions<ComponentsContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
