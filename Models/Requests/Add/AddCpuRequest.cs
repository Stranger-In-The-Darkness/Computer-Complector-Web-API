using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
	/// <summary>
	/// Request of adding <see cref="Data.CPU"/> to the DB
	/// </summary>
	public class AddCpuRequest : AddRequestBase
    {
		/// <summary>
		/// Element to be added
		/// </summary>
		private CPU _element;

		/// <summary>
		/// SQL query parameters
		/// </summary>
		private List<SqlParameter> _parameters;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="element">Element to be added</param>
		public AddCpuRequest(CPU element)
        {
            _element = element;

            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@graphics", _element.IntegratedGraphics ? 1 : 0),

                new SqlParameter("@overclocking", _element.Overcloacking ? 1 : 0)
            };

            if (Validate<string>(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate<string>(_element.Series, "element.Series"))
            {
                _parameters.Add(new SqlParameter("@series", _element.Series));
            }

            if (Validate<string>(_element.Socket, "element.Socket"))
            {
                _parameters.Add(new SqlParameter("@socket", _element.Socket));
            }

            if (Validate<double>(_element.Frequency, "element.Frequency"))
            {
                _parameters.Add(new SqlParameter("@freq", _element.Frequency));
            }

            if (Validate<int>(_element.CoresAmount, "element.CoresAmount"))
            {
                _parameters.Add(new SqlParameter("@cores", _element.CoresAmount));
            }

            if (Validate<int>(_element.ThreadsAmount, "element.ThreadsAmount"))
            {
                _parameters.Add(new SqlParameter("@threads", _element.ThreadsAmount));
            }

            if (Validate<string>(_element.Core, "element.Core"))
            {
                _parameters.Add(new SqlParameter("@core", _element.Core));
            }

            if (Validate<string>(_element.DeliveryType, "element.DeliveryType"))
            {
                _parameters.Add(new SqlParameter("@delivery", _element.DeliveryType));
            }

            if (Validate<string>(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }
        }

		/// <summary>
		/// SQL query adding new element
		/// </summary>
		public string Expression { get; private set; } =
            "INSERT INTO CPU VALUES (@company, @series, @socket, @freq, @cores, @threads, @graphics, @core, @delivery, @overclocking, @title);";

		/// <summary>
		/// SQL parameters for query
		/// </summary>
		public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		/// <summary>
		/// Element to be added
		/// </summary>
		public CPU CPU { get => _element; }
    }
}
