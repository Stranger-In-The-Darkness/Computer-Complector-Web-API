using System.Collections.Generic;
using System.Data.SqlClient;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Get
{
	/// <summary>
	/// Request of getting <see cref="Body"/> element according to filters
	/// </summary>
	public class GetBodiesRequest
    {
		/// <summary>
		/// Array of appliable <see cref="Body.Company"/> values
		/// </summary>
        public string[] Company { get; private set; }

		/// <summary>
		/// Array of appliable <see cref="Body.Formfactor"/> values
		/// </summary>
        public string[] Formfactor { get; private set; }

		/// <summary>
		/// Array of appliable <see cref="Body.Type"/> values
		/// </summary>
        public string[] Type { get; private set; }

		/// <summary>
		/// Array of appliable <see cref="Body.BuildInCharger"/> values
		/// </summary>
        public bool[] BuildInCharger { get; private set; }

		/// <summary>
		/// Array of appliable <see cref="Body.ChargerPower"/> values
		/// </summary>
        public string[] ChargerPower { get; private set; }

		/// <summary>
		/// Array of appliable <see cref="Body.USB3Amount"/> values
		/// </summary>
        public string[] Usb3Ports { get; private set; }

		/// <summary>
		/// ID of selected <see cref="Motherboard"/>
		/// </summary>
        public int? SelectedMotherboard { get; private set; }

		/// <summary>
		/// ID of selected <see cref="Videocard"/>
		/// </summary>
        public int? SelectedVideocard { get; private set; }

		/// <summary>
		/// SQL query
		/// </summary>
        public string Expression { get; } = "SELECT * FROM BODY";

		/// <summary>
		/// SQL parameters for query
		/// </summary>
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="company">Appliable <see cref="Body.Company"/> values</param>
		/// <param name="formfactor">Appliable <see cref="Body.Formfactor"/> values</param>
		/// <param name="type">Appliable <see cref="Body.Type"/> values</param>
		/// <param name="buildInCharger">Appliable <see cref="Body.BuildInCharger"/> values</param>
		/// <param name="chargerPower">Appliable <see cref="Body.ChargerPower"/> values</param>
		/// <param name="usbPorts">Appliable <see cref="Body.USB3Amount"/> values</param>
		/// <param name="selectedMotherboard">ID of selected <see cref="Motherboard"/></param>
		/// <param name="selectedVideocard">ID of selected <see cref="Videocard"/></param>
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
    }
}
