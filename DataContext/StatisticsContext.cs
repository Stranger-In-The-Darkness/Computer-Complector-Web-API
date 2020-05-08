using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Analytics;
using Microsoft.EntityFrameworkCore;

namespace ComputerComplectorWebAPI.DataContext
{
	/// <summary>
	/// DB Context for statistics data
	/// </summary>
	public class StatisticsContext : DbContext
	{
		/// <summary>
		/// Recommendations table
		/// </summary>
		public DbSet<Selection> Selections { get; set; }
		
		/// <summary>
		/// Properties table
		/// </summary>
		public DbSet<Property> Properties { get; set; }

		/// <summary>
		/// Properties values table
		/// </summary>
		public DbSet<PropertyValue> PropertyValues { get; set; }

		public DbSet<SelectionProperties> SelectionProperties { get; set; }

		public StatisticsContext(DbContextOptions<StatisticsContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
