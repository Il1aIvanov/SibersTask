using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SibersDbContext(DbContextOptions<SibersDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         ConfigureProject(modelBuilder);
         ConfigureEmployee(modelBuilder);
         ConfigureRelationships(modelBuilder);
    }

    private static void ConfigureProject(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Project>();

        entity.HasKey(p => p.Id);
    
        entity.Property(p => p.ProjectName)
            .HasMaxLength(128)
            .IsRequired();
    
        entity.Property(p => p.CustomerCompanyName)
            .HasMaxLength(128)
            .IsRequired();
    
        entity.Property(p => p.ExecutorCompanyName)
            .HasMaxLength(128)
            .IsRequired();
    
        entity.Property(p => p.StartDate)
            .IsRequired();
    
        entity.Property(p => p.EndDate)
            .IsRequired();
    
        entity.Property(p => p.Priority)
            .IsRequired();
    }

    private static void ConfigureEmployee(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Employee>();

        entity.HasKey(e => e.Id);
    
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();
    
        entity.Property(e => e.Surname)
            .HasMaxLength(50)
            .IsRequired();
    
        entity.Property(e => e.Patronymic)
            .HasMaxLength(50)
            .IsRequired();
    
        entity.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired();
    
        entity.HasIndex(e => e.Email)
            .IsUnique();
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ProjectManager)
            .WithMany(e => e.ManagedProjects)
            .HasForeignKey(p => p.ProjectManagerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Employees)
            .WithMany(e => e.Projects)
            .UsingEntity(j => j.ToTable("ProjectEmployee"));
    }
}