using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Presentation.Controllers;

[ApiController]
[Route("api/past-procedures/{pastProcedureId}/juries")]
public class PastProcedureJuriesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public PastProcedureJuriesController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PastProcedureJury>>> GetAll(string pastProcedureId, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var q = _db.PastProcedureJuries.AsQueryable()
                 .Where(j => EF.Property<string>(j, "PastProcedureId") == pastProcedureId);
        var list = await q.Skip((page - 1) * pageSize).Take(pageSize)
                          .Include(j => j.JuryMembers)
                          .ToListAsync(ct);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PastProcedureJury>> GetById(string pastProcedureId, string id, CancellationToken ct)
    {
        var entity = await _db.PastProcedureJuries
            .Where(j => j.Id == id && EF.Property<string>(j, "PastProcedureId") == pastProcedureId)
            .Include(j => j.JuryMembers)
            .FirstOrDefaultAsync(ct);

        return entity is null ? NotFound() : Ok(entity);
    }

    public sealed record CreateJuryDto(string? Id, DateTime CreatedAt);

    [HttpPost]
    public async Task<ActionResult<PastProcedureJury>> Create(string pastProcedureId, CreateJuryDto dto, CancellationToken ct)
    {
        // ensure parent exists
        var parent = await _db.PastProcedures.FirstOrDefaultAsync(p => p.Id == pastProcedureId, ct);
        if (parent is null) return NotFound($"PastProcedure '{pastProcedureId}' not found.");

        var entity = new PastProcedureJury(
            id: string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id!,
            type: default, // not used in your ctor now; you showed constructor with (id, type, createdAt) then later without 'type'
            createdAt: dto.CreatedAt);

        // attach and set the shadow FK
        _db.PastProcedureJuries.Add(entity);
        _db.Entry(entity).Property("PastProcedureId").CurrentValue = pastProcedureId;

        await _db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { pastProcedureId, id = entity.Id }, entity);
    }

    public sealed record UpdateJuryDto(DateTime CreatedAt);

    [HttpPut("{id}")]
    public async Task<ActionResult<PastProcedureJury>> Update(string pastProcedureId, string id, UpdateJuryDto dto, CancellationToken ct)
    {
        var entity = await _db.PastProcedureJuries
            .Where(j => j.Id == id && EF.Property<string>(j, "PastProcedureId") == pastProcedureId)
            .FirstOrDefaultAsync(ct);

        if (entity is null) return NotFound();

        _db.Entry(entity).Property("CreatedAt").CurrentValue = dto.CreatedAt;
        await _db.SaveChangesAsync(ct);
        return Ok(entity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string pastProcedureId, string id, CancellationToken ct)
    {
        var entity = await _db.PastProcedureJuries
            .Where(j => j.Id == id && EF.Property<string>(j, "PastProcedureId") == pastProcedureId)
            .FirstOrDefaultAsync(ct);

        if (entity is null) return NotFound();

        _db.PastProcedureJuries.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
