// Program.cs de VotacionApp.Api
using Microsoft.EntityFrameworkCore;
using VotacionApp.Data;
using VotacionApp.Business.Interfaces; // Asegúrate de tener este using
using VotacionApp.Business.Repositories; // Asegúrate de tener este using
using VotacionApp.Business.Services; // Asegúrate de tener este using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias para la capa de negocio
builder.Services.AddScoped<IVotanteRepository, VotanteRepository>();
builder.Services.AddScoped<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<VotacionService>(); // El servicio de negocio

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