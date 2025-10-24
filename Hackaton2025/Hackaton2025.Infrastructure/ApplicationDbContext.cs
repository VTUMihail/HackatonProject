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
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<PastProcedure> PastProcedures { get; set; }

    public DbSet<PastProcedureJuryMember> PastProcedureJuryMembers { get; set; }

    public DbSet<University> Universities { get; set; }

    public DbSet<UniversityFaculty> UniversityFaculties { get; set; }
}
