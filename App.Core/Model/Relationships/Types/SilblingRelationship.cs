using App.Core.Model;
using App.Core.Model.Relationships;

namespace App.Core.Model.Relationships.Types
{
    public class SilblingRelationship : IRelationshipType
    {
        public void UpdateNames(Character character, Relationship rel)
        {
            rel.RelativeRelationshipName = "Silbling";
            rel.OppositeRelativeRelationshipName = "Silbling";
        }
    }
}
