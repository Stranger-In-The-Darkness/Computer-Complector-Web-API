using System;
using System.Collections.Generic;

namespace ComputerComplectorWebAPI.Models.Data
{
	[Serializable]
    public class Videocard
    {
        public int							ID				{ get; set; }
	    public string						Title			{ get; set; }
        public string						Company			{ get; set; }
	    public string						Series			{ get; set; }
	    public string						Proccessor		{ get; set; }
	    public int							VRAM			{ get; set; }
	    public int							Capacity		{ get; set; }
        public string						Memory			{ get; set; }
	    public List<string>					Connectors		{ get; set; }
        public string						Family			{ get; set; }
        public double						Length			{ get; set; }
        public string						Pin				{ get; set; }
		public bool							Compatible		{ get; set; } = true;
		public Dictionary<string, string>	Incompatible	{ get; set; }
	}
}
