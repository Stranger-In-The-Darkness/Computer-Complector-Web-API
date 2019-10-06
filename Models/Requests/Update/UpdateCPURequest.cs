using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateCPURequest
    {
		private CPU _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public CPU CPU { get => _element; }

        public UpdateCPURequest(CPU cpu)
        {
			_element = cpu;

            Parameter = new SqlParameter("@id", cpu?.ID ?? -1);
            Expression = "ALTER CPU WHERE ID = @id";
        }
    }
}
