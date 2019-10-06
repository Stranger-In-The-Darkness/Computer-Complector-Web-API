using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateVideocardRequest
    {
		private Videocard _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public Videocard Videocard { get => _element; }

        public UpdateVideocardRequest(Videocard videocard)
        {
			_element = videocard;

            Parameter = new SqlParameter("@id", videocard?.ID ?? -1);
            Expression = "ALTER VIDEOCARD WHERE ID = @id";
        }
    }
}
