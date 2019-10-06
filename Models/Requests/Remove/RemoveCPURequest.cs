using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Remove
{
    public class RemoveCPURequest
    {
        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public int ID { get; private set; }

        public RemoveCPURequest(int id)
        {
			ID = id;

            Parameter = new SqlParameter("@id", id);
            Expression = "DELETE FROM CPU WHERE ID = @id";
        }
    }
}
