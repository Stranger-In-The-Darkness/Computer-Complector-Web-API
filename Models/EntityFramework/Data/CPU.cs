using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("CPU")]
	public class CPU
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Company { get; set; }
		[Required]
		public string Series { get; set; }
		[Required]
		public string Socket { get; set; }
		[Required]
		public double Frequency { get; set; }
		[Required]
		public int AmountOfCores { get; set; }
		[Required]
		public int AmountOfThreads { get; set; }
		[Required]
		public bool IntegratedGraphics { get; set; }
		[Required]
		public string Core { get; set; }
		[Required]
		public string DeliveryType { get; set; }
		[Required]
		public bool Overclocking { get; set; }

		public static implicit operator DATA.CPU(CPU cpu)
		{
			return new DATA.CPU()
			{
				Company = cpu.Company?.Trim(),
				Core = cpu.Core?.Trim(),
				CoresAmount = cpu.AmountOfCores,
				DeliveryType = cpu.DeliveryType?.Trim(),
				Frequency = cpu.Frequency,
				ID = cpu.ID,
				IntegratedGraphics = cpu.IntegratedGraphics,
				Overcloacking = cpu.Overclocking,
				Series = cpu.Series?.Trim(),
				Socket = cpu.Socket?.Trim(),
				ThreadsAmount = cpu.AmountOfThreads,
				Title = cpu.Title?.Trim()
			};
		}

		public static implicit operator CPU(DATA.CPU cpu)
		{
			return new CPU()
			{
				Company = cpu.Company,
				Core = cpu.Core,
				AmountOfCores = cpu.CoresAmount,
				DeliveryType = cpu.DeliveryType,
				Frequency = cpu.Frequency,
				ID = cpu.ID,
				IntegratedGraphics = cpu.IntegratedGraphics,
				Overclocking = cpu.Overcloacking,
				Series = cpu.Series,
				Socket = cpu.Socket,
				AmountOfThreads = cpu.ThreadsAmount,
				Title = cpu.Title
			};
		}

		public void CopyParameters(CPU cpu)
		{
			Title = cpu.Title;
			Company = cpu.Company;
			Series = cpu.Series;
			Socket = cpu.Socket;
			Frequency = cpu.Frequency;
			AmountOfCores = cpu.AmountOfCores;
			AmountOfThreads = cpu.AmountOfThreads;
			IntegratedGraphics = cpu.IntegratedGraphics;
			Core = cpu.Core;
			DeliveryType = cpu.DeliveryType;
			Overclocking = cpu.Overclocking;
	}
	}
}