using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Get
{
    public class GetBodiesRequest
    {
        public string[]   Company           { get; private set; }
        public string[]   Formfactor        { get; private set; }
        public string[]   Type              { get; private set; }
        public bool[]     BuildInCharger    { get; private set; }
        public string[]   ChargerPower      { get; private set; }
        public string[]   Usb3Ports         { get; private set; }
        //public string   BacklightColor    { get; private set; }

        public int? SelectedMotherboard { get; private set; }
        public int? SelectedVideocard { get; private set; }

        public string Expression { get; } = "SELECT * FROM BODY";
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetBodiesRequest(string[] company, string[] formfactor, string[] type, bool[] buildInCharger, string[] chargerPower,
            string[] usbPorts, int? selectedMotherboard, int? selectedVideocard)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < company.Length; i++)
                {
                    con.Add($"Company=@company{i}");
                    Parameters.Add(new SqlParameter($"@company{i}", Company[i]));
                }
                if (con.Count > 0)
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
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            Type = type;
            if (Type != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Type.Length; i++)
                {
                    con.Add($"Type=@type{i}");
                    Parameters.Add(new SqlParameter($"@type{i}", Type[i]));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            BuildInCharger = buildInCharger;
            if (BuildInCharger != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < BuildInCharger.Length; i++)
                {
                    con.Add($"[Build-inCharger]=@buildInCharger{i}");
                    Parameters.Add(new SqlParameter($"@buildInCharger{i}", BuildInCharger[i]));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            ChargerPower = chargerPower;
            if (ChargerPower != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < ChargerPower.Length; i++)
                {
                    con.Add($"(ChargerPower>=@chargerPower1{i} AND ChargerPower<=@chargerPower2{i})");
                    Parameters.Add(new SqlParameter($"@chargerPower1{i}", ChargerPower[i].Split('-')[0]));
                    Parameters.Add(new SqlParameter($"@chargerPower2{i}", ChargerPower[i].Split('-')[1]));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            Usb3Ports = usbPorts;
            if (Usb3Ports != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Usb3Ports.Length; i++)
                {
                    con.Add($"[USB3.0Amount]=@usbPorts{i}");
                    Parameters.Add(new SqlParameter($"@usbPorts{i}", Usb3Ports[i]));
                }
                if (con.Count > 0)
                    cond.Add(string.Join(" OR ", con));
            }

            SelectedMotherboard = selectedMotherboard;
            if (SelectedMotherboard != null)
            {
                cond.Add("Formfactor = (SELECT TOP 1 Formfactor FROM MOTHERBOARD WHERE ID = @id)");
                Parameters.Add(new SqlParameter("@id", SelectedMotherboard));
            }

            SelectedVideocard = selectedVideocard;
            if (SelectedVideocard != null)
            {
                cond.Add("VideocardMaxLength >= (SELECT TOP 1 Length FROM VIDEOCARD WHERE ID = @vID)");
                Parameters.Add(new SqlParameter("@vID", SelectedVideocard));
            }

            if (cond.Count > 0)
            {
                Expression += $" WHERE {string.Join(" AND ", cond)}";
            }
        }

        public IEnumerable<Body> Filter(IEnumerable<Body> bodies, IEnumerable<Motherboard> motherboards, IEnumerable<Videocard> videocards)
        {
            return bodies.
                Where(e => Company != null ? Company.Contains(e.Company) : true).
                Where(e => Formfactor != null ? Formfactor.Contains(e.Formfactor) : true).
                Where(e => Type != null ? Type.Contains(e.Type) : true).
                Where(e => BuildInCharger != null ? BuildInCharger.Contains(e.BuildInCharger) : true).
                Where(e => ChargerPower != null ? ChargerPower.Select(o => e.ChargerPower >= int.Parse(o.Split('-')[0])).Contains(true) : true).
                Where(e => ChargerPower != null ? ChargerPower.Select(o => e.ChargerPower <= int.Parse(o.Split('-')[1])).Contains(true) : true).
                Where(e => Usb3Ports != null ? Usb3Ports.Contains(e.USB3Amount.ToString()) : true).
                Where(e => { var m = motherboards.FirstOrDefault(i => i.ID == SelectedMotherboard); return m != null ? e.Formfactor == m.Formfactor : true; }).
                Where(e => { var v = videocards.FirstOrDefault(i => i.ID == SelectedVideocard); return v != null ? e.VideocardMaxLength >= int.Parse(v.Length) : true; });
        }

        public GetBodiesRequest()
        {
        }
    }
}
