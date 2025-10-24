using Hackaton2025.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton2025.Infrastructure;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<PastProcedure> PastProcedures { get; set; }
    public DbSet<PastProcedureJury> PastProcedureJuries { get; set; }
    public DbSet<PastProcedureJuryMember> PastProcedureJuryMembers { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<UniversityFaculty> UniversityFaculties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -------------------------
        // Primary keys
        // -------------------------
        modelBuilder.Entity<Teacher>().HasKey(t => t.Id);
        modelBuilder.Entity<PastProcedure>().HasKey(p => p.Id);
        modelBuilder.Entity<PastProcedureJury>().HasKey(j => j.Id);
        modelBuilder.Entity<PastProcedureJuryMember>()
                    .HasKey(m => new { m.PastProcedureJuryId, m.TeacherId }); // composite key
        modelBuilder.Entity<University>().HasKey(u => u.Id);
        modelBuilder.Entity<UniversityFaculty>().HasKey(f => f.Id);

        // -------------------------
        // University ↔ Faculties (1..*)
        // UniversityFaculty has UniversityId (string)
        // -------------------------
        modelBuilder.Entity<University>()
            .HasMany(u => u.Faculties)
            .WithOne() // no navigation back from UniversityFaculty
            .HasForeignKey(f => f.UniversityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UniversityFaculty>()
            .HasIndex(f => f.UniversityId);

        // -------------------------
        // University ↔ Teachers (1..*)
        // Teacher has UniversityId and optional nav University
        // -------------------------
        modelBuilder.Entity<University>()
            .HasMany(u => u.Teachers)
            .WithOne(t => t.University)
            .HasForeignKey(t => t.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Teacher>()
            .HasIndex(t => t.UniversityId);

        // -------------------------
        // UniversityFaculty ↔ Teachers (1..*)
        // Teacher has UniversityFactultyId (note spelling) + optional nav UniversityFaculty
        // -------------------------
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.UniversityFaculty)
            .WithMany() // UniversityFaculty doesn't expose a Teachers collection
            .HasForeignKey(t => t.UniversityFactultyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Teacher>()
            .HasIndex(t => t.UniversityFactultyId);

        // -------------------------
        // PastProcedure ↔ PastProcedureJury (1..*)
        // PastProcedureJury lacks an FK property → use a shadow FK "PastProcedureId"
        // -------------------------
        modelBuilder.Entity<PastProcedureJury>()
            .Property<string>("PastProcedureId"); // shadow FK

        modelBuilder.Entity<PastProcedure>()
            .HasMany(p => p.Juries)
            .WithOne() // no nav back on PastProcedureJury
            .HasForeignKey("PastProcedureId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PastProcedureJury>()
            .HasIndex("PastProcedureId");

        // -------------------------
        // PastProcedureJury ↔ PastProcedureJuryMember (1..*)
        // PastProcedureJuryMember has PastProcedureJuryId and nav PastProcedureJury
        // -------------------------
        modelBuilder.Entity<PastProcedureJuryMember>()
            .HasOne(m => m.PastProcedureJury)
            .WithMany(j => j.JuryMembers)
            .HasForeignKey(m => m.PastProcedureJuryId)
            .OnDelete(DeleteBehavior.Cascade);

        // -------------------------
        // Teacher ↔ PastProcedureJuryMember (1..*)
        // PastProcedureJuryMember has TeacherId and nav Teacher
        // -------------------------
        modelBuilder.Entity<PastProcedureJuryMember>()
            .HasOne(m => m.Teacher)
            .WithMany(t => t.JuryMembers)
            .HasForeignKey(m => m.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PastProcedureJuryMember>()
            .HasIndex(m => m.TeacherId);

        // -------------------------
        // Optional: store enums as strings for readability (comment out if you prefer ints)
        // -------------------------
        modelBuilder.Entity<PastProcedure>()
            .Property(p => p.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Teacher>()
            .Property(t => t.Title)
            .HasConversion<string>();

        // -------------------------
        // Optional: simple column sizes / indexes for Teacher names
        // -------------------------
        modelBuilder.Entity<Teacher>().Property(t => t.FirstName).HasMaxLength(128);
        modelBuilder.Entity<Teacher>().Property(t => t.MiddleName).HasMaxLength(128);
        modelBuilder.Entity<Teacher>().Property(t => t.LastName).HasMaxLength(128);
        modelBuilder.Entity<Teacher>().HasIndex(t => new { t.LastName, t.FirstName });

        
    }
}
