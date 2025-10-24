using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Presentation.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeachersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public TeachersController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Teacher>>> GetAll(int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var list = await _db.Teachers
            .OrderBy(t => t.LastName).ThenBy(t => t.FirstName)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync(ct);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Teacher>> GetById(string id, CancellationToken ct)
    {
        var entity = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == id, ct);
        return entity is null ? NotFound() : Ok(entity);
    }

    public sealed record CreateTeacherDto(
        string? Id,
        TeacherTitle Title,
        string FirstName,
        string? MiddleName,
        string LastName,
        string UniversityId,
        string UniversityFactultyId,
        decimal Distance,
        DateTime SecondLastJuryMemberDate,
        DateTime LastJuryMemberDate);

    [HttpPost]
    public async Task<ActionResult<Teacher>> Create([FromBody] CreateTeacherDto dto, CancellationToken ct)
    {
        var entity = new Teacher(
            id: string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id!,
            title: dto.Title,
            firstName: dto.FirstName,
            middleName: dto.MiddleName ?? string.Empty,
            lastName: dto.LastName,
            universityId: dto.UniversityId,
            university: null,
            universityFactultyId: dto.UniversityFactultyId,
            universityFaculty: null,
            distance: dto.Distance,
            secondLastJuryMemberDate: dto.SecondLastJuryMemberDate,
            lastJuryMemberDate: dto.LastJuryMemberDate);

        _db.Teachers.Add(entity);
        await _db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    public sealed record UpdateTeacherDto(
        string Id,
        TeacherTitle Title,
        string FirstName,
        string? MiddleName,
        string LastName,
        string UniversityId,
        string UniversityFactultyId,
        decimal Distance,
        DateTime? LastJuryMemberDate);

    [HttpPut("{id}")]
    public async Task<ActionResult<Teacher>> Update(string id, [FromBody] UpdateTeacherDto dto, CancellationToken ct)
    {
        if (id != dto.Id) return BadRequest("Id mismatch");

        var entity = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return NotFound();

        entity.Title = dto.Title;
        entity.FirstName = dto.FirstName;
        entity.MiddleName = dto.MiddleName ?? string.Empty;
        entity.LastName = dto.LastName;
        entity.UniversityId = dto.UniversityId;
        entity.UniversityFactultyId = dto.UniversityFactultyId;
        entity.Distance = dto.Distance;

        if (dto.LastJuryMemberDate.HasValue)
            entity.AddLastJuryMemberDate(dto.LastJuryMemberDate.Value);

        await _db.SaveChangesAsync(ct);
        return Ok(entity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var entity = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return NotFound();
        _db.Teachers.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
