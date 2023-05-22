using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetSection("ConnectionStrings");
builder.Services.Configure<Connection>(connection);

builder.Services.AddScoped<AddressesRepository, AddressesRepository>();
builder.Services.AddScoped<AddressesService, AddressesService>();
builder.Services.AddScoped<CartItemsRepository, CartItemsRepository>();
builder.Services.AddScoped<CartItemsServices, CartItemsServices>();
builder.Services.AddScoped<CartsRepository, CartsRepository>();
builder.Services.AddScoped<CartsServices, CartsServices>();
builder.Services.AddScoped<CustomerAddressesRepository, CustomerAddressesRepository>();
builder.Services.AddScoped<CustomerAddressesServices, CustomerAddressesServices>();
builder.Services.AddScoped<CustomersRepository, CustomersRepository>();
builder.Services.AddScoped<CustomersServices, CustomersServices>();
builder.Services.AddScoped<OrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<OrderItemsServices, OrderItemsServices>();
builder.Services.AddScoped<OrderRepository, OrderRepository>();
builder.Services.AddScoped<OrdersServices, OrdersServices>();

builder.Services.AddSingleton<BrandsRepository>();
builder.Services.AddSingleton<BrandsServices>();
builder.Services.AddSingleton<CategoriesRepository>();
builder.Services.AddSingleton<CategoriesServices>();
builder.Services.AddSingleton<PeopleRepository>();
builder.Services.AddSingleton<PeopleServices>();
builder.Services.AddSingleton<ProductBandsRepository>();
builder.Services.AddSingleton<ProductBrandsServices>();
builder.Services.AddSingleton<ProductCategoriesRepository>();
builder.Services.AddSingleton<ProductCategoriesServices>();
builder.Services.AddSingleton<ProductsRepository>();
builder.Services.AddSingleton<ProductsService>();

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
