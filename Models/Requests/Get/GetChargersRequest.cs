using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Get
{
    public class GetChargersRequest
    { 
        public string[]   Company                 { get; private set; }
        public string[]   Series                  { get; private set; }
        public string[]   Power                   { get; private set; }
        public string[]   Sertificate             { get; private set; }
        public int[]      VideoConnectorsAmount   { get; private set; }
        public string[]   ConnectorType           { get; private set; }
	    public string[]   SATAAmount              { get; private set; }
        public string[]   IDEAmount               { get; private set; }
        public string[]   MotherboardConnector    { get; private set; }

        public int? SelectedMotherboard { get; private set; }
        public int? SelectedVideocard { get; private set; }

        public string Expression { get; } = "SELECT * FROM CHARGER";
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetChargersRequest(string[] company, string[] series, string[] power, string[] sertificate, int[] videoConnectorsAmount, 
            string[] connectorType, string[] sataAmount, string[] ideAmount, string[] motherboardConnector, int? selectedMotherboard,
            int? selectedVideocard)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Company.Length; i++)
                {
                    con.Add($"Company=@company{i}");
                    Parameters.Add(new SqlParameter("@company{i}", Company[i]));
                }
                if (con.Count > 0)
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
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            Power = power;
            if (Power != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Power.Length; i++)
                {
                    if (Power[i].Contains('+'))
                    {
                        con.Add($"Power>@power{i}");
                        Parameters.Add(new SqlParameter($"@power{i}", int.Parse(Power[i].Replace("+", ""))));
                    }
                    else
                    {
                        con.Add($"Power>=@power1{i} AND Power<=@power2{i}");
                        Parameters.Add(new SqlParameter($"@power1{i}", int.Parse(Power[i].Split('-')[0])));
                        Parameters.Add(new SqlParameter($"@power2{i}", int.Parse(Power[i].Split('-')[1])));
                    }
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            Sertificate = sertificate;
            if (Sertificate != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Sertificate.Length; i++)
                {
                    con.Add($"Sertificate80Plus=@sertificate{i}");
                    Parameters.Add(new SqlParameter($"@sertificate{i}", Sertificate[i]));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            VideoConnectorsAmount = videoConnectorsAmount;
            if (VideoConnectorsAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < VideoConnectorsAmount.Length; i++)
                {
                    con.Add($"VideoConnectorsAmount=@videoConnectorsAmount{i}");
                    Parameters.Add(new SqlParameter($"@videoConnectorsAmount{i}", VideoConnectorsAmount[i]));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            ConnectorType = connectorType;
            if (ConnectorType != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < ConnectorType.Length; i++)
                {
                    con.Add($"ConnectorType=@connectorType{i}");
                    Parameters.Add(new SqlParameter($"@connectorType{i}", ConnectorType[i]));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            SATAAmount = sataAmount;
            if (SATAAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < SATAAmount.Length; i++)
                {
                    if (SATAAmount[i].Contains('-'))
                    {
                        con.Add($"SATAAmount>=@sataAmount1{i} AND SATAAmount<=@sataAmount2{i}");
                        Parameters.Add(new SqlParameter($"@sataAmount1{i}", int.Parse(SATAAmount[i].Split('-')[0])));
                        Parameters.Add(new SqlParameter($"@sataAmount2{i}", int.Parse(SATAAmount[i].Split('-')[1])));
                    }
                    else
                    {
                        con.Add($"SATAAmount=@sataAmount{i}");
                        Parameters.Add(new SqlParameter($"@sataAmount{i}", int.Parse(SATAAmount[i])));
                    }
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            IDEAmount = ideAmount;
            if (IDEAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < IDEAmount.Length; i++)
                {
                    if (IDEAmount[i].Contains('-'))
                    {
                        con.Add($"IDEAmount>=@ideAmount1{i} AND IDEAmount<=@ideAmount2{i}");
                        Parameters.Add(new SqlParameter($"@ideAmount1{i}", int.Parse(IDEAmount[i].Split('-')[0])));
                        Parameters.Add(new SqlParameter($"@ideAmount2{i}", int.Parse(IDEAmount[i].Split('-')[1])));
                    }
                    else
                    {
                        con.Add($"IDEamount=@ideAmount{i}");
                        Parameters.Add(new SqlParameter($"@ideAmount{i}", int.Parse(IDEAmount[i])));
                    }
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            MotherboardConnector = motherboardConnector;
            if (MotherboardConnector != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < MotherboardConnector.Length; i++)
                {
                    con.Add($"MotherboardConnector like @motherboardConnector{i}");
                    Parameters.Add(new SqlParameter("@motherboardConnector{i}", '%' + MotherboardConnector[i] + '%'));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
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
                Expression += $" WHERE {string.Join(" AND ", cond)}";
            }
        }
    }
}
