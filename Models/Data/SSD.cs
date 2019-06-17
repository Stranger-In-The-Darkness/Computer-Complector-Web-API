using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class SSD
    {
        public int          ID          { get; set; }
	    public string       Title       { get; set; }
	    public string       Company     { get; set; }
	    public string       Series      { get; set; }
	    public int          Capacity    { get; set; }
	    public string       Formfactor  { get; set; }
	    public List<string> Interface   { get; set; }
	    public string       CellType    { get; set; }
    }
}
