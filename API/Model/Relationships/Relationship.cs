using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.Relationships
{
    public class Relationship : EntityBase
    {
        private IRelationshipType _relType;
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int RelatedCharacterId { get; set; }
        public Character RelatedCharacter { get; set; }
        public string RelationshipName { get; set; }
        public RelationshipDirection Direction { get; set; }

        [NotMapped]
        public IRelationshipType RelationshipType
        {
            get
            {
                if(_relType == null)
                {
                    return new RelationshipTypeFactory().CreateRelationship(RelationshipName);
                }
                return _relType;
            }
            set
            {
                _relType = value;
            }
        }
    }
}
