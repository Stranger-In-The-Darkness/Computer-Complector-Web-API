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
	[Table("CHARGER")]
    public class Charger
    { 
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int      ID                      { get; set; }
		[Required]
		public string   Title                   { get; set; }
		[Required]
		public string   Company                 { get; set; }
        public string   Series                  { get; set; }
		[Required]
		public int      Power                   { get; set; }
        public string   Sertificate80Plus       { get; set; }
		[Required]
		public int      VideoConnectorsAmount   { get; set; }
		[Required]
		public string   VideocardConnector      { get; set; }
		[Required]
		public int      SATAAmount              { get; set; }
		[Required]
		public int      IDEAmount               { get; set; }
		[Required]
		public string   MotherboardConnector    { get; set; }
        public string   ConnectorType           { get; set; }

		public static implicit operator DATA.Charger(Charger charger)
		{
			return new DATA.Charger()
			{
				Company = charger.Company,
				ConnectorType = charger.ConnectorType,
				ID = charger.ID,
				IDEAmount = charger.IDEAmount,
				MotherboardConnector = charger.MotherboardConnector,
				Power = charger.Power,
				SATAAmount = charger.SATAAmount,
				Series = charger.Series,
				Sertificate80Plus = charger.Sertificate80Plus,
				Title = charger.Title,
				VideocardConnector = charger.VideocardConnector,
				VideoConnectorsAmount = charger.VideoConnectorsAmount
			};
		}

		public static implicit operator Charger(DATA.Charger charger)
		{
			return new Charger()
			{
				Company = charger.Company,
				ConnectorType = charger.ConnectorType,
				ID = charger.ID,
				IDEAmount = charger.IDEAmount,
				MotherboardConnector = charger.MotherboardConnector,
				Power = charger.Power,
				SATAAmount = charger.SATAAmount,
				Series = charger.Series,
				Sertificate80Plus = charger.Sertificate80Plus,
				Title = charger.Title,
				VideocardConnector = charger.VideocardConnector,
				VideoConnectorsAmount = charger.VideoConnectorsAmount
			};
		}

		public void CopyParameters(Charger charger)
		{
			Company = charger.Company;
			ConnectorType = charger.ConnectorType;
			IDEAmount = charger.IDEAmount;
			MotherboardConnector = charger.MotherboardConnector;
			Power = charger.Power;
			SATAAmount = charger.SATAAmount;
			Series = charger.Series;
			Sertificate80Plus = charger.Sertificate80Plus;
			Title = charger.Title;
			VideocardConnector = charger.VideocardConnector;
			VideoConnectorsAmount = charger.VideoConnectorsAmount;
		}
	}
}
