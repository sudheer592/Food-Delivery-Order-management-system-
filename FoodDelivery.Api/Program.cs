using FoodDelivery.Api.Data;
using FoodDelivery.Api.Middleware;
using FoodDelivery.Api.Repositories;
using FoodDelivery.Api.Repositories.Interfaces;
using FoodDelivery.Api.Services;
using FoodDelivery.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=fooddelivery.db";
builder.Services.AddDbContext<FoodDeliveryDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularOrigin");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>();
    await context.Database.EnsureCreatedAsync();
    await SeedData.InitializeAsync(context);
}

app.Run();
