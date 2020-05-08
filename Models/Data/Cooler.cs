using System;
using System.Collections.Generic;

namespace ComputerComplectorWebAPI.Models.Data
{
	[Serializable]
	public class Cooler
    {
        public int							ID				{ get; set; }
        public string						Title			{ get; set; }
	    public string						Company			{ get; set; }
	    public string						Purpose			{ get; set; }
	    public string						Type			{ get; set; }   
	    public List<string>					Socket			{ get; set; }
	    public string						Material		{ get; set; }
	    public double?						VentDiam		{ get; set; }
	    public bool?						TurnAdj			{ get; set; }
	    public string						Color			{ get; set; }
        public string						Connector		{ get; set; }
		public bool							Compatible		{ get; set; } = true;
		public Dictionary<string, string>	Incompatible	{ get; set; }
	}
}