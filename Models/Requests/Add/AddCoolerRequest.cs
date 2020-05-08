using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
	/// <summary>
	/// Request of adding <see cref="Data.Cooler"/> to the DB
	/// </summary>
	public class AddCoolerRequest : AddRequestBase
    {
		/// <summary>
		/// Element to be added
		/// </summary>
		private Cooler _element;

		/// <summary>
		/// SQL query parameters
		/// </summary>
		private List<SqlParameter> _parameters;

        public AddCoolerRequest(Cooler element)
        {
            _element = element;

            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@diameter", _element.VentDiam),

                new SqlParameter("@adjst", _element.TurnAdj ?? false ? 1 : 0),

                new SqlParameter("@color", _element.Color)
            };

            if (Validate<string>(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate<string>(_element.Purpose, "element.Purpose"))
            {
                _parameters.Add(new SqlParameter("@purpose", _element.Purpose));
            }

            if (Validate<string>(_element.Type, "element.Type"))
            {
                _parameters.Add(new SqlParameter("@type", _element.Purpose));
            }

            if (Validate<string>(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate<string>(_element.Material, "element.Material"))
            {
                _parameters.Add(new SqlParameter("@material", _element.Material));
            }

            if (Validate<string>(_element.Connector, "element.Connector"))
            {
                _parameters.Add(new SqlParameter("@connector", _element.Connector));
            }

            if (_element.Socket != null && _element.Socket.Count > 0)
            {
                for (int i = 0; i<_element.Socket.Count; i++)
                {
                    Expression += $"INSERT INTO COOLER_SOCKET VALUES (SELECT ID FROM COOLER WHERE TITLE = @title, @socket{i});";
                    _parameters.Add(new SqlParameter($"@socket{i}", _element.Socket[i]));
                }
            }
        }

		/// <summary>
		/// SQL query adding new element
		/// </summary>
		public string Expression { get; private set; } = "INSERT INTO COOLER VALUES (@title, @purpose, @type, @company, @material, @diameter, @adjst, @color, @connector);";

		/// <summary>
		/// SQL parameters for query
		/// </summary>
		public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		/// <summary>
		/// Element to be added
		/// </summary>
		public Cooler Cooler { get => _element; }
    }
}
