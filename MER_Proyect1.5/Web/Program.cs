using Business;
using Business.Interfaces;
using Data.Interfaces;
using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// REGISTRO DE DEPENDENCIAS GENERALES
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IGenericService<FormDto>), typeof(GenericService<FormDto, Form>));
builder.Services.AddScoped(typeof(IGenericService<ModuleDto>), typeof(GenericService<ModuleDto, Module>));


// Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Servicios específicos por entidad (si los necesitas para lógica personalizada)
//builder.Services.AddScoped<FormBusiness>();
//builder.Services.AddScoped<IRepository<Form>, FormData>();



/// <summary>
/// Obtiene el proveedor de base de datos y la cadena de conexión desde la configuración.
/// </summary>
string databaseProvider = builder.Configuration["DatabaseProvider"]; 
string connectionString = builder.Configuration.GetConnectionString(databaseProvider);

/// <summary>
/// Dependiendo del proveedor de base de datos, se configura el DbContext y los servicios.
/// </summary>

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    switch (databaseProvider)
    {
        case "SqlServer":
            options.UseSqlServer(connectionString);
            break;
        case "PostgreSql":
            options.UseNpgsql(connectionString);
            break;
        case "MySql":
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            break;
        default:
            throw new InvalidOperationException("Proveedor de base de datos no soportado");
    }
});




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
