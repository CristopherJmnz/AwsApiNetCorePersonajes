using AwsApiNetCorePersonajes.Data;
using AwsApiNetCorePersonajes.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Api personajes aws CristopherJmnz"
    });
});
string connectionString = builder.Configuration.GetConnectionString("MySqlAws");
builder.Services.AddTransient<PersonajesRepository>();
builder.Services.AddDbContext<PersonajesContext>
    (options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddCors(o => o.AddPolicy("CorsEnabled", builder =>
{
    builder.WithOrigins("*")
           .AllowAnyMethod()
           .AllowAnyHeader();

    // U Can Filter Here
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Personajes CristopherJmnz");
    options.RoutePrefix = "";
});

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseCors("CorsEnabled");
app.UseAuthorization();

app.MapControllers();

app.Run();
