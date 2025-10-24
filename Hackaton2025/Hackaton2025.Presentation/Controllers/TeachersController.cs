using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Domain.Models.ValueObjects;
using Hackaton2025.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton2025.Presentation.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {

        private readonly ITeacherService _teacherService;
        private readonly ApplicationDbContext _dbContext;

        public TeachersController(ITeacherService teacherService, ApplicationDbContext dbContext)
        {
            _teacherService = teacherService;
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherViewModel>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherService.GetAllAsync(page, pageSize, cancellationToken);

            var result = teachers.Select(MapToViewModel).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetById(string id, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.GetByIdAsync(id, cancellationToken);
            if (teacher == null)
                return NotFound();

            return Ok(MapToViewModel(teacher));
        }

        [HttpPost]
        public async Task<ActionResult<TeacherViewModel>> Create(
            [FromBody] TeacherViewModel model,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var entity = new Teacher(
                id: string.IsNullOrWhiteSpace(model.Id) ? Guid.NewGuid().ToString("N") : model.Id,
                title: model.Title,
                fullName: model.FullName,
                universityId: model.UniversityId,
                university: null, 
                universityFactultyId: model.UniversityFactultyId,
                universityFaculty: null,
                distance: model.Distance,
                secondLastJuryMemberDate: model.SecondLastJuryMemberDate ?? DateTime.MinValue,
                lastJuryMemberDate: model.LastJuryMemberDate ?? DateTime.MinValue
            );

            _teacherService.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, MapToViewModel(entity));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TeacherViewModel>> Update(
            string id,
            [FromBody] TeacherViewModel model,
            CancellationToken cancellationToken)
        {
            if (id != model.Id)
                return BadRequest("ID must match.");

            var existing = await _teacherService.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                return NotFound();

            existing.Title = model.Title;
            existing.FullName = model.FullName;
            existing.UniversityId = model.UniversityId;
            existing.UniversityFactultyId = model.UniversityFactultyId;
            existing.Distance = model.Distance;

            if (model.LastJuryMemberDate.HasValue)
            {
                existing.AddLastJuryMemberDate(model.LastJuryMemberDate.Value);
            }
                
            _teacherService.Update(existing);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok(MapToViewModel(existing));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var existing = await _teacherService.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                return NotFound();

            _teacherService.Delete(existing);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private static TeacherViewModel MapToViewModel(Teacher entity)
        {
            return new TeacherViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                FullName = entity.FullName,
                UniversityId = entity.UniversityId,
                UniversityFactultyId = entity.UniversityFactultyId,
                Distance = entity.Distance,
                SecondLastJuryMemberDate = entity.SecondLastJuryMemberDate,
                LastJuryMemberDate = entity.LastJuryMemberDate
            };
        }


    }
}
