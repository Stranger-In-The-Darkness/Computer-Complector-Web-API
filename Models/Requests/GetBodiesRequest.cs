using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetBodiesRequest
    {
        public string   Company           { get; private set; }
        public string   Formfactor        { get; private set; }
        public string   Type              { get; private set; }
        public bool?    BuildInCharger    { get; private set; }
        public string   ChargerPower      { get; private set; }
        public string   Usb3Ports         { get; private set; }
        //public string   BacklightColor    { get; private set; }

        public int? SelectedMotherboard { get; private set; }
        public int? SelectedVideocard { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetBodiesRequest(string company, string formfactor, string type, bool? buildInCharger, string chargerPower, 
            string usbPorts, int? selectedMotherboard, int? selectedVideocard)
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

            Type = type;
            if (Type != null)
            {
                cond.Add("Type=@type");
                Parameters.Add(new SqlParameter("@type", Type));
            }

            BuildInCharger = buildInCharger;
            if (BuildInCharger != null)
            {
                cond.Add("[Build-inCharger]=@buildInCharger");
                Parameters.Add(new SqlParameter("@buildInCharger", BuildInCharger));
            }

            ChargerPower = chargerPower;
            if (ChargerPower != null)
            {
                cond.Add("ChargerPower>=@chargerPower1 AND ChargerPower<=@chargerPower2");
                Parameters.Add(new SqlParameter("@chargerPower1", ChargerPower.Split('-')[0]));
                Parameters.Add(new SqlParameter("@chargerPower2", ChargerPower.Split('-')[1]));
            }

            Usb3Ports = usbPorts;
            if (Usb3Ports != null)
            {
                cond.Add("[USB3.0Amount]=@usbPorts");
                Parameters.Add(new SqlParameter("@usbPorts", Usb3Ports));
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
                Expression = string.Join(" AND ", cond);
            }
        }

        public IEnumerable<Body> Filter(IEnumerable<Body> bodies, IEnumerable<Motherboard> motherboards, IEnumerable<Videocard> videocards)
        {
            return bodies.
                Where(e => Company != null ? e.Company == Company : true).
                Where(e => Formfactor != null ? e.Formfactor == Formfactor : true).
                Where(e => Type != null ? e.Type == Type : true).
                Where(e => BuildInCharger != null ? e.BuildInCharger == BuildInCharger : true).
                Where(e => ChargerPower != null ? (e.ChargerPower >= int.Parse(ChargerPower.Split('-')[0])) : true).
                Where(e => ChargerPower != null ? (e.ChargerPower <= int.Parse(ChargerPower.Split('-')[1])) : true).
                Where(e => Usb3Ports != null ? e.USB3Ports == int.Parse(Usb3Ports) : true).
                Where(e => { var m = motherboards.FirstOrDefault(i => i.ID == SelectedMotherboard); return m != null ? e.Formfactor == m.Formfactor : true; }).
                Where(e => { var v = videocards.FirstOrDefault(i => i.ID == SelectedVideocard); return v != null ? e.VideocardMaxLength >= int.Parse(v.Length) : true; });
        }

        public GetBodiesRequest()
        {
        }
    }
}
