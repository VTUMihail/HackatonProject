using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Presentation.Controllers;

[ApiController]
[Route("api/university-faculties")]
public class UniversityFacultiesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public UniversityFacultiesController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UniversityFaculty>>> GetAll(string? universityId = null, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var q = _db.UniversityFaculties.AsQueryable();
        if (!string.IsNullOrWhiteSpace(universityId)) q = q.Where(f => f.UniversityId == universityId);
        var list = await q.OrderBy(f => f.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UniversityFaculty>> GetById(string id, CancellationToken ct)
    {
        var f = await _db.UniversityFaculties.FirstOrDefaultAsync(x => x.Id == id, ct);
        return f is null ? NotFound() : Ok(f);
    }

    public sealed record CreateFacultyDto(string? Id, string UniversityId, string Name);

    [HttpPost]
    public async Task<ActionResult<UniversityFaculty>> Create(CreateFacultyDto dto, CancellationToken ct)
    {
        var entity = new UniversityFaculty(
            id: string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id!,
            universityId: dto.UniversityId,
            name: dto.Name);

        _db.UniversityFaculties.Add(entity);
        await _db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    public sealed record UpdateFacultyDto(string Id, string UniversityId, string Name);

    [HttpPut("{id}")]
    public async Task<ActionResult<UniversityFaculty>> Update(string id, UpdateFacultyDto dto, CancellationToken ct)
    {
        if (id != dto.Id) return BadRequest("Id mismatch");

        var entity = await _db.UniversityFaculties.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return NotFound();

        entity.UniversityId = dto.UniversityId;
        entity.Name = dto.Name;

        await _db.SaveChangesAsync(ct);
        return Ok(entity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var entity = await _db.UniversityFaculties.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return NotFound();
        _db.UniversityFaculties.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
