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

        modelBuilder.Entity<University>().HasData(
            new University { Id = "1", Name = "Софийски университет „Св. Климент Охридски\"" },
            new University { Id = "2", Name = "Технически университет - София" },
            new University { Id = "3", Name = "Пловдивски университет „Паисий Хилендарски\"" },
            new University { Id = "4", Name = "Медицински университет - София" },
            new University { Id = "5", Name = "Нов български университет" },
            new University { Id = "6", Name = "Великотърновски университет „Св. св. Кирил и Методий\"" }, // ВТУ
            new University { Id = "7", Name = "Югозападен университет „Неофит Рилски\"" },
            new University { Id = "8", Name = "Тракийски университет" },
            new University { Id = "9", Name = "Варненски свободен университет „Черноризец Храбър\"" },
            new University { Id = "10", Name = "Бургаски свободен университет" }
        ); modelBuilder.Entity<UniversityFaculty>().HasData(
    // Софийски университет „Св. Климент Охридски"
    new UniversityFaculty("1", "1", "Философски факултет"),
    new UniversityFaculty("2", "1", "Факултет по математика и информатика"),
    new UniversityFaculty("3", "1", "Юридически факултет"),

    // Технически университет - София
    new UniversityFaculty("4", "2", "Факултет по електроника"),
    new UniversityFaculty("5", "2", "Факултет по компютърни системи и технологии"),
    new UniversityFaculty("6", "2", "Факултет по машинно инженерство"),

    // Пловдивски университет „Паисий Хилендарски"
    new UniversityFaculty("7", "3", "Факултет по химия"),
    new UniversityFaculty("8", "3", "Факултет по педагогика"),
    new UniversityFaculty("9", "3", "Факултет по икономически науки"),

    // Медицински университет - София
    new UniversityFaculty("10", "4", "Факултет по медицина"),
    new UniversityFaculty("11", "4", "Факултет по фармация"),
    new UniversityFaculty("12", "4", "Факултет по обществено здраве"),

    // Нов български университет
    new UniversityFaculty("13", "5", "Факултет по изкуствата"),
    new UniversityFaculty("14", "5", "Факултет по информатика"),
    new UniversityFaculty("15", "5", "Факултет по бизнес и администрация"),

    // Великотърновски университет „Св. св. Кирил и Методий“
    new UniversityFaculty("16", "6", "Факултет по математика и информатика"),
    new UniversityFaculty("17", "6", "Факултет по педагогика"),
    new UniversityFaculty("18", "6", "Факултет по изкуствата"),

    // Югозападен университет „Неофит Рилски“
    new UniversityFaculty("19", "7", "Факултет по бизнес и администрация"),
    new UniversityFaculty("20", "7", "Факултет по педагогика"),
    new UniversityFaculty("21", "7", "Факултет по инженерни науки"),

    // Тракийски университет
    new UniversityFaculty("22", "8", "Факултет по аграрни науки"),
    new UniversityFaculty("23", "8", "Факултет по медицина"),
    new UniversityFaculty("24", "8", "Факултет по икономика"),

    // Варненски свободен университет „Черноризец Храбър“
    new UniversityFaculty("25", "9", "Факултет по информационни технологии"),
    new UniversityFaculty("26", "9", "Факултет по мениджмънт"),
    new UniversityFaculty("27", "9", "Факултет по право"),

    // Бургаски свободен университет
    new UniversityFaculty("28", "10", "Факултет по компютърни науки"),
    new UniversityFaculty("29", "10", "Факултет по инженерни науки"),
    new UniversityFaculty("30", "10", "Факултет по бизнес")
);

        modelBuilder.Entity<Teacher>().HasData(
    // University 1: Софийски университет „Св. Климент Охридски"
    new Teacher("1", TeacherTitle.Professor, "Александър", "", "Иванов", "1", null, "1", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("2", TeacherTitle.Professor, "Георги", "", "Георгиев", "1", null, "1", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("3", TeacherTitle.Professor, "Мартин", "", "Димитров", "1", null, "1", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("4", TeacherTitle.Professor, "Димитър", "", "Петров", "1", null, "1", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("5", TeacherTitle.Professor, "Никола", "", "Николов", "1", null, "1", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),

    // University 2: Технически университет - София
    new Teacher("6", TeacherTitle.Professor, "Борис", "", "Христов", "2", null, "2", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("7", TeacherTitle.Professor, "Иван", "", "Стоянов", "2", null, "2", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("8", TeacherTitle.Professor, "Виктор", "", "Тодоров", "2", null, "2", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("9", TeacherTitle.Professor, "Стефан", "", "Илиев", "2", null, "2", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("10", TeacherTitle.Professor, "Йордан", "", "Ангелов", "2", null, "2", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),

    // University 3: Пловдивски университет „Паисий Хилендарски"
    new Teacher("11", TeacherTitle.Professor, "Христо", "", "Иванов", "3", null, "3", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("12", TeacherTitle.Professor, "Емил", "", "Георгиев", "3", null, "3", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("13", TeacherTitle.Professor, "Николай", "", "Димитров", "3", null, "3", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("14", TeacherTitle.Professor, "Петър", "", "Петров", "3", null, "3", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1)),
    new Teacher("15", TeacherTitle.Professor, "Тодор", "", "Николов", "3", null, "3", null, 0, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1))

// Може да продължиш със същия подход за останалите университети.
);
    }
}
