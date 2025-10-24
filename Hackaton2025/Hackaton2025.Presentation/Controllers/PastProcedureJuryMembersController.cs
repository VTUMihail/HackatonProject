using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Presentation.Controllers;

[ApiController]
[Route("api/juries/{pastProcedureJuryId}/members")]
public class PastProcedureJuryMembersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public PastProcedureJuryMembersController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PastProcedureJuryMember>>> GetAll(string pastProcedureJuryId, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var list = await _db.PastProcedureJuryMembers
            .Where(m => m.PastProcedureJuryId == pastProcedureJuryId)
            .Include(m => m.Teacher)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync(ct);
        return Ok(list);
    }

    [HttpGet("{teacherId}")]
    public async Task<ActionResult<PastProcedureJuryMember>> GetById(string pastProcedureJuryId, string teacherId, CancellationToken ct)
    {
        var m = await _db.PastProcedureJuryMembers
            .Include(x => x.Teacher)
            .FirstOrDefaultAsync(x => x.PastProcedureJuryId == pastProcedureJuryId && x.TeacherId == teacherId, ct);
        return m is null ? NotFound() : Ok(m);
    }

    public sealed record CreateMemberDto(string TeacherId);

    [HttpPost]
    public async Task<ActionResult<PastProcedureJuryMember>> Create(string pastProcedureJuryId, CreateMemberDto dto, CancellationToken ct)
    {
        // ensure parent and teacher exist
        var juryExists = await _db.PastProcedureJuries.AnyAsync(j => j.Id == pastProcedureJuryId, ct);
        if (!juryExists) return NotFound($"PastProcedureJury '{pastProcedureJuryId}' not found.");

        var teacherExists = await _db.Teachers.AnyAsync(t => t.Id == dto.TeacherId, ct);
        if (!teacherExists) return NotFound($"Teacher '{dto.TeacherId}' not found.");

        var entity = new PastProcedureJuryMember(pastProcedureJuryId, dto.TeacherId);
        _db.PastProcedureJuryMembers.Add(entity);
        await _db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { pastProcedureJuryId, teacherId = dto.TeacherId }, entity);
    }

    // Composite key; typical "update" is to change Teacher assignment.
    public sealed record UpdateMemberDto(string NewTeacherId);

    [HttpPut("{teacherId}")]
    public async Task<IActionResult> Update(string pastProcedureJuryId, string teacherId, UpdateMemberDto dto, CancellationToken ct)
    {
        var existing = await _db.PastProcedureJuryMembers
            .FirstOrDefaultAsync(x => x.PastProcedureJuryId == pastProcedureJuryId && x.TeacherId == teacherId, ct);
        if (existing is null) return NotFound();

        // simplest approach: remove old link, add new one (transactional)
        using var tx = await _db.Database.BeginTransactionAsync(ct);
        try
        {
            _db.PastProcedureJuryMembers.Remove(existing);
            await _db.SaveChangesAsync(ct);

            var newLink = new PastProcedureJuryMember(pastProcedureJuryId, dto.NewTeacherId);
            _db.PastProcedureJuryMembers.Add(newLink);
            await _db.SaveChangesAsync(ct);

            await tx.CommitAsync(ct);
            return Ok(newLink);
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }
    }

    [HttpDelete("{teacherId}")]
    public async Task<IActionResult> Delete(string pastProcedureJuryId, string teacherId, CancellationToken ct)
    {
        var existing = await _db.PastProcedureJuryMembers
            .FirstOrDefaultAsync(x => x.PastProcedureJuryId == pastProcedureJuryId && x.TeacherId == teacherId, ct);
        if (existing is null) return NotFound();

        _db.PastProcedureJuryMembers.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
