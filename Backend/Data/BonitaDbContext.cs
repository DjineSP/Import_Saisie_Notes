using Backend.Models.BonitaModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Data;

public class BonitaDbContext : DbContext
{
    public BonitaDbContext(DbContextOptions<BonitaDbContext> options) : base(options) { }

    // DbSets pour chaque entité
    public DbSet<Tenant>? Tenants { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<UserContactInfo>? UserContactInfos { get; set; }
    public DbSet<UserLogin>? UserLogins { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<UserMembership>? UserMemberships { get; set; }


    // Configurations supplémentaires (si nécessaire)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration des relations entre les entités
        modelBuilder.Entity<User>()
            .HasOne(u => u.Tenant)
            .WithMany()
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        modelBuilder.Entity<UserContactInfo>()
            .HasOne(uci => uci.Tenant)
            .WithMany()
            .HasForeignKey(uci => uci.TenantId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        modelBuilder.Entity<UserContactInfo>()
            .HasOne(uci => uci.User)
            .WithMany()
            .HasForeignKey(uci => uci.UserId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        modelBuilder.Entity<UserLogin>()
            .HasOne(ul => ul.Tenant)
            .WithMany()
            .HasForeignKey(ul => ul.TenantId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        modelBuilder.Entity<UserLogin>()
            .HasOne(ul => ul.User)
            .WithMany()
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        modelBuilder.Entity<UserMembership>()
            .HasOne(um => um.Tenant)
            .WithMany()
            .HasForeignKey(um => um.TenantId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        modelBuilder.Entity<UserMembership>()
            .HasOne(um => um.User)
            .WithMany()
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        modelBuilder.Entity<UserMembership>()
            .HasOne(um => um.Role)
            .WithMany()
            .HasForeignKey(um => um.RoleId)
            .OnDelete(DeleteBehavior.Restrict);  // Changement de Cascade à Restrict

        // Ajoutez des données de test ici si nécessaire
        modelBuilder.Entity<Tenant>().HasData(
            new Tenant
            {
                Id = 1,
                Name = "facsciences",
                Description = "Faculte des sciences",
                Status = true,
                Created = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc)
            },
            new Tenant
            {
                Id = 2,
                Name = "faclettres",
                Description = "Faculte des lettres",
                Status = false,
                Created = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                TenantId = 1,
                Name = "Admin",
                DisplayName = "Administrator",
                CreationDate = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc),
                LastUpdate = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc)
            },
            new Role
            {
                Id = 2,
                TenantId = 2,
                Name = "Prof",
                DisplayName = "Professor",
                CreationDate = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc),
                LastUpdate = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc)
            },
            new Role
            {
                Id = 3,
                TenantId = 2,
                Name = "Stud",
                DisplayName = "Student",
                CreationDate = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc),
                LastUpdate = new DateTime(2024, 2, 10, 12, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
