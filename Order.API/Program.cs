using Microsoft.EntityFrameworkCore;
using Order.Business.Implementation;
using Order.Business.Interfaces;
using Order.DataContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderConnectionString") ?? throw new InvalidOperationException("Connection string 'OrderConnectionString' not found.")));
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
