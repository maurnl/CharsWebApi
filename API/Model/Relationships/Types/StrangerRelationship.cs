namespace API.Model.Relationships.Types
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
