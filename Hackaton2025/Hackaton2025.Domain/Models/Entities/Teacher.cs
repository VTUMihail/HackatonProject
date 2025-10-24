using Hackaton2025.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton2025.Domain.Models.Entities;
public class Teacher
{
    public string Id { get; }

    public TeacherTitle Title { get; private set; }

    public FullName FullName { get; }

    public string UniversityId { get; }

    public University? University { get; }
    public string UniversityFactultyId { get; }

    public UniversityFaculty? UniversityFaculty { get; }
}
