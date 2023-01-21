namespace API.Dtos
{
    public class RelationshipReadDto
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; } = string.Empty;
        public string Relationship { get; set;} = string.Empty;
    }
}
