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
        public string   Company     { get; set; }
	    public string   Series      { get; set; }
	    public string   Capacity    { get; set; }
	    public string   Formfactor  { get; set; }
	    public string   Interface   { get; set; }
	    public string   CellType    { get; set; }

        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetSSDsRequest(string company, string series, string capacity, string formfactor, string @interface, 
            string cellType, int? selectedMotherboard)
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

            Capacity = capacity;
            if (Capacity != null)
            {
                if (Capacity.Contains('+'))
                {
                    cond.Add("Volume>@capacity");
                    Parameters.Add(new SqlParameter("@capacity", Capacity));
                }
                else
                {
                    cond.Add("Volume=@capacity");
                    Parameters.Add(new SqlParameter("@capacity", Capacity));
                }
            }

            Formfactor = formfactor;
            if (Formfactor != null)
            {
                cond.Add("Formfactor=@formfactor");
                Parameters.Add(new SqlParameter("@formfactor", Formfactor));
            }

            Interface = @interface;
            if (Interface != null)
            {
                cond.Add("ID IN (SELECT DISTINCT SSDID FROM SSD_INTERFACE WHERE Interface=@interface)");
                Parameters.Add(new SqlParameter("@interface", Interface));
            }

            CellType = cellType;
            if (CellType != null)
            {
                cond.Add("CellType=@cellType");
                Parameters.Add(new SqlParameter("@cellType", CellType));
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
