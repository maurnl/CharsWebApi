namespace API.Model.Relationships.Types
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
