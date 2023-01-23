using App.Core.Model;

namespace App.Core.Model.Relationships
{
    public interface IRelationshipBuilder
    {
        RelationshipBuilder Link(Character character);
        RelationshipBuilder WithCharacter(Character character);
        RelationshipBuilder As(IRelationshipType relationship);
        Relationship Build();
    }
}