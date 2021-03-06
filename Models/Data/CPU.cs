﻿using System;
using System.Collections.Generic;

namespace ComputerComplectorWebAPI.Models.Data
{
	[Serializable]
	public class CPU
    {
        public int							ID                  { get; set; }
        public string						Title               { get; set; }
        public string						Company             { get; set; }
        public string						Series              { get; set; }
        public string						Socket              { get; set; }
        public double						Frequency           { get; set; }
        public int							CoresAmount         { get; set; }
        public int							ThreadsAmount       { get; set; }
        public bool							IntegratedGraphics  { get; set; }
        public string						Core                { get; set; }
        public string						DeliveryType        { get; set; }
        public bool							Overcloacking       { get; set; }
		public bool							Compatible			{ get; set; } = true;
		public Dictionary<string, string>	Incompatible		{ get; set; }
	}
}
