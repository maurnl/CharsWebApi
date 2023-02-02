using App.Core.Model.Relationships;

namespace App.Application.Services.Abstractions
{
    public interface IRelationshipsService
    {
        void AddRelationship(int characterId, int relatedCharacterId, string relationship);
    }
}
