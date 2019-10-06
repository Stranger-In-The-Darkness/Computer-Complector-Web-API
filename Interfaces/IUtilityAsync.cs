using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace ComputerComplectorWebAPI.Interfaces
{
    public interface IUtilityAsync
    {
        Task<SqlDataReader> ExecuteReader(string command);

        Task<SqlDataReader> ExecuteReader(string command, params SqlParameter[] parameters);

        Task<SqlDataReader> ExecuteReader(string command, List<SqlParameter> parameters);

        Task<int> ExecuteNonQuery(string command);
        
        Task<int> ExecuteNonQuery(string command, params SqlParameter[] parameters);
        
        Task<int> ExecuteNonQuery(string command, List<SqlParameter> parameters);

        void Close();
    }
}
