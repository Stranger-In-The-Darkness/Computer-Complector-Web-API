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
        public string   Company         { get; set; }
	    public string   Series          { get; set; }
	    public string   Proccessor      { get; set; }
	    public string   VRAM            { get; set; }
        public int?     Capacity        { get; set; }
	    public string   Connector       { get; set; }
	    public string   Family          { get; set; }

        public int? SelectedBody { get; private set; }
        public int? SelectedCharger { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetVideocardsRequest(string company, string series, string proccessor, string vRAM, int? capacity, 
            string connector, string family, int? selectedCharger, int? selectedBody)
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

            Proccessor = proccessor;
            if (Proccessor != null)
            {
                cond.Add("GraphicalProccessor=@proccessor");
                Parameters.Add(new SqlParameter("@proccessor", Proccessor));
            }

            VRAM = vRAM;
            if (VRAM != null)
            {
                cond.Add("VRAM=@vRAM");
                Parameters.Add(new SqlParameter("@vRAM", VRAM));
            }

            Capacity = capacity;
            if (Capacity != null)
            {
                cond.Add("Capacity=@capacity");
                Parameters.Add(new SqlParameter("@capacity", Capacity));
            }

            Connector = connector;
            if (Connector != null)
            {
                cond.Add("ID IN (SELECT DISTINCT VideocardID FROM VIDEOCARD_CONNECTOR WHERE Connector = @connector)");
                Parameters.Add(new SqlParameter("@connector", Connector));
            }

            Family = family;
            if (Family != null)
            {
                cond.Add("Family=@family");
                Parameters.Add(new SqlParameter("@family", Family));
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
