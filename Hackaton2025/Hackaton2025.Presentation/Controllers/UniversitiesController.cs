using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure;
using Hackaton2025.Presentation.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Presentation.Controllers;

[ApiController]
[Route("api/universities")]
public class UniversitiesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public UniversitiesController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UniversityViewModel>>> GetAll(int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var items = await _db.Universities
            .Include(u => u.Faculties)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync(ct);

        var result = items.Select(u => new UniversityViewModel
        {
            Id = u.Id,
            Name = u.Name,
            Faculties = u.Faculties.ToList()
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UniversityViewModel>> GetById(string id, CancellationToken ct)
    {
        var u = await _db.Universities.Include(x => x.Faculties).FirstOrDefaultAsync(x => x.Id == id, ct);
        if (u is null) return NotFound();
        return Ok(new UniversityViewModel { Id = u.Id, Name = u.Name, Faculties = u.Faculties.ToList() });
    }

    public sealed record CreateUniversityDto(string? Id, string Name);

    [HttpPost]
    public async Task<ActionResult<UniversityViewModel>> Create(CreateUniversityDto dto, CancellationToken ct)
    {
        var id = string.IsNullOrWhiteSpace(dto.Id) ? Guid.NewGuid().ToString("N") : dto.Id!;
        var entity = new University(id, dto.Name);
        _db.Universities.Add(entity);
        await _db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id }, new UniversityViewModel { Id = id, Name = entity.Name, Faculties = null});
    }

    public sealed record UpdateUniversityDto(string Name);

    [HttpPut("{id}")]
    public async Task<ActionResult<UniversityViewModel>> Update(string id, UpdateUniversityDto dto, CancellationToken ct)
    {
        var entity = await _db.Universities.Include(u => u.Faculties).FirstOrDefaultAsync(u => u.Id == id, ct);
        if (entity is null) return NotFound();

        // Name is get-only — set via EF change tracker
        _db.Entry(entity).Property("Name").CurrentValue = dto.Name;
        await _db.SaveChangesAsync(ct);

        return Ok(new UniversityViewModel { Id = entity.Id, Name = dto.Name, Faculties = entity.Faculties.ToList() });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var entity = await _db.Universities.FirstOrDefaultAsync(u => u.Id == id, ct);
        if (entity is null) return NotFound();
        _db.Universities.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
