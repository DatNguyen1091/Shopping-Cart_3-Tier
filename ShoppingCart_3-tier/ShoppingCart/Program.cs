using Microsoft.AspNetCore.Authentication.Cookies;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("AllowOrigin", policy =>
{
    //policy.WithOrigins("http://127.0.0.1:5500").AllowCredentials();
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5500").AllowCredentials();
    //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));


builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)    
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });



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

builder.Services.AddSingleton<UsersRepository>();
builder.Services.AddSingleton<UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
endpoints.MapControllers());

app.Run();
