using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
    public class AddVideocardRequest : AddRequestBase
    {
        private Videocard _element;
        private List<SqlParameter> _parameters;

        public AddVideocardRequest(Videocard element)
        {
            _element = element;

            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@length", _element.Length),

                new SqlParameter("@pin", _element.Pin)
            };

            if (Validate(_element.Proccessor, "element.Proccessor"))
            {
                _parameters.Add(new SqlParameter("@gpu", _element.Proccessor));
            }

            if (Validate(_element.VRAM, "element.VRAM"))
            {
                _parameters.Add(new SqlParameter("@vram", _element.VRAM));
            }

            if (Validate(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate(_element.Series, "element.Series"))
            {
                _parameters.Add(new SqlParameter("@series", _element.Series));
            }

            if (Validate(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate(_element.Capacity, "element.Capacity"))
            {
                _parameters.Add(new SqlParameter("@capacity", _element.Capacity));
            }

            if (Validate(_element.Memory, "element.Memory"))
            {
                _parameters.Add(new SqlParameter("@memory", _element.Memory));
            }

            for (int i = 0; i<_element.Connectors.Count; i++)
            {
                Expression += $"INSERT INTO VIDEOCARD_CONNECTOR VALUES (SELECT TOP 1 ID FROM VIDEOCARD WHERE Title = @title, @connector{i});";
                _parameters.Add(new SqlParameter($"@connector{i}", _element.Connectors[i]));
            }
        }

        public string Expression { get; private set; } = 
            "INSERT INTO VIDEOCARD VALUES (@gpu, @vram, @company, @series, @capacity, @memory, @title, @family, @length, @pin);";

        public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		public Videocard Videocard { get => _element; }
    }
}
