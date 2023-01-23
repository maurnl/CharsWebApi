using System.ComponentModel.DataAnnotations.Schema;
using App.Core.Model;

namespace App.Core.Model.Relationships
{
    public class Relationship : EntityBase
    {
        private IRelationshipType _relType;
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int RelatedCharacterId { get; set; }
        public Character RelatedCharacter { get; set; }
        public string RelationshipTypeName { get; set; }
        [NotMapped]
        public string RelativeRelationshipName { get; set; }
        [NotMapped]
        public string OppositeRelativeRelationshipName { get; set; }

        [NotMapped]
        public IRelationshipType RelationshipType
        {
            get
            {
                if (_relType == null)
                {
                    _relType = new RelationshipTypeFactory().CreateRelationship(RelationshipTypeName);
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
