using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("RAM")]
    public class RAM
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int      ID				{ get; set; }
		[Required]
		public string   Title			{ get; set; }
		[Required]
		public string   Company			{ get; set; }
	    public string   Series			{ get; set; }
		[Required]
		public string   MemoryType		{ get; set; }
		[Required]
		public string   Purpose			{ get; set; }
		[Required]
		public int      MemoryVolume	{ get; set; }
		[Required]
		public int      ModulesAmount	{ get; set; }
		[Required]
		public int      Frequency		{ get; set; }
		[Required]
		public string   CASLatency		{ get; set; }

		public static implicit operator DATA.RAM(RAM ram)
		{
			return new DATA.RAM()
			{
				Company = ram.Company?.Trim(),
				ID = ram.ID,
				Purpose = ram.Purpose?.Trim(),
				Title = ram.Title?.Trim(),
				CL = ram.CASLatency?.Trim(),
				Freq = ram.Frequency,
				MemoryType = ram.MemoryType?.Trim(),
				ModuleAmount = ram.ModulesAmount,
				Series = ram.Series?.Trim(),
				Volume = ram.MemoryVolume
			};
		}

		public static implicit operator RAM(DATA.RAM ram)
		{
			return new RAM()
			{
				Company = ram.Company,
				ID = ram.ID,
				Purpose = ram.Purpose,
				Title = ram.Title,
				CASLatency = ram.CL,
				Frequency = ram.Freq,
				MemoryType = ram.MemoryType,
				ModulesAmount = ram.ModuleAmount,
				Series = ram.Series,
				MemoryVolume = ram.Volume
			};
		}

		public void CopyParameters(RAM ram)
		{
			Title = ram.Title;
			Company = ram.Company;
			Series = ram.Series;
			MemoryType = ram.MemoryType;
			Purpose = ram.Purpose;
			MemoryVolume = ram.MemoryVolume;
			ModulesAmount = ram.ModulesAmount;
			Frequency = ram.Frequency;
			CASLatency = ram.CASLatency;
	}
	}
}
