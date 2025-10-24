using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Presentation.RequestResponseModels
{
    public class UniversityViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<UniversityFaculty> Faculties { get; set; }
    }
}
