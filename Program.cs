using Dapper;
using dapper_api.Endpoints;
using dapper_api.Models;
using dapper_api.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serrviceProvider => {
    var _configuration = serrviceProvider.GetRequiredService<IConfiguration>();
    var connecionString = _configuration.GetConnectionString("DefaultConnection") ??
        throw new ApplicationException("Connection string is null");
        
    return new SqlConnectionFactory(connecionString);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCustomerEnpoints();

app.Run();

