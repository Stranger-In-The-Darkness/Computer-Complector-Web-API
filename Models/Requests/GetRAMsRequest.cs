using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetRAMsRequest
    {
        public string   Company      { get; set; }
	    public string   Series       { get; set; }
	    public string   Memory       { get; set; }
	    public string   Type         { get; set; }
	    public int?     Volume       { get; set; }
        public int?     ModuleAmount { get; set; }
        public int?     Freq         { get; set; }
	    public string   CL           { get; set; }

        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetRAMsRequest(string company, string series, string memory, string type, int? volume, int? moduleAmount, 
            int? freq, string cL, int? selectedMotherboard)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                cond.Add("Company=@company");
                Parameters.Add(new SqlParameter("@company", Company));
            }

            Series = series;
            if (Series != null)
            {
                cond.Add("Series=@series");
                Parameters.Add(new SqlParameter("@series", Series));
            }

            Memory = memory;
            if (Memory != null)
            {
                cond.Add("MemoryType=@memory");
                Parameters.Add(new SqlParameter("@memory", Memory));
            }

            Type = type;
            if (Type != null)
            {
                cond.Add("Purpose=@type");
                Parameters.Add(new SqlParameter("@type", Type));
            }

            Volume = volume;
            if (Volume != null)
            {
                cond.Add("MemoryVolume=@volume");
                Parameters.Add(new SqlParameter("@volume", Volume));
            }

            ModuleAmount = moduleAmount;
            if (ModuleAmount != null)
            {
                cond.Add("ModulesCount=@moduleAmount");
                Parameters.Add(new SqlParameter("@moduleAmount", ModuleAmount));
            }

            Freq = freq;
            if (Freq != null)
            {
                cond.Add("Frequency=@freq");
                Parameters.Add(new SqlParameter("@freq", Freq));
            }

            CL = cL;
            if (CL != null)
            {
                cond.Add("CASLatency=@cL");
                Parameters.Add(new SqlParameter("@cL", CL));
            }

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("MemoryType = (SELECT TOP 1 MemoryType FROM MOTHERBOARD WHERE ID = @motherboardID)");
                Parameters.Add(new SqlParameter("@motherboardID", SelectedMotherboard));
            }

            if (cond.Count > 0)
            {
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
