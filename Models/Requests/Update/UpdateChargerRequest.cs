using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateChargerRequest
    {
		private Charger _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public Charger Charger { get => _element; }

        public UpdateChargerRequest(Charger charger)
        {
			_element = charger;

            Parameter = new SqlParameter("@id", charger?.ID ?? -1);
            Expression = "ALTER CHARGER WHERE ID = @id";
        }
    }
}
