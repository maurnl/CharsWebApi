namespace API.Model.Relationships.Types
{
    public class SilblingRelationship : IRelationshipType
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
            return "Silbling";
        }
    }
}
