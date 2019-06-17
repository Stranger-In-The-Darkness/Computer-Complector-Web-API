using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class HDD
    {
        public int                 ID              { get; set; }
	    public string              Title           { get; set; }
	    public string              Company         { get; set; }
	    public string              Formfactor      { get; set; }
	    public int                 Capacity        { get; set; }
	    public List<string>        Interface       { get; set; }
	    public int                 BufferVolume    { get; set; }
	    public int                 Speed           { get; set; }
    }
}
