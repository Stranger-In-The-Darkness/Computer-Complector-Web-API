using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("PROPERTY")]
	public class Property
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Component { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public bool ShowDescription { get; set; }
		public string Description { get; set; }
		public ICollection<PropertyValue> Values { get; set; }

		public static implicit operator DATA.Property(Property p)
		{
			if (p == null) return null;
			return new DATA.Property()
			{
				Description = p.Description,
				Name = p.Name,
				ShowDescription = p.ShowDescription,
				Values = p.Values.Any() ? p.Values.Select(e => e.Value).ToList() : new List<string>(),
				Component = p.Component,
				Text = p.Text
			};
		}
	}
}