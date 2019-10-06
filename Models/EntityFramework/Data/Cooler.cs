#define ENTITY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("COOLER")]
	public class Cooler
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int							ID			{ get; set; }
		[Required]
		public string						Title		{ get; set; }
		[Required]
		public string						Company		{ get; set; }
		[Required]
		public string						Purpose		{ get; set; }
		[Required]
		public string						Type		{ get; set; }
		public ICollection<CoolerSocket>	Socket		{ get; set; }
		[Required]
		public string						Material	{ get; set; }
		public double?						Diameter	{ get; set; }
		public bool?						Adjustement { get; set; }
		public string						Color		{ get; set; }
		[Required]
		public string						Connector	{ get; set; }

		public static implicit operator DATA.Cooler(Cooler cooler)
		{
			return new DATA.Cooler()
			{
				Color = cooler.Color,
				Company = cooler.Company,
				Connector = cooler.Connector,
				ID = cooler.ID,
				Material = cooler.Material,
				Purpose = cooler.Purpose,
				Socket = cooler.Socket.Select(s => s.Socket).ToList(),
				Title = cooler.Title,
				TurnAdj = cooler.Adjustement,
				Type = cooler.Type,
				VentDiam = cooler.Diameter
			};
		}

		public static implicit operator Cooler(DATA.Cooler cooler)
		{
			var e = new Cooler()
			{
				Color = cooler.Color,
				Company = cooler.Company,
				Connector = cooler.Connector,
				ID = cooler.ID,
				Material = cooler.Material,
				Purpose = cooler.Purpose,
				Title = cooler.Title,
				Adjustement = cooler.TurnAdj,
				Type = cooler.Type,
				Diameter = cooler.VentDiam
			};
			e.Socket = cooler.Socket.Select(s => new CoolerSocket() { Cooler = e, Socket = s }).ToList();
			return e;
		}

		public void CopyParameters(Cooler cooler)
		{
			Title = cooler.Title;
			Company = cooler.Company;
			Purpose = cooler.Purpose;
			Type = cooler.Type;
			Material = cooler.Material;
			Diameter = cooler.Diameter;
			Adjustement = cooler.Adjustement;
			Color = cooler.Color;
			Connector = cooler.Connector;
		}
	}
}
