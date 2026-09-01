using Microsoft.EntityFrameworkCore;
using apicampeonatosfifa.infraestructura.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registro del contexto de base de datos (Entity Framework Core + SQL Server)
builder.Services.AddDbContext<CampeonatosFIFAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CampeonatosFIFA")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Endpoint temporal SOLO para verificar que el dominio y el contexto
// se conectan correctamente a la base de datos. Se reemplazará por los
// controladores de la capa de presentación en la siguiente entrega.
app.MapGet("/campeonatos/verificar-contexto", async (CampeonatosFIFAContext contexto) =>
{
    var campeonatos = await contexto.Campeonatos.ToListAsync();
    return Results.Ok(campeonatos);
})
.WithName("VerificarContexto");

app.Run();
