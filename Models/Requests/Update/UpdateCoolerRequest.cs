using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateCoolerRequest
    {
		private Cooler _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public Cooler Cooler { get => _element; }

        public UpdateCoolerRequest(Cooler cooler)
        {
			_element = cooler;
            Parameter = new SqlParameter("@id", cooler?.ID ?? -1);
            Expression = "ALTER COOLER WHERE ID = @id";
        }
    }
}
