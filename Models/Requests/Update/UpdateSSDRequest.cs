using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateSSDRequest
    {
		private SSD _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public SSD SSD { get => _element; }

        public UpdateSSDRequest(SSD ssd)
        {
			_element = ssd;

            Parameter = new SqlParameter("@id", ssd?.ID);
            Expression = "ALTER SSD WHERE ID = @id";
        }
    }
}
