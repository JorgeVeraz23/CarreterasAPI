using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Agrega esta referencia
using APICarreteras.Repository;
using APICarreteras.Repository.IRepositorio;
using APICarreteras;

var builder = WebApplication.CreateBuilder(args);

// Configuración de acceso a la configuración
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Registra los servicios en el contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Registra RedesVialesDbContext
builder.Services.AddDbContext<RedesVialesDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

// Registra el repositorio
builder.Services.AddScoped<ICantonRepositorio, CantonRepositorio>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
