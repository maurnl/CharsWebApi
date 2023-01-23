namespace API.Model.Relationships.Types
{
    public class ParentalRelationship : IRelationshipType
    {
        public void UpdateNames(Character character, Relationship rel)
        {
            if(rel.Character == character)
            {
                rel.RelativeRelationshipName = "Father";
                rel.OppositeRelativeRelationshipName = "Son";
            }
            else
            {
                rel.RelativeRelationshipName = "Son";
                rel.OppositeRelativeRelationshipName = "Father";
            }
        }

    }
}
