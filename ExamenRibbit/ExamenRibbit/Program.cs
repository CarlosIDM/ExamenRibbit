using ExamenRibbit.Core.Productos;
using ExamenRibbit.DataAccess;
using ExamenRibbit.Repository.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//SQLite
string connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionStrings));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//REPOSITORY
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//REPOSITORY


//CLASS
builder.Services.AddTransient<ProductosBL>();
//CLASS

var app = builder.Build();

// Configurar la base de datos para que se cree automáticamente con migraciones si es necesario
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dbContext.Database.Migrate();  // Aplica migraciones si hay alguna pendiente
}


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
