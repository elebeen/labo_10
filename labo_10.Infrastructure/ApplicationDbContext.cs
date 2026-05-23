using Microsoft.EntityFrameworkCore;
using labo_10.Infrastructure.Models;

namespace labo_10.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Tu tabla de usuarios
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; } // Tu tabla intermedia

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 🔴 SOLUCIÓN: Configurar la clave primaria compuesta
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId }); 
        // Cambia 'UserId' y 'RoleId' por los nombres exactos de las propiedades de tu clase UserRole
    }
}