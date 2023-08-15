using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Agrega esta referencia
using APICarreteras.Repository;
using APICarreteras.Repository.IRepositorio;
using APICarreteras;
using System.Text.Json.Serialization;
using APICarreteras.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de acceso a la configuración
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Registra los servicios en el contenedor.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Registra RedesVialesDbContext
builder.Services.AddDbContext<RedesVialesDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});


// Registra el repositorio
builder.Services.AddScoped<ICantonRepositorio, CantonRepositorio>();
builder.Services.AddScoped<ITipoDeViaRepositorio, TipoDeViaRepositorio>();
builder.Services.AddScoped<ICarreteraRepositorio, CarreteraRepositorio>();
builder.Services.AddScoped<ITramoRepositorio, TramoRepositorio>();
builder.Services.AddScoped<IAlcantarilladoRepositorio, AlcantarilladoRepositorio>();
builder.Services.AddScoped<IAccesorioRepositorio, AccesorioRepositorio>();
builder.Services.AddScoped<IPuenteRepositorio,  PuenteRepositorio>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");
app.UseAuthorization();
app.MapControllers();

app.Run();
