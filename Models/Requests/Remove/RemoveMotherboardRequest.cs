using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Remove
{
    public class RemoveMotherboardRequest
    {
        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public int ID { get; private set; }

        public RemoveMotherboardRequest(int id)
        {
			ID = id;
            Parameter = new SqlParameter("@id", id);
            Expression = "DELETE FROM MOTHERBOARD WHERE ID = @id";
        }
    }
}
