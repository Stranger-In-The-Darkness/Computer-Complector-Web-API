using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Requests.Get
{
    public class GetRAMsRequest
    {
        public string[] Company { get; set; }
        public string[] Series { get; set; }
        public string[] Memory { get; set; }
        public string[] Type { get; set; }
        public int[] Volume { get; set; }
        public int[] ModuleAmount { get; set; }
        public int[] Freq { get; set; }
        public string[] CL { get; set; }

        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; } = "SELECT * FROM RAM";
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetRAMsRequest(string[] company, string[] series, string[] memory, string[] type, int[] volume, int[] moduleAmount,
            int[] freq, string[] cL, int? selectedMotherboard)
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
                    cond.Add($"Series=@series{i}");
                    Parameters.Add(new SqlParameter($"@series{i}", Series[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Memory = memory;
            if (Memory != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Memory.Length; i++)
                {
                    cond.Add($"MemoryType=@memory{i}");
                    Parameters.Add(new SqlParameter($"@memory{i}", Memory[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Type = type;
            if (Type != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Type.Length; i++)
                {
                    cond.Add($"Purpose=@type{i}");
                    Parameters.Add(new SqlParameter($"@type{i}", Type[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Volume = volume;
            if (Volume != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Volume.Length; i++)
                {
                    cond.Add($"MemoryVolume=@volume{i}");
                    Parameters.Add(new SqlParameter($"@volume{i}", Volume[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            ModuleAmount = moduleAmount;
            if (ModuleAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < ModuleAmount.Length; i++)
                {
                    cond.Add($"ModulesCount=@moduleAmount{i}");
                    Parameters.Add(new SqlParameter($"@moduleAmount{i}", ModuleAmount[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Freq = freq;
            if (Freq != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Freq.Length; i++)
                {
                    cond.Add($"Frequency=@freq{i}");
                    Parameters.Add(new SqlParameter($"@freq{i}", Freq[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            CL = cL;
            if (CL != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < CL.Length; i++)
                {
                    cond.Add($"CASLatency=@cL{i}");
                    Parameters.Add(new SqlParameter($"@cL{i}", CL[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("MemoryType = (SELECT TOP 1 MemoryType FROM MOTHERBOARD WHERE ID = @motherboardID)");
                Parameters.Add(new SqlParameter("@motherboardID", SelectedMotherboard));
            }

            if (cond.Count > 0)
            {
                Expression += $" WHERE {string.Join(" AND ", cond)}";
            }
        }
    }
}
