using App.Core.Model.Relationships.Types;

namespace App.Core.Model.Relationships
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
