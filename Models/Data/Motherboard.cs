using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class Motherboard
    {
        public int          ID                  { get; set; }
	    public string       Title               { get; set; }
        public string       Company             { get; set; }
        public string       Series              { get; set; }
	    public string       Socket              { get; set; }
	    public string       Chipset             { get; set; }
        public string       CPUCompany          { get; set; }
	    public string       Formfactor          { get; set; }
        public string       MemoryType          { get; set; }
        public int          MemorySlotsAmount   { get; set; }
        public int          MemoryChanelsAmount { get; set; }
        public int          MaxMemory           { get; set; }
        public int          RAMMaxFreq          { get; set; }
        public List<string> Slots               { get; set; }
        public string       Additions           { get; set; }
        public string       Pin                 { get; set; }
        public string       CPUPin              { get; set; }
    }
}
