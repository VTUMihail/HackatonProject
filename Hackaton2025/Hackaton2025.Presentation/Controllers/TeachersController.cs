using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton2025.Presentation.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {

        private static readonly List<TeacherViewModel> _teachers = new()
        {
            new TeacherViewModel
            {
                Id = "1",
                Title = TeacherTitle.Professor,
                FullName = new FullName("Alice","Alice","Alice"),
                UniversityId = "U001",
                UniversityFactultyId = "F001"
            },
            new TeacherViewModel
            {
                Id = "2",
                Title = TeacherTitle.AssociateProfessor,
                FullName = new FullName("BOB","bob","bob"),
                UniversityId = "U001",
                UniversityFactultyId = "F002"
            },
            new TeacherViewModel
            {
                Id = "3",
                Title = TeacherTitle.AssociateProfessor,
                FullName = new FullName("Alice","alice","alice"),
                UniversityId = "U002",
                UniversityFactultyId = "F003"
            }
        };
        [HttpGet("{id}")]
        public ActionResult<TeacherViewModel> GetTeacherById(string id)
        {
            var teacher = _teachers.Find(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        
        [HttpPut("{id}")]
        public ActionResult<TeacherViewModel> UpdateTeacher(string id, [FromBody] TeacherViewModel updated)
        {
            var teacher = _teachers.Find(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            teacher.Title = updated.Title;
            teacher.FullName = updated.FullName;
            teacher.UniversityId = updated.UniversityId;
            teacher.UniversityFactultyId = updated.UniversityFactultyId;

            return Ok(teacher);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(string id)
        {
            var teacher = _teachers.Find(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            _teachers.Remove(teacher);
            return NoContent();
        }


    }
}
