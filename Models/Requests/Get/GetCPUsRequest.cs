using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Requests.Get
{
    public class GetCPUsRequest
    {
        public string[]  Company             { get; set; }
        public string[]  Series              { get; set; }
        public string[]  Socket              { get; set; }
        public int[]     CoresAmount         { get; set; }
        public int[]     ThreadsAmount       { get; set; }
        public bool[]    IntegratedGraphics  { get; set; }
        public string[]  Core                { get; set; }
        public string[]  DeliveryType        { get; set; }
        public bool[]    Overclocking       { get; set; }

        public int? SelectedCooler { get; private set; }
        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; } = "SELECT * FROM CPU";
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetCPUsRequest(string[] company, string[] series, string[] socket, int[] coresAmount, int[] threadsAmount, 
            bool[] integratedGraphics, string[] core, string[] deliveryType, bool[] overcloacking, int? selectedCooler,
            int? selectedMotherboard)
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

            Socket = socket;
            if (Socket != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Socket.Length; i++)
                {
                    con.Add($"Socket=@socket{i}");
                    Parameters.Add(new SqlParameter($"@socket{i}", Socket[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            CoresAmount = coresAmount;
            if (CoresAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < CoresAmount.Length; i++)
                {
                    con.Add($"AmountOfCores=@coresAmount{i}");
                    Parameters.Add(new SqlParameter($"@coresAmount{i}", CoresAmount[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            ThreadsAmount = threadsAmount;
            if (ThreadsAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < ThreadsAmount.Length; i++)
                {
                    con.Add($"AmountOfThreads=@threadsAmount{i}");
                    Parameters.Add(new SqlParameter($"@threadsAmount{i}", ThreadsAmount[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            IntegratedGraphics = integratedGraphics;
            if (IntegratedGraphics != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < IntegratedGraphics.Length; i++)
                {
                    con.Add($"IntegratedGraphics=@integratedGraphics{i}");
                    Parameters.Add(new SqlParameter($"@integratedGraphics{i}", IntegratedGraphics[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Core = core;
            if (Core != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Core.Length; i++)
                {
                    con.Add($"Core=@core{i}");
                    Parameters.Add(new SqlParameter($"@core{i}", Core[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            DeliveryType = deliveryType;
            if (DeliveryType != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < DeliveryType.Length; i++)
                {
                    con.Add($"DeliveryType=@deliveryType{i}");
                    Parameters.Add(new SqlParameter($"@deliveryType{i}", DeliveryType[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Overclocking = overcloacking;
            if (Overclocking != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Overclocking.Length; i++)
                {
                    con.Add($"Overclocking=@overcloacking{i}");
                    Parameters.Add(new SqlParameter($"@overcloacking{i}", Overclocking[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("Socket IN (SELECT TOP 1 Socket FROM MOTHERBOARD WHERE ID = @motherboardID)");
                Parameters.Add(new SqlParameter("@motherboardID", SelectedMotherboard));
            }

            SelectedCooler = selectedCooler;
            if (SelectedCooler != null)
            {
                cond.Add("Socket IN (SELECT Socket FROM COOLER_SOCKET WHERE CoolerID = @coolerID)");
                Parameters.Add(new SqlParameter("@coolerID", SelectedCooler));
            }

            if (cond.Count > 0)
            {
                Expression += $" WHERE {string.Join(" AND ", cond)}";
            }
        }
    }
}
