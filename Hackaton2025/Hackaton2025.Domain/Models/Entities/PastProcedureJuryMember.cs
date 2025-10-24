namespace Hackaton2025.Domain.Models.Entities;

public class PastProcedureJuryMember
{
    public PastProcedureJuryMember(string pastProcedureId, string teacherId)
    {
        PastProcedureId = pastProcedureId;
        TeacherId = teacherId;
    }

    public string PastProcedureId { get; }

    public string TeacherId { get; }
}