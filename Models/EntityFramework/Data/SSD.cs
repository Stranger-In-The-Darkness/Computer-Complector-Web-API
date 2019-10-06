using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("SSD")]
    public class SSD
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int							ID          { get; set; }
		[Required]
		public string						Title       { get; set; }
		[Required]	
		public string						Company     { get; set; }
	    public string						Series      { get; set; }
		[Required]
		public int							Capacity    { get; set; }
		[Required]
		public string						Formfactor  { get; set; }
	    public ICollection<SSDInterface>	Interface   { get; set; }
		[Required]
		public string						CellType    { get; set; }

		public static implicit operator DATA.SSD(SSD ssd)
		{
			return new DATA.SSD()
			{
				Company = ssd.Company,
				ID = ssd.ID,
				Title = ssd.Title,
				Capacity = ssd.Capacity,
				CellType = ssd.CellType,
				Formfactor = ssd.Formfactor,
				Interface = ssd.Interface.Select(e => e.Interface).ToList(),
				Series = ssd.Series
			};
		}
		public static implicit operator SSD(DATA.SSD ssd)
		{
			var el = new SSD()
			{
				Company = ssd.Company,
				ID = ssd.ID,
				Title = ssd.Title,
				Capacity = ssd.Capacity,
				CellType = ssd.CellType,
				Formfactor = ssd.Formfactor,
				Series = ssd.Series
			};

			el.Interface = ssd.Interface.Select(e => new SSDInterface() { SSD = el, Interface = e }).ToList();
			return el;
		}

		public void CopyParameters(SSD ssd)
		{
			Title = ssd.Title;
			Company = ssd.Company;
			Series = ssd.Series;
			Capacity = ssd.Capacity;
			Formfactor = ssd.Formfactor;
			CellType = ssd.CellType;
	}
	}
}
