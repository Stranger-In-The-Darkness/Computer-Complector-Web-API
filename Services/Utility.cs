using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using ComputerComplectorWebAPI.Interfaces;

namespace ComputerComplectorWebAPI.Services
{
    public class Utility : IUtilityAsync
    {
        private SqlConnection _connection;

        public Utility(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        ~Utility()
        {
            Close();
        }

        public async Task<SqlDataReader> Execute(string command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlDataReader reader = null;

            SqlCommand comm = new SqlCommand(command, _connection);

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            reader = await comm.ExecuteReaderAsync();

            return reader;
        }

        public async Task<SqlDataReader> Execute(string command, params SqlParameter[] parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlDataReader reader = null;

            SqlCommand comm = new SqlCommand(command, _connection);
            comm.Parameters.AddRange(parameters.ToArray());

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            reader = await comm.ExecuteReaderAsync();

            return reader;
        }

        public async Task<SqlDataReader> Execute(string command, List<SqlParameter> parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlDataReader reader = null;

            SqlCommand comm = new SqlCommand(command, _connection);
            comm.Parameters.AddRange(parameters.ToArray());

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            reader = await comm.ExecuteReaderAsync();

            return reader;
        }

        public void Close()
        {
            lock (_connection)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
            }
        }
    }
}
