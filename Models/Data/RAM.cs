using System;
using System.Collections.Generic;

namespace ComputerComplectorWebAPI.Models.Data
{
	[Serializable]
    public class RAM
    {
        public int							ID				{ get; set; }
	    public string						Title			{ get; set; }
        public string						Company			{ get; set; }
	    public string						Series			{ get; set; }
	    public string						MemoryType		{ get; set; }
	    public string						Purpose			{ get; set; }
	    public int							Volume			{ get; set; }
        public int							ModuleAmount	{ get; set; }
        public int							Freq			{ get; set; }
	    public string						CL				{ get; set; }
		public bool							Compatible		{ get; set; } = true;
		public Dictionary<string, string>	Incompatible	{ get; set; }
	}
}
