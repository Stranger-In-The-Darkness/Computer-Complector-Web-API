using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateBodyRequest
    {
		private Body _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public Body Body { get => _element; }

        public UpdateBodyRequest(Body body)
        {
			_element = body;
            Parameter = new SqlParameter("@id", body?.ID ?? -1);
            Expression = "ALTER BODY WHERE ID = @id";
        }
    }
}
