namespace Hackaton2025.Domain.Models.Entities;

public class PastProcedureJury
{
    private PastProcedureJury() { }
    public PastProcedureJury(
        string id,
        PastProcedureType type,
        DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
        JuryMembers = [];
    }

    public string Id { get; }

    public DateTime CreatedAt { get; }

    public ICollection<PastProcedureJuryMember> JuryMembers { get; }
}