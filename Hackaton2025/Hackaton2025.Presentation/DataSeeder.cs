using Hackaton2025.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Infrastructure
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            await db.Database.MigrateAsync();



            var rng = new Random();
            string Id() => Guid.NewGuid().ToString("N");

            // Helper data
            var uniNames = new[]
            {
                "National Technical University", "Metropolitan University", "International Institute of Technology",
                "Central State University", "Grand Valley University", "Polytechnic University",
                "Eastern Tech", "Western Tech", "Northern University", "Southern University", "VTU"
            };

            var facNames = new[]
            {
                "Computer Science", "Engineering", "Mathematics", "Physics", "Chemistry",
                "AI and Data Science", "Economics", "Mechanical Engineering", "Civil Engineering", "Statistics"
            };

            var firstNames = new[]
            {
                "Alice", "Bob", "Charlie", "Diana", "Eve", "Frank", "Grace", "Hank",
                "Ivy", "Jack", "Karen", "Liam", "Mia", "Noah", "Olivia", "Paul",
                "Quinn", "Rita", "Sam", "Tina", "Uma", "Victor", "Wendy", "Yara", "Zack"
            };

            var lastNames = new[]
            {
                "Johnson", "Smith", "Brown", "Williams", "Jones", "Garcia", "Davis",
                "Miller", "Wilson", "Taylor", "Anderson", "Thomas", "Jackson", "White"
            };

            var titles = Enum.GetValues(typeof(TeacherTitle)).Cast<TeacherTitle>().ToArray();
            var procTypes = Enum.GetValues(typeof(PastProcedureType)).Cast<PastProcedureType>().ToArray();

            // -------------------
            // Universities
            // -------------------
            var universities = uniNames.Select(n => new University(Id(), n)).ToList();
            db.Universities.AddRange(universities);
            await db.SaveChangesAsync();

            // -------------------
            // Faculties
            // -------------------
            var faculties = new List<UniversityFaculty>();
            foreach (var u in universities)
            {
                for (int i = 0; i < 5; i++)
                {
                    var name = facNames[rng.Next(facNames.Length)];
                    faculties.Add(new UniversityFaculty(Id(), u.Id, name));
                }
            }
            db.UniversityFaculties.AddRange(faculties);
            await db.SaveChangesAsync();

            // -------------------
            // Teachers
            // -------------------
            var teachers = new List<Teacher>();
            foreach (var u in universities)
            {
                var facs = faculties.Where(f => f.UniversityId == u.Id).ToList();
                for (int i = 0; i < 30; i++)
                {
                    var f = facs[rng.Next(facs.Count)];
                    var t = new Teacher(
                        id: Id(),
                        title: titles[rng.Next(titles.Length)],
                        firstName: firstNames[rng.Next(firstNames.Length)],
                        middleName: "",
                        lastName: lastNames[rng.Next(lastNames.Length)],
                        universityId: u.Id,
                        university: u,
                        universityFactultyId: f.Id,
                        universityFaculty: f,
                        distance: Math.Round((decimal)(rng.NextDouble() * 10 + 1), 1),
                        secondLastJuryMemberDate: DateTime.Now.AddMonths(-rng.Next(12, 36)),
                        lastJuryMemberDate: DateTime.Now.AddMonths(-rng.Next(1, 12))
                    );
                    teachers.Add(t);
                }
            }
            db.Teachers.AddRange(teachers);
            await db.SaveChangesAsync();

            // -------------------
            // Past Procedures
            // -------------------
            var procedures = new List<PastProcedure>();
            for (int i = 0; i < 100; i++)
            {
                var p = new PastProcedure(
                    id: Id(),
                    type: procTypes[rng.Next(procTypes.Length)],
                    createdAt: DateTime.Now.AddMonths(-rng.Next(1, 48))
                );
                procedures.Add(p);
            }
            db.PastProcedures.AddRange(procedures);
            await db.SaveChangesAsync();

            // -------------------
            // Juries
            // -------------------
            var juries = new List<PastProcedureJury>();
            foreach (var p in procedures)
            {
                int juryCount = rng.Next(1, 4);
                for (int i = 0; i < juryCount; i++)
                {
                    var jury = new PastProcedureJury(Id(), PastProcedureType.Professor, DateTime.Now.AddMonths(-rng.Next(1, 24)));
                    db.Entry(jury).Property("PastProcedureId").CurrentValue = p.Id;
                    juries.Add(jury);
                }
            }
            db.PastProcedureJuries.AddRange(juries);
            await db.SaveChangesAsync();

            // -------------------
            // Jury Members
            // -------------------
            var members = new List<PastProcedureJuryMember>();
            var teacherIds = teachers.Select(t => t.Id).ToArray();

            foreach (var j in juries)
            {
                int memberCount = rng.Next(3, 6);
                var used = new HashSet<string>();
                for (int i = 0; i < memberCount; i++)
                {
                    string teacherId;
                    do
                    {
                        teacherId = teacherIds[rng.Next(teacherIds.Length)];
                    } while (!used.Add(teacherId));

                    members.Add(new PastProcedureJuryMember(j.Id, teacherId));
                }
            }

            db.PastProcedureJuryMembers.AddRange(members);
            await db.SaveChangesAsync();
        }
    }
}
