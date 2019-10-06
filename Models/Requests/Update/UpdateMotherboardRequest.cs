using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateMotherboardRequest
    {
		private Motherboard _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public Motherboard Motherboard { get => _element; }

        public UpdateMotherboardRequest(Motherboard motherboard)
        {
			_element = motherboard;

            Parameter = new SqlParameter("@id", motherboard?.ID ?? -1);
            Expression = "ALTER MOTHERBOARD WHERE ID = @id";
        }
    }
}
