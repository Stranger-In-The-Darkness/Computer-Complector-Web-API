namespace ComputerComplectorWebAPI.Models.Data
{
	public class Body
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Company { get; set; }
		public string Formfactor { get; set; }
		public string Type { get; set; }
		public bool BuildInCharger { get; set; }
		public int ChargerPower { get; set; }
		public string Color { get; set; }
		public int USB2Amount { get; set; }
		public int USB3Amount { get; set; }
		public int VideocardMaxLength { get; set; }
		public string Additions { get; set; }
	}
}
