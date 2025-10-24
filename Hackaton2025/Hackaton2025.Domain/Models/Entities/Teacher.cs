namespace Hackaton2025.Domain.Models.Entities;
public class Teacher
{
    private Teacher() { }
    public Teacher(
        string id,
        TeacherTitle title,
        string firstName,
        string middleName,
        string lastName,
        string universityId,
        University? university,
        string universityFactultyId,
        UniversityFaculty? universityFaculty,
        decimal distance,
        DateTime secondLastJuryMemberDate,
        DateTime lastJuryMemberDate)
    {
        Id = id;
        Title = title;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        UniversityId = universityId;
        University = university;
        UniversityFactultyId = universityFactultyId;
        UniversityFaculty = universityFaculty;
        Distance = distance;
        SecondLastJuryMemberDate = secondLastJuryMemberDate;
        LastJuryMemberDate = lastJuryMemberDate;
    }

    public string Id { get; set; }

    public TeacherTitle Title { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string UniversityId { get; set; }

    public University? University { get; set; }

    public string UniversityFactultyId { get; set; }

    public UniversityFaculty? UniversityFaculty { get; set; }

    public ICollection<PastProcedureJuryMember> JuryMembers { get; set; }

    public decimal Distance { get; set; }

    public DateTime SecondLastJuryMemberDate { get; set; }

    public DateTime LastJuryMemberDate { get; set; }

    public void AddLastJuryMemberDate(DateTime lastJuryMemberDate)
    {
        SecondLastJuryMemberDate = LastJuryMemberDate;
        LastJuryMemberDate = lastJuryMemberDate;
    }
}