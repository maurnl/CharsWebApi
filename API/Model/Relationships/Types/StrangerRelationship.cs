namespace API.Model.Relationships.Types
{
    public class StrangerRelationship : IRelationshipType
    {
        public RelationshipDirection Direction { get; set; }

        public RelationshipDirection[] GetValidDirections()
        {
            return new RelationshipDirection[]
            {
                RelationshipDirection.NonDirectional
            };
        }

        public string ToText(bool fromCaller)
        {
            return "Stranger";
        }
    }
}
