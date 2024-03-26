using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace dapper_api.Service
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public SqlConnection Create(){

            return new SqlConnection(_connectionString);
        }
    }
}