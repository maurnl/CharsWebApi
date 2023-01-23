namespace API.Model.Relationships
{
    public class RelationshipBuilder
    {
        private Relationship _instance;

        public RelationshipBuilder()
        {
            _instance = new Relationship();
        }

        public RelationshipBuilder Link(Character character)
        {
            _instance.CharacterId = character.Id;
            _instance.Character = character;
            return this;
        }

        public RelationshipBuilder WithCharacter(Character character)
        {
            _instance.RelatedCharacter = character;
            _instance.RelatedCharacterId = character.Id;
            return this;
        }

        public RelationshipBuilder As(IRelationshipType relationship)
        {
            _instance.RelationshipType = relationship;
            return this;
        }

        public Relationship Build()
        {
            return _instance;
        }
    }
}