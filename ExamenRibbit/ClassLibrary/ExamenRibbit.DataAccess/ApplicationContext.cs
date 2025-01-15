using ExamenRibbit.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExamenRibbit.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Productos> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            int product1 = 1;
            int product2 = 2;
            modelBuilder.Entity<Productos>().HasData(
                new Productos { Id = product1, Nombre = "Refresco", Precio = 100, CantidadEnStock = 10, FechaCreacion = DateTime.UtcNow },
                new Productos { Id = product2, Nombre = "Tortilla", Precio = 200, CantidadEnStock = 20, FechaCreacion = DateTime.UtcNow }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
