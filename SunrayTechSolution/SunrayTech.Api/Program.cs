using SunrayTech.Api.Data;
using Microsoft.EntityFrameworkCore;
using SunrayTech.Api.Repositories.Contracts;
using SunrayTech.Api.Repositories;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<SunrayTechDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SunrayTechConnection"))
    );

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.WithOrigins("http://localhost:7064", "https://localhost:7064")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
