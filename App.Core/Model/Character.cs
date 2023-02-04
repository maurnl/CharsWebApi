 using App.Core.Model.Relationships;

namespace App.Core.Model
{
    public class Character : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string OwnerId { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
        public virtual ICollection<Relationship> RelatedTo { get; set; } = new List<Relationship>();
        public virtual CustomUser Owner { get; set; }

        public void UpdateRelationshipNames()
        {
            SetRelativeRelationshipsNames(this.Relationships);
            SetRelativeRelationshipsNames(this.RelatedTo);
        }
        
        private void SetRelativeRelationshipsNames(ICollection<Relationship> rels)
        {
            foreach(var rel in rels)
            {
                if(rel.RelativeRelationshipName is null || rel.OppositeRelativeRelationshipName is null)
                {
                    rel.RelationshipType.UpdateNames(this, rel);
                }
            }
        }
    }
}
