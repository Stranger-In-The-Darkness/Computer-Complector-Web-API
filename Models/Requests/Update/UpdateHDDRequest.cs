using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateHDDRequest
    {
		private HDD _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public HDD HDD { get => _element; }

        public UpdateHDDRequest(HDD hdd)
        {
			_element = hdd;
            Parameter = new SqlParameter("@id", hdd?.ID ?? -1);
            Expression = "ALTER HDD WHERE ID = @id";
        }
    }
}
