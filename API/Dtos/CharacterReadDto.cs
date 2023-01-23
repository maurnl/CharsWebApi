using API.Model.Relationships;

namespace API.Dtos
{
    public class CharacterReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<RelationshipReadDto> Relationships { get; set; } = new List<RelationshipReadDto>();
        public ICollection<RelationshipReadDto> RelatedTo { get; set; } = new List<RelationshipReadDto>();
    }
}
