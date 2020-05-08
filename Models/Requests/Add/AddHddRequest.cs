using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
	/// <summary>
	/// Request of adding <see cref="Data.HDD"/> to the DB
	/// </summary>
	public class AddHddRequest : AddRequestBase
    {
		/// <summary>
		/// Element to be added
		/// </summary>
		private HDD _element;

		/// <summary>
		/// SQL query parameters
		/// </summary>
		private List<SqlParameter> _parameters;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="element">Element to be added</param>
		public AddHddRequest(HDD element)
        {
            _element = element;

            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@series", _element.Series)
            };

            if (Validate<string>(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate<string>(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate<string>(_element.Formfactor, "element.Formfactor"))
            {
                _parameters.Add(new SqlParameter("@formfactor", _element.Formfactor));
            }

            if (Validate<int>(_element.Capacity, "element.Capacity"))
            {
                _parameters.Add(new SqlParameter("@volume", _element.Capacity));
            }

            if (Validate<int>(_element.BufferVolume, "element.BufferVolume"))
            {
                _parameters.Add(new SqlParameter("@buffer", _element.BufferVolume));
            }

            if (Validate<int>(_element.Speed, "element.Speed"))
            {
                _parameters.Add(new SqlParameter("@speed", _element.Speed));
            }

            for (int i = 0; i<_element.Interface.Count; i++)
            {
                Expression += $"INSERT INTO HDD_INTERFACE VALUES (SELECT TOP 1 ID FROM HDD WHERE TITLE = @title, @interface{i});";
                _parameters.Add(new SqlParameter($"@interface{i}", _element.Interface[i]));
            }
        }

		/// <summary>
		/// SQL query adding new element
		/// </summary>
		public string Expression { get; private set; } = 
            "INSERT INTO HDD VALUES (@title, @company, @formfactor, @volume, @buffer, @speed, @series);";

		/// <summary>
		/// SQL parameters for query
		/// </summary>
		public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		/// <summary>
		/// Element to be added
		/// </summary>
		public HDD HDD { get => _element; }
    }
}
