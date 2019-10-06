using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
    public class AddRamRequest : AddRequestBase
    {
        private RAM _element;
        private List<SqlParameter> _parameters;

        public AddRamRequest(RAM element)
        {
            _element = element;

            _parameters = new List<SqlParameter>();

            if (Validate(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate(_element.Purpose, "element.Purpose"))
            {
                _parameters.Add(new SqlParameter("@purpose", _element.Purpose));
            }

            if (Validate(_element.MemoryType, "element.MemoryType"))
            {
                _parameters.Add(new SqlParameter("@memoryType", _element.MemoryType));
            }

            if (Validate(_element.Volume, "element.Volume"))
            {
                _parameters.Add(new SqlParameter("@memoryVolume", _element.Volume));
            }

            if (Validate(_element.Freq, "element.Freq"))
            {
                _parameters.Add(new SqlParameter("@frequency", _element.Freq));
            }

            if (Validate(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate(_element.Series, "element.Series"))
            {
                _parameters.Add(new SqlParameter("@series", _element.Series));
            }

            if (Validate(_element.CL, "element.CL"))
            {
                _parameters.Add(new SqlParameter("@casLatency", _element.CL));
            }
        }

        public string Expression { get; private set; } =
            "INSERT INTO RAM VALUES (@title, @purpose, @memoryType, @memoryVolume, @modulesAmount, @frequency, @company, @series, @casLatency);";

        public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		public RAM RAM { get => _element; }
    }
}
