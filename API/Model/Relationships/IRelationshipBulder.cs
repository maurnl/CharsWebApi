namespace API.Model.Relationships
{
    public interface IRelationshipBuilder
    {
        RelationshipBuilder Link(Character character);
        RelationshipBuilder WithCharacter(Character character);
        RelationshipBuilder As(IRelationshipType relationship);
        RelationshipBuilder WithDirection(RelationshipDirection direction);
        Relationship Build();
    }
}