using Microsoft.EntityFrameworkCore;
using ViaPlan.Data;
using ViaPlan.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ViaPlanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ViaPlanDb")));

builder.Services.AddScoped<TripServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();