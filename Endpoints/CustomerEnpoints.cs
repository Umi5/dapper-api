using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using dapper_api.Models;
using dapper_api.Service;
using Microsoft.Data.SqlClient;

namespace dapper_api.Endpoints
{
    public static class CustomerEnpoints
    {
        public static void MapCustomerEnpoints(this IEndpointRouteBuilder builder){
            
            builder.MapGet("customers", async (SqlConnectionFactory _sqlConnectionFactory) => 
                {
                    var connection = _sqlConnectionFactory.Create();

                    const string sql = "SELECT Id, Name, Email FROM Customers";

                    var customers = await connection.QueryAsync<Customer>(sql);

                    return Results.Ok(customers);
                });

             builder.MapGet("customers/{id}", async (int id, SqlConnectionFactory _sqlConnectionFactory) => 
                {
                    var connection = _sqlConnectionFactory.Create();

                    const string sql = "SELECT Id, Name, Email FROM Customers WHERE Id = @CustomerId";

                    var customer = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { CustomerId = id });

                    
                    return customer is not null ? Results.Ok(customer) : Results.NotFound("Customer not found");
                });

        }
    }
}