using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
    public class AddSsdRequest : AddRequestBase
    {
        private SSD _element;
        private List<SqlParameter> _parameters;

        public AddSsdRequest(SSD element)
        {
            _element = element;

            _parameters = new List<SqlParameter>();

            if (Validate(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate(_element.Capacity, "element.Capacity"))
            {
                _parameters.Add(new SqlParameter("@volume", _element.Capacity));
            }

            if (Validate(_element.Series, "element.Series"))
            {
                _parameters.Add(new SqlParameter("@series", _element.Series));
            }

            if (Validate(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate(_element.Formfactor, "element.Formfactor"))
            {
                _parameters.Add(new SqlParameter("@formfactor", _element.Formfactor));
            }

            if (Validate(_element.CellType, "element.CellType"))
            {
                _parameters.Add(new SqlParameter("@cellType", _element.CellType));
            }

            for (int i = 0; i < _element.Interface.Count; i++)
            {
                Expression += $"INSERT INTO SSD_INTERFACE VALUES (SELECT TOP 1 ID FROM SSD WHERE TITLE = @title, @interface{i});";
                _parameters.Add(new SqlParameter($"@interface{i}", _element.Interface[i]));
            }
        }

        public string Expression { get; private set; } = 
            "INSERT INTO SSD VALUES (@title, @volume, @series, @company, @formfactor, @cellType);";

        public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		public SSD SSD { get => _element; }
    }
}
