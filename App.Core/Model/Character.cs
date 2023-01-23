 using App.Core.Model.Relationships;

namespace App.Core.Model
{
    public class Character : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
        public virtual ICollection<Relationship> RelatedTo { get; set; } = new List<Relationship>();

        public void UpdateRelationshipNames()
        {
            foreach (var rel in Relationships)
            {
                if (rel.RelativeRelationshipName == null
                    || rel.OppositeRelativeRelationshipName == null)
                {
                    rel.RelationshipType.UpdateNames(this, rel);
                }
            }

            foreach (var rel in RelatedTo)
            {
                if (rel.RelativeRelationshipName == null
                    || rel.OppositeRelativeRelationshipName == null)
                {
                    rel.RelationshipType.UpdateNames(this, rel);
                }
            }
        }
    }
}
