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
        Task<SqlDataReader> Execute(string command);

        Task<SqlDataReader> Execute(string command, params SqlParameter[] parameters);

        Task<SqlDataReader> Execute(string command, List<SqlParameter> parameters);

        void Close();
    }
}
