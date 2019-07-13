using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetSSDsRequest
    {
        public string[]   Company     { get; set; }
	    public string[]   Series      { get; set; }
	    public string[]   Capacity    { get; set; }
	    public string[]   Formfactor  { get; set; }
	    public string[]   Interface   { get; set; }
	    public string[]   CellType    { get; set; }

        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetSSDsRequest(string[] company, string[] series, string[] capacity, string[] formfactor, string[] @interface, 
            string[] cellType, int? selectedMotherboard)
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

            Capacity = capacity;
            if (Capacity != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Capacity.Length; i++)
                {
                    if (Capacity[i].Contains('+'))
                    {
                        con.Add($"Volume>@capacity{i}");
                        Parameters.Add(new SqlParameter($"@capacity{i}", Capacity[i]));
                    }
                    else
                    {
                        con.Add($"Volume=@capacity{i}");
                        Parameters.Add(new SqlParameter($"@capacity{i}", Capacity[i]));
                    }
                }
                cond.Add(string.Join(" OR ", con));
            }

            Formfactor = formfactor;
            if (Formfactor != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Formfactor.Length; i++)
                {
                    con.Add($"Formfactor=@formfactor{i}");
                    Parameters.Add(new SqlParameter($"@formfactor{i}", Formfactor[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Interface = @interface;
            if (Interface != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Interface.Length; i++)
                {
                    con.Add($"ID IN (SELECT DISTINCT SSDID FROM SSD_INTERFACE WHERE Interface=@interface{i})");
                    Parameters.Add(new SqlParameter($"@interface{i}", Interface[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            CellType = cellType;
            if (CellType != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < CellType.Length; i++)
                {
                    con.Add($"CellType=@cellType{i}");
                    Parameters.Add(new SqlParameter($"@cellType{i}", CellType[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("NOT (SELECT Count(Interface) FROM SSD_INTERFACE WHERE SSDID = ID AND Interface IN (SELECT DISTINCT (SELECT TOP 1 Value FROM string_split(SLOT, ' ')) FROM MOTHERBOARD_SLOTS WHERE MotherboardID = @motherboardID)) = 0");
                Parameters.Add(new SqlParameter("@motherboardID", SelectedMotherboard));
            }

            if (cond.Count > 0)
            {
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
