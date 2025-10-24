using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Presentation.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton2025.Presentation.Controllers
{
    [Route("api/past-procedures")]
    [ApiController]
    public class PastProceduresController : ControllerBase
    {
        private static readonly List<PastProcedureViewModel> _procedures = new()
        {
            new PastProcedureViewModel
            {
                Id = "1",
                CreatedAt = DateTime.Now,
                Type = PastProcedureType.Professor,


            },
            new PastProcedureViewModel
            {
                Id = "1",
                CreatedAt = DateTime.Now,
                Type = PastProcedureType.Professor,
                JuryMembers = new List<PastProcedureJuryMember>
                {
                    { new PastProcedureJuryMember("1","2") },
                    { new PastProcedureJuryMember("1","3")},
                }

            }
        };
        [HttpGet]
        public ActionResult<List<PastProcedureViewModel>> GetAll()
        {
            return Ok(_procedures);
        }

        [HttpGet("{id}")]
        public ActionResult<PastProcedureViewModel> GetById(string id)
        {
            var item = _procedures.FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }


        [HttpPut("{id}")]
        public ActionResult<PastProcedureViewModel> Update(string id, [FromBody] PastProcedureViewModel input)
        {
            if (input == null || id != input.Id) return BadRequest("Route id and body id must match.");

            var existing = _procedures.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Type = input.Type;
            existing.CreatedAt = input.CreatedAt == default ? existing.CreatedAt : input.CreatedAt;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existing = _procedures.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            _procedures.Remove(existing);
            return NoContent();
        }
    }
}
