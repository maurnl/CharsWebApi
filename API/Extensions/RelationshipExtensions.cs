using API.Model;
using API.Model.Relationships;
using Microsoft.Extensions.FileSystemGlobbing;

namespace API.Extensions
{
    public static class RelationshipExtensions
    {
        public static Relationship ValidateDirection(this Relationship relationship)
        {
            foreach (var item in relationship.RelationshipType.GetValidDirections())
            {
                if(relationship.RelationshipType.Direction == item)
                {
                    return relationship;
                }
            }
            throw new NotSupportedException();
        }

        public static string ToText(this Relationship relationship, Character caller)
        {
            return caller == relationship.Character ? relationship.RelationshipType?.ToText(true) : relationship.RelationshipType?.ToText(false);
        }
    }
}