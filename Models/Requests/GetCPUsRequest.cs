using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetCPUsRequest
    {
        public string  Company             { get; set; }
        public string  Series              { get; set; }
        public string  Socket              { get; set; }
        //public string  Frequency           { get; set; }
        public int?    CoresAmount         { get; set; }
        public int?    ThreadsAmount       { get; set; }
        public bool?   IntegratedGraphics  { get; set; }
        public string  Core                { get; set; }
        public string  DeliveryType        { get; set; }
        public bool?   Overcloacking       { get; set; }

        public int? SelectedCooler { get; private set; }
        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetCPUsRequest(string company, string series, string socket, int? coresAmount, int? threadsAmount, 
            bool? integratedGraphics, string core, string deliveryType, bool? overcloacking, int? selectedCooler,
            int? selectedMotherboard)
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

            Socket = socket;
            if (Socket != null)
            {
                cond.Add("Socket=@socket");
                Parameters.Add(new SqlParameter("@socket", Socket));
            }

            CoresAmount = coresAmount;
            if (CoresAmount != null)
            {
                cond.Add("AmountOfCores=@coresAmount");
                Parameters.Add(new SqlParameter("@coresAmount", CoresAmount));
            }

            ThreadsAmount = threadsAmount;
            if (ThreadsAmount != null)
            {
                cond.Add("AmountOfThreads=@threadsAmount");
                Parameters.Add(new SqlParameter("@threadsAmount", ThreadsAmount));
            }

            IntegratedGraphics = integratedGraphics;
            if (IntegratedGraphics != null)
            {
                cond.Add("IntegratedGraphics=@integratedGraphics");
                Parameters.Add(new SqlParameter("@integratedGraphics", IntegratedGraphics));
            }

            Core = core;
            if (Core != null)
            {
                cond.Add("Core=@core");
                Parameters.Add(new SqlParameter("@core", Core));
            }

            DeliveryType = deliveryType;
            if (DeliveryType != null)
            {
                cond.Add("DeliveryType=@deliveryType");
                Parameters.Add(new SqlParameter("@deliveryType", DeliveryType));
            }

            Overcloacking = overcloacking;
            if (Overcloacking != null)
            {
                cond.Add("Overclocking=@overcloacking");
                Parameters.Add(new SqlParameter("@overcloacking", Overcloacking));
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
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
