using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using ComputerComplectorWebAPI.Interfaces;

namespace ComputerComplectorWebAPI.Services
{
    public class DBUtility : IUtilityAsync
    {
        private SqlConnection _connection;

        public DBUtility(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null or empty!");
            }
            _connection = new SqlConnection(connectionString);
        }

        ~DBUtility()
        {
            Close();
        }

        public async Task<SqlDataReader> ExecuteReader(string command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlCommand comm = new SqlCommand(command, _connection);

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            return await comm.ExecuteReaderAsync();
        }

        public async Task<SqlDataReader> ExecuteReader(string command, params SqlParameter[] parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlCommand comm = new SqlCommand(command, _connection);
            comm.Parameters.AddRange(parameters.ToArray());

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            return await comm.ExecuteReaderAsync();
        }

        public async Task<SqlDataReader> ExecuteReader(string command, List<SqlParameter> parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlCommand comm = new SqlCommand(command, _connection);
            comm.Parameters.AddRange(parameters.ToArray());

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            return await comm.ExecuteReaderAsync();
        }

        public async Task<int> ExecuteNonQuery(string command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlCommand comm = new SqlCommand(command, _connection);

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            return await comm.ExecuteNonQueryAsync();
        }

        public async Task<int> ExecuteNonQuery(string command, params SqlParameter[] parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlCommand comm = new SqlCommand(command, _connection);
            comm.Parameters.AddRange(parameters);

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            return await comm.ExecuteNonQueryAsync();
        }

        public async Task<int> ExecuteNonQuery(string command, List<SqlParameter> parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            SqlCommand comm = new SqlCommand(command, _connection);
            comm.Parameters.AddRange(parameters.ToArray());

            lock (_connection)
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
            }

            return await comm.ExecuteNonQueryAsync();
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
