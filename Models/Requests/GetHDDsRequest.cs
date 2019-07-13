using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetHDDsRequest
    {
        public string[]   Company         { get; private set; }
	    public string[]   Formfactor      { get; private set; }
	    public string[]   Volume          { get; private set; }
	    public string[]   Interface       { get; private set; }
	    public int[]      BufferVolume    { get; private set; }
	    public int[]      Speed           { get; private set; }
        public string[]   Series          { get; private set; }

        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; }
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetHDDsRequest(string[] company, string[] formfactor, string[] volume, string[] @interface, int[] bufferVolume, 
            int[] speed, string[] series, int? selectedMotherboard)
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

            Volume = volume;
            if (Volume != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Volume.Length; i++)
                {
                    if (Volume[i].Contains('-'))
                    {
                        con.Add($"Volume>=@volume1{i} AND Volume<=@volume2{i}");
                        Parameters.Add(new SqlParameter($"@volume1{i}", Volume[i].Split('-')[0]));
                        Parameters.Add(new SqlParameter($"@volume2{i}", Volume[i].Split('-')[1]));
                    }
                    else if (Volume[i].Contains('+'))
                    {
                        con.Add($"Volume>@volume{i}");
                        Parameters.Add(new SqlParameter($"@volume{i}", Volume[i].Replace("+", "")));
                    }
                    else
                    {
                        con.Add($"Volume=@volume{i}");
                        Parameters.Add(new SqlParameter($"@volume{i}", Volume[i]));
                    }
                }
                cond.Add(string.Join(" OR ", con));
            }

            Interface = @interface;
            if (Interface != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Interface.Length; i++)
                {
                    con.Add($"ID IN (SELECT DISTINCT HDDID FROM HDD_INTERFACE WHERE Interface = @interface{i})");
                    Parameters.Add(new SqlParameter($"@interface{i}", Interface[i]));
                }
            }

            BufferVolume = bufferVolume;
            if (BufferVolume != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < BufferVolume.Length; i++)
                {
                    con.Add($"BufferVolume=@bufferVolume{i}");
                    Parameters.Add(new SqlParameter($"@bufferVolume{i}", BufferVolume[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Speed = speed;
            if (Speed != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Speed.Length; i++)
                {
                    con.Add($"Speed=@speed{i}");
                    Parameters.Add(new SqlParameter($"@speed{i}", Speed[i]));
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

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("NOT (SELECT Count(Interface) FROM HDD_INTERFACE WHERE HDDID = ID AND Interface IN (SELECT DISTINCT (SELECT TOP 1 Value FROM string_split(Slot, ' ')) FROM MOTHERBOARD_SLOTS WHERE MotherboardID = @motherboardID)) = 0");
                Parameters.Add(new SqlParameter("@motherboardID", SelectedMotherboard));
            }

            if (cond.Count > 0)
            {
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
