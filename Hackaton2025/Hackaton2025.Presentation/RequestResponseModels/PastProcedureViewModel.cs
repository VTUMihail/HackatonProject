using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Presentation.RequestResponseModels
{
    public class PastProcedureViewModel
    {
        public string Id { get; set; }

        public PastProcedureType Type { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public ICollection<PastProcedureJuryMember> JuryMembers { get; set; }
    }
}
