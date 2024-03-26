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
            
            var group = builder.MapGroup("customers");

            group.MapGet("", async (SqlConnectionFactory _sqlConnectionFactory) => 
            {
                using var connection = _sqlConnectionFactory.Create();

                const string sql = "SELECT Id, Name, Email FROM Customers";

                var customers = await connection.QueryAsync<Customer>(sql);

                return Results.Ok(customers);
            });

            group.MapGet("{id}", async (int id, SqlConnectionFactory _sqlConnectionFactory) => 
            {
                using var connection = _sqlConnectionFactory.Create();

                const string sql = "SELECT Id, Name, Email FROM Customers WHERE Id = @CustomerId";

                var customer = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { CustomerId = id });

                
                return customer is not null ? Results.Ok(customer) : Results.NotFound("Customer not found");
            });

            group.MapPost("", async (Customer customer ,SqlConnectionFactory _sqlConnectionFactory) => 
            {
                using var connection = _sqlConnectionFactory.Create();

                const string sql = "INSERT INTO Customers (Id, Name, Email) VALUES (@Id, @Name, @Email)";

                await connection.ExecuteAsync(sql, customer);

                return Results.Ok();
            });

            group.MapPut("{id}", async (int id, Customer customer ,SqlConnectionFactory _sqlConnectionFactory) => 
            {
                customer.Id = id; //Para esto se utilizaria un DTO
                using var connection = _sqlConnectionFactory.Create();

                const string sql = "UPDATE Customers SET Id= @Id , Name= @Name, Email= @Email WHERE Id = @Id";

                await connection.ExecuteAsync(sql, customer);
                return Results.NoContent();
            });

            group.MapDelete("{id}" , async (int id, SqlConnectionFactory _sqlConnectionFactory) => {
                using var connection = _sqlConnectionFactory.Create();

                const string sql = "DELETE FROM Customers WHERE Id =@CustomerId";
                await connection.ExecuteAsync(sql, new { CustomerId = id } );

                return Results.NoContent();
            });
        }
    }
}