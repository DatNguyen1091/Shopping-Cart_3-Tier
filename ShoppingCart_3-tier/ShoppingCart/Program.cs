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

builder.Services.AddSingleton<AddressesRepository>();
builder.Services.AddSingleton<AddressesService>();
builder.Services.AddSingleton<CartItemsRepository>();
builder.Services.AddSingleton<CartItemsServices>();
builder.Services.AddSingleton<CartsRepository>();
builder.Services.AddSingleton<CartsServices>();
builder.Services.AddSingleton<CustomerAddressesRepository>();
builder.Services.AddSingleton<CustomerAddressesServices>();
builder.Services.AddSingleton<CustomersRepository>();
builder.Services.AddSingleton<CustomersServices>();
builder.Services.AddSingleton<OrderItemsRepository>();
builder.Services.AddSingleton<OrderItemsServices>();
builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<OrdersServices>();
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

builder.Services.AddSingleton<ListProCateReposiory>();
builder.Services.AddSingleton<ListProCateServices>();

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
