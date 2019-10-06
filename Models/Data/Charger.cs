﻿namespace ComputerComplectorWebAPI.Models.Data
{
	public class Charger
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Company { get; set; }
		public string Series { get; set; }
		public int Power { get; set; }
		public string Sertificate80Plus { get; set; }
		public int VideoConnectorsAmount { get; set; }
		public string VideocardConnector { get; set; }
		public int SATAAmount { get; set; }
		public int IDEAmount { get; set; }
		public string MotherboardConnector { get; set; }
		public string ConnectorType { get; set; }
	}
}
