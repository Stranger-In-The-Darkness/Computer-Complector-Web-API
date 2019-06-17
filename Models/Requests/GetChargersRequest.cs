using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetChargersRequest
    { 
        public string   Company                 { get; private set; }
        public string   Series                  { get; private set; }
        public string   Power                   { get; private set; }
        public string   Sertificate             { get; private set; }
        public int?     VideoConnectorsAmount   { get; private set; }
        public string   ConnectorType           { get; private set; }
	    public string   SATAAmount              { get; private set; }
        public string   IDEAmount               { get; private set; }
        public string   MotherboardConnector    { get; private set; }

        public int? SelectedMotherboard { get; private set; }
        public int? SelectedVideocard { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetChargersRequest(string company, string series, string power, string sertificate, int? videoConnectorsAmount, 
            string connectorType, string sataAmount, string ideAmount, string motherboardConnector, int? selectedMotherboard,
            int? selectedVideocard)
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

            Power = power;
            if (Power != null)
            {
                if (Power.Contains('+'))
                {
                    cond.Add("Power>@power");
                    Parameters.Add(new SqlParameter("@power", int.Parse(Power.Replace("+", ""))));
                }
                else
                {
                    cond.Add("Power>=@power1 AND Power<=@power2");
                    Parameters.Add(new SqlParameter("@power1", int.Parse(Power.Split('-')[0])));
                    Parameters.Add(new SqlParameter("@power", int.Parse(Power.Split('-')[1])));
                }
            }

            Sertificate = sertificate;
            if (Sertificate != null)
            {
                cond.Add("Sertificate80Plus=@sertificate");
                Parameters.Add(new SqlParameter("@sertificate", Sertificate));
            }

            VideoConnectorsAmount = videoConnectorsAmount;
            if (VideoConnectorsAmount != null)
            {
                cond.Add("VideoConnectorsAmount=@videoConnectorsAmount");
                Parameters.Add(new SqlParameter("@videoConnectorsAmount", VideoConnectorsAmount));
            }

            ConnectorType = connectorType;
            if (ConnectorType != null)
            {
                cond.Add("ConnectorType=@connectorType");
                Parameters.Add(new SqlParameter("@connectorType", ConnectorType));
            }

            SATAAmount = sataAmount;
            if (SATAAmount != null)
            {
                if (SATAAmount.Contains('-'))
                {
                    cond.Add("SATAAmount>=@sataAmount1 AND SATAAmount<=@sataAmount2");
                    Parameters.Add(new SqlParameter("@sataAmount1", int.Parse(SATAAmount.Split('-')[0])));
                    Parameters.Add(new SqlParameter("@sataAmount2", int.Parse(SATAAmount.Split('-')[1])));
                }
                else
                {
                    cond.Add("SATAAmount=@sataAmount");
                    Parameters.Add(new SqlParameter("@sataAmount", int.Parse(SATAAmount)));
                }
            }

            IDEAmount = ideAmount;
            if (IDEAmount != null)
            {
                if (SATAAmount.Contains('-'))
                {
                    cond.Add("IDEAmount>=@ideAmount1 AND IDEAmount<=@ideAmount2");
                    Parameters.Add(new SqlParameter("@ideAmount1", int.Parse(SATAAmount.Split('-')[0])));
                    Parameters.Add(new SqlParameter("@ideAmount2", int.Parse(SATAAmount.Split('-')[1])));
                }
                else
                {
                    cond.Add("IDEamount=@sataAmount");
                    Parameters.Add(new SqlParameter("@ideAmount", int.Parse(SATAAmount)));
                }
            }

            MotherboardConnector = motherboardConnector;
            if (MotherboardConnector != null)
            {
                cond.Add("MotherboardConnector like @motherboardConnector");
                Parameters.Add(new SqlParameter("@motherboardConnector", '%' + MotherboardConnector + '%'));
            }

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("MotherboardConnector = (SELECT TOP 1 Pin FROM MOTHERBOARD WHERE ID = @mID)");
                cond.Add("ConnectorType = (SELECT TOP 1 CPUPin FROM MOTHERBOARD WHERE ID = @mID)");
                Parameters.Add(new SqlParameter("@mID", SelectedMotherboard));
            }

            SelectedVideocard = selectedVideocard;
            if (SelectedVideocard != null)
            {
                cond.Add("VideocardConnector = (SELECT TOP 1 Pin FROM VIDEOCARD WHERE ID = @vID)");
                Parameters.Add(new SqlParameter("@vID", SelectedVideocard));
            }

            if (cond.Count > 0)
            {
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
