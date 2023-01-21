namespace API.Model.Relationships
{
    public interface IRelationshipType
    {
        RelationshipDirection Direction { get; set; }
        RelationshipDirection[] GetValidDirections();
        string ToText(bool fromCaller);
    }
}
