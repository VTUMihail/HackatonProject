using Hackaton2025.Presentation.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton2025.Presentation.Controllers
{
    [Route("api/universities")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private static readonly List<UniversityViewModel> _universities = new()
        {
            new UniversityViewModel
            {
                Id = "1",
                Name = "University 1"

            },
            new UniversityViewModel
            {
                Id = "2",
                Name = "University 2"

            },
            new UniversityViewModel
            {
                Id = "3",
                Name = "University 3"

            },

        };

        [HttpGet("{id}")]
        public ActionResult<UniversityViewModel> GetUniversityById(string id)
        {
            var university = _universities.Find(t => t.Id == id);
            if (university == null)
                return NotFound();

            return Ok(university);
        }

        [HttpGet]
        public List<UniversityViewModel> GetAllTeachers()
        {
            return _universities.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<UniversityViewModel> UpdateTeacher(string id, [FromBody] UniversityViewModel updated)
        {
            var teacher = _universities.Find(t => t.Id == id);
            if (teacher == null)
                return NotFound();



            return Ok(teacher);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(string id)
        {
            var teacher = _universities.Find(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            _universities.Remove(teacher);
            return NoContent();
        }
    }
}
