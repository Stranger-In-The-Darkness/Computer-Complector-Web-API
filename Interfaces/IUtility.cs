using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace ComputerComplectorWebAPI.Interfaces
{
    public interface IUtility
    {
        SqlDataReader Execute(string command);

        SqlDataReader Execute(string command, params SqlParameter[] parameters);

        SqlDataReader Execute(string command, List<SqlParameter> parameters);

        void Close();
    }
}
