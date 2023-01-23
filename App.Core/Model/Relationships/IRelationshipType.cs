using App.Core.Model;

namespace App.Core.Model.Relationships
{
    public interface IRelationshipType
    {
        void UpdateNames(Character character, Relationship rel);
    }
}
