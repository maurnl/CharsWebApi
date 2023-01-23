using App.Core.Model;
using App.Core.Model.Relationships;

namespace App.Core.Model.Relationships.Types
{
    public class StrangerRelationship : IRelationshipType
    {
        public void UpdateNames(Character character, Relationship rel)
        {
            rel.RelativeRelationshipName = "Stranger";
            rel.OppositeRelativeRelationshipName = "Stranger";
        }
    }
}
