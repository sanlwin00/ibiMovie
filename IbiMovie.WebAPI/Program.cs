using IbiMovie.Application.Interfaces;
using IbiMovie.Application.UseCases;
using IbiMovie.Infra.Data;
using IbiMovie.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add MySQL Db Service:
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionStr,
        new MySqlServerVersion(new Version(8, 0, 0)),
        options => { 
            options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            options.MigrationsAssembly("IbiMovie.WebAPI"); }
        );
    });

// Inject dependencies
builder.Services.AddScoped<IbiMovie.Application.Interfaces.IActorService, IbiMovie.Infra.Repositories.ActorService>();
builder.Services.AddScoped<IbiMovie.Application.Interfaces.IMovieService, IbiMovie.Infra.Repositories.MovieService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Handle ReferenceLoop
builder.Services.AddControllers().AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
