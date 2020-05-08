using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("HDD")]
	public class HDD
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Title { get; set; }
		public string Series { get; set; }
		[Required]
		public string Company { get; set; }
		[Required]
		public string Formfactor { get; set; }
		[Required]
		public int Capacity { get; set; }
		public ICollection<HDDInterface> Interface { get; set; }
		[Required]
		public int BufferVolume { get; set; }
		[Required]
		public int Speed { get; set; }

		public static implicit operator DATA.HDD(HDD hdd)
		{
			return new DATA.HDD()
			{
				Company = hdd.Company?.Trim(),
				ID = hdd.ID,
				Title = hdd.Title?.Trim(),
				BufferVolume = hdd.BufferVolume,
				Capacity = hdd.Capacity,
				Formfactor = hdd.Formfactor?.Trim(),
				Interface = hdd.Interface?.Select(e => e.Interface.Trim()).ToList(),
				Series = hdd.Series?.Trim(),
				Speed = hdd.Speed
			};
		}

		public static implicit operator HDD(DATA.HDD hdd)
		{
			var el = new HDD()
			{
				Company = hdd.Company,
				ID = hdd.ID,
				Title = hdd.Title,
				BufferVolume = hdd.BufferVolume,
				Capacity = hdd.Capacity,
				Formfactor = hdd.Formfactor,
				Series = hdd.Series,
				Speed = hdd.Speed
			};

			el.Interface = hdd.Interface?.Select(e => new HDDInterface() { HDD = el, Interface = e }).ToList();
			return el;
		}

		public void CopyParameters(HDD hdd)
		{
			Title = hdd.Title;
			Series = hdd.Series;
			Company = hdd.Company;
			Formfactor = hdd.Formfactor;
			Capacity = hdd.Capacity;
			BufferVolume = hdd.BufferVolume;
			Speed = hdd.Speed;
	}
	}
}
