namespace Hackaton2025.Domain.Models.Entities;

public class PastProcedureJuryMember
{
    private PastProcedureJuryMember() { }
    public PastProcedureJuryMember(string pastProcedureId, string teacherId)
    {
        PastProcedureJuryId = pastProcedureId;
        TeacherId = teacherId;
    }

    public string PastProcedureJuryId { get; }

    public PastProcedureJury? PastProcedureJury { get; }

    public string TeacherId { get; }

    public Teacher? Teacher { get; }
}