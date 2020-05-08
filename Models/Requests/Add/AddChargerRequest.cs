using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
	/// <summary>
	/// Request of adding <see cref="Data.Charger"/> to the DB
	/// </summary>
	public class AddChargerRequest : AddRequestBase
    {
		/// <summary>
		/// Element to be added
		/// </summary>
        private Charger _element;

		/// <summary>
		/// SQL query parameters
		/// </summary>
        private List<SqlParameter> _parameters;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="element">Element to be added</param>
        public AddChargerRequest(Charger element)
        {
            _element = element;

            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@series", _element.Series),

                new SqlParameter("@sertificate", _element.Sertificate80Plus),

                new SqlParameter("@connector", _element.ConnectorType)
            };

            if (Validate<string>(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate<string>(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate<int>(_element.Power, "element.Power"))
            {
                _parameters.Add(new SqlParameter("@power", _element.Power));
            }

            if (Validate<int>(_element.VideoConnectorsAmount, "element.VideoConnectorsAmount"))
            {
                _parameters.Add(new SqlParameter("@videoConnAmount", _element.VideoConnectorsAmount));
            }

            if (Validate<string>(_element.VideocardConnector, "element.VideocardConnector"))
            {
                _parameters.Add(new SqlParameter("@videocardPin", _element.VideocardConnector));
            }

            if (Validate<int>(_element.SATAAmount, "element.SATAAmount"))
            {
                _parameters.Add(new SqlParameter("@sata", _element.SATAAmount));
            }

            if (Validate<int>(_element.IDEAmount, "element.IDEAmount"))
            {
                _parameters.Add(new SqlParameter("@ide", _element.IDEAmount));
            }

            if (Validate<string>(_element.MotherboardConnector, "element.MotherboardConnector"))
            {
                _parameters.Add(new SqlParameter("@motherboardPin", _element.MotherboardConnector));
            }
        }

		/// <summary>
		/// SQL query adding new element
		/// </summary>
        public string Expression { get; private set; } = "INSERT INTO CHARGER VALUES (@title, @company, @series, @power, @sertificate, @videoConnAmount, @videocardPin, @sata, @ide, @motherboardPin, @connector);";

		/// <summary>
		/// SQL parameters for query
		/// </summary>
        public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		/// <summary>
		/// Element to be added
		/// </summary>
		public Charger Charger { get => _element; }
    }
}
