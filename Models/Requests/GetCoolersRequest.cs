using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetCoolersRequest
    {
	    public string   Company             { get; private set; }
	    public string   Purpose             { get; private set; }
	    public string   Type                { get; private set; }       
	    public string   Socket              { get; private set; }
	    public string   Material            { get; private set; }
	    public string   VentDiam            { get; private set; }
	    public string   TurnAdj             { get; private set; }

        public int?     SelectedCpu         { get; private set; }
        public int?     SelectedMotherboard { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetCoolersRequest(string company, string purpose, string type, string socket, string material, string ventDiam, 
            string turnAdj, int? selectedCpu, int? selectedMotherboard)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                cond.Add("Company=@company");
                Parameters.Add(new SqlParameter("@company", Company));
            }

            Purpose = purpose;
            if (Purpose != null)
            {
                cond.Add("Purpose=@purpose");
                Parameters.Add(new SqlParameter("@purpose", Purpose));
            }

            Type = type;
            if (Type != null)
            {
                cond.Add("Type=@type");
                Parameters.Add(new SqlParameter("@type", Type));
            }

            Socket = socket;
            if (Socket != null)
            {
                cond.Add("ID IN (SELECT CoolerID FROM COOLER_SOCKET WHERE Socket = @socket)");
                Parameters.Add(new SqlParameter("@socket", Socket));
            }

            Material = material;
            if (Material != null)
            {
                cond.Add("Material=@material");
                Parameters.Add(new SqlParameter("@material", Material));
            }

            VentDiam = ventDiam;
            if (VentDiam != null)
            {
                if (VentDiam.Contains('-'))
                {
                    cond.Add("Diameter>=@ventDiam1 AND Diameter<=@ventDiam2");
                    Parameters.Add(new SqlParameter("@ventDiam1", VentDiam.Split('-')[0]));
                    Parameters.Add(new SqlParameter("@ventDiam2", VentDiam.Split('-')[1]));
                }
                else if (VentDiam.Contains('+'))
                {
                    cond.Add("Diameter>@ventDiam");
                    Parameters.Add(new SqlParameter("@ventDiam", VentDiam.Replace("+", "")));
                }
                else
                {
                    cond.Add("Diameter=@ventDiam");
                    Parameters.Add(new SqlParameter("@ventDiam", VentDiam));
                }
            }

            TurnAdj = turnAdj;
            if (TurnAdj != null)
            {
                cond.Add("Adjustement=@turnAdj");
                Parameters.Add(new SqlParameter("@turnAdj", TurnAdj));
            }

            SelectedCpu = selectedCpu;
            if (SelectedCpu != null)
            {
                cond.Add("(SELECT TOP 1 Socket FROM CPU c WHERE c.ID = @cpuID) in (SELECT Socket FROM COOLER_SOCKET WHERE CoolerID = ID)");
                Parameters.Add(new SqlParameter("@cpuID", SelectedCpu.Value));
            }

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("(SELECT TOP 1 Socket FROM MOTHERBOARD WHERE ID = @motherboardID) in (SELECT Socket FROM COOLER_SOCKET WHERE CoolerID = ID)");
                Parameters.Add(new SqlParameter("@motherboardID", SelectedMotherboard.Value));
            }

            if (cond.Count > 0)
            {
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
