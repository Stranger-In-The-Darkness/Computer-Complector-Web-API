using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using DATA = ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.EntityFramework.Models.Data
{
	[Table("BODY")]
	public class Body
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int		ID					{ get; set; }
		[Required]
		public string	Title				{ get; set; }
		[Required]
		public string	Company				{ get; set; }
		public ICollection<BodyFormfactor> Formfactor { get; set; }
		//[Required]
		//public string	Formfactor			{ get; set; }
		[Required]
		public string	Type				{ get; set; }
		[Required]
		[Column("Build-inCharger")]
		public bool		BuildInCharger		{ get; set; }
		[Required]
		public int		ChargerPower		{ get; set; }
		[Required]
		public string	Color				{ get; set; }
		[Required]
		[Column("USB2.0Amount")]
		public int		USB2Amount			{ get; set; }
		[Required]
		[Column("USB3.0Amount")]
		public int		USB3Amount			{ get; set; }
		[Required]
		public double		VideocardMaxLength	{ get; set; }
		public string	Additions			{ get; set; }

		public static implicit operator DATA.Body(Body body)
		{
			return new DATA.Body()
			{
				Additions = body.Additions?.Trim(),
				BuildInCharger = body.BuildInCharger,
				ChargerPower = body.ChargerPower,
				Color = body.Color?.Trim(),
				Company = body.Company?.Trim(),
				Formfactor = body.Formfactor?.Select(e => e.Formfactor.Trim()).ToList(),
				ID = body.ID,
				Title = body.Title?.Trim(),
				Type = body.Type?.Trim(),
				USB2Amount = body.USB2Amount,
				USB3Amount = body.USB3Amount,
				VideocardMaxLength = body.VideocardMaxLength
			};
		}

		public static implicit operator Body(DATA.Body body)
		{
			var e = new Body()
			{
				Additions = body.Additions,
				BuildInCharger = body.BuildInCharger,
				ChargerPower = body.ChargerPower,
				Color = body.Color,
				Company = body.Company,
				ID = body.ID,
				Title = body.Title,
				Type = body.Type,
				USB2Amount = body.USB2Amount,
				USB3Amount = body.USB3Amount,
				VideocardMaxLength = body.VideocardMaxLength
			};

			e.Formfactor = body.Formfactor?.Select(f => new BodyFormfactor() { Body = e, Formfactor = f }).ToList();
			return e;
		}

		public void CopyParameters(Body body)
		{
			Additions = body.Additions;
			BuildInCharger = body.BuildInCharger;
			ChargerPower = body.ChargerPower;
			Color = body.Color;
			Company = body.Company;
			Formfactor = body.Formfactor;
			Title = body.Title;
			Type = body.Type;
			USB2Amount = body.USB2Amount;
			USB3Amount = body.USB3Amount;
			VideocardMaxLength = body.VideocardMaxLength;
		}
	}
}
