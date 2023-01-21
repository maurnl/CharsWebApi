using API.Model.Relationships.Types;

namespace API.Model.Relationships
{
    public class RelationshipTypeFactory
    {
        public IRelationshipType CreateRelationship(string relationshipName)
        {
            return relationshipName switch
            {
                "parental" => new ParentalRelationship(),
                "silbling" => new SilblingRelationship(),
                "stranger" => new StrangerRelationship(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
