using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetVideocardsRequest
    {
        public string[]   Company         { get; set; }
	    public string[]   Series          { get; set; }
	    public string[]   Proccessor      { get; set; }
	    public string[]   VRAM            { get; set; }
        public int[]      Capacity        { get; set; }
	    public string[]   Connector       { get; set; }
	    public string[]   Family          { get; set; }

        public int? SelectedBody { get; private set; }
        public int? SelectedCharger { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetVideocardsRequest(string[] company, string[] series, string[] proccessor, string[] vRAM, int[] capacity, 
            string[] connector, string[] family, int? selectedCharger, int? selectedBody)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Company.Length; i++)
                {
                    con.Add($"Company=@company{i}");
                    Parameters.Add(new SqlParameter($"@company{i}", Company[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Series = series;
            if (Series != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Series.Length; i++)
                {
                    con.Add($"Series=@series{i}");
                    Parameters.Add(new SqlParameter($"@series{i}", Series[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Proccessor = proccessor;
            if (Proccessor != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Proccessor.Length; i++)
                {
                    con.Add($"GraphicalProccessor=@proccessor{i}");
                    Parameters.Add(new SqlParameter($"@proccessor{i}", Proccessor[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            VRAM = vRAM;
            if (VRAM != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < VRAM.Length; i++)
                {
                    con.Add($"VRAM=@vRAM{i}");
                    Parameters.Add(new SqlParameter($"@vRAM{i}", VRAM[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Capacity = capacity;
            if (Capacity != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Capacity.Length; i++)
                {
                    con.Add($"Capacity=@capacity{i}");
                    Parameters.Add(new SqlParameter($"@capacity{i}", Capacity[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Connector = connector;
            if (Connector != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Connector.Length; i++)
                {
                    con.Add($"ID IN (SELECT DISTINCT VideocardID FROM VIDEOCARD_CONNECTOR WHERE Connector = @connector{i})");
                    Parameters.Add(new SqlParameter($"@connector{i}", Connector[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Family = family;
            if (Family != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Family.Length; i++)
                {
                    con.Add($"Family=@family{i}");
                    Parameters.Add(new SqlParameter($"@family{i}", Family[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            SelectedCharger = selectedCharger;
            if (SelectedCharger != null)
            {
                cond.Add("Pin = (SELECT TOP 1 VideocardConnector FROM CHARGER WHERE ID = @cID)");
                Parameters.Add(new SqlParameter("@cID", SelectedCharger));
            }

            SelectedBody = selectedBody;
            if (SelectedBody != null)
            {
                cond.Add("Length <= (SELECT TOP 1 VideocardMaxLength FROM BODY WHERE ID = @bID)");
                Parameters.Add(new SqlParameter("@bID", SelectedBody));
            }

            if (cond.Count > 0)
            {
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
