using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("MOTHERBOARD")]
    public class Motherboard
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int          ID                    { get; set; }
		[Required]
		public string       Title                 { get; set; }
		[Required]
		public string       Company               { get; set; }
        public string       Series                { get; set; }
		[Required]
		public string       Socket                { get; set; }
		[Required]
		public string       Chipset               { get; set; }
        public string       CPUCompany            { get; set; }
		[Required]
		public string       Formfactor            { get; set; }
		[Required]
		public string       MemoryType            { get; set; }
		[Required]
		public int          AmountOfMemorySlots   { get; set; }
		[Required]
		public int          AmountOfMemoryChanels { get; set; }
		[Required]
		public int          MaximumMemory         { get; set; }
		[Required]
		public int          MaximumRAMFrequency   { get; set; }
        public ICollection<MotherboardSlot> Slots { get; set; }
		[Required]
		public string       Pin                   { get; set; }
		[Required]
		public string       CPUPin                { get; set; }

		public static implicit operator DATA.Motherboard(Motherboard motherboard)
		{
			return new DATA.Motherboard()
			{
				Company = motherboard.Company?.Trim(),
				ID = motherboard.ID,
				Socket = motherboard.Socket?.Trim(),
				Title = motherboard.Title?.Trim(),
				Chipset = motherboard.Chipset?.Trim(),
				CPUCompany = motherboard.CPUCompany?.Trim(),
				CPUPin = motherboard.CPUPin?.Trim(),
				Formfactor = motherboard.Formfactor?.Trim(),
				MaxMemory = motherboard.MaximumMemory,
				MemoryChanelsAmount = motherboard.AmountOfMemoryChanels,
				MemorySlotsAmount = motherboard.AmountOfMemorySlots,
				MemoryType = motherboard.MemoryType?.Trim(),
				Pin = motherboard.Pin?.Trim(),
				RAMMaxFreq = motherboard.MaximumRAMFrequency,
				Series = motherboard.Series?.Trim(),
				Slots = motherboard.Slots?.Select(e => e.Slot.Trim()).ToList()
			};
		}

		public static implicit operator Motherboard(DATA.Motherboard motherboard)
		{
			var el = new Motherboard()
			{
				Company = motherboard.Company,
				ID = motherboard.ID,
				Socket = motherboard.Socket,
				Title = motherboard.Title,
				Chipset = motherboard.Chipset,
				CPUCompany = motherboard.CPUCompany,
				CPUPin = motherboard.CPUPin,
				Formfactor = motherboard.Formfactor,
				MaximumMemory = motherboard.MaxMemory,
				AmountOfMemoryChanels = motherboard.MemoryChanelsAmount,
				AmountOfMemorySlots = motherboard.MemorySlotsAmount,
				MemoryType = motherboard.MemoryType,
				Pin = motherboard.Pin,
				MaximumRAMFrequency = motherboard.RAMMaxFreq,
				Series = motherboard.Series
			};
			el.Slots = motherboard.Slots?.Select(e => new MotherboardSlot() { Motherboard = el, Slot = e }).ToList();
			return el;
		}

		public void CopyParameters(Motherboard motherboard)
		{
			Title = motherboard.Title;
			Company = motherboard.Company;
			Series = motherboard.Series;
			Socket = motherboard.Socket;
			Chipset = motherboard.Chipset;
			CPUCompany = motherboard.CPUCompany;
			Formfactor = motherboard.Formfactor;
			MemoryType = motherboard.MemoryType;
			AmountOfMemorySlots = motherboard.AmountOfMemorySlots;
			AmountOfMemoryChanels = motherboard.AmountOfMemoryChanels;
			MaximumMemory = motherboard.MaximumMemory;
			MaximumRAMFrequency = motherboard.MaximumRAMFrequency;
			Pin = motherboard.Pin;
			CPUPin = motherboard.CPUPin;
	}
	}
}
