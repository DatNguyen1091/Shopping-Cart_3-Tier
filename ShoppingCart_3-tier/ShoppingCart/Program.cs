using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetSection("ConnectionStrings");
builder.Services.Configure<Connection>(connection);

builder.Services.AddSingleton<AddressesRepository>();
builder.Services.AddSingleton<AddressesService>();

builder.Services.AddSingleton<ProductsRepository>();
builder.Services.AddSingleton<ProductsService>();

//builder.Services.AddScoped<AddressesRepository, AddressesRepository>();
//builder.Services.AddScoped<AddressesService, AddressesService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Connection.ConnectionString = "Server=DATNGUYEN\\SQLEXPRESS;Database=ShoppingCart000;Integrated Security=True;";

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
