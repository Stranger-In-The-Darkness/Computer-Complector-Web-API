using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class Body
    {
        public int      ID                  { get; set; }
        public string   Title               { get; set; }
        public string   Company             { get; set; }
        public string   Formfactor          { get; set; }
        public string   Type                { get; set; }
        public bool     BuildInCharger      { get; set; }
        public int      ChargerPower        { get; set; }
        public string   Color               { get; set; }
		public int      USB2Ports           { get; set; }
        public int      USB3Ports           { get; set; }
        public string   Additions           { get; set; }
        public int      VideocardMaxLength  { get; set; }
    }
}
