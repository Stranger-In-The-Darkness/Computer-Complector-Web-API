using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Get
{
    public class GetCoolersRequest
    {
	    public string[]   Company             { get; private set; }
	    public string[]   Purpose             { get; private set; }
	    public string[]   Type                { get; private set; }       
	    public string[]   Socket              { get; private set; }
	    public string[]   Material            { get; private set; }
	    public string[]   VentDiam            { get; private set; }
	    public string[]   TurnAdj             { get; private set; }

        public int?     SelectedCpu         { get; private set; }
        public int?     SelectedMotherboard { get; private set; }

        public string Expression { get; } = "SELECT * FROM COOLER c JOIN COOLER_SOCKET cs on c.ID = cs.CoolerID";
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetCoolersRequest(string[] company, string[] purpose, string[] type, string[] socket, string[] material, string[] ventDiam, 
            string[] turnAdj, int? selectedCpu, int? selectedMotherboard)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                for (int i = 0; i < Company.Length; i++)
                {
                    cond.Add($"Company=@company{i}");
                    Parameters.Add(new SqlParameter($"@company{i}", Company[i]));
                }
            }

            Purpose = purpose;
            if (Purpose != null)
            {
                for (int i = 0; i < Purpose.Length; i++)
                {
                    cond.Add($"Purpose=@purpose{i}");
                    Parameters.Add(new SqlParameter($"@purpose{i}", Purpose[i]));
                }
            }

            Type = type;
            if (Type != null)
            {
                for (int i = 0; i < Type.Length; i++)
                {
                    cond.Add($"Type=@type{i}");
                    Parameters.Add(new SqlParameter($"@type{i}", Type[i]));
                }
            }

            Socket = socket;
            if (Socket != null)
            {
                for (int i = 0; i < Socket.Length; i++)
                {
                    cond.Add($"ID IN (SELECT CoolerID FROM COOLER_SOCKET WHERE Socket = @socket{i})");
                    Parameters.Add(new SqlParameter($"@socket{i}", Socket[i]));
                }
            }

            Material = material;
            if (Material != null)
            {
                for (int i = 0; i < Material.Length; i++)
                {
                    cond.Add($"Material=@material{i}");
                    Parameters.Add(new SqlParameter("@material{i}", Material[i]));
                }
            }

            VentDiam = ventDiam;
            if (VentDiam != null)
            {
                for (int i = 0; i < VentDiam.Length; i++)
                {
                    if (VentDiam[i].Contains('-'))
                    {
                        cond.Add($"Diameter>=@ventDiam1{i} AND Diameter<=@ventDiam2{i}");
                        Parameters.Add(new SqlParameter($"@ventDiam1{i}", VentDiam[i].Split('-')[0]));
                        Parameters.Add(new SqlParameter($"@ventDiam2{i}", VentDiam[i].Split('-')[1]));
                    }
                    else if (VentDiam[i].Contains('+'))
                    {
                        cond.Add($"Diameter>@ventDiam{i}");
                        Parameters.Add(new SqlParameter($"@ventDiam{i}", VentDiam[i].Replace("+", "")));
                    }
                    else
                    {
                        cond.Add($"Diameter=@ventDiam{i}");
                        Parameters.Add(new SqlParameter($"@ventDiam{i}", VentDiam[i]));
                    }
                }
            }

            TurnAdj = turnAdj;
            if (TurnAdj != null)
            {
                for (int i = 0; i < TurnAdj.Length; i++)
                {
                    cond.Add($"Adjustement=@turnAdj{i}");
                    Parameters.Add(new SqlParameter($"@turnAdj{i}", TurnAdj[i]));
                }
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
                Expression += $" WHERE {string.Join(" AND ", cond)}";
            }
        }
    }
}
