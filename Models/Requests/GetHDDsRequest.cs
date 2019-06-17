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
        public string   Company         { get; private set; }
	    public string   Formfactor      { get; private set; }
	    public string   Volume          { get; private set; }
	    public string   Interface       { get; private set; }
	    public int?     BufferVolume    { get; private set; }
	    public int?     Speed           { get; private set; }
        public string   Series          { get; private set; }

        public int? SelectedMotherboard { get; private set; }

        public string Expression { get; }
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetHDDsRequest(string company, string formfactor, string volume, string @interface, int? bufferVolume, 
            int? speed, string series, int? selectedMotherboard)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                cond.Add("Company=@company");
                Parameters.Add(new SqlParameter("@company", Company));
            }

            Formfactor = formfactor;
            if (Formfactor != null)
            {
                cond.Add("Formfactor=@formfactor");
                Parameters.Add(new SqlParameter("@formfactor", Formfactor));
            }

            Volume = volume;
            if (Volume != null)
            {
                if (Volume.Contains('-'))
                {
                    cond.Add("Volume>=@volume1 AND Volume<=@volume2");
                    Parameters.Add(new SqlParameter("@volume1", Volume.Split('-')[0]));
                    Parameters.Add(new SqlParameter("@volume2", Volume.Split('-')[1]));
                }
                else if (Volume.Contains('+'))
                {
                    cond.Add("Volume>@volume");
                    Parameters.Add(new SqlParameter("@volume", Volume.Replace("+", "")));
                }
                else
                {
                    cond.Add("Volume=@volume");
                    Parameters.Add(new SqlParameter("@volume", Volume));
                }
            }

            Interface = @interface;
            if (Interface != null)
            {
                cond.Add("ID IN (SELECT DISTINCT HDDID FROM HDD_INTERFACE WHERE Interface = @interface)");
                Parameters.Add(new SqlParameter("@interface", Interface));
            }

            BufferVolume = bufferVolume;
            if (BufferVolume != null)
            {
                cond.Add("BufferVolume=@bufferVolume");
                Parameters.Add(new SqlParameter("@bufferVolume", BufferVolume));
            }

            Speed = speed;
            if (Speed != null)
            {
                cond.Add("Speed=@speed");
                Parameters.Add(new SqlParameter("@speed", Speed));
            }
            Series = series;
            if (Series != null)
            {
                cond.Add("Series=@series");
                Parameters.Add(new SqlParameter("@series", Series));
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
