namespace Hackaton2025.Domain.Models.Entities;

public class PastProcedureJuryMember
{
    public PastProcedureJuryMember(string pastProcedureId, string teacherId)
    {
        PastProcedureId = pastProcedureId;
        TeacherId = teacherId;
    }

    public string PastProcedureId { get; }

    public PastProcedure? PastProcedure { get; }

    public string TeacherId { get; }

    public Teacher? Teacher { get; }
}