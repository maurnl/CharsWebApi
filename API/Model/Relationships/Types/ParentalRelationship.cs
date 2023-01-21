namespace API.Model.Relationships.Types
{
    public class ParentalRelationship : IRelationshipType
    {
        public RelationshipDirection Direction { get; set; }

        public RelationshipDirection[] GetValidDirections()
        {
            return new RelationshipDirection[] 
            {
                RelationshipDirection.FromRight,
                RelationshipDirection.FromLeft
            };
        }

        public string ToText(bool fromCaller)
        {
            return fromCaller && Direction == RelationshipDirection.FromRight ? "Children" : "Parent";
        }
    }
}
