using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Update
{
    public class UpdateRAMRequest
    {
		private RAM _element;

        public string Expression { get; } = null;
        public SqlParameter Parameter { get; } = null;

		public RAM RAM { get => _element; }

        public UpdateRAMRequest(RAM ram)
        {
			_element = ram;

            Parameter = new SqlParameter("@id", ram?.ID ?? -1);
            Expression = "ALTER RAM WHERE ID = @id";
        }
    }
}
