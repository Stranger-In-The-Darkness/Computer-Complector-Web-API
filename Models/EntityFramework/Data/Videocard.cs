using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("VIDEOCARD")]
    public class Videocard
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int          ID						{ get; set; }
		[Required]
		public string       Title					{ get; set; }
		[Required]
		public string       Company					{ get; set; }
	    public string       Series					{ get; set; }
		[Required]
		public string       GraphicalProccessor		{ get; set; }
		[Required]
		public int          VRAM					{ get; set; }
		[Required]
		public int          Capacity				{ get; set; }
		[Required]
		public string       Memory					{ get; set; }
	    public ICollection<VideocardConnector> Connectors	{ get; set; }
		[Required]
		public string       Family					{ get; set; }
		[Required]
		public string       Length					{ get; set; }
		[Required]
		public string       Pin						{ get; set; }

		public static implicit operator DATA.Videocard(Videocard videocard)
		{
			return new DATA.Videocard()
			{
				Company = videocard.Company,
				ID = videocard.ID,
				Title = videocard.Title,
				Capacity = videocard.Capacity,
				Connectors = videocard.Connectors.Select(e => e.Connector).ToList(),
				Family = videocard.Family,
				Length = videocard.Length,
				Memory = videocard.Memory,
				Pin = videocard.Pin,
				Proccessor = videocard.GraphicalProccessor,
				Series = videocard.Series,
				VRAM = videocard.VRAM
			};
		}

		public static implicit operator Videocard(DATA.Videocard videocard)
		{
			var el = new Videocard()
			{
				Company = videocard.Company,
				ID = videocard.ID,
				Title = videocard.Title,
				Capacity = videocard.Capacity,
				Family = videocard.Family,
				Length = videocard.Length,
				Memory = videocard.Memory,
				Pin = videocard.Pin,
				GraphicalProccessor = videocard.Proccessor,
				Series = videocard.Series,
				VRAM = videocard.VRAM
			};
			el.Connectors = videocard.Connectors.Select(e => new VideocardConnector() { Videocard = el, Connector = e }).ToList();
			return el;
		}

		public void CopyParameters(Videocard videocard)
		{
			Title = videocard.Title;
			Company = videocard.Company;
			Series = videocard.Series;
			GraphicalProccessor = videocard.GraphicalProccessor;
			VRAM = videocard.VRAM;
			Capacity = videocard.Capacity;
			Memory = videocard.Memory;
			Family = videocard.Family;
			Length = videocard.Length;
			Pin = videocard.Pin;
	}
	}
}
